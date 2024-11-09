using WebITSC.DB.Data.Entity;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface IInscripcionCarreraRepositorio : IRepositorio<InscripcionCarrera>
    {
        Task<List<InscripcionCarrera>> FullGetAll();
        Task<InscripcionCarrera> FullGetById(int id);
        Task<InscripcionCarrera> GetInscripcionByAlumnoYCarrera(int alumnoId, int carreraId);
        Task<List<InscripcionCarrera>> GetInscripcionesPorCarreraYcohorteOpcional(int carreraId, int? cohorte = null);
        Task<List<InscripcionCarrera>> ObtenerInscripcionesPorCarreraYcohorte(int carreraId, int? cohorte);
    }
}