using Microsoft.AspNetCore.Mvc;
using WebITSC.Admin.Server.Repositorio;
using WebITSC.DB.Data.Entity;
using WebITSC.Shared.General.DTO;

namespace Repositorio.General
{
    public interface ICertificadoAlumnoRepositorio: IRepositorio<CertificadoAlumno>
    {
        byte[] GenerarCertificadoPDF(GetDatosCertificadosDTO datos);
        Task<ActionResult<GetDatosCertificadosDTO>> SelectDatosCertificado(string? nombre, string? apellido, string? documento);
    }
}