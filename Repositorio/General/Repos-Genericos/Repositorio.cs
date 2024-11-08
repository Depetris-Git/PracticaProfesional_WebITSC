using AutoMapper;
using WebITSC.DB.Data;
//using WebITSC.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebITSC.Admin.Server.Repositorio
{
    public class Repositorio<E> : IRepositorio<E> where E : class, IEntityBase
    {
        private readonly Context context;

        public Repositorio(Context context)
        {
            this.context = context;
        }

        //____________________________________________________________________________________________
        public async Task<bool> Existe(int id)
        {
            var existe = await context.Set<E>()
                             .AnyAsync(x => x.Id == id);
            return existe;
        }
        //____________________________________________________________________________________________
        public async Task<List<E>> Select()
        {
            return await context.Set<E>().ToListAsync();
        }
        //____________________________________________________________________________________________
        public async Task<E> SelectById(int id)
        {
            E? sel = await context.Set<E>()
                // .AsNoTracking()
                .FirstOrDefaultAsync(
                x => x.Id == id);
            return sel;
        }
        //____________________________________________________________________________________________
        public async Task<int> Insert(E entidad)
        {
            try
            {
                await context.Set<E>().AddAsync(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }

            catch (Exception err)
            {
                throw err;
            }
        }
        //____________________________________________________________________________________
        public async Task<bool> Update(int id, E entidad)
        {
            if (id != entidad.Id)
            {
                return false;
            }
            var EntidadExistente = await SelectById(id);

            if (EntidadExistente == null)
            {
                return false;
            }

            try
            {
                context.Entry(EntidadExistente).CurrentValues.SetValues(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        //____________________________________________________________________________________________
        //____________________________________________________________________________________________
        public async Task<bool> Delete(int id)
        {
            try
            {
                var entidad = await SelectById(id);
                if (entidad == null)
                {
                    return false;
                }

                context.Set<E>().Remove(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log el error (puedes agregar un log adecuado aquí)
                throw new Exception($"Ocurrió un error al eliminar la entidad con ID {id}: {ex.Message}");
            }
        }



    }
}
