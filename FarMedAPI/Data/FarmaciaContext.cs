using Microsoft.EntityFrameworkCore;
using FarMedAPI.Models;

namespace FarMedAPI.Data
{
    public class FarmaciaContext : DbContext
    {
        public FarmaciaContext(DbContextOptions<FarmaciaContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<DetallePedido> DetallesPedidos { get; set; } = null!;
        public DbSet<Empleado> Empleados { get; set; } = null!;
        public DbSet<Entrega> Entregas { get; set; } = null!;
        public DbSet<Farmacia> Farmacias { get; set; } = null!;
        public DbSet<Laboratorio> Laboratorios { get; set; } = null!;
        public DbSet<Pedido> Pedidos { get; set; } = null!;
        public DbSet<Producto> Productos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configuración de la clave compuesta para DetallePedido
            modelBuilder.Entity<DetallePedido>()
                .HasKey(dp => new { dp.PedidoId, dp.ProductoId });
                
            // Configuración de la relación entre DetallePedido y Pedido
            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.Pedido)
                .WithMany(p => p.DetallesPedidos)
                .HasForeignKey(dp => dp.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Configuración de la relación entre DetallePedido y Producto
            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.Producto)
                .WithMany(p => p.DetallesPedidos)
                .HasForeignKey(dp => dp.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Configuración de la relación entre Pedido y Cliente
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Configuración de la relación entre Pedido y Farmacia
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Farmacia)
                .WithMany(f => f.Pedidos)
                .HasForeignKey(p => p.FarmaciaId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Configuración de la relación entre Pedido y Empleado
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Empleado)
                .WithMany(e => e.Pedidos)
                .HasForeignKey(p => p.EmpleadoId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Configuración de la relación entre Entrega y Pedido
            modelBuilder.Entity<Entrega>()
                .HasOne(e => e.Pedido)
                .WithOne(p => p.Entrega)
                .HasForeignKey<Entrega>(e => e.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Configuración de la relación entre Empleado y Farmacia
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Farmacia)
                .WithMany(f => f.Empleados)
                .HasForeignKey(e => e.FarmaciaId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Configuración de la relación entre Producto y Categoria
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.SetNull);
                
            // Configuración de la relación entre Producto y Laboratorio
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Laboratorio)
                .WithMany(l => l.Productos)
                .HasForeignKey(p => p.LaboratorioId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}