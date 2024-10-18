using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
//using WebITSC.DB.Migrations;
//using WebITSC.Client.Pages.Alumnos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebITSC.Admin.Server.Repositorio

{
    public class NotaRepositorio : Repositorio<Nota>, INotaRepositorio
    {
        private readonly Context context;

        public NotaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Nota> FullGetById(int id)
        {
            return await context.Notas
                .Include(n => n.CursadoMateria)
                .Include(n => n.Evaluacion)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Nota>> FullGetAll()
        {
            return await context.Notas
                .Include(n => n.CursadoMateria)
                .Include(n => n.Evaluacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> SelectNotasByTurno(int turnoId)
        {
            // Verificamos que exista el turno
            var turno = await context.Turnos
                .Include(t => t.MateriaEnPlanEstudio) // Incluimos la relación de materias
                .FirstOrDefaultAsync(t => t.Id == turnoId);

            if (turno == null)
            {
                return null;
            }

            // Consulta para obtener los datos relevantes de las notas
            var notas = await context.Notas
                .Include(n => n.CursadoMateria)
                    .ThenInclude(cm => cm.Alumno)
                        .ThenInclude(a => a.Usuario)
                            .ThenInclude(u => u.Persona) // Incluimos Persona relacionada
                .Include(n => n.Evaluacion)
                .Where(n => n.CursadoMateria.TurnoId == turnoId) // Filtrar por TurnoId
                .Select(n => new
                {
                    ValorNota = n.ValorNota,
                    Asistencia = n.Asistencia,
                    TipoEvaluacion = n.Evaluacion.TipoEvaluacion,
                    FechaEvaluacion = n.Evaluacion.Fecha,
                    Folio = n.Evaluacion.Folio,
                    Libro = n.Evaluacion.Libro,
                    Alumno = new
                    {
                        NombreAlumno = n.CursadoMateria.Alumno.Usuario.Persona.Nombre,
                        ApellidoAlumno = n.CursadoMateria.Alumno.Usuario.Persona.Apellido,
                        EstadoAlumno = n.CursadoMateria.Alumno.Estado,
                        Legajo = context.InscripcionesCarrera
                            .Where(ic => ic.AlumnoId == n.CursadoMateria.AlumnoId)
                            .Select(ic => ic.Legajo)
                            .FirstOrDefault()
                    },
                    Materia = turno.MateriaEnPlanEstudio.Materia.Nombre, // Nombre de la materia relacionada
                    Turno = new
                    {
                        Horario = turno.Horario,
                        Sede = turno.Sede,
                        Anno = turno.AnnoCicloLectivo
                    }
                })
                .ToListAsync();

            return notas;
        }
    }
}
