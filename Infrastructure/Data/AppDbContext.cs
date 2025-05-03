using Microsoft.EntityFrameworkCore;
using UltimaPrueba.Domain.Model;

namespace UltimaPrueba.Infrastructure.Data
{
    // Clase que representa el contexto de la base de datos de la aplicación
    // Hereda de DbContext para poder utilizar Entity Framework Core
    public class AppDbContext : DbContext
    {
        // DbSet para tabla de Usuarios
        public DbSet<Usuario> Usuarios { get; set; }

        // DbSet para tabla de Clientes
        public DbSet<Cliente> Clientes { get; set; }

        // DbSet para tabla de Cuentas bancarias
        public DbSet<Cuenta> Cuentas { get; set; }

        // DbSet para tabla de Transacciones
        public DbSet<Transaccion> Transacciones { get; set; }

        // Constructor que recibe opciones de configuración del DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Método para configurar el modelo de datos y las relaciones entre entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura relación: Una Cuenta pertenece a un Cliente
            modelBuilder.Entity<Cuenta>().HasOne(c => c.Cliente).WithMany(c => c.Cuentas).HasForeignKey(c => c.ClienteId);

            // Configura relación: Un Cliente puede tener muchas Cuentas (con eliminación en cascada)
            modelBuilder.Entity<Cliente>().HasMany(c => c.Cuentas).WithOne(c => c.Cliente).HasForeignKey(c => c.ClienteId).OnDelete(DeleteBehavior.Cascade);

            // Configura relación: Una Transacción tiene una Cuenta de origen (sin eliminar transacciones al eliminar la cuenta)
            modelBuilder.Entity<Transaccion>().HasOne(t => t.CuentaOrigen).WithMany().HasForeignKey(t => t.CuentaOrigenId).OnDelete(DeleteBehavior.Restrict);

            // Configura relación: Una Transacción tiene una Cuenta de destino (sin eliminar transacciones al eliminar la cuenta)
            modelBuilder.Entity<Transaccion>().HasOne(t => t.CuentaDestino).WithMany().HasForeignKey(t => t.CuentaDestinoId).OnDelete(DeleteBehavior.Restrict);

            // Llama al método base para aplicar configuraciones adicionales
            base.OnModelCreating(modelBuilder);
        }
    }
}