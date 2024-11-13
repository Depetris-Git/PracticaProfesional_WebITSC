using Microsoft.AspNetCore.Mvc;
using WebITSC.Admin.Server.Repositorio;
using WebITSC.DB.Data.Entity;
using WebITSC.Shared.General.DTO;

namespace Repositorio.General
{
    public interface ICertificadoAlumnoRepositorio: IRepositorio<CertificadoAlumno>
    {
        byte[] GenerarCertificadoPDF(GetDatosCertificadosDTO datos);
        Task<ActionResult<Alumno>> SelectAlumnoByDoc(string documento);
        Task<ActionResult<GetDatosCertificadosDTO>> SelectDatosCertificado(string documento);
    }
}