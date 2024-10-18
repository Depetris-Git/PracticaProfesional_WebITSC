using WebITSC.DB.Data.Entity;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface IUsuarioRepositorio: IRepositorio<Usuario>
    {
        Task<List<Usuario>> FullGetAll();
        Task<Usuario> FullGetById(int id);
    }
}