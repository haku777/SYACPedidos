using System.ComponentModel.DataAnnotations;

namespace PedidosSYAC.Common.Dto.Clientes
{
    public class ClientesCreacionDto
    {
        //[Range(100000000, 2000000000, ErrorMessage = "La identificacion no puede ser mayor de 10 digitos")]
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
    }
}
