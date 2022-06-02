using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CloudflareLlaveGenerada")]
    [ApiController]
    public class CloudflareLlaveGeneradaController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public CloudflareLlaveGeneradaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        // GET: api/<CloudflareLlaveGeneradaController>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodosRegistros()
        {
            try
            {
                CloudflareLlaveGeneradaRepositorio cloudflareUsuarioLlaveRepositorio = new CloudflareLlaveGeneradaRepositorio(_integraDBContext);
                var registros = cloudflareUsuarioLlaveRepositorio.ObtenerListaCloudflareLlaveGenerada();
                return Ok(registros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPersonal}/{Usuario}")]
        [HttpPost]
        public ActionResult InsertarCloudflareLlaveGenerada(int IdPersonal, string Usuario)
        {
            string urlToPost = string.Empty, _jsonRespuesta = string.Empty;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CloudflareUsuarioLlaveRepositorio cloudflareUsuarioLlaveRepositorio = new CloudflareUsuarioLlaveRepositorio(_integraDBContext);
                CloudflareLlaveGeneradaRepositorio cloudflareLlaveGeneradaRepositorio = new CloudflareLlaveGeneradaRepositorio(_integraDBContext);

                var usuarioCloud = cloudflareUsuarioLlaveRepositorio.ObtenerRegistroCloudflareUsuarioLlavePorIdPersonal(IdPersonal);

                if (usuarioCloud != null)
                {
                    
                    urlToPost = "https://api.cloudflare.com/client/v4/accounts/" + usuarioCloud.AccountId + "/stream/keys";

                    resultadoCloudflareLlaveGeneradaBO _resultadoobjeto = new resultadoCloudflareLlaveGeneradaBO();

                    try
                    {
                        using (WebClient client = new WebClient())
                        {
                            //client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            //client.Headers[HttpRequestHeader.Accept] = "application/json";
                            client.Headers.Add("X-Auth-Key", usuarioCloud.AuthKey);
                            client.Headers.Add("X-Auth-Email", usuarioCloud.AuthEmail);
                            var resultado = client.UploadString(urlToPost, "");

                            _jsonRespuesta = resultado;

                            var _resultado = JsonConvert.DeserializeObject<resultadoCloudflareLlaveGeneradaBO>(resultado);
                            _resultadoobjeto = _resultado;
                        }
                    }
                    catch (Exception ex)
                    {
                        _resultadoobjeto = null;
                    }

                    if (_resultadoobjeto != null)
                    {
                        CloudflareLlaveGeneradaBO cloudflareLlaveGenerada = new CloudflareLlaveGeneradaBO();


                        using (TransactionScope scope = new TransactionScope())
                        {
                            cloudflareLlaveGenerada.IdCloudflareUsuarioLlave = usuarioCloud.Id;
                            cloudflareLlaveGenerada.JsonRespuesta = _jsonRespuesta;
                            cloudflareLlaveGenerada.KeyId = _resultadoobjeto.result.id;
                            cloudflareLlaveGenerada.KeyPem = _resultadoobjeto.result.pem;
                            cloudflareLlaveGenerada.KeyJwk = _resultadoobjeto.result.jwk;
                            cloudflareLlaveGenerada.Created = _resultadoobjeto.result.created;
                            cloudflareLlaveGenerada.Success = _resultadoobjeto.success;
                            if (_resultadoobjeto.success)
                            {
                                cloudflareLlaveGenerada.Valido = true;
                            }
                            else
                            {
                                cloudflareLlaveGenerada.Valido = false;
                            }
                            
                            cloudflareLlaveGenerada.Estado = true;
                            cloudflareLlaveGenerada.UsuarioCreacion = Usuario;
                            cloudflareLlaveGenerada.UsuarioModificacion = Usuario;
                            cloudflareLlaveGenerada.FechaCreacion = DateTime.Now;
                            cloudflareLlaveGenerada.FechaModificacion = DateTime.Now;

                            cloudflareLlaveGeneradaRepositorio.Insert(cloudflareLlaveGenerada);
                            scope.Complete();
                        }
                    }
                    
                }

                return Ok(_jsonRespuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPersonal}/{Usuario}")]
        [HttpPost]
        public ActionResult RegistrarVideoCloudflareDescargaLink(int IdPersonal, string Usuario)
        {
            string urlToPost = string.Empty, _jsonRespuesta = string.Empty;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CloudflareUsuarioLlaveRepositorio cloudflareUsuarioLlaveRepositorio = new CloudflareUsuarioLlaveRepositorio(_integraDBContext);
                CloudflareLlaveGeneradaRepositorio cloudflareLlaveGeneradaRepositorio = new CloudflareLlaveGeneradaRepositorio(_integraDBContext);

                var usuarioCloud = cloudflareUsuarioLlaveRepositorio.ObtenerRegistroCloudflareUsuarioLlavePorIdPersonal(IdPersonal);

                if (usuarioCloud != null)
                {

                    urlToPost = "https://api.cloudflare.com/client/v4/accounts/" + usuarioCloud.AccountId;

                    var _registroVideo = cloudflareUsuarioLlaveRepositorio.ObtenerRegistroVideoRegistradoCloudflareAula();

                    if (_registroVideo != null && _registroVideo.Count > 0)
                    {
                        foreach (var item in _registroVideo)
                        {

                        }
                    }


                    resultadoCloudflareLlaveGeneradaBO _resultadoobjeto = new resultadoCloudflareLlaveGeneradaBO();

                    try
                    {
                        using (WebClient client = new WebClient())
                        {
                            //client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            //client.Headers[HttpRequestHeader.Accept] = "application/json";
                            client.Headers.Add("X-Auth-Key", usuarioCloud.AuthKey);
                            client.Headers.Add("X-Auth-Email", usuarioCloud.AuthEmail);
                            var resultado = client.UploadString(urlToPost, "");

                            _jsonRespuesta = resultado;

                            var _resultado = JsonConvert.DeserializeObject<resultadoCloudflareLlaveGeneradaBO>(resultado);
                            _resultadoobjeto = _resultado;
                        }
                    }
                    catch (Exception ex)
                    {
                        _resultadoobjeto = null;
                    }

                    if (_resultadoobjeto != null)
                    {
                        CloudflareLlaveGeneradaBO cloudflareLlaveGenerada = new CloudflareLlaveGeneradaBO();


                        using (TransactionScope scope = new TransactionScope())
                        {
                            cloudflareLlaveGenerada.IdCloudflareUsuarioLlave = usuarioCloud.Id;
                            cloudflareLlaveGenerada.JsonRespuesta = _jsonRespuesta;
                            cloudflareLlaveGenerada.KeyId = _resultadoobjeto.result.id;
                            cloudflareLlaveGenerada.KeyPem = _resultadoobjeto.result.pem;
                            cloudflareLlaveGenerada.KeyJwk = _resultadoobjeto.result.jwk;
                            cloudflareLlaveGenerada.Created = _resultadoobjeto.result.created;
                            cloudflareLlaveGenerada.Success = _resultadoobjeto.success;
                            if (_resultadoobjeto.success)
                            {
                                cloudflareLlaveGenerada.Valido = true;
                            }
                            else
                            {
                                cloudflareLlaveGenerada.Valido = false;
                            }

                            cloudflareLlaveGenerada.Estado = true;
                            cloudflareLlaveGenerada.UsuarioCreacion = Usuario;
                            cloudflareLlaveGenerada.UsuarioModificacion = Usuario;
                            cloudflareLlaveGenerada.FechaCreacion = DateTime.Now;
                            cloudflareLlaveGenerada.FechaModificacion = DateTime.Now;

                            cloudflareLlaveGeneradaRepositorio.Insert(cloudflareLlaveGenerada);
                            scope.Complete();
                        }
                    }

                }

                return Ok(_jsonRespuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
