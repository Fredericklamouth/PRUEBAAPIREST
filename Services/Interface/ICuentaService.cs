using System.Threading.Tasks;
using UltimaPrueba.Domain.Model;
using UltimaPrueba.DTO;

namespace UltimaPrueba.Services.Interface
{
    // Define el contrato para los servicios relacionados con la lógica de negocio de cuentas.
    public interface ICuentaService
    {
        /*
         Obtiene una lista de todas las cuentas existentes.
         Retorna una lista de objetos CuentaDto.
         */
        Task<List<CuentaDto>> ObtenerCuentasAsync();

        /*
         Crea una nueva cuenta en el sistema.
         Recibe un DTO con los datos de la cuenta.
         Retorna la cuenta creada como CuentaDto.
         */
        Task<CuentaDto> CrearCuentaAsync(CuentaCreateDto dto);

        /*
         Actualiza los datos de una cuenta existente.
         Recibe un DTO con los datos actualizados.
         Retorna true si la actualización fue exitosa, false si no se encontró la cuenta.
         */
        Task<bool> ActualizarCuentaAsync(CuentaDto dto);

        /*
         Elimina una cuenta según su ID.
         Retorna true si la eliminación fue exitosa, false si no se encontró la cuenta.
         */
        Task<bool> EliminarCuentaAsync(int id);

        /*
         Agrega saldo a una cuenta específica.
         Recibe un DTO con el ID de la cuenta y el monto a agregar.
         Retorna true si la operación fue exitosa, false en caso contrario.
         */
        Task<bool> AgregarSaldoAsync(AgregarSaldoDto dto);
    }
}
