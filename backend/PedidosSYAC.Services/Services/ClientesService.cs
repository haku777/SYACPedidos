using AutoMapper;
using AutoMapper.QueryableExtensions;
using PedidosSYAC.Common.Constants;
using Microsoft.EntityFrameworkCore;
using PedidosSYAC.Common.Dto.Clientes;
using PedidosSYAC.DataAccess;
using PedidosSYAC.DataAccess.Entity;
using PedidosSYAC.Services.Services.Interfaces;
using System.Data;

namespace PedidosSYAC.Services.Services
{
    public class ClientesService : IClientes
    {
        private readonly PedidosContext _context;
        private readonly IMapper _mapper;
        public ClientesService(PedidosContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<ClientesDto>> Get()
        {
            var clientes = await _context.Clientes.ToListAsync();
            //mapeamos todos los clientes evitando mapear uno a uno
            //return _mapper.Map<List<ClientesDto>>(clientes);

            //mapeo solo de las columnas necesarias
            return await _context.Clientes.ProjectTo<ClientesDto>(_mapper.ConfigurationProvider).ToListAsync();
        }


        public async Task<ClientesDto> GetByIdentificacion(string Identificacion)
        {
            var autor = await _context.Clientes.FirstOrDefaultAsync(a=>a.Identificacion == Identificacion);
            ClientesDto mapAutor = _mapper.Map<ClientesDto>(autor);
            return mapAutor;
        }

        public async Task<ClientesDto> AddCliente(ClientesCreacionDto cliente)
        {
            var existeCliente = await GetByIdentificacion(cliente.Identificacion);

            if (existeCliente != null) throw new Exception(Messages.existCliente);

            var nuevoClienteMapeado = _mapper.Map<Clientes>(cliente);
            var result = await _context.Clientes.AddAsync(nuevoClienteMapeado);
            await _context.SaveChangesAsync();
            ClientesDto nuevoCliente = await GetByIdentificacion(result.Entity.Identificacion);
            return nuevoCliente;
        }
  
        public async Task UpdateCliente(ClientesActualizarDto cliente)
        {
            var updateCliente = _context.Clientes.FirstOrDefault(a=>a.Identificacion == cliente.Identificacion);
            if (updateCliente != null) {
                updateCliente.Identificacion= cliente.Identificacion;
                updateCliente.Nombre= cliente.Nombre;
                updateCliente.Direccion= cliente.Direccion;
                await _context.SaveChangesAsync();
            }
        }


        public async Task<int> DeleteClienteAsync(string identification) 
        {
            return await _context.Clientes.Where(a=>a.Identificacion == identification).ExecuteDeleteAsync();
        }
    }
}
