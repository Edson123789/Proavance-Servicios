using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Nancy.Json;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CloudflareBO
    {
        private string urlToBase = "https://api.cloudflare.com/client/v4/";
        private string token = "KCAv-Y8yjhONWd88aFJIu2IYO2FZJbYhD-Je5TPw";

        public bool LimpiarCacheBsgInstitute()
        {
            string resultado;
            var parametros = new {purge_everything = true};

            using (WebClient client = new WebClient())
            {
                string urlToPost = urlToBase + "zones/ff0e3e3d87d144ba4592189b6dacbbe9/purge_cache";
                client.Encoding = Encoding.UTF8;
                var serializer = new JavaScriptSerializer();
                var serializedResult = serializer.Serialize(parametros);
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";                
                resultado = client.UploadString(urlToPost, serializedResult);
            }

            return resultado.Contains("true,");
        }
    }
}
