using WebITSC.DB.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using WebITSC.Shared.General.DTO;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface INotaRepositorio : IRepositorio<Nota>
    {
        Task<List<Nota>> FullGetAll();
        Task<Nota> FullGetById(int id);
        
        Task<List<GetNotaDTO>> SelectNotasByTurno(int turnoId);
    }
}