using ContabilidadBackend.Core.Interfaces;
using ContabilidadBackend.Core.Services;
using ContabilidadBackend.Core.Interfaces;
using ContabilidadBackend.Core.Services;
using ContabilidadBackend.Consumos;
using ContabilidadBackend.Infrastructure.Data;
using ContabilidadBackend.Infrastructure.Repositories;
using ContabilidadBackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración de puerto para Railway
builder.WebHost.UseUrls("http://0.0.0.0:8080");

// -----------------------------------------------------------------------------
// 1. ZONA DE BASE DE DATOS 
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_PUBLIC_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ContabilidadContext>(options =>
    options.UseNpgsql(databaseUrl)
);

// 2. REPOSITORIOS Y SERVICIOS

builder.Services.AddScoped<GenericRepository<Ingreso>>();
builder.Services.AddScoped<GenericRepository<Egreso>>();
builder.Services.AddScoped<GenericRepository<Presupuesto>>();
builder.Services.AddScoped<GenericRepository<CuentaPorCobrar>>();
builder.Services.AddScoped<GenericRepository<CuentaPorPagar>>();

builder.Services.AddScoped<IIngresoService, IngresoService>();
builder.Services.AddScoped<IPresupuestoService, PresupuestoService>();



builder.Services.AddHttpClient<SucursalService>();
builder.Services.AddHttpClient<RRHHService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contabilidad API", Version = "v1" });
});

var app = builder.Build();

app.UseCors("AllowAll");

// -----------------------------------------------------------------------------
// 3. MIGRACIONES 

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContabilidadContext>();
    db.Database.Migrate();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
