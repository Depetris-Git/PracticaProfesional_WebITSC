using Microsoft.AspNetCore.Mvc;
using WebITSC.DB.Data.Entity;
using AutoMapper;
using WebITSC.Admin.Server.Repositorio;
using WebITSC.Shared.General.DTO.Alumnos;
using WebITSC.Shared.General.DTO.Persona;
using WebITSC.Shared.General.DTO.UsuariosDTO;
using Repositorio.General;


namespace WebITSC.Server.Controllers.General
{
    [ApiController]
    [Route("api/Alumnos")]
    public class AlumnosController : ControllerBase
    {
        private readonly IAlumnoRepositorio eRepositorio;
        private readonly IMapper mapper;
        private readonly IPersonaRepositorio personaRepositorio;
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IInscripcionCarreraRepositorio inscripcionCarreraRepositorio;
        private readonly ICarreraRepositorio carreraRepositorio;
        private readonly ITipoDocumentoRepositorio tipoDocumentoRepositorio;

        // Constructor
        public AlumnosController(IAlumnoRepositorio eRepositorio,
                                  IMapper mapper,
                                  IPersonaRepositorio personaRepositorio,
                                  IUsuarioRepositorio usuarioRepositorio,
                                  IInscripcionCarreraRepositorio inscripcionCarreraRepositorio,
                                  ICarreraRepositorio carreraRepositorio,
                                  ITipoDocumentoRepositorio tipoDocumentoRepositorio)
        {
            this.eRepositorio = eRepositorio;
            this.mapper = mapper;
            this.personaRepositorio = personaRepositorio;
            this.usuarioRepositorio = usuarioRepositorio;
            this.inscripcionCarreraRepositorio = inscripcionCarreraRepositorio;
            this.carreraRepositorio = carreraRepositorio;
            this.tipoDocumentoRepositorio = tipoDocumentoRepositorio;   

        }

        // Obtener todos los alumnos
        [HttpGet]
        public async Task<ActionResult<List<GetAlumnoDTO>>> GetAll()
        {
            var alumno = await eRepositorio.FullGetAll();
            return Ok(alumno);
        }

        // Obtener alumno por ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetAlumnoDTO>> GetById(int id)
        {
            var alumno = await eRepositorio.FullGetById(id);
            if (alumno == null) return NotFound();
            return Ok(alumno);
        }

        #region Peticiones Get

        // Buscar alumnos por criterios
        [HttpGet("buscar")] // api/Alumnos/buscar
        public async Task<ActionResult<IEnumerable<GetPersonaDTO>>> BuscarAlumnos(
            [FromQuery] string? nombre,
            [FromQuery] string? apellido,
            [FromQuery] string? documento,
            [FromQuery] int? cohorte)
        {
            var alumnos = await eRepositorio.BuscarAlumnos(nombre, apellido, documento, cohorte);

            if (alumnos == null || !alumnos.Any())
            {
                return NotFound("No se encontraron alumnos.");
            }

            // Mapea los alumnos a GetPersonaDTO
            var alumnosDTO = alumnos.Select(alumno => mapper.Map<GetPersonaDTO>(alumno)).ToList();

            return Ok(alumnosDTO);
        }

        #endregion

        // Crear nuevo alumno
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CrearAlumnoDTO crearAlumnoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Paso 1: Validar que la carrera exista
            var carrera = await carreraRepositorio.GetCarreraByIdAsync(crearAlumnoDTO.CarreraId);
            if (carrera == null)
            {
                return BadRequest("La carrera especificada no existe.");
            }
            ////  Validar que el TipoDocumentoId sea válido
            var tipoDocumento = await tipoDocumentoRepositorio.GetByIdAsync(crearAlumnoDTO.TipoDocumentoId);
            if (tipoDocumento == null)
            {
                return BadRequest("El tipo de documento especificado no existe.");
            }
            // Paso 2: Crear la Persona
            var persona = new Persona
            {
                Nombre = crearAlumnoDTO.Nombre,
                Apellido = crearAlumnoDTO.Apellido,
                Documento = crearAlumnoDTO.Documento,
                TipoDocumentoId = crearAlumnoDTO.TipoDocumentoId
            };

            // Usamos el repositorio de Persona para agregarla a la base de datos
            await personaRepositorio.Insert(persona);

            // Paso 3: Crear el Usuario
            var crearUsuarioDTO = new CrearUsuarioDTO
            {
                Email = crearAlumnoDTO.Email,
                Contrasena = crearAlumnoDTO.Contrasena // Asegúrate de encriptar la contraseña
            };

            var usuario = new Usuario
            {
                Email = crearUsuarioDTO.Email,
                Contrasena = crearUsuarioDTO.Contrasena, // Asegúrate de encriptar la contraseña antes de guardar
                PersonaId = persona.Id, // Asociamos la Persona al Usuario
                //Estado = crearUsuarioDTO.Estado = true
            };

            // Usamos el repositorio de Usuario para agregarlo a la base de datos
            await usuarioRepositorio.Insert(usuario);

            // Paso 4: Crear el Alumno
            var alumno = mapper.Map<Alumno>(crearAlumnoDTO);
            alumno.UsuarioId = usuario.Id;  // Asignamos el UsuarioId después de crear el Usuario
            await eRepositorio.Insert(alumno); // Usamos el repositorio de Alumno para agregarlo a la base de datos

            // Paso 5: Validar si el alumno ya está inscrito en esta carrera
            var inscripcionExistente = await inscripcionCarreraRepositorio
                .GetInscripcionByAlumnoYCarrera(alumno.Id, crearAlumnoDTO.CarreraId);

            if (inscripcionExistente != null)
            {
                return BadRequest("El alumno ya está inscrito en esta carrera.");
            }

            // Paso 6: Inscribir al alumno en la carrera
            var inscripcionCarrera = new InscripcionCarrera
            {
                AlumnoId = alumno.Id,
                CarreraId = crearAlumnoDTO.CarreraId,
                Cohorte = DateTime.Now.Year , // O la fecha que sea adecuada
                EstadoAlumno = "Activo",
                Legajo = "legajo",
                LibroMatriz = "Libro Matriz",
                NroOrdenAlumno ="NumOrdenAlumno"
            };

            await inscripcionCarreraRepositorio.Insert(inscripcionCarrera); // Inscribir al alumno en la carrera

            // Mapea el Alumno a GetAlumnoDTO para la respuesta
            var getAlumnoDTO = mapper.Map<GetAlumnoDTO>(alumno);

            // Retorna el nuevo alumno creado, con un código HTTP 201 (creado)
            return CreatedAtAction(nameof(GetById), new { id = alumno.Id }, getAlumnoDTO);
        }

        // Actualizar alumno
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Alumno entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }

            // Verifica si la entidad existe antes de continuar
            var sel = await eRepositorio.SelectById(id);
            if (sel == null)
            {
                return NotFound($"El alumno con ID {id} no existe.");
            }

            // Actualiza la entidad
            mapper.Map(entidad, sel); // Mapea los nuevos valores a la entidad existente

            try
            {
                // Usa `eRepositorio.Update` para actualizar
                var updated = await eRepositorio.Update(id, sel);
                if (updated)
                {
                    return Ok();
                }
                return BadRequest("No se pudo actualizar el alumno.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Eliminar alumno
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Verifica si el alumno existe antes de intentar eliminarlo
            var existe = await eRepositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El alumno con ID {id} no existe.");
            }

            // Procede a eliminar el alumno
            var alumnoEliminado = await eRepositorio.Delete(id);
            if (alumnoEliminado)
            {
                return Ok($"El alumno con ID {id} fue eliminado correctamente.");
            }
            else
            {
                return BadRequest("No se pudo eliminar el alumno.");
            }
        }
    }
}



