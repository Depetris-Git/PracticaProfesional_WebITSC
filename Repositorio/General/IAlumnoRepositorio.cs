using WebITSC.DB.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using WebITSC.Shared.General.DTO;

namespace WebITSC.Admin.Server.Repositorio
{
    public interface IAlumnoRepositorio
    {
        Task<ActionResult<IEnumerable<BuscarAlumnoDTO>>> BuscarAlumnos(string? nombre, string? apellido, string? documento, int? cohorte);
        Task<bool> Delete(int id);
        Task<bool> Existe(int id);
        Task<List<Alumno>> FullGetAll();
        Task<Alumno> FullGetById(int id);
        Task<ActionResult<int>> Insert(Alumno entidad);
        Task<Alumno> SelectById(int id);
        Task Update(int id, Alumno sel);
    }
}