using AutoMapper;
using WebITSC.Admin.Server.Repositorio;

using WebITSC.DB.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using WebITSC.Shared.General.DTO.Carreraa;
using WebITSC.Shared.General.DTO.Alumnos;
using Repositorio.General;

namespace WebITSC.Admin.Server.Controllers
{
    namespace WebITSC.Admin.Server.Controllers
    {
        [ApiController]
        [Route("api/Carreras")]
        public class CarrerasController : ControllerBase
        {
            private readonly ICarreraRepositorio eRepositorio;
            private readonly IMapper mapper;

            public CarrerasController(ICarreraRepositorio eRepositorio,
                                                IMapper mapper)
            {
                this.eRepositorio = eRepositorio;
                this.mapper = mapper;
            }

            [HttpGet]
            public async Task<ActionResult<List<GetCarreraDTO>>> GetAll()
            {
                var carreras = await eRepositorio.Select();
                return Ok(carreras);
            }

            [HttpGet("{id:int}")]
            public async Task<ActionResult<GetCarreraDTO>> Get(int id)
            {
                // Seleccionamos la entidad Carrera
                Carrera? sel = await eRepositorio.SelectById(id);

                if (sel == null)
                {
                    return NotFound();
                }

                // Usamos AutoMapper para mapear la entidad Carrera a GetCarreraDTO
                var carreraDto = mapper.Map<GetCarreraDTO>(sel);

                return Ok(carreraDto);
            }


            [HttpGet("existe/{id:int}")]
            public async Task<ActionResult<bool>> Existe(int id)
            {
                var existe = await eRepositorio.Existe(id);
                return existe;

            }


            [HttpPost]
            public async Task<ActionResult<int>> Post(CrearCarreraDTO entidadDTO)
            {
                try
                {
                    Carrera entidad = mapper.Map<Carrera>(entidadDTO);

                    return await eRepositorio.Insert(entidad);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            [HttpPut("{id:int}")]
            public async Task<ActionResult> Put(int id, [FromBody] Carrera entidad)
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


                sel = mapper.Map<Carrera>(entidad);

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
                Carrera EntidadABorrar = new Carrera();
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
}
