using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
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
    /// Controlador: WhatsAppUsuario
    /// Autor: Jose Villena
    /// Fecha: 03/05/2020
    /// <summary>
    /// Gestiona todas la propiedades de la tabla t_WhatsAppUsuario
    /// </summary>
    [Route("api/WhatsAppUsuario")]
    public class WhatsAppUsuarioController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public WhatsAppUsuarioController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult WhatsAppIdentificacionUsuariosServidores([FromBody] WhatsAppUsuarioDTO DTO)
        {

            if (DTO != null)
            {
                bool bnadera = false;
                List<string> listaCredenciales = new List<string>();

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
                    WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

                    var _credencialesHost =_repCredenciales.ObtenerCredencialesHost();
                    

                    foreach (var credencial in _credencialesHost)
                    {
                        string urlToPost = credencial.UrlWhatsApp + "/v1/users/login";

                        var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(DTO.IdPersonal, credencial.IdPais);

                        if (tokenValida == null || Convert.ToDateTime(DTO.ExpiresAfter) >= tokenValida.ExpiresAfter)
                        {
                            var client = new RestClient(@urlToPost);
                            var request = new RestSharp.RestRequest(Method.POST);
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("Content-Length", "");
                            request.AddHeader("Accept-Encoding", "gzip, deflate");
                            request.AddHeader("Host", credencial.IpHost);
                            request.AddHeader("Cache-Control", "no-cache");
                            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(DTO.UserUsername + ":" + DTO.UserPassword)));
                            request.AddHeader("Content-Type", "application/json");
                            IRestResponse response = client.Execute(request);

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                                foreach (var item in datos.users)
                                {
                                    TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                                    modelCredencial.IdWhatsAppUsuario = tokenValida.IdWhatsAppUsuario;
                                    modelCredencial.IdWhatsAppConfiguracion = credencial.Id;
                                    modelCredencial.UserAuthToken = item.token;
                                    modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                    modelCredencial.EsMigracion = true;
                                    modelCredencial.Estado = true;
                                    modelCredencial.FechaCreacion = DateTime.Now;
                                    modelCredencial.FechaModificacion = DateTime.Now;
                                    modelCredencial.UsuarioCreacion = "whatsapp";
                                    modelCredencial.UsuarioModificacion = "whatsapp";

                                    var rpta = _repTokenUsuario.Insert(modelCredencial);
                                }

                                bnadera = true;
                                listaCredenciales.Add(response.Content);
                            }
                            else
                            {
                                bnadera = true;
                                listaCredenciales.Add(response.StatusCode.ToString());
                            }
                        }
                    }

                    return Ok(listaCredenciales);

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


        [Route("[action]")]
        [HttpPost]
        public ActionResult WhatsAppIdentificacionUsuarioServidor([FromBody] WhatsAppUsuarioDTO DTO)
        {

            if (DTO != null)
            {
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
                    WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

                    var _credencialesHost = _repCredenciales.ObtenerCredencialHost(DTO.IdPais);
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(DTO.IdPersonal, DTO.IdPais);

                    string urlToPost = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                    if (tokenValida == null || Convert.ToDateTime(DTO.ExpiresAfter) >= tokenValida.ExpiresAfter)
                    {

                        var validarUsuario = _repTokenUsuario.ValidarUsuario(DTO.IdPersonal);

                        var client = new RestClient(@urlToPost);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(DTO.UserUsername + ":" + DTO.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                            foreach (var item in datos.users)
                            {
                                TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                                modelCredencial.IdWhatsAppUsuario = validarUsuario.IdWhatsAppUsuario;
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
                            }

                            return Ok(datos);
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    else
                    {
                        return Ok(tokenValida);
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

        [Route("[action]")]
        [HttpPost]
        public ActionResult WhatsAppInsertarUsuario([FromBody] WhatsAppDatoUsuarioDTO DTO)
        {

            if (DTO != null)
            {
                bool banderaLogin = false;
                string _tokenComunicacion = string.Empty;
                string urlToPostUsuario = string.Empty;

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
                    WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

                    var _credencialesHost = _repCredenciales.ObtenerCredencialesHost();
                    

                    string urlToPost = string.Empty;
                    string resultado = string.Empty;

                    WhatsAppUsuarioRepositorio _funcionObjeto = new WhatsAppUsuarioRepositorio(_integraDBContext);
                    WhatsAppUsuarioBO modelUsuarioWhatsApp = new WhatsAppUsuarioBO();

                    modelUsuarioWhatsApp.IdPersonal = DTO.IdPersonal;
                    modelUsuarioWhatsApp.UserUsername = DTO.UserUsername;
                    modelUsuarioWhatsApp.UserPassword = DTO.UserPassword;
                    modelUsuarioWhatsApp.RolUser = DTO.RolUser;
                    modelUsuarioWhatsApp.EsMigracion = true;
                    modelUsuarioWhatsApp.Estado = true;
                    modelUsuarioWhatsApp.FechaCreacion = DateTime.Now;
                    modelUsuarioWhatsApp.FechaModificacion = DateTime.Now;
                    modelUsuarioWhatsApp.UsuarioCreacion = DTO.UsuarioSistema;
                    modelUsuarioWhatsApp.UsuarioModificacion = DTO.UsuarioSistema;
                    var rpta = _funcionObjeto.Insert(modelUsuarioWhatsApp);

                    if (_credencialesHost != null)
                    {
                        foreach (var itemCredenciales in _credencialesHost)
                        {
                            
                            var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(4589, itemCredenciales.IdPais);

                            //Primero vemos las credenciales del admin para poder registrar al usuario
                            if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                            {
                                urlToPostUsuario = itemCredenciales.UrlWhatsApp + "/v1/users/login";
                                var userLogin = _repTokenUsuario.CredencialUsuarioLogin(4589);

                                var client = new RestClient(urlToPostUsuario);
                                var request = new RestSharp.RestRequest(Method.POST);
                                request.AddHeader("cache-control", "no-cache");
                                request.AddHeader("Content-Length", "");
                                request.AddHeader("Accept-Encoding", "gzip, deflate");
                                request.AddHeader("Host", itemCredenciales.IpHost);
                                request.AddHeader("Cache-Control", "no-cache");
                                request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                                request.AddHeader("Content-Type", "application/json");
                                IRestResponse response = client.Execute(request);

                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                                    foreach (var item in datos.users)
                                    {
                                        TWhatsAppUsuarioCredencial datosCredencial = new TWhatsAppUsuarioCredencial();

                                        datosCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                                        datosCredencial.IdWhatsAppConfiguracion = itemCredenciales.Id;
                                        datosCredencial.UserAuthToken = item.token;
                                        datosCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                        datosCredencial.EsMigracion = true;
                                        datosCredencial.Estado = true;
                                        datosCredencial.FechaCreacion = DateTime.Now;
                                        datosCredencial.FechaModificacion = DateTime.Now;
                                        datosCredencial.UsuarioCreacion = "whatsapp";
                                        datosCredencial.UsuarioModificacion = "whatsapp";

                                        var rptaCredenciales = _repTokenUsuario.Insert(datosCredencial);

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

                            //Se procede a crear a los usuarios en los servidores de whatsapp
                            userRegister _usuarioRegistro = new userRegister();

                            _usuarioRegistro.username = DTO.UserUsername;
                            _usuarioRegistro.password = DTO.UserPassword;

                            urlToPost = itemCredenciales.UrlWhatsApp + "/v1/users";

                            try
                            {
                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;

                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_usuarioRegistro);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = myParameters.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = itemCredenciales.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }
                            }
                            catch (Exception eerr)
                            {
                            }

                            var datoRespuesta = JsonConvert.DeserializeObject<userLogeo>(resultado);

                            if (string.IsNullOrEmpty(resultado) || datoRespuesta.errors == null) //datoRespuesta.errors == null
                            {
                                urlToPostUsuario = itemCredenciales.UrlWhatsApp + "/v1/users/login";

                                var client = new RestClient(urlToPostUsuario);
                                var request = new RestSharp.RestRequest(Method.POST);
                                request.AddHeader("cache-control", "no-cache");
                                request.AddHeader("Content-Length", "");
                                request.AddHeader("Accept-Encoding", "gzip, deflate");
                                request.AddHeader("Host", itemCredenciales.IpHost);
                                request.AddHeader("Cache-Control", "no-cache");
                                request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(modelUsuarioWhatsApp.UserUsername + ":" + modelUsuarioWhatsApp.UserPassword)));
                                request.AddHeader("Content-Type", "application/json");
                                IRestResponse response = client.Execute(request);

                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                                    foreach (var item in datos.users)
                                    {
                                        TWhatsAppUsuarioCredencial datosCredencial = new TWhatsAppUsuarioCredencial();

                                        datosCredencial.IdWhatsAppUsuario = modelUsuarioWhatsApp.Id;
                                        datosCredencial.IdWhatsAppConfiguracion = itemCredenciales.Id;
                                        datosCredencial.UserAuthToken = item.token;
                                        datosCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                        datosCredencial.EsMigracion = true;
                                        datosCredencial.Estado = true;
                                        datosCredencial.FechaCreacion = DateTime.Now;
                                        datosCredencial.FechaModificacion = DateTime.Now;
                                        datosCredencial.UsuarioCreacion = "whatsapp";
                                        datosCredencial.UsuarioModificacion = "whatsapp";

                                        var rptaCredenciales = _repTokenUsuario.Insert(datosCredencial);
                                    }

                                    banderaLogin = true;

                                }
                                else
                                {
                                    banderaLogin = false;
                                }
                            }

                        }
                    }

                    var _resultadoInsert = _funcionObjeto.ObtnerCredencialUsuarioPorId(modelUsuarioWhatsApp.Id);

                    return Ok(_resultadoInsert);
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

        [Route("[action]")]
        [HttpPut]
        public ActionResult WhatsAppModificarUsuario([FromBody] WhatsAppDatoUsuarioDTO DTO)
        {

            if (DTO != null)
            {
                string urlToPostUsuario = string.Empty;
                bool banderaLogin = false;
                string _tokenComunicacion = string.Empty;

                try
                {
                    WhatsAppUsuarioRepositorio _funcionObjeto = new WhatsAppUsuarioRepositorio(_integraDBContext);
                    WhatsAppUsuarioBO modelCredencial = new WhatsAppUsuarioBO();

                    modelCredencial.Id = DTO.Id;
                    modelCredencial.IdPersonal = DTO.IdPersonal;
                    modelCredencial.UserUsername = DTO.UserPassword;
                    modelCredencial.UserPassword = DTO.UserPassword;
                    modelCredencial.RolUser = DTO.RolUser;
                    modelCredencial.EsMigracion = true;
                    modelCredencial.FechaModificacion = DateTime.Now;
                    modelCredencial.UsuarioModificacion = DTO.UsuarioSistema;

                    var rpta = _funcionObjeto.Update(modelCredencial);

                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
                    WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

                    string urlToPost = string.Empty;
                    string resultado = string.Empty;

                    var _credencialesHost = _repCredenciales.ObtenerCredencialesHost();

                    if (_credencialesHost != null)
                    {
                        foreach (var itemCredenciales in _credencialesHost)
                        {

                            var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(4589, itemCredenciales.IdPais);

                            //Primero vemos las credenciales del admin para poder registrar al usuario
                            if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                            {
                                urlToPostUsuario = itemCredenciales.UrlWhatsApp + "/v1/users/login";
                                var userLogin = _repTokenUsuario.CredencialUsuarioLogin(0);

                                var client = new RestClient(urlToPostUsuario);
                                var request = new RestSharp.RestRequest(Method.POST);
                                request.AddHeader("cache-control", "no-cache");
                                request.AddHeader("Content-Length", "");
                                request.AddHeader("Accept-Encoding", "gzip, deflate");
                                request.AddHeader("Host", itemCredenciales.IpHost);
                                request.AddHeader("Cache-Control", "no-cache");
                                request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                                request.AddHeader("Content-Type", "application/json");
                                IRestResponse response = client.Execute(request);

                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                                    foreach (var item in datos.users)
                                    {
                                        TWhatsAppUsuarioCredencial datosCredencial = new TWhatsAppUsuarioCredencial();

                                        datosCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                                        datosCredencial.IdWhatsAppConfiguracion = itemCredenciales.Id;
                                        datosCredencial.UserAuthToken = item.token;
                                        datosCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                        datosCredencial.EsMigracion = true;
                                        datosCredencial.Estado = true;
                                        datosCredencial.FechaCreacion = DateTime.Now;
                                        datosCredencial.FechaModificacion = DateTime.Now;
                                        datosCredencial.UsuarioCreacion = "whatsapp";
                                        datosCredencial.UsuarioModificacion = "whatsapp";

                                        var rptaCredenciales = _repTokenUsuario.Insert(datosCredencial);

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

                            //Se procede a crear a los usuarios en los servidores de whatsapp
                            userRegister _usuarioRegistro = new userRegister();

                            _usuarioRegistro.username = DTO.UserUsername;
                            _usuarioRegistro.password = DTO.UserPassword;

                            urlToPost = itemCredenciales.UrlWhatsApp + "/v1/users";

                            try
                            {
                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;

                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_usuarioRegistro);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = myParameters.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = itemCredenciales.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }
                            }
                            catch (Exception eerr)
                            {
                            }

                            var datoRespuesta = JsonConvert.DeserializeObject<userLogeo>(resultado);

                            if (string.IsNullOrEmpty(resultado) || datoRespuesta.errors == null) //datoRespuesta.errors == null
                            {
                                urlToPostUsuario = itemCredenciales.UrlWhatsApp + "/v1/users/login";

                                var client = new RestClient(urlToPostUsuario);
                                var request = new RestSharp.RestRequest(Method.POST);
                                request.AddHeader("cache-control", "no-cache");
                                request.AddHeader("Content-Length", "");
                                request.AddHeader("Accept-Encoding", "gzip, deflate");
                                request.AddHeader("Host", itemCredenciales.IpHost);
                                request.AddHeader("Cache-Control", "no-cache");
                                request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(modelCredencial.UserUsername + ":" + modelCredencial.UserPassword)));
                                request.AddHeader("Content-Type", "application/json");
                                IRestResponse response = client.Execute(request);

                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                                    foreach (var item in datos.users)
                                    {
                                        TWhatsAppUsuarioCredencial datosCredencial = new TWhatsAppUsuarioCredencial();

                                        datosCredencial.IdWhatsAppUsuario = modelCredencial.Id;
                                        datosCredencial.IdWhatsAppConfiguracion = itemCredenciales.Id;
                                        datosCredencial.UserAuthToken = item.token;
                                        datosCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                        datosCredencial.EsMigracion = true;
                                        datosCredencial.Estado = true;
                                        datosCredencial.FechaCreacion = DateTime.Now;
                                        datosCredencial.FechaModificacion = DateTime.Now;
                                        datosCredencial.UsuarioCreacion = "whatsapp";
                                        datosCredencial.UsuarioModificacion = "whatsapp";

                                        var rptaCredenciales = _repTokenUsuario.Insert(datosCredencial);
                                    }

                                    banderaLogin = true;

                                }
                                else
                                {
                                    banderaLogin = false;
                                }
                            }

                        }
                    }

                    return Ok(rpta);
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

        [Route("[action]/{NombrePersonal}/{IdPersonal}")]
        [HttpDelete]
        public ActionResult WhatsAppEliminarUsuario(string NombrePersonal, int IdPersonal)
        {

            if (IdPersonal != 0)
            {
                try
                {
                    WhatsAppUsuarioRepositorio _funcionObjeto = new WhatsAppUsuarioRepositorio(_integraDBContext);
                    
                    var rpta = _funcionObjeto.Delete(IdPersonal, NombrePersonal);
                    return Ok(rpta);
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

        [Route("[action]")]
        [HttpGet]
        public ActionResult WhatsAppObtnerUsuario()
        {

            try
            {
                WhatsAppUsuarioRepositorio _funcionObjeto = new WhatsAppUsuarioRepositorio(_integraDBContext);

                var rpta = _funcionObjeto.ObtnerCredencialesUsuario();
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult WhatsAppObtnerListaPersonal()
        {

            try
            {
                WhatsAppUsuarioRepositorio _funcionObjeto = new WhatsAppUsuarioRepositorio(_integraDBContext);

                var rpta = _funcionObjeto.ObtnerListaPersonal();
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        /// Tipo Función: GET
        /// Autor: Jose Villena
        /// Fecha: 03/05/2021
        /// Versión: 1.0
        /// <summary>
        ///Obtiene usuario whatsapp valido
        /// </summary>
        /// <param name="idPersonal">Id del personal</param>
        /// <returns>retorna un objeto tipo WhatsAppUsuariosDTO</returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult UsuarioWhatsAppValido(int idPersonal)
        {

            try
            {
                WhatsAppUsuarioRepositorio _funcionObjeto = new WhatsAppUsuarioRepositorio(_integraDBContext);

                var rpta = _funcionObjeto.UsuarioWhatsAppValido(idPersonal);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}