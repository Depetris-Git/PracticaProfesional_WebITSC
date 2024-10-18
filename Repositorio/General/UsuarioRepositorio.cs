using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebITSC.Admin.Server.Repositorio
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        private readonly Context context;
        public UsuarioRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<Usuario> FullGetById(int id)
        {
            return await context.Usuarios
                .Include(u => u.Persona)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Usuario>> FullGetAll()
        {
            return await context.Usuarios
                .Include(u => u.Persona)
                .ToListAsync();
        }

    }
}
