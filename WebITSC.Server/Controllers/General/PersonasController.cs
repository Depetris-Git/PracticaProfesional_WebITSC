using AutoMapper;
using WebITSC.Admin.Server.Repositorio;

using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using WebITSC.Shared.General.DTO;

namespace WebITSC.Server.Controllers.General
{
    [ApiController]
    [Route("api/Personas")]
    public class PersonasController : ControllerBase
    {
        private readonly IRepositorio<Persona> repositorio;
        private readonly IPersonaRepositorio eRepositorio;
        private readonly IMapper mapper;

        public PersonasController(IRepositorio<Persona> repositorio,
                                  IPersonaRepositorio eRepositorio,
                                  IMapper mapper)
        {
            this.repositorio = repositorio;
            this.eRepositorio = eRepositorio;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<Persona>>> GetAll()
        {
            var personas = await eRepositorio.FullGetAll();

            return Ok(personas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Persona>> GetById(int id)
        {
            var persona = await eRepositorio.FullGetById(id);
            if (persona == null) return NotFound();

            return Ok(persona);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CrearPersonaDTO PersonaDTO)
        {
            var persona = mapper.Map<Persona>(PersonaDTO);
            await eRepositorio.FullInsert(persona);
            return persona.Id;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CrearPersonaDTO PersonaDTO)
        {
            var persona = mapper.Map<Persona>(PersonaDTO);

            if (id != persona.Id) return BadRequest();

            await eRepositorio.FullUpdate(persona);
            return Ok();
        }

        //----------------------------------------------------------------------------------------------------------------
        #region Peticiones Get

        //[HttpGet]
        //public async Task<ActionResult<List<Persona>>> Get()
        //{
        //    return await repositorio.Select();
        //}

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<Persona>> Get(int id)
        //{
        //    Persona? sel = await repositorio.SelectById(id);
        //    if (sel == null)
        //    {
        //        return NotFound();
        //    }
        //    return sel;
        //}

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;

        }

        #endregion

        //[HttpPost]
        //public async Task<ActionResult<int>> Post(CrearPersonaDTO entidadDTO)
        //{
        //    try
        //    {
        //        Persona entidad = mapper.Map<Persona>(entidadDTO);

        //        return await repositorio.Insert(entidad);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        //[HttpPut("{id:int}")]
        //public async Task<ActionResult> Put(int id, [FromBody] Persona entidad)
        //{
        //    if (id != entidad.Id)
        //    {
        //        return BadRequest("Datos incorrectos");
        //    }
        //    var sel = await repositorio.SelectById(id);
        //    //sel = Seleccion

        //    if (sel == null)
        //    {
        //        return NotFound("No existe el tipo de documento buscado.");
        //    }


        //    sel = mapper.Map<Persona>(entidad); 

        //    try
        //    {
        //        await repositorio.Update(id, sel);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"La persona {id} no existe");
            }
            Persona EntidadABorrar = new Persona();
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
