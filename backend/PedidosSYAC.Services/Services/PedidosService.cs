using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PedidosSYAC.Common.Dto.Pedidos;
using PedidosSYAC.Common.Dto.Productos;
using PedidosSYAC.DataAccess;
using PedidosSYAC.DataAccess.Entity;
using PedidosSYAC.Services.Interfaces;
using PedidosSYAC.Services.Services.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedidosSYAC.Services.Services
{
    public class PedidosService : IPedidos
    {
        private readonly PedidosContext _context;
        private readonly IMapper _mapper;
        private readonly IClientes _clientes;
        public PedidosService(PedidosContext context, IMapper mapper, IClientes clientes)
        {
            _context = context; _mapper = mapper;
            _clientes = clientes;
        }

        public async Task<List<PedidosDto>> Get() 
        { 
            //var pedidos = await _context.Pedidos.ToListAsync();
            var pedidos = await _context.Pedidos.ProjectTo<PedidosDto>(_mapper.ConfigurationProvider).ToListAsync();
            List<PedidosDto> listaPedidos = _mapper.Map<List<PedidosDto>>(pedidos);
            return listaPedidos;
        }

        public async Task<List<PedidosDto>> GetByIdCliente(string IdCliente) 
        {
            return await _context.Pedidos.ProjectTo<PedidosDto>(_mapper.ConfigurationProvider).Where(p=>p.Cliente.identificacion == IdCliente).ToListAsync();
        }

        public async Task<PedidosDto> AddPedido(PedidosCreacionDto PedidoCreacion) 
        {
            //validacion de stock mediante front en el producto a seleccionar
            var cliente = await _context.Clientes.Where(c=>c.Identificacion==PedidoCreacion.IndentificacionCliente).FirstOrDefaultAsync();
            if (cliente is null) throw new Exception("no existe cliente con esa identificacion");

            //ValorTotal pedido
            var valorTotal = ValorTotalPedido(PedidoCreacion.ListaIdProductos);

            var pedido = new Pedidos()
            {
                Id_Cliente = cliente.Id,
                Id_Estado = (int)Enums.Estados.Registrado,
                ValorTotal = valorTotal
            };
            var nuevoPedido = _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            //calcular producto por cantidad PedidoCreacion.Productos (cantidad x valor unitario)
            //por cada producto agregamos el registro y sus valores por producto cantidad y valor para pedido
            var productosAgregar = new List<ProductosXPedido>();

            foreach (var item in PedidoCreacion.ListaIdProductos)
            {
                var valorUnitarioProducto = _context.Productos.FirstOrDefault(p => p.Id == item.IdProducto);
                productosAgregar.Add(new ProductosXPedido
                    {
                        Id_Pedido = pedido.Id,
                        Id_Producto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        ValorPorCantidad = valorUnitarioProducto.ValorUnitario * item.Cantidad
                    }
                );
            }

            _context.ProductosXPedido.AddRange(productosAgregar);
            await _context.SaveChangesAsync();
            return new PedidosDto();
        }



        public decimal ValorTotalPedido(List<ProductosCantidadDto> productos)
        {
            if (productos == null || !productos.Any()) return 0;

            //de forma declarativa podemos pasar todo de una vez, de forma declarativa podriamos validar si esta pidiendo mas stock del que hay
            //var idsBusqueda = productos.Select(p => p.IdProducto).ToList();

            //var productosDb = _context.Productos
            //.Where(p => idsBusqueda.Contains(p.Id))
            //.ToList();

            //return productosDb.Sum(pDb =>
            //pDb.ValorUnitario * productos.First(pDto => pDto.IdProducto == pDb.Id).Cantidad
            //);
            decimal totalPedido = 0;
            foreach (var producto in productos)
            {
                var productoDb = _context.Productos.FirstOrDefault(p => p.Id == producto.IdProducto);
                if (productoDb != null)
                {
                    if (productoDb.Cantidad < producto.Cantidad) throw new Exception("cantidad supera el stock disponible");
                    totalPedido += (productoDb.ValorUnitario * producto.Cantidad);
                }
                else throw new Exception("producto inexistente");
            }
            return totalPedido;
        }




        public async void Stock(PedidosCreacionDto PedidoCreacion) 
        {
            //var hayProductos = PedidoCreacion.Productos.Select(p => p.Id).ToList();
            ////var hayProductos = _context.Productos.Where(x => x.Cantidad <= PedidoCreacion.Producto);

            //var productosDb =await _context.Productos.Where(p => hayProductos.Contains(p.Id)).ToListAsync();

            //var sinStock = PedidoCreacion.Productos.Where(p => productosDb.Any(db => db.Id == p.Id && db.Cantidad < p.Cantidad)).ToList();

            //if (sinStock.Any())
            //{
                throw new Exception("Algunos productos no tienen stock suficiente.");
            //}
        }
    }
}
