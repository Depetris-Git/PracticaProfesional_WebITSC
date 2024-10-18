using WebITSC.DB.Data.Entity;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface ITurnoRepositorio : IRepositorio<Turno>
    {
        Task<List<Turno>> FullGetAll();
        Task<Turno> FullGetById(int id);
    }
}