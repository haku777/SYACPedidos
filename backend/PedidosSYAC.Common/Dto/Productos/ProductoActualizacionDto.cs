using System.ComponentModel.DataAnnotations;


namespace PedidosSYAC.Common.Dto.Productos
{
    public class ProductoActualizacionDto
    {
        [Required]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}
