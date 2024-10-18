using AutoMapper;
using WebITSC.Admin.Server.Repositorio;

using WebITSC.DB.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using WebITSC.Shared.General.DTO;

namespace WebITSC.Admin.Server.Controllers
{
    namespace WebITSC.Admin.Server.Controllers
    {
        [ApiController]
        [Route("api/CertificadoAlumnos")]
        public class CertificadoAlumnosController : ControllerBase
        {
            private readonly IRepositorio<CertificadoAlumno> repositorio;
            private readonly IMapper mapper;

            public CertificadoAlumnosController(IRepositorio<CertificadoAlumno> repositorio,
                                      IMapper mapper)
            {

                this.repositorio = repositorio;
                this.mapper = mapper;
            }

            #region Peticiones Get

            [HttpGet]
            public async Task<ActionResult<List<CertificadoAlumno>>> Get()
            {
                return await repositorio.Select();
            }

            [HttpGet("{id:int}")]
            public async Task<ActionResult<CertificadoAlumno>> Get(int id)
            {
                CertificadoAlumno? sel = await repositorio.SelectById(id);
                if (sel == null)
                {
                    return NotFound();
                }
                return sel;
            }

            [HttpGet("existe/{id:int}")]
            public async Task<ActionResult<bool>> Existe(int id)
            {
                var existe = await repositorio.Existe(id);
                return existe;

            }

            #endregion

            [HttpPost]
            public async Task<ActionResult<int>> Post(CrearCertificadoAlumnoDTO entidadDTO)
            {
                try
                {
                    CertificadoAlumno entidad = mapper.Map<CertificadoAlumno>(entidadDTO);

                    return await repositorio.Insert(entidad);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            [HttpPut("{id:int}")]
            public async Task<ActionResult> Put(int id, [FromBody] CertificadoAlumno entidad)
            {
                if (id != entidad.Id)
                {
                    return BadRequest("Datos incorrectos");
                }
                var sel = await repositorio.SelectById(id);
                //sel = Seleccion

                if (sel == null)
                {
                    return NotFound("No existe el tipo de documento buscado.");
                }


                sel = mapper.Map<CertificadoAlumno>(entidad);

                try
                {
                    await repositorio.Update(id, sel);
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
                var existe = await repositorio.Existe(id);
                if (!existe)
                {
                    return NotFound($"La persona {id} no existe");
                }
                CertificadoAlumno EntidadABorrar = new CertificadoAlumno();
                EntidadABorrar.Id = id;

                if (await repositorio.Delete(id))
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
}
