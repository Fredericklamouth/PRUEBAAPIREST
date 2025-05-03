using UltimaPrueba.Domain.Model;
using UltimaPrueba.DTO;

namespace UltimaPrueba.Services.Interface
{
    // Define el contrato para los servicios relacionados con la lógica de negocio de clientes.
    public interface IClienteService
    {
        /*
         Obtiene la lista de todos los clientes registrados.
         Retorna una lista de objetos Cliente.
         */
        Task<List<Cliente>> ObtenerClientesAsync();

        /*
         Crea un nuevo cliente en el sistema.
         Recibe un objeto Cliente como parámetro.
         Retorna el cliente creado.
         */
        Task<Cliente> CrearClienteAsync(Cliente cliente);

        /*
         Actualiza los datos de un cliente existente.
         Recibe un DTO con los datos actualizados.
         Retorna true si la actualización fue exitosa, false si no se encontró el cliente.
         */
        Task<bool> ActualizarClienteAsync(ClienteUpdateDto clienteDto);

        /*
         Elimina un cliente del sistema según su identificador.
         Retorna true si la eliminación fue exitosa, false si el cliente no se encontró.
         */
        Task<bool> EliminarClienteAsync(Guid id);

        /*
         Calcula y retorna el balance total del cliente (suma de los saldos de sus cuentas).
         Recibe el identificador del cliente.
         Retorna el balance como decimal.
         */
        Task<decimal> ObtenerBalanceClienteAsync(Guid clienteId);
    }
}
