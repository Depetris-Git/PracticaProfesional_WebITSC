using AutoMapper;
using WebITSC.Admin.Server.Repositorio;

using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using WebITSC.Shared.General.DTO;

namespace WebITSC.Server.Controllers.General
{
    [ApiController]
    [Route("api/Coordinador")]
    public class CoordinadorController : ControllerBase
    {
        private readonly IRepositorio<Coordinador> repositorio;
        private readonly IMapper mapper;

        public CoordinadorController(IRepositorio<Coordinador> repositorio,
                                  IMapper mapper)
        {

            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        #region Peticiones Get

        [HttpGet]
        public async Task<ActionResult<List<Coordinador>>> Get()
        {
            return await repositorio.Select();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Coordinador>> Get(int id)
        {
            Coordinador? sel = await repositorio.SelectById(id);
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
        public async Task<ActionResult<int>> Post(CrearCoordinadorDTO entidadDTO)
        {
            try
            {
                Coordinador entidad = mapper.Map<Coordinador>(entidadDTO);

                return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Coordinador entidad)
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


            sel = mapper.Map<Coordinador>(entidad);

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
                return NotFound($"El Coordinador {id} no existe");
            }
            Coordinador EntidadABorrar = new Coordinador();
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
