using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedidosSYAC.DataAccess.Entity
{
    public class ProductosXPedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Id_Pedido { get; set; }
        [ForeignKey("Id_Pedido")]
        public virtual Pedidos Pedido { get; set; }
        public int Id_Producto { get; set; }
        [ForeignKey("Id_Producto")]
        public virtual Productos Producto { get; set; }
        public int Cantidad { get; set; }
        [Precision(18, 2)]
        public decimal ValorPorCantidad { get; set; }
    }
}
