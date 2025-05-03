namespace UltimaPrueba.Domain.Model
{
    //Modelo Cliente: Permite manejar la informacion de los clientes
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public ICollection<Cuenta> Cuentas { get; set; } = new List<Cuenta>();

    }
}
