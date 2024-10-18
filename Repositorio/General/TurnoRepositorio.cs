using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebITSC.Admin.Server.Repositorio
{
    public class TurnoRepositorio : Repositorio<Turno>, ITurnoRepositorio
    {
        private readonly Context context;
        public TurnoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<Turno> FullGetById(int id)
        {
            return await context.Turnos
                .Include(u => u.MateriaEnPlanEstudio)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Turno>> FullGetAll()
        {
            return await context.Turnos
                .Include(u => u.MateriaEnPlanEstudio)
                .ToListAsync();
        }

    }
}