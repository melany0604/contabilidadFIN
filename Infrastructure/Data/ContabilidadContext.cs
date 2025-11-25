using ContabilidadBackend.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContabilidadBackend.Infrastructure.Data
{
    public class ContabilidadContext : DbContext
    {
        public ContabilidadContext(DbContextOptions<ContabilidadContext> options) : base(options) { }

        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<Egreso> Egresos { get; set; }
        public DbSet<CuentaPorCobrar> CuentasPorCobrar { get; set; }
        public DbSet<CuentaPorPagar> CuentasPorPagar { get; set; }
        public DbSet<Presupuesto> Presupuestos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ingreso
            modelBuilder.Entity<Ingreso>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Ingreso>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            // Egreso
            modelBuilder.Entity<Egreso>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Egreso>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            // CuentaPorCobrar
            modelBuilder.Entity<CuentaPorCobrar>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<CuentaPorCobrar>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            // CuentaPorPagar
            modelBuilder.Entity<CuentaPorPagar>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<CuentaPorPagar>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            // Presupuesto
            modelBuilder.Entity<Presupuesto>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Presupuesto>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
