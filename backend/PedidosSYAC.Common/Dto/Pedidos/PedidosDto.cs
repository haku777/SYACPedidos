using PedidosSYAC.Common.Dto.Clientes;
using PedidosSYAC.Common.Dto.Estados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSYAC.Common.Dto.Pedidos
{
    public class PedidosDto
    {
        public int Id { get; set; }
        //son necesarios nuevos dto aparte para cargar datos no sensibles y agregarlos al mapper con la entidad correspondiente
        public virtual ClientesDataDto Cliente { get; set; }
        public virtual EstadosDto Estado { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
