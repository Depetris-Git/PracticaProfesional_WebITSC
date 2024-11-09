using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WebITSC.Shared.General.DTO.BuscarAlumnosDTOs;

namespace WebITSC.Admin.Server.Repositorio
{
    public class AlumnoRepositorio : Repositorio<Alumno>, IAlumnoRepositorio
    {
        private readonly Context context;

        public AlumnoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Alumno>> FullGetAll()
        {
            return await context.Alumnos
                .Include(a => a.Usuario)              // Asegúrate de incluir el Usuario
                .ThenInclude(u => u.Persona)          // Y también incluir Persona dentro de Usuario
                .ToListAsync();
        }

        public async Task<Alumno> FullGetById(int id)
        {
            return await context.Alumnos
                .Include(a => a.Usuario)
                .ThenInclude(u => u.Persona)         // Asegúrate de incluir Persona
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        //________________________________________________
        public async Task<ActionResult<IEnumerable<BuscarAlumnoDTO>>> BuscarAlumnos(string? nombre, string? apellido, string? documento, int? cohorte)
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
            
            var resultados = await query.Select(a => new BuscarAlumnoDTO
            {
                Nombre = a.Usuario.Persona.Nombre,
                Apellido = a.Usuario.Persona.Apellido,
                Documento = a.Usuario.Persona.Documento,
                TipoDocumento = a.Usuario.Persona.TipoDocumento.Nombre,
                Email = a.Usuario.Email,
                EstadoUsuario = a.Usuario.Estado,
                Sexo = a.Sexo,
                FechaNacimiento = a.FechaNacimiento,
                Edad = a.Edad,
                Cuil = a.CUIL,
                Pais = a.Pais,
                Provincia = a.Provincia,
                Departamento = a.Departamento,
                TituloBase = a.TituloBase,
                FotocopiaDNI = a.FotocopiaDNI,
                ConstanciaCUIL = a.ConstanciaCUIL,
                PartidaNacimiento = a.PartidaNacimiento,
                Analitico = a.Analitico,
                FotoCarnet = a.FotoCarnet,
                Cus = a.CUS,
                EstadoAlumno = a.Estado,
                Telefono = a.Usuario.Persona.Telefono,
                Domicilio = a.Usuario.Persona.Domicilio,
                Certificados = a.CertificadosAlumno.Select(ca => new CertificadoAlumnoDTO
                {
                    Id = ca.Id,
                    FechaEmision = ca.FechaEmision
                }).ToList(),
                InscripcionesEnCarreras = a.InscripcionesCarreras.Select(ic => new InscripcionesCarrerasDTO
                {
                    NombreCarrera = ic.Carrera.Nombre,
                    Cohorte = ic.Cohorte,
                    Legajo = ic.Legajo,
                    LibroMatriz = ic.LibroMatriz,
                    NumeroDeOrden = ic.NroOrdenAlumno,
                    EstadoAlumnoEnCarrera = ic.EstadoAlumno

                }).ToList(),
                MateriasQueCursa = a.MateriasCursadas.Select(mqc => new MateriasCursadasDTO
                {
                    NombreMateria = mqc.Turno.MateriaEnPlanEstudio.Materia.Nombre,
                    ResolucionMinisterial = mqc.Turno.MateriaEnPlanEstudio.Materia.ResolucionMinisterial,
                    FechaInscripcion = mqc.FechaInscripcion,
                    Anno = mqc.Turno.MateriaEnPlanEstudio.Materia.Anno,
                    Formacion = mqc.Turno.MateriaEnPlanEstudio.Materia.Formacion,
                    CondicionActual = mqc.CondicionActual,
                    VencimientoCondicion = mqc.VencimientoCondicion
                }).ToList()

            }).ToListAsync();
            return resultados;
            
        }

            
        public async Task<bool> EliminarAlumno(int alumnoId)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    // 1. Eliminar las inscripciones en carreras relacionadas con el alumno
                    var inscripciones = await context.InscripcionesCarrera
                        .Where(i => i.AlumnoId == alumnoId)
                        .ToListAsync();

                    if (inscripciones.Any())
                    {
                        context.InscripcionesCarrera.RemoveRange(inscripciones);  // Eliminar todas las inscripciones
                        await context.SaveChangesAsync();  // Guardar cambios
                    }

                    // 2. Eliminar el alumno
                    var alumno = await context.Alumnos
                        .Where(a => a.Id == alumnoId)
                        .FirstOrDefaultAsync();

                    if (alumno != null)
                    {
                        context.Alumnos.Remove(alumno);  // Eliminar el alumno
                        await context.SaveChangesAsync();  // Guardar cambios
                    }

                    // 3. Eliminar el usuario asociado al alumno
                    var usuario = await context.Usuarios
                        .Where(u => u.Id == alumno.UsuarioId)
                        .FirstOrDefaultAsync();

                    if (usuario != null)
                    {
                        context.Usuarios.Remove(usuario);  // Eliminar el usuario
                        await context.SaveChangesAsync();  // Guardar cambios
                    }

                    // 4. Eliminar la persona asociada al usuario
                    var persona = await context.Personas
                        .Where(p => p.Id == usuario.PersonaId)
                        .FirstOrDefaultAsync();

                    if (persona != null)
                    {
                        context.Personas.Remove(persona);  // Eliminar la persona
                        await context.SaveChangesAsync();  // Guardar cambios
                    }

                    // Confirmar la transacción
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();  // Revertir los cambios en caso de error
                                                        // Loguear el error si es necesario
                    return false;
                }

            }

        } 
    }
}
