using UltimaPrueba.Domain.Model;

namespace UltimaPrueba.Domain.Interfaces
{
    /*
     * Interfaz que define las operaciones de acceso a datos para la entidad Cliente.
     * Esta capa de abstracción permite separar la lógica de acceso a datos de la lógica de negocio.
     */
    public interface IClienteRepository
    {
        // Obtiene todos los clientes registrados en la base de datos.
        Task<List<Cliente>> ObtenerTodosAsync();

        // Crea un nuevo cliente y lo guarda en la base de datos.
        // Retorna el cliente creado con su ID generado.
        Task<Cliente> CrearClienteAsync(Cliente cliente);

        // Obtiene un cliente por su identificador único (GUID).
        // Si no se encuentra, retorna null.
        Task<Cliente?> ObtenerPorIdAsync(Guid id);

        // Actualiza los datos de un cliente existente.
        // Retorna el cliente actualizado.
        Task<Cliente> ActualizarClienteAsync(Cliente cliente);

        // Elimina un cliente de la base de datos.
        Task EliminarClienteAsync(Cliente cliente);

        // Obtiene un cliente por su ID, incluyendo sus cuentas asociadas.
        // Es útil para operaciones donde se necesita la relación Cliente-Cuentas.
        Task<Cliente> ObtenerPorIdConCuentasAsync(Guid id);
    }
}
