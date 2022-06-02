using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Servicios.Interface;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Service.DTOs;
namespace BSI.Integra.Aplicacion.Servicios.Services
{
    public class InteraccionMailChimpService : IMailChimp
    {
        private readonly string _mailchimpKey = "a607090294d386ecee0fdf944962f2f8-us12";
        private readonly string _baseUrl = "https://us12.api.mailchimp.com/3.0/";
        public string GetMailChimpKey()
        {
            return _mailchimpKey;
        }

        /// <summary>
        /// Obtiene las interacciones por correo abierto de mailchimp por campaña
        /// </summary>
        /// <param name="idCampaniaMailChimp">Id de la campania Mailchimp</param>
        /// <param name="count">Conteo para descarga</param>
        /// <param name="offset">Margen inicial</param>
        /// <returns>Objeto de clase CorreoAbiertoPorCampaniaMailchimp</returns>
        public CorreoAbiertoPorCampaniaMailchimp ObtenerInteraccionCorreoAbierto(string idCampaniaMailChimp, int offset, int count)
        {
            try
            {
                var _url = string.Concat(_baseUrl, "reports/", idCampaniaMailChimp, "/open-details?offset=", offset, "&count=", count);

                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", this.GetMailChimpKey());
                var result = client.GetAsync(new Uri(_url)).Result;
                if (result.IsSuccessStatusCode)
                {
                    var jsonContent = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<CorreoAbiertoPorCampaniaMailchimp>(jsonContent);
                }
                else
                {
                    throw new Exception("Error" + result.StatusCode);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los enlaces por campaña mailChimp
        /// </summary>
        /// <param name="idCampaniaMailChimp">Id original de la campania Mailchimp</param>
        /// <param name="offset">Offset para la descarga</param>
        /// <param name="count">Contador para la descarga</param>
        /// <returns>Objeto de clase ClickEnlacePorCampaniaMailchimp</returns>
        public ClickEnlacePorCampaniaMailchimp ObtenerEnlaces(string idCampaniaMailChimp, int offset, int count)
        {
            try
            {
                var url = string.Concat(_baseUrl, "reports/", idCampaniaMailChimp, "/click-details?offset=", offset, "&count=", count);

                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", this.GetMailChimpKey());
                var result = client.GetAsync(new Uri(url)).Result;
                if (result.IsSuccessStatusCode)
                {
                    var jsonContent = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ClickEnlacePorCampaniaMailchimp>(jsonContent);
                }
                else
                {
                    throw new Exception("Error" + result.StatusCode);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las interacciones de click en enlace por campaña y enlace de MailChimp
        /// </summary>
        /// <param name="idCampaniaMailChimp"></param>
        /// <param name="idUrlMailChimp"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public ClickEnlaceContactoPorCampaniaMailchimp ObtenerInteraccionClickEnlace(string idCampaniaMailChimp, string idUrlMailChimp, int offset, int count)
        {
            try
            {
                var _url = string.Concat(_baseUrl, "reports/", idCampaniaMailChimp, "/click-details/", idUrlMailChimp, "/members?offset=", offset, "&count=", count);

                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", this.GetMailChimpKey());
                var result = client.GetAsync(new Uri(_url)).Result;
                if (result.IsSuccessStatusCode)
                {
                    var jsonContent = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ClickEnlaceContactoPorCampaniaMailchimp>(jsonContent);
                }
                else
                {
                    throw new Exception("Error" + result.StatusCode);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las interacciones de click en enlace por campaña y enlace de MailChimp
        /// </summary>
        /// <param name="idCampaniaMailChimp">Id original de campania mailchimp</param>
        /// <param name="idUrlMailChimp">Id original de la url mailchimp</param>
        /// <returns>Objeto de clase ClickEnlaceContactoPorCampaniaMailchimp</returns> 
        public ClickEnlaceContactoPorCampaniaMailchimp ObtenerInteraccionEnlace(string idCampaniaMailChimp, string idUrlMailChimp)
        {
            try
            {
                var _url = string.Concat(_baseUrl, "reports/", idCampaniaMailChimp, "/click-details/", idUrlMailChimp, "/members");

                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", this.GetMailChimpKey());
                var result = client.GetAsync(new Uri(_url)).Result;
                if (result.IsSuccessStatusCode)
                {
                    var jsonContent = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ClickEnlaceContactoPorCampaniaMailchimp>(jsonContent);
                }
                else
                {
                    throw new Exception("Error" + result.StatusCode);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los detalles de una campaña
        /// Contactos
        /// </summary>
        /// <param name="idListaMailChimp">Id de la lista de MailChimp, formato de Mailchimp</param>
        /// <param name="offset">Margen inicial</param>
        /// <param name="count">Cantidad a requerir</param>
        /// <returns>Objeto de clase MailChimpListaDetalleDTO</returns>
        public MailChimpListaDetalleDTO DescargaListaDetalllePorListaMailChimp(string idListaMailChimp, int offset, int count)
        {
            try
            {
                var url = string.Concat(_baseUrl, "lists/", idListaMailChimp, "/members?offset=", offset, "&count=", count);

                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", this.GetMailChimpKey());
                var result = client.GetAsync(new Uri(url)).Result;
                if (result.IsSuccessStatusCode)
                {
                    var jsonContent = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<MailChimpListaDetalleDTO>(jsonContent);
                }
                else
                {
                    throw new Exception("Error" + result.StatusCode);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Descarga informacion de la campania MailChimp
        /// </summary>
        /// <param name="idCampaniaMailChimp">Id de la campania de MailChimp, formato de Mailchimp</param>
        /// <returns>Objeto de clase MailChimpDetalleCampaniaDTO</returns>
        public MailChimpDetalleCampaniaDTO DescargaInformacionCampaniaMailChimp(string idCampaniaMailChimp)
        {
            try
            {
                string url = string.Concat(_baseUrl, "campaigns/", idCampaniaMailChimp);

                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", this.GetMailChimpKey());
                var result = client.GetAsync(new Uri(url)).Result;
                if (result.IsSuccessStatusCode)
                {
                    var jsonContent = result.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<MailChimpDetalleCampaniaDTO>(jsonContent);
                }
                else
                {
                    throw new Exception("Error: " + result.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Descarga informacion de la campania MailChimp
        /// </summary>
        /// <param name="idCampaniaMailChimp">Id de la campania de MailChimp, formato de Mailchimp</param>
        /// <returns>Objeto de clase MailChimpReporteDTO</returns>
        public MailChimpReporteDTO DescargaCantidadEnviadosMailChimp(string idCampaniaMailChimp)
        {
            try
            {
                string url = string.Concat(_baseUrl, "reports/", idCampaniaMailChimp);

                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", this.GetMailChimpKey());
                var result = client.GetAsync(new Uri(url)).Result;
                if (result.IsSuccessStatusCode)
                {
                    var jsonContent = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<MailChimpReporteDTO>(jsonContent);
                }
                else
                {
                    throw new Exception("Error: " + result.StatusCode);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
