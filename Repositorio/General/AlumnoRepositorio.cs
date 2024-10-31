using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebITSC.Admin.Server.Repositorio
{
    public class AlumnoRepositorio : Repositorio<Alumno>, IAlumnoRepositorio
    {
        private readonly Context context;

        public AlumnoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Alumno> FullGetById(int id)
        {
            return await context.Alumnos
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        //________________________________________________
        public async Task<List<Alumno>> FullGetAll()
        {
            return await context.Alumnos
                .Include(a => a.Usuario)
                .ToListAsync();
        }
        //________________________________________________

        public async Task<IEnumerable<Alumno>> BuscarAlumnos(string? nombre, string? apellido, string? documento, int? cohorte)
        {
            var query = context.Alumnos.Include(a => a.Usuario).AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre) ||
                !string.IsNullOrWhiteSpace(apellido) ||
                !string.IsNullOrWhiteSpace(documento) ||
                cohorte.HasValue)
            {
                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    query = query.Where(a => a.Usuario.Persona.Nombre.Contains(nombre));
                }

                if (!string.IsNullOrWhiteSpace(apellido))
                {
                    query = query.Where(a => a.Usuario.Persona.Apellido.Contains(apellido));
                }

                if (!string.IsNullOrWhiteSpace(documento))
                {
                    query = query.Where(a => a.Usuario.Persona.Documento.Contains(documento));
                }

                if (cohorte.HasValue)
                {
                    query = query.Where(a => a.InscripcionesCarreras.Any(ic => ic.Cohorte == cohorte));
                }
            }
            { 
                var resultados = await query.ToListAsync();
                return resultados;
            }
        
        }

            //public async Task<ActionResult<IEnumerable<Alumno>>> BuscarAlumnos(string? nombre, string? apellido, string? documento, int? cohorte)
            //{
            //    var query = context.Alumnos.Include(a => a.Usuario).AsQueryable();

            //    if (!string.IsNullOrWhiteSpace(nombre))
            //    {
            //        query = query.Where(a => a.Usuario.Persona.Nombre.Contains(nombre));
            //    }

            //    if (!string.IsNullOrWhiteSpace(apellido))
            //    {
            //        query = query.Where(a => a.Usuario.Persona.Apellido.Contains(apellido));
            //    }

            //    if (!string.IsNullOrWhiteSpace(documento))
            //    {
            //        query = query.Where(a => a.Usuario.Persona.Documento.Contains(documento));
            //    }


            //    if (cohorte.HasValue)
            //    {
            //        query = query.Where(a => a.InscripcionesCarreras.Any(ic => ic.Cohorte == cohorte));
            //    }

            //    var resultados = await query.ToListAsync();

            //    return resultados;
            //}

            Task<ActionResult<int>> IAlumnoRepositorio.Insert(Alumno entidad)
        {
            throw new NotImplementedException();
        }

        //------------------------------------------------------------------------------------------------
        Task IAlumnoRepositorio.Update(int id, Alumno sel)
        {
            throw new NotImplementedException();
        }
    }
}
