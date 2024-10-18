using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebITSC.Admin.Server.Repositorio
{
    public class InscripcionCarreraRepositorio : Repositorio<InscripcionCarrera>,
                                                 IInscripcionCarreraRepositorio
    {
        private readonly Context context;

        public InscripcionCarreraRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<InscripcionCarrera> FullGetById(int id)
        {
            return await context.InscripcionesCarrera
                .Include(p => p.Alumno)
                .Include(p => p.Carrera)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<InscripcionCarrera>> FullGetAll()
        {
            return await context.InscripcionesCarrera
                .Include(p => p.Alumno)
                .Include(p => p.Carrera)
                .ToListAsync();
        }
    }
}