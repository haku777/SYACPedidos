using PedidosSYAC.Common.Dto.Pedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSYAC.Services.Services.Interfaces
{
    public interface IPedidos
    {
        Task<List<PedidosDto>> Get();
        Task<List<PedidosDto>> GetByIdCliente(string IdCliente);
        Task<PedidosDto> AddPedido(PedidosCreacionDto PedidoCreacion);
    }
}
