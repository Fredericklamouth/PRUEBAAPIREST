namespace UltimaPrueba.Domain.Model
{
    //Modelo Cuenta: Permite manejar la informaciones de las cuentas de los clientes
    public class Cuenta
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }

        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<Transaccion> Transacciones { get; set; }
    }
}

