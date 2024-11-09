using WebITSC.DB.Data.Entity;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface IPersonaRepositorio : IRepositorio<Persona>
    {
        Task<Persona> FullGetById(int id);
        Task<List<Persona>> FullGetAll();
        Task FullInsert(Persona persona);
        Task FullUpdate(Persona persona);
        //Task<Persona> ObtenerPersonaConCohorte(int personaId);
    }
}
