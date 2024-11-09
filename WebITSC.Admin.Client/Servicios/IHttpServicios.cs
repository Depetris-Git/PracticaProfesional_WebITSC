
namespace WebITSC.Admin.Client.Servicios
{
    public interface IHttpServicios
    {
        Task<HttpRespuesta<O>> Get<O>(string url);
        Task<HttpRespuesta<object>> Post<O>(string url, O entidad);
    }
}