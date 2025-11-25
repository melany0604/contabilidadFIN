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
// 1. ZONA DE BASE DE DATOS (ESTÁ COMENTADA PARA EL PRIMER DESPLIEGUE)
//    DESCOMENTAR ESTO CUANDO YA TENGAS POSTGRESQL CONECTADO EN RAILWAY
// -----------------------------------------------------------------------------

/* 
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_PUBLIC_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ContabilidadContext>(options =>
    options.UseNpgsql(databaseUrl)
);
*/

// NOTA: Si al desplegar te da error de "Dependency Injection" por falta del Context,
// puedes descomentar la siguiente línea temporalmente usando una base en memoria (si tienes el paquete)
// o simplemente dejarlo así, ya que Railway necesita que compile primero.

// -----------------------------------------------------------------------------
// FIN ZONA BASE DE DATOS
// -----------------------------------------------------------------------------


// -----------------------------------------------------------------------------
// 2. REPOSITORIOS Y SERVICIOS
//    SI EL DESPLIEGUE FALLA PORQUE NO ENCUENTRA "ContabilidadContext", 
//    TENDRÁS QUE COMENTAR ESTAS LÍNEAS TEMPORALMENTE HASTA QUE DESCOMENTES LA DB ARRIBA.
// -----------------------------------------------------------------------------
builder.Services.AddScoped<GenericRepository<Ingreso>>();
builder.Services.AddScoped<GenericRepository<Egreso>>();
builder.Services.AddScoped<GenericRepository<Presupuesto>>();
builder.Services.AddScoped<GenericRepository<CuentaPorCobrar>>();
builder.Services.AddScoped<GenericRepository<CuentaPorPagar>>();

builder.Services.AddScoped<IIngresoService, IngresoService>();
builder.Services.AddScoped<IPresupuestoService, PresupuestoService>();
// -----------------------------------------------------------------------------


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
// 3. MIGRACIONES AUTOMÁTICAS (COMENTADO)
//    DESCOMENTAR CUANDO TENGAS POSTGRESQL PARA QUE SE CREEN LAS TABLAS SOLAS
// -----------------------------------------------------------------------------

/*
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContabilidadContext>();
    db.Database.Migrate();
}
*/

// -----------------------------------------------------------------------------
// FIN MIGRACIONES
// -----------------------------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// En Railway a veces queremos ver Swagger en producción para probar
// Puedes descomentar esto si quieres ver Swagger en el link de Railway:
// app.UseSwagger();
// app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();