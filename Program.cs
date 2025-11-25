using ContabilidadBackend.Application.Services;
using ContabilidadBackend.Consumos;
using ContabilidadBackend.Core.Entities;
using ContabilidadBackend.Core.Interfaces;
using ContabilidadBackend.Infrastructure.Data;
using ContabilidadBackend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:8080");

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_PUBLIC_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ContabilidadContext>(options =>
    options.UseNpgsql(databaseUrl)
);

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

builder.Services.AddScoped<IIngresoService, IngresoService>();
builder.Services.AddScoped<IPresupuestoService, PresupuestoService>();
builder.Services.AddScoped<INominaService, NominaService>();
builder.Services.AddScoped<IEgresoService, EgresoService>();
builder.Services.AddScoped<ISolicitudGastoService, SolicitudGastoService>();
builder.Services.AddScoped<IPedidoFabricaService, PedidoFabricaService>();
builder.Services.AddScoped<ICierreVentasService, CierreVentasService>();
builder.Services.AddScoped<IFacturacionMensualService, FacturacionMensualService>();

builder.Services.AddHttpClient<SucursalService>();
builder.Services.AddHttpClient<RRHHService>();
builder.Services.AddHttpClient<MarketingService>();
builder.Services.AddHttpClient<VentasService>();
builder.Services.AddHttpClient<DistribucionConsumerService>();

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContabilidadContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
