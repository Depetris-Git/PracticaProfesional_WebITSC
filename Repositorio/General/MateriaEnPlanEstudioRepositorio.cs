using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebITSC.Admin.Server.Repositorio
{
    public class MateriaEnPlanEstudioRepositorio : Repositorio<MateriaEnPlanEstudio>, IMateriaEnPlanEstudioRepositorio
    {
        private readonly Context context;

        public MateriaEnPlanEstudioRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<MateriaEnPlanEstudio> FullGetById(int id)
        {
            return await context.MateriasEnPlanEstudio
                .Include(p => p.Materia)
                .Include(p => p.PlanEstudio)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<MateriaEnPlanEstudio>> FullGetAll()
        {
            return await context.MateriasEnPlanEstudio
                .Include(p => p.Materia)
                .Include(p => p.PlanEstudio)
                .ToListAsync();
        }
    }
}
