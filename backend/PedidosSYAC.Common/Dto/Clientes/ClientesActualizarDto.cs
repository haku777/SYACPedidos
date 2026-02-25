using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSYAC.Common.Dto.Clientes
{
    public class ClientesActualizarDto
    {
        [Required]
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
    }
}
