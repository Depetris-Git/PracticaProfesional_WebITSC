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

        // Implementación del método Insert para agregar un Alumno
        public async Task<ActionResult<int>> Insert(Alumno entidad)
        {
            // Asegúrate de que la entidad que recibes no sea nula
            if (entidad == null)
            {
                return new BadRequestResult(); // O el manejo de error que desees
            }

            // Agregar el Alumno al DbContext
            await context.Alumnos.AddAsync(entidad);  // _context es tu DbContext (reemplázalo con el nombre correcto)

            // Guardar los cambios en la base de datos
            await context.SaveChangesAsync();

            // Devuelves el ID del nuevo Alumno (presuponiendo que el ID es auto-generado por la base de datos)
            return new ActionResult<int>(entidad.Id);  // Suponiendo que 'Id' es la propiedad que representa el identificador
        }


        //------------------------------------------------------------------------------------------------
        // Implementación del método Update para actualizar un Alumno existente
        public async Task Update(int id, Alumno entidad)
        {
            // Verificamos si el alumno con el ID especificado existe
            var alumnoExistente = await context.Alumnos.FindAsync(id);

            if (alumnoExistente == null)
            {
                // Si el alumno no existe, puedes manejar el error (retornar un NotFound, por ejemplo)
                throw new KeyNotFoundException("Alumno no encontrado");
            }

            // Actualizamos los campos de la entidad existente con los nuevos valores
            alumnoExistente.Nombre = entidad.Nombre;
            alumnoExistente.Apellido = entidad.Apellido;
            alumnoExistente.Documento = entidad.Documento;
            alumnoExistente.Sexo = entidad.Sexo;
            alumnoExistente.FechaNacimiento = entidad.FechaNacimiento;
            alumnoExistente.Edad = entidad.Edad;
            alumnoExistente.CUIL = entidad.CUIL;
            alumnoExistente.Pais = entidad.Pais;
            alumnoExistente.Provincia = entidad.Provincia;
            alumnoExistente.Departamento = entidad.Departamento;

            // Guardar los cambios en la base de datos
            await context.SaveChangesAsync();
        }

    }
}
