using System.ComponentModel.DataAnnotations;

namespace CxcProject.Models
{
    public class AsientoContable
    {
        public int Id { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        public string Cuenta { get; set; }
        [Display(Name = "Tipo de Movimiento")]
        public string TipoMovimiento { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public bool Estado { get; set; }
        public Cliente Cliente { get; set; }
    }
}
