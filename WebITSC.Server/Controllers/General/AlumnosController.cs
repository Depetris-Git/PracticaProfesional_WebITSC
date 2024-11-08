using Microsoft.AspNetCore.Mvc;
using WebITSC.DB.Data.Entity;
using AutoMapper;
using WebITSC.Admin.Server.Repositorio;
using WebITSC.Shared.General.DTO;
using WebITSC.Shared.General.DTO.Alumnos;
using WebITSC.Shared.General.DTO.Persona;
using static WebITSC.Shared.General.DTO.Alumnos.CrearAlumnoDTO;
using WebITSC.Shared.General.DTO.UsuariosDTO;

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
        public async Task<ActionResult<List<GetAlumnoDTO>>> GetAll()
        {
            var alumno = await eRepositorio.FullGetAll();

            return Ok(alumno);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetAlumnoDTO>> GetById(int id)
        {
            var alumno = await eRepositorio.FullGetById(id);
            if (alumno == null) return NotFound();

            return Ok(alumno);
        }
        #region Peticiones Get


        [HttpGet("buscar")] //api/Alumnos/buscar
        public async Task<ActionResult<IEnumerable<GetPersonaDTO>>> BuscarAlumnos(
            [FromQuery] string? nombre,
            [FromQuery] string? apellido,
            [FromQuery] string? documento,
            [FromQuery] int? cohorte )
        {
            var alumnos = await alumnoRepositorio.BuscarAlumnos(nombre, apellido, documento, cohorte);

            if (alumnos == null || !alumnos.Any())
            {
                return NotFound("No se encontraron alumnos.");
            }
            
            // Mapea los alumnos a GetPersonaDTO
            var alumnosDTO = alumnos.Select(alumno => mapper.Map<GetPersonaDTO>(alumno)).ToList();


            return Ok(alumnos);
        }

        #endregion

        //[HttpPost]
        //public async Task<ActionResult<int>> Post(CrearAlumnoDTO entidadDTO)
        //{
        //    try
        //    {
        //        Alumno entidad = mapper.Map<Alumno>(entidadDTO);

        //        return await eRepositorio.Insert(entidad);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult<GetAlumnoDTO>> CreateAlumno([FromBody] CrearAlumnoDTO CrearAlumnoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Paso 1: Crear la Persona
            var persona = new Persona
            {
                Nombre = CrearAlumnoDTO.Nombre,
                Apellido = CrearAlumnoDTO.Apellido,
                Documento = CrearAlumnoDTO.Documento,
                TipoDocumentoId = CrearAlumnoDTO.TipoDocumentoId
            };

            // Usamos el repositorio de Persona para agregarla a la base de datos
            await personaRepositorio.Insert(persona);

            // Paso 2: Crear el Usuario
            var crearUsuarioDTO = new CrearUsuarioDTO
            {
                Email = CrearAlumnoDTO.Email,
                Contrasena = CrearAlumnoDTO.Contrasena // Asegúrate de encriptar la contraseña
            };

            var usuario = new Usuario
            {
                Email = crearUsuarioDTO.Email,
                Contrasena = crearUsuarioDTO.Contrasena, // Asegúrate de encriptar la contraseña antes de guardar
                PersonaId = persona.Id // Asociamos la Persona al Usuario
            };

            // Usamos el repositorio de Usuario para agregarlo a la base de datos
            await usuarioRepositorio.Insert(usuario);

            // Paso 3: Crear el Alumno y asociarlo al Usuario
            var alumno = mapper.Map<Alumno>(CrearAlumnoDTO);
            alumno.UsuarioId = usuario.Id;  // Asignamos el UsuarioId después de crear el Usuario
            await alumnoRepositorio.Insert(alumno);

            // Usamos el repositorio de Alumno para agregarlo a la base de datos
            await alumnoRepositorio.Insert(alumno);

            // Mapea el Alumno a GetAlumnoDTO para la respuesta
            var getAlumnoDTO = mapper.Map<GetAlumnoDTO>(alumno);

            // Retorna el nuevo alumno creado, con un código HTTP 201 (creado)
            return CreatedAtAction(nameof(GetById), new { id = alumno.Id }, getAlumnoDTO);
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

