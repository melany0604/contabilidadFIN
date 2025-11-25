using ContabilidadBackend.Application.Services;
using ContabilidadBackend.Consumos;
using ContabilidadBackend.Core.Entities;
using ContabilidadBackend.Core.Interfaces;
using ContabilidadBackend.Infrastructure.Data;
using ContabilidadBackend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración para Railway (escuchar en el puerto correcto)
builder.WebHost.UseUrls("http://0.0.0.0:8080");

// 1. CONFIGURACIÓN DE BASE DE DATOS
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_PUBLIC_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ContabilidadContext>(options =>
    options.UseNpgsql(databaseUrl)
);

// 2. INYECCIÓN DE REPOSITORIOS (GENÉRICOS)
builder.Services.AddScoped<GenericRepository<Ingreso>>();
builder.Services.AddScoped<GenericRepository<Egreso>>();
builder.Services.AddScoped<GenericRepository<Presupuesto>>();
builder.Services.AddScoped<GenericRepository<CuentaPorCobrar>>();
builder.Services.AddScoped<GenericRepository<CuentaPorPagar>>();
builder.Services.AddScoped<GenericRepository<Nomina>>();
builder.Services.AddScoped<GenericRepository<SolicitudGasto>>();
builder.Services.AddScoped<GenericRepository<PedidoFabrica>>();
builder.Services.AddScoped<GenericRepository<CierreVentas>>();
builder.Services.AddScoped<GenericRepository<FacturacionMensual>>();

// 3. INYECCIÓN DE SERVICIOS DE APLICACIÓN (LÓGICA DE NEGOCIO)
builder.Services.AddScoped<IIngresoService, IngresoService>();
builder.Services.AddScoped<IPresupuestoService, PresupuestoService>();
builder.Services.AddScoped<INominaService, NominaService>(); // Importante para arreglar tus errores anteriores
builder.Services.AddScoped<IEgresoService, EgresoService>();
builder.Services.AddScoped<ISolicitudGastoService, SolicitudGastoService>();
builder.Services.AddScoped<IPedidoFabricaService, PedidoFabricaService>();
builder.Services.AddScoped<ICierreVentasService, CierreVentasService>();
builder.Services.AddScoped<IFacturacionMensualService, FacturacionMensualService>();

// 4. SERVICIOS EXTERNOS (HTTP CLIENTS)
builder.Services.AddHttpClient<SucursalService>();
builder.Services.AddHttpClient<RRHHService>();
builder.Services.AddHttpClient<MarketingService>();
builder.Services.AddHttpClient<VentasService>();
builder.Services.AddHttpClient<DistribucionConsumerService>();

// 5. CONFIGURACIÓN CORS
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

// 6. MIGRACIONES Y RESET DE BD (LÓGICA DESTRUCTIVA)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContabilidadContext>();
    try
    {
        Console.WriteLine("--> BORRANDO BD ANTIGUA (Para corregir error de tablas faltantes)...");
        // ESTA LÍNEA BORRA LA BD Y PERMITE CREARLA DE NUEVO (Solo útil si tienes problemas de esquema)
        db.Database.EnsureDeleted();

        Console.WriteLine("--> Aplicando migraciones...");
        db.Database.Migrate();
        Console.WriteLine("--> Migraciones OK. Tablas creadas correctamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"--> Error Migraciones: {ex.Message}");
    }
}

// 7. SWAGGER (Activado siempre para poder probar en Railway)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
