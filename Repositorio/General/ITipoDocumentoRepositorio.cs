using WebITSC.Admin.Server.Repositorio;
using WebITSC.DB.Data.Entity;

namespace Repositorio.General
{
    public interface ITipoDocumentoRepositorio : IRepositorio<TipoDocumento>
    {
        Task<TipoDocumento> GetByIdAsync(int id);
    }
}