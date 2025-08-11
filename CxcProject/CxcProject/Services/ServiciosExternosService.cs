using CxcProject.Interfaces;
using System.Text;
using System.Xml;

namespace CxcProject.Services
{
    public class ServiciosExternosService : IServiciosExternosService
    {
        private readonly HttpClient _httpClient;

        public ServiciosExternosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<string> EnviarSoapRequestAsync(string url, string soapAction, string soapBody)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("SOAPAction", soapAction);
            request.Content = new StringContent(soapBody, Encoding.UTF8, "text/xml");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<decimal> ObtenerTasaCambioAsync(string codigoMoneda)
        {
            string url = "http://apec-wspublicos-app-dev2.eba-cs23gmph.us-east-2.elasticbeanstalk.com/TasaCambio.asmx";
            string action = "http://tempuri.org/ITasaCambioServiceSoap/ObtenerTasa";

            string body = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
               <soapenv:Header/>
               <soapenv:Body>
                  <tem:ObtenerTasa>
                     <tem:codigoMoneda>{codigoMoneda}</tem:codigoMoneda>
                  </tem:ObtenerTasa>
               </soapenv:Body>
             </soapenv:Envelope>";

            var responseXml = await EnviarSoapRequestAsync(url, action, body);

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseXml);

            var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("tem", "http://tempuri.org/");

            var node = xmlDoc.SelectSingleNode("//tem:ObtenerTasaResult", nsmgr);
            if (node != null && decimal.TryParse(node.InnerText, out decimal tasa))
            {
                return tasa;
            }

            throw new Exception("No se pudo obtener la tasa de cambio del servicio externo.");
        }

        public async Task<decimal> ObtenerInflacionAsync(string periodo)
        {
            string url = "http://apec-wspublicos-app-dev2.eba-cs23gmph.us-east-2.elasticbeanstalk.com/Inflacion.asmx";
            string action = "http://tempuri.org/ObtenerIndiceInflacion";

            string body = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                       <soapenv:Header/>
                       <soapenv:Body>
                          <tem:ObtenerIndiceInflacion>
                             <tem:periodo>{periodo}</tem:periodo>
                          </tem:ObtenerIndiceInflacion>
                       </soapenv:Body>
                     </soapenv:Envelope>";

            var responseXml = await EnviarSoapRequestAsync(url, action, body);

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseXml);

            var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("tem", "http://tempuri.org/");

            var node = xmlDoc.SelectSingleNode("//tem:ObtenerIndiceInflacionResult", nsmgr);
            if (node != null && decimal.TryParse(node.InnerText, out decimal inflacion))
            {
                return inflacion;
            }

            throw new Exception("No se pudo obtener el índice de inflación del servicio externo.");
        }

        public async Task<string> ConsultarSaludFinancieraAsync(string cedulaRnc)
        {
            string url = "http://apec-wspublicos-app-dev2.eba-cs23gmph.us-east-2.elasticbeanstalk.com/SaludFinanciera.asmx";
            string action = "http://tempuri.org/ConsultarSaludFinanciera";

            string body = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                               <soapenv:Header/>
                               <soapenv:Body>
                                  <tem:ConsultarSaludFinanciera>
                                     <tem:cedulaRnc>{cedulaRnc}</tem:cedulaRnc>
                                  </tem:ConsultarSaludFinanciera>
                               </soapenv:Body>
                             </soapenv:Envelope>";

            return await EnviarSoapRequestAsync(url, action, body);
        }

        public async Task<string> ConsultarHistorialCrediticioAsync(string cedulaRnc)
        {
            string url = "http://apec-wspublicos-app-dev2.eba-cs23gmph.us-east-2.elasticbeanstalk.com/HistorialCrediticio.asmx";
            string action = "http://tempuri.org/ConsultarHistorial";

            string body = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                               <soapenv:Header/>
                               <soapenv:Body>
                                  <tem:ConsultarHistorial>
                                     <tem:cedulaRnc>{cedulaRnc}</tem:cedulaRnc>
                                  </tem:ConsultarHistorial>
                               </soapenv:Body>
                             </soapenv:Envelope>";

            return await EnviarSoapRequestAsync(url, action, body);
        }
    }
}