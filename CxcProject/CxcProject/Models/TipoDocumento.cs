using System.ComponentModel.DataAnnotations;

namespace CxcProject.Models
{
    public class TipoDocumento
    {
        public int Id { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Display(Name = "Cuenta Contable")]
        public string CuentaContable { get; set; }
        public bool Estado { get; set; }
    }
}
