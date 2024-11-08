using WebITSC.Admin.Server.Repositorio;
using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebITSC.Admin.Client.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
// Añadir controladores y configurar opciones JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantener nombres de propiedades sin cambios
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Para evitar ciclos de referencia
    });

// Configuración de AutoMapper (solo una vez)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registrar HttpClient y servicios de cliente
builder.Services.AddScoped<IHttpServicios, HttpServicios>();
builder.Services.AddHttpClient();

// Configurar la base de datos
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Cambiar a tu cadena de conexión

// Registrar repositorios
builder.Services.AddScoped<IAlumnoRepositorio, AlumnoRepositorio>();
builder.Services.AddScoped<IRepositorio<Carrera>, Repositorio<Carrera>>();
builder.Services.AddScoped<IRepositorio<CertificadoAlumno>, Repositorio<CertificadoAlumno>>();
builder.Services.AddScoped<IRepositorio<Clase>, Repositorio<Clase>>();
builder.Services.AddScoped<IRepositorio<ClaseAsistencia>, Repositorio<ClaseAsistencia>>();
builder.Services.AddScoped<IRepositorio<Coordinador>, Repositorio<Coordinador>>();
builder.Services.AddScoped<IRepositorio<Correlatividad>, Repositorio<Correlatividad>>();
builder.Services.AddScoped<IRepositorio<CUPOF_Coordinador>, Repositorio<CUPOF_Coordinador>>();
builder.Services.AddScoped<IRepositorio<CUPOF_Profesor>, Repositorio<CUPOF_Profesor>>();
builder.Services.AddScoped<ICursadoMateriaRepositorio, CursadoMateriaRepositorio>();
builder.Services.AddScoped<IEvaluacionRepositorio, EvaluacionRepositorio>();
builder.Services.AddScoped<IInscripcionCarreraRepositorio, InscripcionCarreraRepositorio>();
builder.Services.AddScoped<IRepositorio<MAB>, Repositorio<MAB>>();
builder.Services.AddScoped<IRepositorio<Materia>, Repositorio<Materia>>();
builder.Services.AddScoped<IMateriaEnPlanEstudioRepositorio, MateriaEnPlanEstudioRepositorio>();
builder.Services.AddScoped<INotaRepositorio, NotaRepositorio>();
builder.Services.AddScoped<IRepositorio<Persona>, Repositorio<Persona>>();
builder.Services.AddScoped<IPlanEstudioRepositorio, PlanEstudioRepositorio>();
builder.Services.AddScoped<IRepositorio<Profesor>, Repositorio<Profesor>>();
builder.Services.AddScoped<IRepositorio<TipoDocumento>, Repositorio<TipoDocumento>>();
builder.Services.AddScoped<ITurnoRepositorio, TurnoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();

// Configuración de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Construir la aplicación
var app = builder.Build();

// Configurar el pipeline de solicitud HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Servir archivos estáticos y páginas Razor
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

// Configurar enrutamiento
app.UseRouting();

app.MapRazorPages(); // Si usas Razor Pages, habilita esta línea
app.MapControllers(); // Habilita los controladores de API

app.UseAuthorization(); // Habilitar autorización (si tienes configurado algún sistema de auth)

app.MapFallbackToFile("index.html"); // Para SPAs con Blazor o aplicaciones front-end

// Ejecutar la aplicación
app.Run();
