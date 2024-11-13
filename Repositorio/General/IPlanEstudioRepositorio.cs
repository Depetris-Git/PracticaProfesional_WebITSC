using WebITSC.DB.Data.Entity;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface IPlanEstudioRepositorio : IRepositorio<PlanEstudio>
    {
        Task<List<PlanEstudio>> FullGetAll();
        Task<PlanEstudio> FullGetById(int id);
        Task<PlanEstudio> GetByCarreraAnno(int carreraId, int anno);
        Task<int> GetIdByCarreraAnno(int carreraId, int anno);
    }
}