using Microsoft.AspNetCore.Mvc;
using WebITSC.Shared.General.DTO;
using WebITSC.DB.Data.Entity;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface IAlumnoRepositorio: IRepositorio<Alumno>
    {
        Task<ActionResult<IEnumerable<BuscarAlumnoDTO>>> BuscarAlumnos(string? nombre, string? apellido, string? documento, int? cohorte);
        Task<bool> Delete(int id);
        Task<bool> Existe(int id);
        Task<List<Alumno>> FullGetAll();
        Task<Alumno> FullGetById(int id);
        Task<bool> EliminarAlumno(int alumnoId);
    }
}