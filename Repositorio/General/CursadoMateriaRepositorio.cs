using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebITSC.Admin.Server.Repositorio
{
    public class CursadoMateriaRepositorio : Repositorio<CursadoMateria>, ICursadoMateriaRepositorio
    {
        private readonly Context context;
        public CursadoMateriaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<CursadoMateria> FullGetById(int id)
        {
            return await context.CursadosMateria
                .Include(u => u.Alumno)
                .Include(u => u.Turno)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CursadoMateria>> FullGetAll()
        {
            return await context.CursadosMateria
                .Include(u => u.Alumno)
                .Include(u => u.Turno)
                .ToListAsync();
        }

    }
}