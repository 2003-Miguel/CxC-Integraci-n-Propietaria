using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CxcProject.Interfaces;
using CxcProject.Models;

namespace CxcProject.Services
{
    public class ContabilidadService : IContabilidadService
    {
        private readonly HttpClient _httpClient;

        public ContabilidadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> EnviarAsientoContableAsync(AsientoContable asiento)
        {
            var payload = new
            {
                descripcion = asiento.Descripcion,
                auxiliar_Id = 5, //AUXILIAR: CUENTAS X COBRAR
                cuenta_Id = int.Parse(asiento.Cuenta),
                tipoMovimiento = asiento.TipoMovimiento.ToUpper(),
                fechaAsiento = asiento.Fecha.ToString("yyyy-MM-dd"),
                montoAsiento = asiento.Monto,
                estado_Id = asiento.Estado
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "http://3.80.223.142:3001/api/public/entradas-contables");
            request.Content = content;
            request.Headers.Add("x-api-key", "ak_live_e030145cab28d2cf2623fdc8bc9f2fb6ba0038253704b703");

            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                Console.WriteLine($"Error al enviar asiento: {response.StatusCode} - {responseString}");
                return false;
            }
        }

        public async Task<string> ObtenerEntradasContablesAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://3.80.223.142:3001/api/public/entradas-contables");
            request.Headers.Add("x-api-key", "ak_live_e030145cab28d2cf2623fdc8bc9f2fb6ba0038253704b703");

            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return responseString;
            }
            else
            {
                Console.WriteLine($"Error al obtener entradas: {response.StatusCode} - {responseString}");
                return null;
            }
        }
    }
}