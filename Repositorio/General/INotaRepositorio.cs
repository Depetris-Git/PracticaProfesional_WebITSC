using WebITSC.DB.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface INotaRepositorio : IRepositorio<Nota>
    {
        Task<List<Nota>> FullGetAll();
        Task<Nota> FullGetById(int id);
        Task<IEnumerable<object>> SelectNotasByTurno(int turnoId);
    }
}