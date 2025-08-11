using System.ComponentModel.DataAnnotations;

namespace CxcProject.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        [Display(Name = "Tipo de Movimiento")]
        public string TipoMovimiento { get; set; }
        [Display(Name = "Tipo de Documento")]
        public int TipoDocumentoId { get; set; }
        [Display(Name = "Número de Documento")]
        public string NumeroDocumento { get; set; }
        public DateTime Fecha { get; set; }
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        public decimal Monto { get; set; }
        [Display(Name = "Tipo de Documento")]
        public TipoDocumento TipoDocumento { get; set; }
        public Cliente Cliente { get; set; }
    }
}
