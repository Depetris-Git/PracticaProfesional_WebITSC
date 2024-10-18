using WebITSC.DB.Data.Entity;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface IEvaluacionRepositorio : IRepositorio<Evaluacion>
    {
        Task<List<Evaluacion>> FullGetAll();
        Task<Evaluacion> FullGetById(int id);
    }
}