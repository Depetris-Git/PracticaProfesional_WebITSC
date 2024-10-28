
namespace WebITSC.Admin.Client.Servicios
{
    public interface IHttpServicios
    {
        Task<HttpRespuesta<T>> BuscarAlumnos<T>(string url);
        Task<HttpRespuesta<T>> Get<T>(string url);
    }
}