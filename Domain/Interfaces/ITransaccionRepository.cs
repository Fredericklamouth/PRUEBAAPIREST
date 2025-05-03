using UltimaPrueba.Domain.Model;

namespace UltimaPrueba.Domain.Interfaces
{
    /*
         Interfaz que define las operaciones de acceso a datos relacionadas con transacciones.
         Se utiliza para abstraer la lógica de persistencia de transacciones en la base de datos.
     */
    public interface ITransaccionRepository
    {
        // Obtiene la lista de todas las transacciones registradas.
        Task<List<Transaccion>> ObtenerTodasAsync();

        // Crea una nueva transacción en la base de datos.
        // Retorna la transacción creada con sus datos completos.
        Task<Transaccion> CrearAsync(Transaccion transaccion);
    }
}
