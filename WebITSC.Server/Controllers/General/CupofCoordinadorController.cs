using AutoMapper;
using WebITSC.Admin.Server.Repositorio;

using WebITSC.DB.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using WebITSC.Shared.General.DTO;


namespace WebITSC.Server.Controllers.General
{
    [ApiController]
    [Route("api/CUPOF_Coordinadores")]
    public class CUPOF_CoordinadoresController : ControllerBase
    {
        private readonly IRepositorio<CUPOF_Coordinador> repositorio;
        private readonly IMapper mapper;

        public CUPOF_CoordinadoresController(IRepositorio<CUPOF_Coordinador> repositorio,
                                  IMapper mapper)
        {

            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        #region Peticiones Get

        [HttpGet]
        public async Task<ActionResult<List<CUPOF_Coordinador>>> Get()
        {
            return await repositorio.Select();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CUPOF_Coordinador>> Get(int id)
        {
            CUPOF_Coordinador? sel = await repositorio.SelectById(id);
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
        public async Task<ActionResult<int>> Post(CrearCUPOF_CoordinadorDTO entidadDTO)
        {
            try
            {
                CUPOF_Coordinador entidad = mapper.Map<CUPOF_Coordinador>(entidadDTO);

                return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CUPOF_Coordinador entidad)
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


            sel = mapper.Map<CUPOF_Coordinador>(entidad);

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
            CUPOF_Coordinador EntidadABorrar = new CUPOF_Coordinador();
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

