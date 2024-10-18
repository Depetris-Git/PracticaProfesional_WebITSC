using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebITSC.Admin.Server.Repositorio
{
    public class PlanEstudioRepositorio : Repositorio<PlanEstudio>, IPlanEstudioRepositorio
    {
        private readonly Context context;
        public PlanEstudioRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<PlanEstudio> FullGetById(int id)
        {
            return await context.PlanesEstudio
                .Include(u => u.Carrera)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<PlanEstudio>> FullGetAll()
        {
            return await context.PlanesEstudio
                .Include(u => u.Carrera)
                .ToListAsync();
        }

    }
}
