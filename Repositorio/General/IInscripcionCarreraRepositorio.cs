using WebITSC.DB.Data.Entity;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface IInscripcionCarreraRepositorio : IRepositorio<InscripcionCarrera>
    {
        Task<List<InscripcionCarrera>> FullGetAll();
        Task<InscripcionCarrera> FullGetById(int id);
    }
}