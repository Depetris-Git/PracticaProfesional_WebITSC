using Microsoft.AspNetCore.Mvc;
using WebITSC.DB.Data.Entity;
using AutoMapper;
using WebITSC.Admin.Server.Repositorio;
using WebITSC.Shared.General.DTO;

namespace WebITSC.Server.Controllers.General
{
    [ApiController]
    [Route("api/Alumnos")]
    public class AlumnosController : ControllerBase
    {

        private readonly IAlumnoRepositorio eRepositorio;
        private readonly IMapper mapper;
        private readonly IAlumnoRepositorio alumnoRepositorio;
        private readonly IPersonaRepositorio personaRepositorio;
        private readonly IUsuarioRepositorio usuarioRepositorio;

        public AlumnosController(IAlumnoRepositorio eRepositorio,
                                  IMapper mapper,
                                  IAlumnoRepositorio alumnoRepositorio,
                                  IPersonaRepositorio personaRepositorio,
                                  IUsuarioRepositorio usuarioRepositorio)
        {

            this.eRepositorio = eRepositorio;
            this.mapper = mapper;
            this.alumnoRepositorio = alumnoRepositorio;
            this.personaRepositorio = personaRepositorio;
            this.usuarioRepositorio = usuarioRepositorio;
        }
        [HttpGet]
        public async Task<ActionResult<List<Alumno>>> GetAll()
        {
            var alumno = await eRepositorio.FullGetAll();

            return Ok(alumno);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Alumno>> GetById(int id)
        {
            var alumno = await eRepositorio.FullGetById(id);
            if (alumno == null) return NotFound();

            return Ok(alumno);
        }
        #region Peticiones Get


        [HttpGet("buscar")] //api/Alumnos/buscar
        public async Task<ActionResult<IEnumerable<Alumno>>> BuscarAlumnos(
            [FromQuery] string? nombre,
            [FromQuery] string? apellido,
            [FromQuery] string? documento,
            [FromQuery] int? cohorte)
        {
            var alumnos = await alumnoRepositorio.BuscarAlumnos(nombre, apellido, documento, cohorte);

            if (alumnos == null || !alumnos.Value.Any())
            {
                return NotFound("No se encontraron alumnos.");
            }

            return Ok(alumnos.Value);
        }

        #endregion

        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearAlumnoDTO entidadDTO)
        {
            try
            {
                Alumno entidad = mapper.Map<Alumno>(entidadDTO);

                return await eRepositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Alumno entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await eRepositorio.SelectById(id);
            //sel = Seleccion

            if (sel == null)
            {
                return NotFound("No existe el tipo de documento buscado.");
            }


            sel = mapper.Map<Alumno>(entidad);

            try
            {
                await eRepositorio.Update(id, sel);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await eRepositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"La persona {id} no existe");
            }
            Alumno EntidadABorrar = new Alumno();
            EntidadABorrar.Id = id;

            if (await eRepositorio.Delete(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }



    }

}

