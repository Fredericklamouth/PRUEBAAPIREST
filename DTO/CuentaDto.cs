namespace UltimaPrueba.DTO
{

    //DTO Informacion importante a la hora de crear una cuenta
    public class CuentaCreateDto
    {
        public string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
        public Guid ClienteId { get; set; }
    }

    //DTO Informacion importante a la hora de obtener la informacion una cuenta
    public class CuentaDto
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
        public Guid ClienteId { get; set; }

    }

    //DTO Informacion importante a la hora de obtener la informacion una cuenta
    public class AgregarSaldoDto
    {
        public int CuentaId { get; set; }
        public decimal Monto { get; set; }
    }
}
