using Microsoft.EntityFrameworkCore;
using PedidosSYAC.DataAccess.Entity;

namespace PedidosSYAC.DataAccess
{
    public class PedidosContext : DbContext
    {
        public PedidosContext(
            DbContextOptions<PedidosContext> options) : base(options) 
        { 
        }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<ProductosXPedido> ProductosXPedido { get; set; }


        //se agregan los estados :) para evitar la molestia de crearlos XD
        //pdt: se puede agregar el singleton para su llamado y evitar estar haciendo la consulta
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estados>().HasData(
                    new() { Id = 1, Estado = "Registrado" },
                    new() { Id = 2, Estado = "Confirmar" },
                    new() { Id = 3, Estado = "Anular" }
            );

            modelBuilder.Entity<Clientes>().HasData(
                    new Clientes { Id = 1, Identificacion = "1230000007", Nombre = "Jimmy", Direccion = "DirSYAC" }
            );
            modelBuilder.Entity<Productos>().HasData(
                new Productos { Id = 1, Nombre = "Magnat", Cantidad = 25, ValorUnitario= 50 },
                new Productos { Id = 2, Nombre = "Code Noir", Cantidad = 25, ValorUnitario = 60 }
            );

            modelBuilder.Entity<Pedidos>().HasData(
                new Pedidos { Id = 1, Id_Cliente = 1, Id_Estado = 1, ValorTotal = 210 }
            );

            modelBuilder.Entity<ProductosXPedido>().HasData(
                new ProductosXPedido { Id = 1, Id_Pedido = 1, Id_Producto = 1, Cantidad = 2, ValorPorCantidad = 100 },
                new ProductosXPedido { Id = 2, Id_Pedido = 1, Id_Producto = 2, Cantidad = 1, ValorPorCantidad = 60 }
            );
        }

    }
}