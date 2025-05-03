namespace UltimaPrueba.Domain.Model
{
    //Modelo TRANSACCION: Me permite manejar y almacenar los datos que se obtienen cuando se realiza una transaccion
    public class Transaccion
    {
        public int Id { get; set; }
        public int CuentaOrigenId { get; set; }      
        public int CuentaDestinoId { get; set; }        
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string Tipo { get; set; }                
        public string Descripcion { get; set; }

        public Cuenta CuentaOrigen { get; set; }
        public Cuenta CuentaDestino { get; set; }
    }
}
