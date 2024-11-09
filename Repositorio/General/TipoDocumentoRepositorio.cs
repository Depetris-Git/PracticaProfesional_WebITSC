using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebITSC.Admin.Server.Repositorio;
using WebITSC.DB.Data.Entity;
using WebITSC.DB.Data;

namespace Repositorio.General
{
    public class TipoDocumentoRepositorio : Repositorio<TipoDocumento>, ITipoDocumentoRepositorio
    {
        private readonly Context context;

        public TipoDocumentoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        // Método para obtener TipoDocumento por Id
        public async Task<TipoDocumento> GetByIdAsync(int id)
        {
            return await context.Set<TipoDocumento>().FindAsync(id);
        }
    }
}
