using AutoMapper;
using WebITSC.DB.Data.Entity;
using WebITSC.Shared.General.DTO;
using WebITSC.Shared.General.DTO.Alumnos;
using WebITSC.Shared.General.DTO.Persona;
using WebITSC.Shared.General.DTO.UsuariosDTO;


namespace WebITSC.Admin.Server.UTIL
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearAlumnoDTO, Alumno>().ForMember(dest => dest.UsuarioId, opt => opt.Ignore()) // Lo asignamos después
                                               .ForMember(dest => dest.Id, opt => opt.Ignore());
                                               // Ignoramos el Id porque es autogenerado

            CreateMap<Alumno, GetAlumnoDTO>()
                                           .ForMember(dest => dest.NombrePersona, opt => opt.MapFrom(src => src.Usuario.Persona.Nombre))
                                           .ForMember(dest => dest.ApellidoPersona, opt => opt.MapFrom(src => src.Usuario.Persona.Apellido))
                                           .ForMember(dest => dest.DocumentoPersona, opt => opt.MapFrom(src => src.Usuario.Persona.Documento))
                                           .ForMember(dest => dest.TelefonoPersona, opt => opt.MapFrom(src => src.Usuario.Persona.Telefono))
                                           .ForMember(dest => dest.DomicilioPersona, opt => opt.MapFrom(src => src.Usuario.Persona.Domicilio));

            CreateMap<Usuario, GetUsuarioDTO>().ForMember(dest => dest.NombrePersona, opt => opt.MapFrom(src => src.Persona.Nombre))
                                               .ForMember(dest => dest.ApellidoPersona, opt => opt.MapFrom(src => src.Persona.Apellido))
                                               .ForMember(dest => dest.DocumentoPersona, opt => opt.MapFrom(src => src.Persona.Documento));
           
            CreateMap<CrearPersonaDTO, Persona>();
            CreateMap<Persona, GetPersonaDTO>();
            
            CreateMap<CrearCarreraDTO, Carrera>();
            CreateMap<CrearCertificadoAlumnoDTO, CertificadoAlumno>();
            CreateMap<CrearClaseDTO, Clase>();
            CreateMap<CrearClaseAsistenciaDTO, ClaseAsistencia>();
            CreateMap<CrearCoordinadorDTO, Coordinador>();
            CreateMap<CrearCorrelatividadDTO, Correlatividad>();
            CreateMap<CrearCUPOF_CoordinadorDTO, CUPOF_Coordinador>();
            CreateMap<CrearCursadoMateriaDTO, CursadoMateria>();
            CreateMap<CrearEvaluacionDTO, Evaluacion>();
            CreateMap<CrearInscripcionCarreraDTO, InscripcionCarrera>();
            CreateMap<CrearMABDTO, MAB>();
            CreateMap<CrearMateriaDTO, Materia>();
            CreateMap<CrearMateriaEnPlanEstudioDTO, MateriaEnPlanEstudio>();
            CreateMap<CrearNotaDTO, Nota>();
            CreateMap<CrearPlanEstudioDTO, PlanEstudio>();
            CreateMap<CrearProfesorDTO, Profesor>();
            CreateMap<CrearTipoDocumentoDTO, TipoDocumento>();
            CreateMap<CrearTurnoDTO, Turno>();
            CreateMap<CrearUsuarioDTO, Usuario>();
        }
    }
}
