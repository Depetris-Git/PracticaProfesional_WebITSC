using AutoMapper;
using WebITSC.DB.Data.Entity;
using WebITSC.Shared.General.DTO;
using WebITSC.Shared.General.DTO.Alumnos;


namespace WebITSC.Admin.Server.UTIL
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearAlumnoDTO, Alumno>();

            CreateMap<Persona, GetPersonaDTO>().ForMember(dest => dest.Cohorte, opt => opt.MapFrom(src => src.inscripcion_Carrera.Cohorte));
                


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
            CreateMap<CrearPersonaDTO, Persona>();
            CreateMap<CrearPlanEstudioDTO, PlanEstudio>();
            CreateMap<CrearProfesorDTO, Profesor>();
            CreateMap<CrearTipoDocumentoDTO, TipoDocumento>();
            CreateMap<CrearTurnoDTO, Turno>();
            CreateMap<CrearUsuarioDTO, Usuario>();
        }
    }
}
