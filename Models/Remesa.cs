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

        [Required(ErrorMessage = "El remitente es requerido.")]
        public string Remitente { get; set; }

        [Required(ErrorMessage = "El destinatario es requerido.")]
        public string Destinatario { get; set; }

        [Required(ErrorMessage = "El país de destino es requerido.")]
        public string PaísDestino { get; set; }

        [Required(ErrorMessage = "El tipo de moneda es requerido.")]
        public string TipoMoneda { get; set; } // "USD" or "BTC"

        [Required(ErrorMessage = "El monto es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public decimal Monto { get; set; }

        public decimal? TasaCambio { get; set; } // Nullable for USD transactions

        public DateTime FechaEnvio { get; set; }

        public DateTime? FechaRecepcion { get; set; } // Nullable, not yet received

        public string Estado { get; set; } // "Pendiente", "Enviada", "Recibida", "Cancelada"
    }
}
