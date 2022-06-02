using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
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
    /// Controlador: WhatsAppMensajes
    /// Autor: ---, Jashin Salazar
    /// Fecha: 10/05/2021
    /// <summary>
    /// Contiene las acciones para la interaccion con los mensajes de WhatsApp.
    /// </summary>
    [Route("api/WhatsAppMensajes")]
    public class WhatsAppMensajesController : Controller
    {
        // GET: WhatsAppMensajes
        private readonly integraDBContext _integraDBContext;
        public WhatsAppMensajesController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// TipoFuncion: POST
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los mensajes de WhatssApp
        /// </summary>
        /// <returns> Objeto TWhatsAppMensajeEnviado  </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult WhatsAppMensaje([FromBody] WhatsAppMensajeEnviadoDTO DTO)
        {
            WhatsAppDesuscritoRepositorio _repwhatsAppDesuscrito = new WhatsAppDesuscritoRepositorio(_integraDBContext);
            if (DTO != null)
            {
                string Celular = "";
                if (DTO.IdPais == 51)
                {
                    Celular = DTO.WaTo.Substring(2, 9);
                }
                else if (DTO.IdPais == 57)
                {
                    Celular = "00" + DTO.WaTo;
                }
                else if (DTO.IdPais == 591)
                {
                    Celular = "00" + DTO.WaTo;
                }
                else
                {
                    Celular = "00" + DTO.WaTo;
                }
                if (!_repwhatsAppDesuscrito.Exist(w => w.NumeroTelefono == Celular))
                {
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

                        var _credencialesHost = _repCredenciales.ObtenerCredencialHost(DTO.IdPais);
                        var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(DTO.IdPersonal, DTO.IdPais);

                        string urlToPost = _credencialesHost.UrlWhatsApp;

                        string resultado = string.Empty, _waType = string.Empty;

                        TWhatsAppMensajeEnviado mensajeEnviado = new TWhatsAppMensajeEnviado();

                        if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                        {
                            string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                            var userLogin = _repTokenUsuario.CredencialUsuarioLogin(DTO.IdPersonal);

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

                        if (banderaLogin)
                        {
                            switch (DTO.WaType.ToLower())
                            {
                                case "text":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages";
                                    _waType = "text";

                                    MensajeTextoEnvio _mensajeTexto = new MensajeTextoEnvio();

                                    _mensajeTexto.to = DTO.WaTo;
                                    _mensajeTexto.type = DTO.WaType;
                                    _mensajeTexto.recipient_type = DTO.WaRecipientType;
                                    _mensajeTexto.text = new text();

                                    _mensajeTexto.text.body = DTO.WaBody;

                                    using (WebClient client = new WebClient())
                                    {
                                        //client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeTexto);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeTexto);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "hsm":

                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "template";

                                    MensajePlantillaWhatsAppEnvioTemplate _mensajePlantilla = new MensajePlantillaWhatsAppEnvioTemplate();

                                    _mensajePlantilla.to = DTO.WaTo;
                                    _mensajePlantilla.type = "template";
                                    _mensajePlantilla.template = new template();

                                    _mensajePlantilla.template.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                    _mensajePlantilla.template.name = DTO.WaBody;

                                    _mensajePlantilla.template.language = new language();
                                    _mensajePlantilla.template.language.policy = "deterministic";
                                    _mensajePlantilla.template.language.code = "es";

                                    _mensajePlantilla.template.components = new List<components>();
                                    components Componente = new components();
                                    Componente.type = "body";


                                    if (DTO.datosPlantillaWhatsApp != null)
                                    {
                                        Componente.parameters = new List<parameters>();
                                        foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                        {
                                            parameters Dato = new parameters();
                                            Dato.type = "text";
                                            Dato.text = listaDatos.texto;

                                            Componente.parameters.Add(Dato);
                                        }
                                    }

                                    _mensajePlantilla.template.components.Add(Componente);

                                    using (WebClient Client = new WebClient())
                                    {
                                        Client.Encoding = Encoding.UTF8;
                                        var MensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(_mensajePlantilla);
                                        string MyParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = Client.UploadString(urlToPost, MyParameters);
                                    }

                                    break;
                                case "image":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "image";

                                    MensajeImagenEnvio _mensajeImagen = new MensajeImagenEnvio();
                                    _mensajeImagen.to = DTO.WaTo;
                                    _mensajeImagen.type = DTO.WaType;
                                    _mensajeImagen.recipient_type = DTO.WaRecipientType;

                                    _mensajeImagen.image = new image();

                                    _mensajeImagen.image.caption = DTO.WaCaption;
                                    _mensajeImagen.image.link = DTO.WaLink;

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeImagen);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeImagen);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "document":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "document";

                                    MensajeDocumentoEnvio _mensajeDocumento = new MensajeDocumentoEnvio();
                                    _mensajeDocumento.to = DTO.WaTo;
                                    _mensajeDocumento.type = DTO.WaType;
                                    _mensajeDocumento.recipient_type = DTO.WaRecipientType;

                                    _mensajeDocumento.document = new document();

                                    _mensajeDocumento.document.caption = DTO.WaCaption;
                                    _mensajeDocumento.document.link = DTO.WaLink;
                                    _mensajeDocumento.document.filename = DTO.WaFileName;

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeDocumento);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeDocumento);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                            }

                            var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);

                            foreach (var itemGuardar in datoRespuesta.messages)
                            {
                                WhatsAppMensajeEnviadoRepositorio _mensajeEnviadoRepositorio = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);

                                mensajeEnviado.WaId = itemGuardar.id;
                                mensajeEnviado.WaTo = DTO.WaTo;
                                mensajeEnviado.WaType = _waType;
                                mensajeEnviado.WaRecipientType = DTO.WaRecipientType;
                                mensajeEnviado.WaBody = DTO.WaBody;
                                mensajeEnviado.WaCaption = DTO.WaCaption;
                                mensajeEnviado.WaLink = DTO.WaLink;
                                mensajeEnviado.WaFileName = DTO.WaFileName;
                                mensajeEnviado.IdPais = DTO.IdPais;
                                if (DTO.IdAlumno != 0)
                                {
                                    mensajeEnviado.IdAlumno = DTO.IdAlumno;
                                }
                                else
                                {
                                    mensajeEnviado.IdAlumno = null;
                                }

                                mensajeEnviado.IdPersonal = DTO.IdPersonal;
                                mensajeEnviado.Estado = true;
                                mensajeEnviado.FechaCreacion = DateTime.Now;
                                mensajeEnviado.FechaModificacion = DateTime.Now;
                                mensajeEnviado.UsuarioCreacion = DTO.usuario;
                                mensajeEnviado.UsuarioModificacion = DTO.usuario;

                                _mensajeEnviadoRepositorio.Insert(mensajeEnviado);
                            }



                            return Ok(mensajeEnviado.WaId);
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
                    return BadRequest("El numero esta desuscrito");
                }
                
            }
            else
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }

        }
        
        /// TipoFuncion: POST
        /// Autor: , Carlos Crispin.
        /// Fecha: 07/04/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los mensajes de WhatssApp
        /// </summary>
        /// <returns> Objeto TWhatsAppMensajeEnviado  </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult WhatsAppMensajeVersionTemplate([FromBody] WhatsAppMensajeEnviadoDTO DTO)
        {
            WhatsAppDesuscritoRepositorio _repwhatsAppDesuscrito = new WhatsAppDesuscritoRepositorio(_integraDBContext);
            if (DTO != null)
            {
                string Celular = "";
                if (DTO.IdPais == 51)
                {
                    Celular = DTO.WaTo.Substring(2, 9);
                }
                else if (DTO.IdPais == 57)
                {
                    Celular = "00" + DTO.WaTo;
                }
                else if (DTO.IdPais == 591)
                {
                    Celular = "00" + DTO.WaTo;
                }
                else
                {
                    Celular = "00" + DTO.WaTo;
                }
                if (!_repwhatsAppDesuscrito.Exist(w => w.NumeroTelefono == Celular))
                {
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

                        var _credencialesHost = _repCredenciales.ObtenerCredencialHost(DTO.IdPais);
                        var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(DTO.IdPersonal, DTO.IdPais);

                        string urlToPost = _credencialesHost.UrlWhatsApp;

                        string resultado = string.Empty, _waType = string.Empty;

                        TWhatsAppMensajeEnviado mensajeEnviado = new TWhatsAppMensajeEnviado();

                        if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                        {
                            string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                            var userLogin = _repTokenUsuario.CredencialUsuarioLogin(DTO.IdPersonal);

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

                        if (banderaLogin)
                        {
                            switch (DTO.WaType.ToLower())
                            {
                                case "text":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages";
                                    _waType = "text";

                                    MensajeTextoEnvio _mensajeTexto = new MensajeTextoEnvio();

                                    _mensajeTexto.to = DTO.WaTo;
                                    _mensajeTexto.type = DTO.WaType;
                                    _mensajeTexto.recipient_type = DTO.WaRecipientType;
                                    _mensajeTexto.text = new text();

                                    _mensajeTexto.text.body = DTO.WaBody;

                                    using (WebClient client = new WebClient())
                                    {
                                        //client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeTexto);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeTexto);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "hsm":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "template";

                                    MensajePlantillaWhatsAppEnvio _mensajePlantilla = new MensajePlantillaWhatsAppEnvio();

                                    _mensajePlantilla.to = DTO.WaTo;
                                    _mensajePlantilla.type = "template";
                                    _mensajePlantilla.template = new template();

                                    _mensajePlantilla.template.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                    _mensajePlantilla.template.name = DTO.WaBody;

                                    _mensajePlantilla.template.language = new language();
                                    _mensajePlantilla.template.language.policy = "deterministic";
                                    _mensajePlantilla.template.language.code = "es";

                                    _mensajePlantilla.template.components = new List<components>();
                                    components Componente = new components();
                                    Componente.type = "body";

                                    if (DTO.datosPlantillaWhatsApp != null)
                                    {
                                        Componente.parameters = new List<parameters>();
                                        foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                        {
                                            parameters Dato = new parameters();
                                            Dato.type = "text";
                                            Dato.text = listaDatos.texto;

                                            Componente.parameters.Add(Dato);
                                        }
                                    }
                                    _mensajePlantilla.template.components.Add(Componente);

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajePlantilla);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "image":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "image";

                                    MensajeImagenEnvio _mensajeImagen = new MensajeImagenEnvio();
                                    _mensajeImagen.to = DTO.WaTo;
                                    _mensajeImagen.type = DTO.WaType;
                                    _mensajeImagen.recipient_type = DTO.WaRecipientType;

                                    _mensajeImagen.image = new image();

                                    _mensajeImagen.image.caption = DTO.WaCaption;
                                    _mensajeImagen.image.link = DTO.WaLink;

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeImagen);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeImagen);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "document":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "document";

                                    MensajeDocumentoEnvio _mensajeDocumento = new MensajeDocumentoEnvio();
                                    _mensajeDocumento.to = DTO.WaTo;
                                    _mensajeDocumento.type = DTO.WaType;
                                    _mensajeDocumento.recipient_type = DTO.WaRecipientType;

                                    _mensajeDocumento.document = new document();

                                    _mensajeDocumento.document.caption = DTO.WaCaption;
                                    _mensajeDocumento.document.link = DTO.WaLink;
                                    _mensajeDocumento.document.filename = DTO.WaFileName;

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeDocumento);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeDocumento);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                            }

                            var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);

                            foreach (var itemGuardar in datoRespuesta.messages)
                            {
                                WhatsAppMensajeEnviadoRepositorio _mensajeEnviadoRepositorio = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);

                                mensajeEnviado.WaId = itemGuardar.id;
                                mensajeEnviado.WaTo = DTO.WaTo;
                                mensajeEnviado.WaType = _waType;
                                mensajeEnviado.WaRecipientType = DTO.WaRecipientType;
                                mensajeEnviado.WaBody = DTO.WaBody;
                                mensajeEnviado.WaCaption = DTO.WaCaption;
                                mensajeEnviado.WaLink = DTO.WaLink;
                                mensajeEnviado.WaFileName = DTO.WaFileName;
                                mensajeEnviado.IdPais = DTO.IdPais;
                                if (DTO.IdAlumno != 0)
                                {
                                    mensajeEnviado.IdAlumno = DTO.IdAlumno;
                                }
                                else
                                {
                                    mensajeEnviado.IdAlumno = null;
                                }

                                mensajeEnviado.IdPersonal = DTO.IdPersonal;
                                mensajeEnviado.Estado = true;
                                mensajeEnviado.FechaCreacion = DateTime.Now;
                                mensajeEnviado.FechaModificacion = DateTime.Now;
                                mensajeEnviado.UsuarioCreacion = DTO.usuario;
                                mensajeEnviado.UsuarioModificacion = DTO.usuario;

                                _mensajeEnviadoRepositorio.Insert(mensajeEnviado);
                            }



                            return Ok(mensajeEnviado.WaId);
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
                    return BadRequest("El numero esta desuscrito");
                }

            }
            else
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }

        }
        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene mensaje de WhatssApp de tipo multimedia
        /// </summary>
        /// <returns> String </returns>
        [Route("[action]/{WaId}")]
        [HttpGet]
        public ActionResult WhatsAppMensajeMultimedia(string WaId)
        {
            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;

            try
            {
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                WhatsAppMensajeRecibidoRepositorio _mensajeRecibidoRepositorio = new WhatsAppMensajeRecibidoRepositorio(_integraDBContext);
                var _rptMensajeRecibido = _mensajeRecibidoRepositorio.GetBy(x => x.WaId == WaId).FirstOrDefault();

                WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
                WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);
                

                var _credencialesHost = _repCredenciales.ObtenerCredencialHost(_rptMensajeRecibido.IdPais);
                var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(4589, _rptMensajeRecibido.IdPais);

                if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                {
                    string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                    var userLogin = _repTokenUsuario.CredencialUsuarioLogin(4589);

                    var client1 = new RestClient(urlToPostUsuario);
                    var request1 = new RestSharp.RestRequest(Method.POST);
                    request1.AddHeader("cache-control", "no-cache");
                    request1.AddHeader("Content-Length", "");
                    request1.AddHeader("Accept-Encoding", "gzip, deflate");
                    request1.AddHeader("Host", _credencialesHost.IpHost);
                    request1.AddHeader("Cache-Control", "no-cache");
                    request1.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                    request1.AddHeader("Content-Type", "application/json");
                    IRestResponse response1 = client1.Execute(request1);

                    if (response1.StatusCode == HttpStatusCode.OK)
                    {
                        var datos = JsonConvert.DeserializeObject<userLogeo>(response1.Content);

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

                if (banderaLogin)
                {
                    string urlToPost = _credencialesHost.UrlWhatsApp;

                    string resultado = string.Empty, _waType = string.Empty;

                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/media/" + _rptMensajeRecibido.WaIdTypeMensaje;

                    var client = new RestClient(urlToPost);
                    var request = new RestSharp.RestRequest(Method.GET);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Connection", "keep-alive");
                    request.AddHeader("Accept-Encoding", "gzip, deflate");
                    request.AddHeader("Host", _credencialesHost.IpHost);
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Accept", "*/*");
                    request.AddHeader("Authorization", "Bearer " + _tokenComunicacion);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    IRestResponse response = client.Execute(request);

                    WhatsAppMensajeRecibidoRepositorio _objetoRecibido = new WhatsAppMensajeRecibidoRepositorio(_integraDBContext);

                    string respuesta = string.Empty;

                    if (_rptMensajeRecibido.WaType.Contains("image"))
                    {
                        respuesta = _objetoRecibido.guardarArchivos(response.RawBytes, _rptMensajeRecibido.WaType, response.ContentType, _rptMensajeRecibido.WaIdTypeMensaje + ".jpeg", _rptMensajeRecibido.IdPais);
                    }
                    else if (_rptMensajeRecibido.WaType.Contains("voice"))
                    {
                        respuesta = _objetoRecibido.guardarArchivos(response.RawBytes, _rptMensajeRecibido.WaType, response.ContentType, _rptMensajeRecibido.WaIdTypeMensaje +"_"+ DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("HHmmss") + ".ogg", _rptMensajeRecibido.IdPais);
                    }
                    else if (_rptMensajeRecibido.WaType.Contains("video"))
                    {
                        respuesta = _objetoRecibido.guardarArchivos(response.RawBytes, _rptMensajeRecibido.WaType, response.ContentType, _rptMensajeRecibido.WaIdTypeMensaje + ".mp4", _rptMensajeRecibido.IdPais);
                    }
                    else if (_rptMensajeRecibido.WaType.Contains("audio"))
                    {
                        respuesta = _objetoRecibido.guardarArchivos(response.RawBytes, _rptMensajeRecibido.WaType, response.ContentType, _rptMensajeRecibido.WaIdTypeMensaje + ".mp4", _rptMensajeRecibido.IdPais);
                    }
                    else
                    {
                        respuesta = _objetoRecibido.guardarArchivos(response.RawBytes, _rptMensajeRecibido.WaType, response.ContentType, _rptMensajeRecibido.WaFileName, _rptMensajeRecibido.IdPais);
                    }

                    _rptMensajeRecibido.WaFile = respuesta;
                    _mensajeRecibidoRepositorio.Update(_rptMensajeRecibido);


                    return Ok(respuesta);
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

        /// TipoFuncion: POST
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Adjunta archivo para mensaje de WhatsApp
        /// </summary>
        /// <returns> String </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult adjuntarArchivoWhatsApp([FromForm] IFormFile Files)
        {
            string respuesta = string.Empty;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                WhatsAppMensajeRecibidoRepositorio _objetoRecibido = new WhatsAppMensajeRecibidoRepositorio(_integraDBContext);

                using (var ms = new MemoryStream())
                {
                    Files.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    respuesta = _objetoRecibido.guardarArchivos(fileBytes, Files.ContentType, Files.FileName);
                }

                if (string.IsNullOrEmpty(respuesta))
                {
                    return Ok(new { Resultado = "Error" });
                }
                else
                {
                    return Ok(new { Resultado = "Ok", UrlArchivo = respuesta, NombreArchivo = Files.FileName });
                }


            }
            catch (Exception Ex)
            {
                return Ok(new { Resultado = "Error" });
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el ultimo mensjae de chat de WhatsApp por Id de personal.
        /// </summary>
        /// <returns> objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult WhatsAppUltimoMensajeChat(int IdPersonal)
        {

            if (IdPersonal != null)
            {
                try
                {
                    WhatsAppMensajeEnviadoRepositorio _objetoMensaje = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);

                    var _restultado = _objetoMensaje.ListaUltimoMensajeChats(IdPersonal);

                    if (_restultado != null)
                    {
                        return Ok(_restultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
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

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/03/2021
        /// Versión: 1.1
        /// <summary>
        /// Obtiene últimos mensajes recibido de chat de cada alumno por IdPersonal
        /// </summary>
        /// <returns> objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult WhatsAppUltimoMensajeRecibidosChat(int IdPersonal)
        {

            if (IdPersonal != null)
            {
                try
                {
                    WhatsAppMensajeEnviadoRepositorio _objetoMensaje = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);
                    var _restultado = _objetoMensaje.ListaUltimoMensajeChatsRecibido(IdPersonal);
                    if (_restultado != null)
                    {
                        return Ok(_restultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
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

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/03/2021
        /// Versión: 1.1
        /// <summary>
        /// Obtiene historial de mensajes recibidos a chat de asesor
        /// </summary>
        /// <returns> Lista de objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [Route("[action]/{IdPersonal}/{Numero}/{Area}")]
        [HttpGet]
        public ActionResult HistorialMensajeRecibidosChat(int IdPersonal, string Numero, string Area)
        {

            if (IdPersonal != null)
            {
                try
                {
                    WhatsAppMensajeEnviadoRepositorio _objetoMensaje = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);
                    var _restultado = _objetoMensaje.HistorialChatsRecibido(IdPersonal, Numero, Area);
                    if (_restultado != null)
                    {
                        return Ok(_restultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
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

        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene historial de mensajes recibidos a chat de asesor
        /// </summary>
        /// <returns> Lista de objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [Route("[action]/{IdPersonal}/{Numero}/{Area}/{IdTipoAgenda}")]
        [HttpGet]
        public ActionResult HistorialMensajeRecibidosChat(int IdPersonal, string Numero, string Area, int IdTipoAgenda)
        {
            if (IdPersonal != null)
            {
                try
                {
                    var _repWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);
                    var _restultado = _repWhatsAppMensajeEnviado.HistorialChatsRecibido(IdPersonal, Numero, Area, IdTipoAgenda);
                    if (_restultado != null)
                    {
                        return Ok(_restultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
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

        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el ultimo mensaje recibido de WhatsApp por asesor.
        /// </summary>
        /// <returns> Lista de objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult WhatsAppUltimoMensajeEnviadosChat(int IdPersonal)
        {

            if (IdPersonal != null)
            {
                try
                {
                    WhatsAppMensajeEnviadoRepositorio _objetoMensaje = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);

                    var _restultado = _objetoMensaje.ListaUltimoMensajeChatsEnviado(IdPersonal);

                    if (_restultado != null)
                    {
                        return Ok(_restultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
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

        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene historial de mensajes de chat de asesor.
        /// </summary>
        /// <returns> Lista de objetoDTO: List<WhatsAppHistorialMensajesDTO> </returns>
        [Route("[action]/{IdPersonal}/{Numero}/{Area}")]
        [HttpGet]
        public ActionResult WhatsAppHistorialMensajeChat(int IdPersonal, string Numero, string Area)
        {

            if (IdPersonal != null)
            {
                try
                {
                    WhatsAppMensajeEnviadoRepositorio _objetoMensaje = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);
                                      

                    var _restultado = _objetoMensaje.ListaHistorialMensajeChat(IdPersonal, Numero, Area);

                    if (_restultado != null)
                    {
                        return Ok(_restultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
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

        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene mensajes multimedia de WhatsApp por id de WhatsApp.
        /// </summary>
        /// <returns> String </returns>
        [Route("[action]/{WaId}")]
        [HttpGet]
        public ActionResult WhatsAppObtenerMensajeMultimedia(string WaId)
        {

            if (WaId != null)
            {
                try
                {
                    WhatsAppMensajeEnviadoRepositorio _objetoMensaje = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);

                    var _restultado = _objetoMensaje.ObtenerMensajeMultimedia(WaId);

                    if (_restultado != null)
                    {
                        return Ok(_restultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
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

        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene conversacion por numero.
        /// </summary>
        /// <returns> objeto PersonalAlumdnoDTO </returns>
        [Route("[action]/{Numero}/{IdPais}")]
        [HttpGet]
        public ActionResult ObtenerConversacionPorNumero(string Numero,int IdPais)
        {
            try
            {
                WhatsAppMensajeEnviadoRepositorio _repWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                string Celular  = "";
                if (IdPais == 51)
                {
                    Celular = Numero.Substring(2, 9);
                }
                else if (IdPais == 57)
                {
                    Celular = "00" + Numero;
                }
                else if (IdPais == 591)
                {
                    Celular = "00" + Numero;
                }
                else
                {
                    Celular = "00" + Numero;
                }

                var Oportunidad = _repOportunidad.ObtenerOportunidadPorNumero(Celular);                

                if (Oportunidad == null)
                {
                    var DatosConversacion = _repWhatsAppMensajeEnviado.ObtenerConversacionNumero(Numero);
                    if (DatosConversacion == null) // si no tine conversaciones buscar si existe alumno
                    {
                        return Ok(DatosConversacion);
                    }
                    return Ok(DatosConversacion);
                }

                return Ok(Oportunidad);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene conversacion por oportunidad.
        /// </summary>
        /// <returns> objeto PersonalAlumdnoDTO </returns>
        [Route("[action]/{Numero}/{IdPais}")]
        [HttpGet]
        public ActionResult ObtenerConversacionPorOportunidad(string Numero, int IdPais)
        {
            try
            {
                WhatsAppMensajeEnviadoRepositorio _repWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                string Celular = "";
                if (IdPais == 51)
                {
                    Celular = Numero.Substring(2, 9);
                }
                else if (IdPais == 57)
                {
                    Celular = "00" + Numero;
                }
                else if (IdPais == 591)
                {
                    Celular = "00" + Numero;
                }
                else
                {
                    Celular = "00" + Numero;
                }

                var Oportunidad = _repOportunidad.ObtenerOportunidadPorNumero(Celular);


                return Ok(Oportunidad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene configuracion de personal.
        /// </summary>
        /// <returns> objeto PersonalAlumdnoDTO </returns>
        [Route("[action]/{Numero}/{idCentroCosto}/{IdPais}")]
        [HttpGet]
        public ActionResult ObtenerPersonalConfiguracion(string Numero, int idCentroCosto, int IdPais)
        {
            try
            {
                AsesorChatRepositorio _repAsesorChat = new AsesorChatRepositorio(_integraDBContext);

                string Celular = "";
                if (IdPais == 51)
                {
                    Celular = Numero.Substring(2, 9);
                }
                else if (IdPais == 57)
                {
                    Celular = "00" + Numero;
                }
                else if (IdPais == 591)
                {
                    Celular = "00" + Numero;
                }
                else
                {
                    Celular = "00" + Numero;
                }

                var Oportunidad = _repAsesorChat.ObtenerOportunidadPorNumero(idCentroCosto, Celular);
                
                return Ok(Oportunidad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Validar numero libre.
        /// </summary>
        /// <returns> Boolean </returns>
        [Route("[action]/{Numero}/{IdPais}/{IdCentroCosto}/{IdPersonal}")]
        [HttpGet]
        public ActionResult ValidarNumeroLibre(string Numero, int IdPais,int IdCentroCosto,int IdPersonal)
        {
            try
            {
                WhatsAppMensajeEnviadoRepositorio _repWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                PgeneralRepositorio _repPgeneral = new PgeneralRepositorio(_integraDBContext);

                string Celular = "";
                if (IdPais == 51)
                {
                    Celular = Numero.Substring(2, 9);
                }
                else if (IdPais == 57)
                {
                    Celular = "00" + Numero;
                }
                else if (IdPais == 591)
                {
                    Celular = "00" + Numero;
                }
                else
                {
                    Celular = "00" + Numero;
                }

                var programaGeneral = _repPgeneral.ObtenerPGeneralPEspecificoPorCentroCosto(IdCentroCosto);

                var Oportunidad = _repOportunidad.ValidarOportunidadesWhatsApp(Celular, programaGeneral.IdProgramaGeneral);

                foreach (var item in Oportunidad)
                {
                    if ((item.FaseOportunidad.ToUpper() == "IS" || item.FaseOportunidad.ToUpper() == "M") && (item.IdPgeneral == programaGeneral.IdProgramaGeneral) && item.IdPersonal != IdPersonal)
                    {
                        return Ok(false);
                    }
                }
                foreach (var item in Oportunidad)
                {
                    if (item.IdPersonal != IdPersonal)
                    {
                        return Ok(false);
                    }
                }

                //if (Oportunidad == null )
                //{
                //    if (Oportunidad.Count()>0)
                //    {
                //        var DatosConversacion = _repWhatsAppMensajeEnviado.ObtenerConversacionNumero(Numero);
                //        if(DatosConversacion != null)
                //        return Ok(false);
                //    }
                    
                //}

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el asesor con menos chats offline.
        /// </summary>
        /// <returns> Objeto PersonalNumeroMinimoChatDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerAsesorConMenorChatsOffLine()
        {
            try
            {
                WhatsAppMensajeEnviadoRepositorio _repWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);
                var respuesta = _repWhatsAppMensajeEnviado.ObtenerAsesorConMenorChat();

                return Ok(respuesta);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Valida las plantillas para mensajes enviados.
        /// </summary>
        /// <returns> Boolean </returns>
        [Route("[action]/{Plantilla}/{Numero}")]
        [HttpGet]
        public ActionResult ValidarplantillasEnviadas(string Plantilla, string Numero)
        {

            //marketin
            try
            {
                WhatsAppMensajeEnviadoRepositorio _repWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);
                var respuesta = _repWhatsAppMensajeEnviado.ValidarPlantillasEnviadas(Plantilla, Numero);

                return Ok(respuesta);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Optiene oportunidad por asesor y alumno.
        /// </summary>
        /// <returns> Objeto OportunidadDatosChatWhatsAppDTO </returns>
        [Route("[action]/{IdPersonal}/{IdAlumno}/{Numero}")]
        [HttpGet]
        public ActionResult ObtenerOportunidadPorAsesoryAlumno(int IdPersonal, int IdAlumno,string Numero)
        {
            try
            {
                OportunidadRepositorio _repWhatsAppMensajeEnviado = new OportunidadRepositorio(_integraDBContext);
                var Oportunidad = _repWhatsAppMensajeEnviado.ObtenerOportunidadPorAsesoryAlumno(IdAlumno, IdPersonal , Numero);
                
                return Ok(new {respuesta=Oportunidad != null ? true:false ,Oportunidad });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// TipoFuncion: POST
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el ultimo mensaje recibido por oportunidad.
        /// </summary>
        /// <returns> Objeto WhatsAppMensajesRecibidosOperacionesDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult WhatsAppUltimoMensajeRecibidosPorOportunidad([FromBody] Dictionary<string, string> Filtros)
        {
                                   
            if (Filtros != null)
            {
                try
                {
                    WhatsAppMensajeEnviadoRepositorio _objetoMensaje = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);

                    
                    var IdPersonal = Filtros.Where(w => w.Key == "IdAsesor").FirstOrDefault();
                    
                    
                    var _restultado = _objetoMensaje.ObtenerMensajesRecibidosOperaciones(Convert.ToInt32(IdPersonal.Value));

                    if (_restultado != null)
                    {
                        return Ok(_restultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
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

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/03/2021
        /// Versión: 1.1
        /// <summary>
        /// Obtiene últimos mensajes recibido de chat de cada alumno por IdPersonal y validación de Mensaje Ofensivo
        /// </summary>
        /// <returns> objetoDTO: List<WhatsAppMensajesControlOfensivoDTO>  </returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult WhatsAppUltimoMensajeRecibidosChatControlMensajes(int IdPersonal)
        {
            if (IdPersonal != null)
            {
                try
                {
                    WhatsAppMensajeEnviadoRepositorio objetoMensaje = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);
                    var resultado = objetoMensaje.ListaUltimoMensajeChatsRecibidoControlMensaje(IdPersonal);
                    if (resultado != null)
                    {
                        return Ok(resultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
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

        /// TipoFuncion: GET
        /// Autor: Edgar S.
        /// Fecha: 27/04/2021
        /// Versión: 1.1
        /// <summary>
        /// Obtiene últimos mensajes recibido de chat de cada alumno por IdPersonal y validación de Mensaje Ofensivo
        /// </summary>
        /// <returns> objetoDTO: List<WhatsAppMensajesControlOfensivoDTO>  </returns>
        [Route("[action]/{IdPersonal}/{Numero}/{Area}")]
        [HttpGet]
        public ActionResult WhatsAppHistorialMensajeChatControlMensaje(int IdPersonal, string Numero, string Area)
        {

            if (IdPersonal != null)
            {
                try
                {
                    WhatsAppMensajeEnviadoRepositorio objetoMensaje = new WhatsAppMensajeEnviadoRepositorio(_integraDBContext);

                    var resultado = objetoMensaje.ListaHistorialMensajeChatControlMensaje(IdPersonal, Numero, Area);

                    if (resultado != null)
                    {
                        return Ok(resultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
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
    }
}