using UltimaPrueba.Domain.Model;
using UltimaPrueba.DTO;

namespace UltimaPrueba.Services.Interface
{
    // Define el contrato para los servicios relacionados con las transacciones entre cuentas.
    public interface ITransaccionService
    {
        /*
         Obtiene la lista de todas las transacciones registradas en el sistema.
         Retorna una lista de objetos Transaccion.
         */
        Task<List<Transaccion>> ObtenerTransaccionesAsync();

        /*
         Realiza una transacción entre dos cuentas (origen y destino).
         Recibe un DTO con los datos necesarios para realizar la operación.
         Retorna la transacción realizada.
         */
        Task<Transaccion> RealizarTransaccionAsync(TrasaccionDto dto);
    }
}
