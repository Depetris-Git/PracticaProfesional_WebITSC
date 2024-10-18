using WebITSC.DB.Data.Entity;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface ICursadoMateriaRepositorio : IRepositorio<CursadoMateria>
    {
        Task<List<CursadoMateria>> FullGetAll();
        Task<CursadoMateria> FullGetById(int id);
    }
}