using WebITSC.Admin.Server.Repositorio;
using WebITSC.DB.Data.Entity;

namespace Repositorio.General
{
    public interface ICarreraRepositorio: IRepositorio<Carrera>
    {
        Task<int> GetByNombre(string nombreCarrera);
        Task<Carrera> GetCarreraByIdAsync(int carreraId);
    }
}