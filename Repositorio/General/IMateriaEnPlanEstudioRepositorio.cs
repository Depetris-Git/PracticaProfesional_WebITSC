using WebITSC.DB.Data.Entity;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface IMateriaEnPlanEstudioRepositorio : IRepositorio<MateriaEnPlanEstudio>
    {
        Task<List<MateriaEnPlanEstudio>> FullGetAll();
        Task<MateriaEnPlanEstudio> FullGetById(int id);
    }
}