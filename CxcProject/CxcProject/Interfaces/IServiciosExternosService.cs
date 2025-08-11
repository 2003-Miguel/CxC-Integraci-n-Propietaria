namespace CxcProject.Interfaces
{
    public interface IServiciosExternosService
    {
        Task<decimal> ObtenerTasaCambioAsync(string codigoMoneda);
        Task<decimal> ObtenerInflacionAsync(string periodo);
        Task<string> ConsultarSaludFinancieraAsync(string cedulaRnc);
        Task<string> ConsultarHistorialCrediticioAsync(string cedulaRnc);
    }
}