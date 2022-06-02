using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Servicios.SCode.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BSI.Integra.Aplicacion.Servicios.SCode.BO
{
    /// BO: APIGraphFacebook
    /// Autor: Joao Benavente - Richard Zenteno - Gian Miranda
    /// Fecha: 23/09/2021
    /// <summary>
    /// BO para la logica de peticiones hacia Facebook
    /// </summary>
    public class APIGraphFacebook
    {
        //Token de acceso a la Pagina de la APP bsgrupo.com 
        private static string accessTokenBSGrupocom = "EAAIbfrfWKZCABACsSffzxD2tPmU4sLZANWKlEiwYNGL8KiYiSqi3q37TpuTxBB08oQPHZCZAx9CBc0v843RYi9zzV81MCxHSJiN1iZCOFRngHO7vgU08NfAcizfWHrqm6XfGzhShxCiPA2k3BmDgedGeZC80aHzO1dkQeO2KHxDC2ZCUgyHapG8";

        // token anterior  a la Pagina de la APP bsgrupo.com  [COMENTADO]
        //private static string accessTokenBSGrupocom = "EAAIbfrfWKZCABAMIZB20v7pAPpNyLkS96zHYa01GLyy3Y0nvJhzhi3pPPUsVrJYADFrrgqjfQs4wFmJhcpVSPEWjr9su8TFBwt4F1Apfbrdk8UEKvRkeFgHKNZBg0wSmNZC1oUCjETcdodowYqVCL2EnarxHW1GqUyXxYPvcFWjyoMiRyuorU3gqRUoTZBNMZD";
        private static string accessTokenMessengerPrueba = "EAAJZAJpNCGn0BADm5flTDpewD8i7yvi86VgOYZBpZAg2EDlZCycSqZCZBK3rvH8xkQ7b5a7MLzD48yif5eDwsg3ukJnDY3WO9rP3PbAW0yAl4EHui5xajSIgn1bLAqbuTJMJZC21MbhN762edklpvM8kdYXRLoZCWvkKfSDKZBTQDZBNBUhnssMGeaTXv6pDxq4Ks4AMRiKy6RGTJT2b7qXZBOo";
        private static string urlAPIGraphV4 = "https://graph.facebook.com/v4.0/";
        private static string urlAPIGraphV5 = "https://graph.facebook.com/v5.0/";
        private static string urlAPIGraphV6 = "https://graph.facebook.com/v6.0/";
        private static string urlApiGraphV12 = "https://graph.facebook.com/v12.0/";


        public APIGraphFacebook()
        {

        }

        /// <summary>
        /// Obtiene informacion basica en base al id de Facebook guardado en la DB de dicha plataforma
        /// </summary>
        /// <param name="IdPostFacebook">Cadena con el Id segun formato de Facebook del post</param>
        /// <returns>Objeto de clase ApiGraphFacebookPostDTO</returns>
        public ApiGraphFacebookPostDTO ObtenerInformacionPublicacion(string IdPostFacebook)
        {
            string rpta;
            ApiGraphFacebookPostDTO response = new ApiGraphFacebookPostDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var url = urlApiGraphV12 + IdPostFacebook + "?fields=permalink_url,full_picture,message&access_token=" + accessTokenBSGrupocom;
                    rpta = wc.DownloadString(new Uri(url));
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookPostDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Obtiene informacion basica en base al id de Facebook guardado en la DB de dicha plataforma
        /// </summary>
        /// <param name="IdComentarioFacebook">Cadena con el Id segun formato de Facebook del comentario</param>
        /// <returns>Objeto de clase ApiGraphFacebookComentarioInformacionDTO</returns>
        public ApiGraphFacebookComentarioInformacionDTO ObtenerInformacionComentario(string IdComentarioFacebook)
        {
            string rpta;
            ApiGraphFacebookComentarioInformacionDTO response = new ApiGraphFacebookComentarioInformacionDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var url = urlApiGraphV12 + IdComentarioFacebook + "?fields=created_time&access_token=" + accessTokenBSGrupocom;
                    rpta = wc.DownloadString(new Uri(url));
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookComentarioInformacionDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Obtiene informacion basica en base al id de Facebook guardado en la DB de dicha plataforma
        /// </summary>
        /// <param name="IdComentarioFacebook">Cadena con el Id segun formato de Facebook del comentario</param>
        /// <returns>Objeto de clase ApiGraphFacebookEliminarComentarioResponseDTO</returns>
        public ApiGraphFacebookEliminarComentarioResponseDTO EliminarComentario(string IdComentarioFacebook)
        {
            string rpta;
            ApiGraphFacebookEliminarComentarioResponseDTO response = new ApiGraphFacebookEliminarComentarioResponseDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var url = urlApiGraphV12 + IdComentarioFacebook + "?access_token=" + accessTokenBSGrupocom;
                    rpta = wc.UploadString(new Uri(url), "DELETE", "");
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookEliminarComentarioResponseDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Responde el comentario por inbox de la plataforma de Facebook
        /// </summary>
        /// <param name="respuestaComentarioInboxDTO">Objeto de clase RespuestaComentarioInboxDTO</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseMensajeInboxDTO</returns>
        public ApiGraphFacebookResponseMensajeInboxDTO ResponderComentarioInbox(RespuestaComentarioInboxDTO respuestaComentarioInboxDTO)
        {
            string rpta;
            ApiGraphFacebookResponseMensajeInboxDTO response = new ApiGraphFacebookResponseMensajeInboxDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var objeto = new
                    {
                        recipient = new { comment_id = respuestaComentarioInboxDTO.IdCommentFacebook },
                        message = new { text = respuestaComentarioInboxDTO.Mensaje }
                    };
                    var objetoSerializado = JsonConvert.SerializeObject(objeto);

                    var url = urlApiGraphV12 + "me/messages?access_token=" + accessTokenMessengerPrueba;
                    rpta = wc.UploadString(new Uri(url), objetoSerializado);
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseMensajeInboxDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Responde mediante inbox al PSID (Page-scoped Id) dado por Facebook
        /// </summary>
        /// <param name="PSID">PSID (Page-scoped Id) dado por Facebook</param>
        /// <param name="texto">Texto a enviar en el mensaje</param>
        /// <param name="accessToken">Token de acceso valido para el uso de la API de Facebook</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseMensajeInboxDTO</returns>
        public ApiGraphFacebookResponseMensajeInboxDTO ResponderInbox(string PSID, string texto, string accessToken)
        {
            string rpta;
            ApiGraphFacebookResponseMensajeInboxDTO response = new ApiGraphFacebookResponseMensajeInboxDTO();

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var objeto = new
                    {
                        recipient = new { id = PSID },
                        message = new { text = texto }
                    };
                    var objetoSerializado = JsonConvert.SerializeObject(objeto);

                    var url = urlApiGraphV12 + "me/messages?access_token=" + accessToken;
                    rpta = wc.UploadString(new Uri(url), objetoSerializado);
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseMensajeInboxDTO>(rpta);
                    return response;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Responde mediante inbox solo el arreglo de respuestas rapidas al PSID (Page-scoped Id) dado por Facebook
        /// </summary>
        /// <param name="PSID">PSID (Page-scoped Id) dado por Facebook</param>
        /// <param name="texto">Texto a enviar en el mensaje</param>
        /// <param name="accessToken">Token de acceso valido para el uso de la API de Facebook</param>
        /// <param name="listaAreas">Array de objetos genericos con la lista de areas</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseMensajeInboxDTO</returns>
        public ApiGraphFacebookResponseMensajeInboxDTO ResponderInboxQuickReplies(string PSID, string texto, string accessToken, object[] listaAreas)
        {
            string rpta;
            ApiGraphFacebookResponseMensajeInboxDTO response = new ApiGraphFacebookResponseMensajeInboxDTO();

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var objeto = new
                    {
                        recipient = new { id = PSID },
                        messaging_type = "RESPONSE",
                        message = new
                        {
                            text = texto,
                            quick_replies = listaAreas
                        }
                    };
                    var objetoSerializado = JsonConvert.SerializeObject(objeto);

                    var url = urlApiGraphV12 + "me/messages?access_token=" + accessToken;
                    rpta = wc.UploadString(new Uri(url), objetoSerializado);
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseMensajeInboxDTO>(rpta);
                    return response;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Obtiene la informacion basica del usuario (nombres y apellidos)
        /// </summary>
        /// <param name="recipient_id">Id del recipiente del cual se obtendra la informacion basica (nombres y apellidos)</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseUsuarioNombreDTO</returns>
        public ApiGraphFacebookResponseUsuarioNombreDTO ObtenerInformacionUsuario(string recipient_id)
        {
            string rpta;
            ApiGraphFacebookResponseUsuarioNombreDTO response = new ApiGraphFacebookResponseUsuarioNombreDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var url = urlApiGraphV12 + recipient_id + "?fields=first_name,last_name&access_token=" + accessTokenBSGrupocom;
                    rpta = wc.DownloadString(new Uri(url));
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseUsuarioNombreDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Obtiene la informacion basica del usuario (nombres y apellidos) pasando token personalizado
        /// </summary>
        /// <param name="recipient_id">Id del recipiente del cual se obtendra la informacion basica (nombres y apellidos)</param>
        /// <param name="token">Token de Facebook para su consulta a la API</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseUsuarioNombreDTO</returns>
        public ApiGraphFacebookResponseUsuarioNombreDTO ObtenerInformacionUsuarioConToken(string recipient_id, string token)
        {
            string rpta;

            ApiGraphFacebookResponseUsuarioNombreDTO response = new ApiGraphFacebookResponseUsuarioNombreDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var url = urlApiGraphV12 + recipient_id + "?fields=first_name,last_name&access_token=" + token;
                    rpta = wc.DownloadString(new Uri(url));
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseUsuarioNombreDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Envia archivo adjunto a Messenger (URL de dicho archivo)
        /// </summary>
        /// <param name="PSID">PSID (Page-scoped Id) dado por Facebook</param>
        /// <param name="urlArchivoAdjunto">Url del archivo adjunto</param>
        /// <param name="tipo">Tipo de archivo a mandar</param>
        /// <param name="accessToken">Token de accceso a la API de Facebook</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseMensajeArchivoAdjuntoDTO</returns>
        public ApiGraphFacebookResponseMensajeArchivoAdjuntoDTO EnviarArchivoAdjuntoMessenger(string PSID, string urlArchivoAdjunto, string tipo, string accessToken)
        {
            string rpta;
            ApiGraphFacebookResponseMensajeArchivoAdjuntoDTO response = new ApiGraphFacebookResponseMensajeArchivoAdjuntoDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var objeto = new
                    {
                        recipient = new { id = PSID },
                        message = new
                        {
                            attachment = new
                            {
                                type = tipo,
                                payload = new
                                {
                                    url = urlArchivoAdjunto,
                                    //url = "https://repositorioaudiollamada.blob.core.windows.net/grabacionsoftphone/2019/12/13/1319/20191213_093522_1319_00573142687064_58e432278764272000148k63970rmwp.wav",
                                    is_reusable = true
                                }
                            }
                        }
                    };
                    var objetoSerializado = JsonConvert.SerializeObject(objeto);

                    var url = urlApiGraphV12 + "me/messages?access_token=" + accessToken;
                    rpta = wc.UploadString(new Uri(url), objetoSerializado);
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseMensajeArchivoAdjuntoDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        #region Campañas
        /// <summary>
        /// Crea una campania de Facebook
        /// </summary>
        /// <param name="special_ad_category">Categoria de publicidad especial</param>
        /// <param name="Nombre">Nombre de la campania Facebook a crear</param>
        /// <param name="objective">Objetivo de la campania Facebook</param>
        /// <param name="status">Estado inicial de la campania</param>
        /// <param name="cuentaPublicitaria">Cuenta publicitaria en la cual se creara la campania Facebook</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseCrearDTO</returns>
        public ApiGraphFacebookResponseCrearDTO CrearCampaña(string special_ad_category, string Nombre, string objective, string status, string cuentaPublicitaria)
        {
            string rpta;
            ApiGraphFacebookResponseCrearDTO response = new ApiGraphFacebookResponseCrearDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var objeto = new
                    {
                        special_ad_category = special_ad_category,
                        name = Nombre,
                        objective = objective,
                        status = status,
                        access_token = accessTokenBSGrupocom
                    };
                    var objetoSerializado = JsonConvert.SerializeObject(objeto);

                    var url = urlApiGraphV12 + cuentaPublicitaria + "/campaigns";
                    rpta = wc.UploadString(new Uri(url), objetoSerializado);
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseCrearDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Crea una audiencia de Facebook sin una campania enlazada
        /// </summary>
        /// <param name="Nombre">Nombre de la audiencia de Facebook a crear</param>
        /// <param name="subtype">Subtipo de audiencia de Facebook</param>
        /// <param name="description">Descripcion de la audiencia de Facebook</param>
        /// <param name="customer_file_source">Archivo fuente para la creacion de la audiencia</param>
        /// <param name="cuentaPublicitaria">Cuenta publicitaria en la cual se creara la audiencia Facebook</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseCrearDTO</returns>
        public ApiGraphFacebookResponseCrearDTO CrearAudiencia(string Nombre, string subtype, string description, string customer_file_source, string cuentaPublicitaria)
        {
            string rpta;
            ApiGraphFacebookResponseCrearDTO response = new ApiGraphFacebookResponseCrearDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var objeto = new
                    {
                        name = Nombre,
                        subtype = subtype,
                        description = description,
                        customer_file_source = customer_file_source
                    };
                    var objetoSerializado = JsonConvert.SerializeObject(objeto);

                    var url = urlApiGraphV12 + cuentaPublicitaria + "/customaudiences?access_token=" + accessTokenBSGrupocom;
                    rpta = wc.UploadString(new Uri(url), objetoSerializado);
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseCrearDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Sube usuarios para la audiencia de Facebook
        /// </summary>
        /// <param name="FacebookPaginaId">Id de la pagina de Facebook</param>
        /// <param name="FacebookUsuarioIds">Id de los usuarios de Facebook</param>
        /// <param name="FacebookAudienciaId">Id de la audiencia de Facebook a la cual se le subira los contactos</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseSubirListasDTO</returns>
        public ApiGraphFacebookResponseSubirListasDTO SubirUsuariosParaAudienciaPorUsuariosFacebook(string FacebookPaginaId, string[][] FacebookUsuarioIds, string FacebookAudienciaId)
        {
            string rpta;
            ApiGraphFacebookResponseSubirListasDTO response = new ApiGraphFacebookResponseSubirListasDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var objeto = new
                    {
                        payload = new
                        {
                            schema = new string[] { "PAGEUID" },
                            page_ids = new string[] { FacebookPaginaId },
                            data = FacebookUsuarioIds
                        }
                    };
                    var objetoSerializado = JsonConvert.SerializeObject(objeto);

                    var url = urlApiGraphV12 + FacebookAudienciaId + "/users?access_token=" + accessTokenBSGrupocom;
                    rpta = wc.UploadString(new Uri(url), objetoSerializado);
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseSubirListasDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Crea un conjunto de anuncio de Messenger
        /// </summary>
        /// <param name="Nombre">Nombre del conjunto de anuncio</param>
        /// <param name="daily_budget">Presupuesto diario del conjunto de anuncio</param>
        /// <param name="campaign_id">Id de la campania segun DB de Facebook</param>
        /// <param name="custom_audiences">Audiencia personzaliadas</param>
        /// <param name="start_time">Fecha de inicio del conjunto de anuncio</param>
        /// <param name="end_time">Fecha de fin del conjunto de anuncio</param>
        /// <param name="cuentaPublicitaria">Cuenta publicitaria a la cual se vincula el conjunto anuncio</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseCrearDTO</returns>
        public ApiGraphFacebookResponseCrearDTO CrearConjuntoAnuncioMessenger(string Nombre, string daily_budget, string campaign_id, string custom_audiences, DateTime start_time, DateTime? end_time, string page_id, string cuentaPublicitaria)
        {
            string startTime = start_time.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszz'00'");
            string rpta;
            ApiGraphFacebookResponseCrearDTO response = new ApiGraphFacebookResponseCrearDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    object objeto;
                    if (end_time != null)
                    {
                        DateTime auxiliar = end_time ?? DateTime.Now;
                        var endTime = auxiliar.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszz'00'");
                        objeto = new
                        {
                            name = Nombre,
                            optimization_goal = "IMPRESSIONS",
                            billing_event = "IMPRESSIONS",
                            bid_amount = "500",
                            daily_budget = daily_budget,
                            campaign_id = campaign_id,
                            targeting = new
                            {
                                publisher_platforms = new string[] { "messenger" },
                                messenger_positions = new string[] { "sponsored_messages" },
                                custom_audiences = new object[] { new { id = custom_audiences } },
                                device_platforms = new string[] { "mobile", "desktop" }
                            },
                            start_time = startTime,
                            end_time = endTime,
                            status = "PAUSED",
                            promoted_object = new
                            {
                                page_id = page_id
                            },
                            access_token = accessTokenBSGrupocom
                        };
                    }
                    else
                    {
                        objeto = new
                        {
                            name = Nombre,
                            optimization_goal = "IMPRESSIONS",
                            billing_event = "IMPRESSIONS",
                            bid_amount = "500",
                            daily_budget = daily_budget,
                            campaign_id = campaign_id,
                            targeting = new
                            {
                                publisher_platforms = new string[] { "messenger" },
                                messenger_positions = new string[] { "sponsored_messages" },
                                custom_audiences = new object[] { new { id = custom_audiences } },
                                device_platforms = new string[] { "mobile", "desktop" }
                            },
                            start_time = startTime,
                            //end_time = end_time,
                            status = "PAUSED",
                            promoted_object = new
                            {
                                page_id = page_id
                            },
                            access_token = accessTokenBSGrupocom
                        };
                    }
                    var objetoSerializado = JsonConvert.SerializeObject(objeto);

                    var url = urlApiGraphV12 + cuentaPublicitaria + "/adsets";
                    rpta = wc.UploadString(new Uri(url), objetoSerializado);
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseCrearDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Actualizar un conjunto de anuncio de Messenger
        /// </summary>
        /// <param name="Nombre">Nombre del conjunto de anuncio</param>
        /// <param name="daily_budget">Presupuesto diario del conjunto de anuncio</param>
        /// <param name="start_time">Fecha de inicio del conjunto de anuncio</param>
        /// <param name="end_time">Fecha de fin del conjunto de anuncio</param>
        /// <param name="FacebookIdConjuntoAnuncio">Id del conjunto anuncio segun DB de Facebook</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseActualizarDTO</returns>
        public ApiGraphFacebookResponseActualizarDTO ActualizarConjuntoAnuncioMessenger(string Nombre, string daily_budget, DateTime start_time, DateTime? end_time, string FacebookIdConjuntoAnuncio)
        {
            string startTime = start_time.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszz'00'");
            string rpta;
            ApiGraphFacebookResponseActualizarDTO response = new ApiGraphFacebookResponseActualizarDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    object objeto;

                    if (end_time != null)
                    {
                        DateTime auxiliar = end_time ?? DateTime.Now;
                        var endtime = auxiliar.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszz'00'");
                        objeto = new
                        {
                            name = Nombre,
                            daily_budget = daily_budget,
                            start_time = startTime,
                            end_time = endtime,
                            access_token = accessTokenBSGrupocom
                        };
                    }
                    else
                    {
                        objeto = new
                        {
                            name = Nombre,
                            daily_budget = daily_budget,
                            start_time = startTime,
                            access_token = accessTokenBSGrupocom
                        };
                    }
                    var objetoSerializado = JsonConvert.SerializeObject(objeto);

                    var url = urlApiGraphV12 + FacebookIdConjuntoAnuncio;
                    rpta = wc.UploadString(new Uri(url), objetoSerializado);
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseActualizarDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Crea un anuncio creativo en la plataforma de Facebook
        /// </summary>
        /// <param name="page_id">Id de la pagina de Facebook</param>
        /// <param name="object_type">Tipo de objeto</param>
        /// <param name="mensaje">Mensaje del anuncio creativo</param>
        /// <param name="cuentaPublicitaria">Cuenta publicitaria a la cual se vincula el conjunto anuncio</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseCrearDTO</returns>
        public ApiGraphFacebookResponseCrearDTO CrearAnuncioCreativo(string page_id, string object_type, string mensaje, string cuentaPublicitaria)
        {
            string rpta;
            ApiGraphFacebookResponseCrearDTO response = new ApiGraphFacebookResponseCrearDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var objeto = new
                    {
                        object_id = page_id,
                        object_type = object_type,
                        messenger_sponsored_message = new
                        {
                            message = new
                            {
                                text = mensaje
                            }
                        },
                        access_token = accessTokenBSGrupocom
                    };
                    var objetoSerializado = JsonConvert.SerializeObject(objeto);

                    var url = urlApiGraphV12 + cuentaPublicitaria + "/adcreatives";
                    rpta = wc.UploadString(new Uri(url), objetoSerializado);
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseCrearDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Crea un anuncio en la plataforma de Facebook
        /// </summary>
        /// <param name="name">Nombre del anuncio de Facebook</param>
        /// <param name="adset_id">Id del conjunto de anuncio de Facebook</param>
        /// <param name="creative_id">Id de archivo adjunto para el anuncio</param>
        /// <param name="status">Estado del anuncio</param>
        /// <param name="cuentaPublicitaria">Cuenta publicitaria a la cual se vincula el conjunto anuncio</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseCrearDTO</returns>
        public ApiGraphFacebookResponseCrearDTO CrearAnuncio(string name, string adset_id, string creative_id, string status, string cuentaPublicitaria)
        {
            string rpta;
            ApiGraphFacebookResponseCrearDTO response = new ApiGraphFacebookResponseCrearDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var objeto = new
                    {
                        name = name,
                        adset_id = adset_id,
                        creative = new
                        {
                            creative_id = creative_id
                        },
                        status = status,
                        access_token = accessTokenBSGrupocom
                    };
                    var objetoSerializado = JsonConvert.SerializeObject(objeto);

                    var url = urlApiGraphV12 + cuentaPublicitaria + "/ads";
                    rpta = wc.UploadString(new Uri(url), objetoSerializado);
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseCrearDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Actualiza un anuncio en la plataforma de Facebook
        /// </summary>
        /// <param name="name">Nombre del anuncio de Facebook</param>
        /// <param name="facebookIdAnuncio">Id del anuncio de Facebook</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseActualizarDTO</returns>
        public ApiGraphFacebookResponseActualizarDTO ActualizarAnuncio(string name, string facebookIdAnuncio)
        {
            string rpta;
            ApiGraphFacebookResponseActualizarDTO response = new ApiGraphFacebookResponseActualizarDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    var objeto = new
                    {
                        name = name,
                        access_token = accessTokenBSGrupocom
                    };
                    var objetoSerializado = JsonConvert.SerializeObject(objeto);

                    var url = urlApiGraphV12 + facebookIdAnuncio;
                    rpta = wc.UploadString(new Uri(url), objetoSerializado);
                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseActualizarDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        #endregion

        #region ReportesInsights
        /// <summary>
        /// Obtener datos insights del conjunto de anuncio de Messenger Masivo
        /// </summary>
        /// <param name="fecha">Fecha de obtencion de los datos insights</param>
        /// <param name="FacebookIdConjuntoAnuncio">Id del conjunto anuncio de Facebook</param>
        /// <returns>Objeto de clase ApiGraphFacebookResponseActualizarDTO</returns>
        public ApiGraphFacebookResponseInsightConjutnoAnuncioDTO InsightConjuntoAnuncioMessengerMasivo(DateTime fecha, string FacebookIdConjuntoAnuncio)
        {
            string rpta;
            ApiGraphFacebookResponseInsightConjutnoAnuncioDTO response = new ApiGraphFacebookResponseInsightConjutnoAnuncioDTO();
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    string dia = fecha.Day < 10 ? "0" + fecha.Day : fecha.Day + "";
                    string mes = fecha.Month < 10 ? "0" + fecha.Month : fecha.Month + "";
                    string fechaString = fecha.Year + "-" + mes + "-" + dia;
                    var url = urlApiGraphV12 + FacebookIdConjuntoAnuncio + "/insights?" + "fields=impressions,reach&time_range[since]=" + fechaString + "&time_range[until]=" + fechaString + "&access_token=" + accessTokenBSGrupocom;
                    rpta = wc.DownloadString(new Uri(url));

                    response = JsonConvert.DeserializeObject<ApiGraphFacebookResponseInsightConjutnoAnuncioDTO>(rpta);
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        #endregion
    }
}
