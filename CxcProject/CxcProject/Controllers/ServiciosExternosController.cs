using Microsoft.AspNetCore.Mvc;
using CxcProject.Interfaces;

namespace CxcProject.Controllers
{
    public class ServiciosExternosController : Controller
    {
        private readonly IServiciosExternosService _service;

        public ServiciosExternosController(IServiciosExternosService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TasaCambio(string moneda = "USD")
        {
            var tasa = await _service.ObtenerTasaCambioAsync(moneda);

            ViewBag.TasaCambio = tasa;
            ViewBag.MonedaSeleccionada = moneda;

            // Diccionario para mostrar nombre completo de las monedas
            ViewBag.NombresMonedas = new Dictionary<string, string>
           {
               {"USD", "Dólar estadounidense"},
               {"MXN", "Peso mexicano"},
               {"EUR", "Euro"},
               {"CAD", "Dólar canadiense"},
               {"JPY", "Yen japonés"}
            };

            return View();
        }

        public async Task<IActionResult> Inflacion(string periodo)
        {
            var inflacion = await _service.ObtenerInflacionAsync(periodo);
            ViewBag.Periodo = periodo;
            ViewBag.Inflacion = inflacion;

            ViewBag.Anios = Enumerable.Range(2020, 10).ToList();
            ViewBag.Meses = new Dictionary<int, string> {
                 {1,"Enero"}, {2,"Febrero"}, {3,"Marzo"}, {4,"Abril"},
                 {5,"Mayo"}, {6,"Junio"}, {7,"Julio"}, {8,"Agosto"},
                 {9,"Septiembre"}, {10,"Octubre"}, {11,"Noviembre"}, {12,"Diciembre"}
             };

            return View();
        }

        public async Task<IActionResult> SaludFinanciera(string cedulaRnc)
        {
            var resultado = await _service.ConsultarSaludFinancieraAsync(cedulaRnc);
            ViewBag.CedulaRnc = cedulaRnc;
            ViewBag.SaludFinanciera = resultado;
            return View();
        }

        public async Task<IActionResult> HistorialCrediticio(string cedulaRnc)
        {
            var resultado = await _service.ConsultarHistorialCrediticioAsync(cedulaRnc);
            ViewBag.CedulaRnc = cedulaRnc;
            ViewBag.Historial = resultado;
            return View();
        }

    }
}