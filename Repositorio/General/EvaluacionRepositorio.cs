using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebITSC.Admin.Server.Repositorio
{
    public class EvaluacionRepositorio : Repositorio<Evaluacion>, IEvaluacionRepositorio
    {
        private readonly Context context;
        public EvaluacionRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<Evaluacion> FullGetById(int id)
        {
            return await context.Evaluaciones
                .Include(u => u.Turno)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Evaluacion>> FullGetAll()
        {
            return await context.Evaluaciones
                .Include(u => u.Turno)
                .ToListAsync();
        }

    }
}