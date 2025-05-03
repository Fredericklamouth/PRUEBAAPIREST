using UltimaPrueba.Domain.Model;

namespace UltimaPrueba.Domain.Interfaces
{
    // Define el contrato para las operaciones de acceso a datos relacionadas con cuentas.
    public interface ICuentaRepository
    {
        /*
         Obtiene todas las cuentas registradas en el sistema.
         Retorna una lista de objetos Cuenta.
         */
        Task<List<Cuenta>> ObtenerTodasAsync();

        /*
         Obtiene una cuenta específica por su identificador único.
         Retorna el objeto Cuenta si se encuentra, o null si no existe.
         */
        Task<Cuenta> ObtenerPorIdAsync(int id);

        /*
         Crea una nueva cuenta en el sistema.
         Recibe un objeto Cuenta como parámetro y lo guarda.
         Retorna la cuenta creada.
         */
        Task<Cuenta> CrearAsync(Cuenta cuenta);

        /*
         Actualiza la información de una cuenta existente.
         Retorna true si la operación fue exitosa, false en caso contrario.
         */
        Task<bool> ActualizarAsync(Cuenta cuenta);

        /*
         Elimina una cuenta del sistema por su identificador.
         Retorna true si la eliminación fue exitosa, false si la cuenta no fue encontrada.
         */
        Task<bool> EliminarAsync(int id);
    }
}

