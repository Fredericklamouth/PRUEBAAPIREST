using UltimaPrueba.DTO;

namespace UltimaPrueba.Services.Interface
{
    public interface IAuthService
    {
        /*
         Realiza el proceso de inicio de sesión de un usuario.
         Recibe las credenciales mediante un objeto LoginDto.
         Retorna un token JWT como cadena si las credenciales son válidas.
         */
        Task<string> LoginAsync(LoginDto loginDto);
    }
}
