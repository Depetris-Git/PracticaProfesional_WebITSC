using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebITSC.Shared.General.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<ActionResult<IEnumerable<BuscarAlumnoDTO>>> BuscarAlumnos(string? nombre, string? apellido, string? documento, int? cohorte)
        {
            var query = context.Alumnos.Include(a => a.Usuario).AsQueryable();

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

        Task<ActionResult<int>> IAlumnoRepositorio.Insert(Alumno entidad)
        {
            throw new NotImplementedException();
        }

        Task IAlumnoRepositorio.Update(int id, Alumno sel)
        {
            throw new NotImplementedException();
        }
    }
}
