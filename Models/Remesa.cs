using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROGRA_PARCIAL.Models
{
    [Table("t_remesas")]
    public class Remesa
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        [Required]
        public string? NombreRemitente { get; set; }
        [Required]
        public string? NombreDestinario { get; set; }
        [Required]
        public string? PaisOrigen { get; set; }
        [Required]
        public string? PaisDestino { get; set; }
        [Required]
        public decimal MontoEnviado { get; set; }
        [Required]
        public string? TipoMoneda { get; set; }
        [Required]
        public decimal TasaCambio { get; set; }
        [Required]
        public decimal MontoFinal { get; set; }
        [Required]
        public string? Estado { get; set; }
    }
}
