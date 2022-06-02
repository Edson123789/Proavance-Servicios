using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Servicios.Interface;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BSI.Integra.Aplicacion.Servicios.BO
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
        /// <param name="idCampaniaMailChimp"></param>
        /// <returns></returns>
        public CorreoAbiertoPorCampaniaMailchimp ObtenerInteraccionCorreoAbierto(string idCampaniaMailChimp, int offset, int count)
        {
            try
            {
                var _url = string.Concat(_baseUrl, "reports/", idCampaniaMailChimp, "/open-details?offset=",offset,"&count=", count);

                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", this.GetMailChimpKey());
                var result = client.GetAsync(new Uri(_url)).Result;
                if (result.IsSuccessStatusCode)
                {
                    var jsonContent = result.Content.ReadAsStringAsync().Result;
                    return  JsonConvert.DeserializeObject<CorreoAbiertoPorCampaniaMailchimp>(jsonContent);
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
        /// <param name="idCampaniaMailchimp"></param>
        /// <returns></returns>
        public ClickEnlacePorCampaniaMailchimp ObtenerEnlaces(string idCampaniaMailChimp, int offset, int count)
        {
            try
            {
                var _url = string.Concat(_baseUrl, "reports/", idCampaniaMailChimp, "/click-details?offset=", offset, "&count=", count);

                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", this.GetMailChimpKey());
                var result = client.GetAsync(new Uri(_url)).Result;
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
        public ClickEnlaceContactoPorCampaniaMailchimp ObtenerInteraccionEnlace(string idCampaniaMailChimp, string idUrlMailChimp, int offset, int count)
        {
            try
            {
                var _url = string.Concat(_baseUrl, "reports/", idCampaniaMailChimp, "/click-details/",idUrlMailChimp, "/members?offset=", offset, "&count=", count);

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
        /// <param name="idCampaniaMailChimp"></param>
        /// <param name="idUrlMailChimp"></param>
        /// <returns></returns> 
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
    }
}
