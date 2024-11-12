using WebITSC.Admin.Server.Repositorio;
using WebITSC.DB.Data;
using WebITSC.DB.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebITSC.Admin.Client.Servicios;
using System.Text.Json;
using Repositorio.General;
using WebITSC.Server.Controllers.General;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// servicio controller 
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(
    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Si necesitas mantener los nombres de propiedades
    });

//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
//    });

//swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Mi API", Version = "v1" });
});


// servicio controller
builder.Services.AddControllers();


//servicio Client
builder.Services.AddRazorPages();
builder.Services.AddScoped<IHttpServicios, HttpServicios>();
builder.Services.AddHttpClient();

//SERVICIO DE MAPPER
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//servicio que conecta la bd
builder.Services.AddDbContext<Context>(op => op.UseSqlServer("name=conn"));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IAlumnoRepositorio, AlumnoRepositorio>();
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
builder.Services.AddScoped<ITurnoRepositorio, TurnoRepositorio>();
builder.Services.AddScoped<ICertificadoAlumnoRepositorio, CertificadoAlumnoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
builder.Services.AddScoped<ICarreraRepositorio, CarreraRepositorio>();
builder.Services.AddScoped<ITipoDocumentoRepositorio, TipoDocumentoRepositorio>();   

builder.Services.AddScoped< IHttpServicios, HttpServicios>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.UseExceptionHandler("/error"); // Cambia esto para devolver JSON si es necesario

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
