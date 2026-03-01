using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PedidosSYAC.Common.Dto.Clientes;
using PedidosSYAC.Common.Dto.Productos;
using PedidosSYAC.DataAccess;
using PedidosSYAC.DataAccess.Entity;
using PedidosSYAC.Services.Interfaces;

namespace PedidosSYAC.Services.Services
{
    public class ProdutosService : IProductos
    {
        private PedidosContext _context;
        public IMapper _mapper;
        public ProdutosService(PedidosContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public  async Task<List<ProductoDto>> Get()
        {
            var Productos =  await _context.Productos.ToListAsync();
            List<ProductoDto> listaProductos = new List<ProductoDto>();
            foreach (var Producto in Productos) {
                var producto = _mapper.Map<ProductoDto>(Producto);
                listaProductos.Add(producto);
            }

            return listaProductos;
        }

        public async Task<ProductoDto> GetById(int IdProducto)
        {
            var productoBusqueda = await _context.Productos.ProjectTo<ProductoDto>(_mapper.ConfigurationProvider).Where(p => p.Id == IdProducto).ToListAsync();
            ProductoDto producto = _mapper.Map<ProductoDto>(productoBusqueda.FirstOrDefault());
            return producto;
        }

        public async Task<ProductoDto> GetByName(string nombreProducto)
        {
            var productoBusqueda = await _context.Productos.FirstOrDefaultAsync(b => b.Nombre == nombreProducto);
            ProductoDto producto = _mapper.Map<ProductoDto>(productoBusqueda);
            return producto;
        }

        public async Task<ProductoDto> AddProducto(ProductoCreacionDto producto)
        {
            Productos productoMapeado = _mapper.Map<Productos>(producto);
            var result = await _context.Productos.AddAsync(productoMapeado);
            await _context.SaveChangesAsync();
            ProductoDto productoAgregado = await GetByName(result.Entity.Nombre);
            return productoAgregado;
        }

        public async Task UpdateProducto(ProductoActualizacionDto producto)
        {
            var existsProducto = _context.Productos.FirstOrDefault(b => b.Id == producto.Id);
            if (existsProducto != null)
            {
                existsProducto.Nombre = producto.Nombre;
                existsProducto.Cantidad = producto.Cantidad;
                existsProducto.ValorUnitario = producto.ValorUnitario;
                
                await _context.SaveChangesAsync();
            }
        }

        public void DeleteProducto(ProductoDto producto) {
            var productoExistente = _context.Productos.FirstOrDefault(a => a.Id == producto.Id);
            if (productoExistente != null)
            {
                _context.Productos.Remove(productoExistente);
                _context.SaveChanges();
            }
        }

    }
}
