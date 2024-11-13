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
        public async Task<List<MateriaEnPlanEstudio>> FullGetByPlanEstudio(int planEstudioId)
        {
            return await context.MateriasEnPlanEstudio
                .Include(p => p.Materia)
                .Include(p => p.PlanEstudio)
                .Where(p => p.PlanEstudioId == planEstudioId)
                .ToListAsync();
        }
        public async Task<List<MateriaEnPlanEstudio>> FullGetByMateria(int materiaId)
        {
            return await context.MateriasEnPlanEstudio
                .Include(p => p.Materia)
                .Include(p => p.PlanEstudio)
                .Where(p => p.MateriaId == materiaId)
                .ToListAsync();
        }

        public async Task DeleteByPlanEstudioId(int planEstudioId)
        {
            var materiasEnPlan = await context.MateriasEnPlanEstudio
                .Where(p => p.PlanEstudioId == planEstudioId)
                .ToListAsync();

            if (materiasEnPlan.Any())
            {
                context.MateriasEnPlanEstudio.RemoveRange(materiasEnPlan);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteByMateriaId(int materiaId)
        {
            var materiasEnPlan = await context.MateriasEnPlanEstudio
                .Where(p => p.MateriaId == materiaId)
                .ToListAsync();

            if (materiasEnPlan.Any())
            {
                context.MateriasEnPlanEstudio.RemoveRange(materiasEnPlan);
                await context.SaveChangesAsync();
            }
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
