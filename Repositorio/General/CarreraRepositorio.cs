using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebITSC.Admin.Server.Repositorio;
using WebITSC.DB.Data.Entity;
using WebITSC.DB.Data;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.General
{
    public class CarreraRepositorio : Repositorio<Carrera>, ICarreraRepositorio
    {

        private readonly Context context;

        public CarreraRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<Carrera> GetCarreraByIdAsync(int carreraId)
        {
            return await context.Set<Carrera>().FindAsync(carreraId);
        }
    }
}