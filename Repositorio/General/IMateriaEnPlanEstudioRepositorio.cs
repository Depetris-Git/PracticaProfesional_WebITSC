using WebITSC.DB.Data.Entity;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface IMateriaEnPlanEstudioRepositorio : IRepositorio<MateriaEnPlanEstudio>
    {
        Task DeleteByMateriaId(int materiaId);
        Task DeleteByPlanEstudioId(int planEstudioId);
        Task<List<MateriaEnPlanEstudio>> FullGetAll();
        Task<MateriaEnPlanEstudio> FullGetById(int id);
        Task<List<MateriaEnPlanEstudio>> FullGetByMateria(int materiaId);
        Task<List<MateriaEnPlanEstudio>> FullGetByPlanEstudio(int planEstudioId);
    }
}