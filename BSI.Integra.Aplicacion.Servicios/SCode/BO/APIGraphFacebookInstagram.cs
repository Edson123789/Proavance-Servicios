using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System;
using System.Net;

namespace BSI.Integra.Aplicacion.Servicios.SCode.BO
{
    /// BO: APIGraphFacebookInstagram
    /// Autor: Joao Benavente - Richard Zenteno - Gian Miranda
    /// Fecha: 23/09/2021
    /// <summary>
    /// BO para la logica de peticiones hacia Facebook Instagram
    /// </summary>
    public class APIGraphFacebookInstagram
    {
        //Token de acceso a la Pagina de la APP bsgrupo.com 
        private static string accessTokenBSGrupocom = "EAAIbfrfWKZCABAMIZB20v7pAPpNyLkS96zHYa01GLyy3Y0nvJhzhi3pPPUsVrJYADFrrgqjfQs4wFmJhcpVSPEWjr9su8TFBwt4F1Apfbrdk8UEKvRkeFgHKNZBg0wSmNZC1oUCjETcdodowYqVCL2EnarxHW1GqUyXxYPvcFWjyoMiRyuorU3gqRUoTZBNMZD";
        private static string urlAPIGraphV5 = "https://graph.facebook.com/v5.0/";
        private static string urlApiGraphV12 = "https://graph.facebook.com/v12.0/";

        public APIGraphFacebookInstagram()
        {

        }

        /// <summary>
        /// Obtiene la publicacion de usuario
        /// </summary>
        /// <param name="InstagramComentarioId">Id del comentario de Instagram</param>
        /// <returns>Objeto de clase ApiGraphInstagramUsuarioPublicacionDTO</returns>
        public ApiGraphInstagramUsuarioPublicacionDTO ObtenerUsuarioPublicacion(string InstagramComentarioId)
        {

            string rpta;
            ApiGraphInstagramUsuarioPublicacionDTO apiGraphInstagramUsuarioPublicacionDTO = new ApiGraphInstagramUsuarioPublicacionDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var url = urlApiGraphV12 + InstagramComentarioId + "?fields=username,timestamp,media&access_token=" + accessTokenBSGrupocom;
                    rpta = wc.DownloadString(new Uri(url));
                    apiGraphInstagramUsuarioPublicacionDTO = JsonConvert.DeserializeObject<ApiGraphInstagramUsuarioPublicacionDTO>(rpta);

                    url = urlApiGraphV12 + apiGraphInstagramUsuarioPublicacionDTO.media.id + "?fields=media_type,media_url,caption,id&access_token=" + accessTokenBSGrupocom;
                    rpta = wc.DownloadString(new Uri(url));
                    apiGraphInstagramUsuarioPublicacionDTO.media = JsonConvert.DeserializeObject<ApiGraphInstagramPublicacionDTO>(rpta);

                    return apiGraphInstagramUsuarioPublicacionDTO;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Responde a un comentario de Inbox
        /// </summary>
        /// <param name="respuestaComentarioInstagramDTO">Objeto de clase RespuestaComentarioInstagramDTO</param>
        /// <returns>Objeto de clase ApiGraphInstagramResponseComentarioDTO</returns>
        public ApiGraphInstagramResponseComentarioDTO ResponderComentarioInbox(RespuestaComentarioInstagramDTO respuestaComentarioInstagramDTO)
        {
            string rpta;
            ApiGraphInstagramResponseComentarioDTO response = new ApiGraphInstagramResponseComentarioDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var objeto = new
                    {
                    };
                    var objetoSerializado = JsonConvert.SerializeObject(objeto);

                    var url = urlApiGraphV12 + "/" + respuestaComentarioInstagramDTO.InstagramIdComentario + "/replies?message=" + respuestaComentarioInstagramDTO.Mensaje + "&access_token=" + accessTokenBSGrupocom;
                    rpta = wc.UploadString(new Uri(url), objetoSerializado);
                    response = JsonConvert.DeserializeObject<ApiGraphInstagramResponseComentarioDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}
