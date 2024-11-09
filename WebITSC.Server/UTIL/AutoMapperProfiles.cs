using AutoMapper;
using WebITSC.DB.Data.Entity;
using WebITSC.Shared.General.DTO;
using WebITSC.Shared.General.DTO.Alumnos;
using WebITSC.Shared.General.DTO.Carreraa;
using WebITSC.Shared.General.DTO.InscripcionCarrera;
using WebITSC.Shared.General.DTO.Persona;
using WebITSC.Shared.General.DTO.TipoDocumento;
using WebITSC.Shared.General.DTO.UsuariosDTO;


namespace WebITSC.Admin.Server.UTIL
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearAlumnoDTO, Alumno>();

            // Mapeo de CrearAlumnoDTO a Persona
            CreateMap<CrearAlumnoDTO, Persona>()
                                    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                                    .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
                                    .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Documento))
                                    .ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom(src => src.TipoDocumentoId))
                                    .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                                    .ForMember(dest => dest.Domicilio, opt => opt.MapFrom(src => src.Domicilio));
            
            


            // Mapeo de CrearAlumnoDTO a Usuario (también podrías hacerlo si la lógica de tu usuario depende del DTO)
            CreateMap<CrearAlumnoDTO, Usuario>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Contrasena, opt => opt.MapFrom(src => src.Contrasena));


            CreateMap<Alumno, GetAlumnoDTO>()
                                           .ForMember(dest => dest.NombrePersona, opt => opt.MapFrom(src => src.Usuario.Persona.Nombre))
                                           .ForMember(dest => dest.ApellidoPersona, opt => opt.MapFrom(src => src.Usuario.Persona.Apellido))
                                           .ForMember(dest => dest.DocumentoPersona, opt => opt.MapFrom(src => src.Usuario.Persona.Documento))
                                           .ForMember(dest => dest.TelefonoPersona, opt => opt.MapFrom(src => src.Usuario.Persona.Telefono))
                                           .ForMember(dest => dest.DomicilioPersona, opt => opt.MapFrom(src => src.Usuario.Persona.Domicilio))
                                           .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado)) // Si deseas incluir Estado también
                                           .ForMember(dest => dest.Cohorte, opt => opt.MapFrom(src => src.InscripcionesCarreras.FirstOrDefault().Cohorte))
                                           .ForMember(dest => dest.CarreraId, opt => opt.MapFrom(src => src.InscripcionesCarreras.FirstOrDefault().Carrera.Id));

            //_usuario________________________________________________________________________________________________________________________________
            CreateMap<CrearUsuarioDTO, Usuario>();
            CreateMap<Usuario, GetUsuarioDTO>()
                                            .ForMember(dest => dest.NombrePersona, opt => opt.MapFrom(src => src.Persona.Nombre))
                                            .ForMember(dest => dest.ApellidoPersona, opt => opt.MapFrom(src => src.Persona.Apellido))
                                            .ForMember(dest => dest.DocumentoPersona, opt => opt.MapFrom(src => src.Persona.Documento))
                                            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                                            .ForMember(dest => dest.Contrasena, opt => opt.MapFrom(src => src.Contrasena));
       
            //_persona_____________________________________________________________________________________________________________________________________
            CreateMap<CrearPersonaDTO, Persona>().ForPath(dest => dest.TipoDocumento.Nombre, opt => opt.MapFrom(src => src.TipoDocumentoId));
            CreateMap<Persona, GetPersonaDTO>().ForMember(dest => dest.Cohorte, opt => opt.MapFrom(src => DateTime.Now.Year)); // Si "Cohorte" es parte de "Inscripcion_Carrera" en Persona
                                               


            //_carrera_____________________________________________________________________________________________________________________________________
            CreateMap<CrearCarreraDTO, Carrera>();
            CreateMap<Carrera, GetCarreraDTO>();

            //_inscripcion_____________________________________________________________________________________________________________________________________
            CreateMap<CrearInscripcionCarreraDTO, InscripcionCarrera>();
            CreateMap<InscripcionCarrera, GetIncripcionCarreraDTO>()
                                                .ForMember(dest => dest.AlumnoNombre, opt => opt.MapFrom(src => src.Alumno.Usuario.Persona.Nombre))
                                                .ForMember(dest => dest.AlumnoApellido, opt => opt.MapFrom(src => src.Alumno.Usuario.Persona.Apellido))
                                                .ForMember(dest => dest.AlumnoDocumento, opt => opt.MapFrom(src => src.Alumno.Usuario.Persona.Documento))
                                                //.ForMember(dest => dest.CarreraName, opt => opt.MapFrom(src => src.Alumno.Usuario.Persona.inscripcion_Carrera.Carrera.Nombre))
                                                .ForMember(dest => dest.Cohorte, opt => opt.MapFrom(src => src.Cohorte));

            //_TDOCUMENTO_____________________________________________________________________________________________________________________________________
            CreateMap<CrearTipoDocumentoDTO, TipoDocumento>();
            CreateMap<TipoDocumento, GetTipoDocumentoDTO>();

            CreateMap<CrearCertificadoAlumnoDTO, CertificadoAlumno>();
            CreateMap<CrearClaseDTO, Clase>();
            CreateMap<CrearClaseAsistenciaDTO, ClaseAsistencia>();
            CreateMap<CrearCoordinadorDTO, Coordinador>();
            CreateMap<CrearCorrelatividadDTO, Correlatividad>();
            CreateMap<CrearCUPOF_CoordinadorDTO, CUPOF_Coordinador>();
            CreateMap<CrearCursadoMateriaDTO, CursadoMateria>();
            CreateMap<CrearEvaluacionDTO, Evaluacion>();
            CreateMap<CrearMABDTO, MAB>();
            CreateMap<CrearMateriaDTO, Materia>();
            CreateMap<CrearMateriaEnPlanEstudioDTO, MateriaEnPlanEstudio>();
            CreateMap<CrearNotaDTO, Nota>();
            CreateMap<CrearPlanEstudioDTO, PlanEstudio>();
            CreateMap<CrearProfesorDTO, Profesor>();
            CreateMap<CrearTurnoDTO, Turno>();
            
        }
    }
}
