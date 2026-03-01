using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PedidosSYAC.Common.Constants
{
    public class Notas
    {
        //terminal de NuGet Consol
        //default proyect ubicacion de las entidades
        //con esto agregamos la migracion al EF, desde la consola de nugets en tools
        //Add-Migration inicial -StartupProject PedidosSYAC

        //posterior a eso para agregar los registros y o cambios es necesario ejecutar la actualizacion
        //Update-Database -StartupProject PedidosSYAC

        //Remove-Migration

        //clase static se usa sin contrato,solo para consulta de valores calculados que no cambian calcularValores.GetValores //clase removida
    }
    public class Metodos {
        //Controller hereda de ControllerBase --controller(general)
        //ControllerBase
        //Ok() 200
        //BadRequest() 400
        //Created()	201	Se usa después de un POST. Indica que el recurso se creó con éxito. Suele incluir la URL del nuevo recurso.
        //NoContent() 204	El proceso terminó bien pero no hay nada que devolver(común en un PUT o DELETE).
        //Unauthorized() 401	El usuario no ha iniciado sesión o el token es inválido.
        //Forbidden()    403	El usuario está autenticado pero no tiene permisos para ver ese recurso específico.
        //NotFound()     404	Intentaste buscar un ID que no existe en la base de datos.
        //Conflict()     409	Hay un conflicto de lógica(ej.intentar registrar un usuario con un email que ya existe).
        //UnprocessableEntity()   422	Los datos son sintácticamente correctos pero violan reglas de negocio complejas.
        //StatusCode(500)	500	Se usa para errores genéricos o inesperados que ocurrieron en tu código.
    }
}
