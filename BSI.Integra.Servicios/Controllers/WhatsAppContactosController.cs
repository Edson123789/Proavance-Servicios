using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Servicios.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Newtonsoft.Json.NetCore;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/WhatsAppContactos")]
    public class WhatsAppContactosController : Controller
    {
        public string UrlHostWhatsApp = "https://167.71.101.242:9090";
        public string IpHostWhatsApp = "167.71.101.242:9090";
        public string UserAuthToken = "eyJhbGciOiAiSFMyNTYiLCAidHlwIjogIkpXVCJ9.eyJ1c2VyIjoianJpdmVyYSIsImlhdCI6MTU2NTk4MzM2OCwiZXhwIjoxNTY2NTg4MTY4LCJ3YTpyYW5kIjoyNDIwNjg1ODM0MjM0NDQ4Njg2fQ.OD5A53Pd8FPLr6dd4Es6vxPS3hlSle1VLYZOY7TPd0k";
        private readonly integraDBContext _integraDBContext;

        public WhatsAppContactosController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]/{IdPersonal}/{IdPais}")]
        [HttpPost]
        public ActionResult WhatsAppValidarNumeros(int IdPersonal, int IdPais, [FromBody] ValidarNumerosWhatsAppDTO DTO)
        {

            if (DTO != null)
            {
                string urlToPost;
                bool banderaLogin = false;
                string _tokenComunicacion = string.Empty;

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
                    WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

                    var _credencialesHost = _repCredenciales.ObtenerCredencialHost(IdPais);
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(IdPersonal, IdPais);

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _repTokenUsuario.CredencialUsuarioLogin(IdPersonal);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                            foreach (var item in datos.users)
                            {
                                TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                                modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                                modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                                modelCredencial.UserAuthToken = item.token;
                                modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                modelCredencial.EsMigracion = true;
                                modelCredencial.Estado = true;
                                modelCredencial.FechaCreacion = DateTime.Now;
                                modelCredencial.FechaModificacion = DateTime.Now;
                                modelCredencial.UsuarioCreacion = "whatsapp";
                                modelCredencial.UsuarioModificacion = "whatsapp";

                                var rpta = _repTokenUsuario.Insert(modelCredencial);

                                _tokenComunicacion = item.token;
                            }

                            banderaLogin = true;

                        }
                        else
                        {
                            banderaLogin = false;
                        }

                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                    if (banderaLogin)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Encoding = Encoding.UTF8;

                            var serializer = new JavaScriptSerializer();

                            var serializedResult = serializer.Serialize(DTO);
                            string myParameters = serializedResult;
                            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                            client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                            client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                            client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado = client.UploadString(urlToPost, myParameters);


                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);

                        return Ok(datoRespuesta.contacts);
                    }
                    else
                    {
                        return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                    }
                    
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
            
        }

        [Route("[action]/{IdPersonal}/{IdPais}")]
        [HttpPost]
        public ActionResult WhatsAppValidarNumerosAsync(int idPersonal, [FromBody] ValidarNumerosWhatsAppAsyncDTO DTO)
        {

            if (DTO != null)
            {
                string urlToPost = UrlHostWhatsApp + "/v1/contacts";

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    using (WebClient client = new WebClient())
                    {
                        client.Encoding = Encoding.UTF8;

                        var serializer = new JavaScriptSerializer();

                        var serializedResult = serializer.Serialize(DTO);
                        string myParameters = serializedResult;
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + UserAuthToken;
                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                        client.Headers[HttpRequestHeader.Host] = IpHostWhatsApp;
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        resultado = client.UploadString(urlToPost, myParameters);

                    }

                    var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);

                    return Ok(datoRespuesta);



                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }

        }
    }
}