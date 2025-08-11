using System.ComponentModel.DataAnnotations;

namespace CxcProject.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        [Display(Name = "Límite de Crédito")]
        public decimal LimiteCredito { get; set; }
        public bool Estado { get; set; }
    }
}
