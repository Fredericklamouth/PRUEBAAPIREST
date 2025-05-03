namespace UltimaPrueba.Domain.Model
{

    //Modelo USUARIO: Me permite administrar los usuarios que tienen acceso a las distintas funciones del API REST
    public class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
