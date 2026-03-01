using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSYAC.Common.Dto.Productos
{
    public class ProductoDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }

    }
}
