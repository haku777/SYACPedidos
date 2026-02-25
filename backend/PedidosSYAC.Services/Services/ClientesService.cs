using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
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
            var nuevoCliente = _mapper.Map<Clientes>(cliente);
            var existeCliente = await GetByIdentificacion(cliente.Identificacion);
            if (existeCliente!=null) throw new Exception("cliente existente");
            var result = await _context.Clientes.AddAsync(nuevoCliente);
            await _context.SaveChangesAsync();
            ClientesDto newBookAdded = await GetByIdentificacion(result.Entity.Identificacion);
            return newBookAdded;
        }
  
        public async Task UpdateCliente(ClientesActualizarDto cliente)
        {
            var xd = _context.Clientes.FirstOrDefault(a=>a.Identificacion == cliente.Identificacion);
            if (xd != null) {
                xd.Identificacion= cliente.Identificacion;
                xd.Nombre= cliente.Nombre;
                xd.Direccion= cliente.Direccion;
                await _context.SaveChangesAsync();
            }
        }


        public async Task<int> DeleteClienteAsync(string identification) 
        {
            return await _context.Clientes.Where(a=>a.Identificacion == identification).ExecuteDeleteAsync();
        }
    }
}
