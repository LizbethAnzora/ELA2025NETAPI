using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Reclutamiento.Context;
using Reclutamiento.Implementaciones;
using Reclutamiento.Interfaces;
using EFCore.NamingConventions.Internal;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conString = builder.Configuration.GetConnectionString("Conn");
builder.Services.AddDbContext<ReclutamientoContext>(options => options.UseMySql(conString, ServerVersion.AutoDetect(conString))
.UseSnakeCaseNamingConvention());

// Inyección de Dependencias (DI) para repositorios y servicios.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IVacanteRepository, VacanteRepository>();
builder.Services.AddScoped<ISolicitudRepository, SolicitudRepository>();
builder.Services.AddScoped<IRespuestaSolicitudRepository, RespuestaSolicitudRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IVacanteService, VacanteService>();
builder.Services.AddScoped<ISolicitudService, SolicitudService>();
builder.Services.AddScoped<IRespuestaSolicitudService, RespuestaSolicitudService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reclutamiento API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reclutamiento API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();



app.Run();