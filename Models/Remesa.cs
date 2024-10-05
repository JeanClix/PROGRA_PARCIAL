using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROGRA_PARCIAL.Models
{
    [Table("t_remesas")]
    public class Remesa
    {
        [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Remitente { get; set; }
        public string Destinatario { get; set; }
        public string PaísDestino { get; set; }
        public string TipoMoneda { get; set; } // "USD" or "BTC"
        public decimal Monto { get; set; }
        public decimal? TasaCambio { get; set; } // Nullable for USD transactions
        public DateTime FechaEnvio { get; set; }
        public DateTime? FechaRecepcion { get; set; } // Nullable, not yet received
        public string Estado { get; set; } // "Pendiente", "Enviada", "Recibida", "Cancelada"
    }
}
