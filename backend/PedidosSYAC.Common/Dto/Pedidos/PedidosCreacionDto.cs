using PedidosSYAC.Common.Dto.Clientes;
using System.ComponentModel.DataAnnotations;

namespace PedidosSYAC.Common.Dto.Pedidos
{
    public class PedidosCreacionDto
    {
        public string IndentificacionCliente { get; set; }
        public List<ProductosCantidadDto> ListaIdProductos { get; set; }
    }

    public class ProductosCantidadDto 
    { 
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
