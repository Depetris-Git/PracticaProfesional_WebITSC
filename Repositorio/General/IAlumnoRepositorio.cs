using Microsoft.AspNetCore.Mvc;
using WebITSC.DB.Data.Entity;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface IAlumnoRepositorio: IRepositorio<Alumno>
    {
        Task<IEnumerable<Alumno>> BuscarAlumnos(string? nombre, string? apellido, string? documento, int? cohorte);
        Task<List<Alumno>> FullGetAll();
        Task<Alumno> FullGetById(int id);
        Task<bool> EliminarAlumno(int alumnoId);
    }
}