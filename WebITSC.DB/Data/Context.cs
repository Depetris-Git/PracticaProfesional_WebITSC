
using WebITSC.DB.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebITSC.DB.Data
{
	public class Context : DbContext
    {
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<CertificadoAlumno> CertificadosAlumno { get; set; }
        public DbSet<Clase> Clases { get; set; }
        public DbSet<ClaseAsistencia> ClaseAsistencias { get; set; }
        public DbSet<Coordinador> Coordinadores { get; set; }
        public DbSet<Correlatividad> Correlatividades { get; set; }
        public DbSet<CUPOF_Coordinador> CUPOFs_Coordinador { get; set; }
        public DbSet<CUPOF_Profesor> CUPOFs_Profesor { get; set; }
        public DbSet<CursadoMateria> CursadosMateria { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
        public DbSet<InscripcionCarrera> InscripcionesCarrera { get; set; }
        public DbSet<MAB> MABs { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<MateriaEnPlanEstudio> MateriasEnPlanEstudio { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<PlanEstudio> PlanesEstudio { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<TipoDocumento> TiposDocumento { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                                     .SelectMany(t => t.GetForeignKeys())
                                     .Where(fk => !fk.IsOwnership &&
                                     fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
