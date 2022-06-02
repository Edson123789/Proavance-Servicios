using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
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
    [Route("api/PostulanteWhatsApp")]
    [ApiController]
    public class PostulanteWhatsAppController : ControllerBase
    {
		private readonly integraDBContext _integraDBContext;
		private readonly WhatsAppMensajeEnviadoPostulanteRepositorio _repWhatsAppMensajeEnviadoPostulante;
		private readonly WhatsAppConfiguracionRepositorio _repCredenciales;
		private readonly WhatsAppUsuarioCredencialRepositorio _repTokenUsuario;
		private readonly WhatsAppMensajeRecibidoPostulanteRepositorio _repWhatsAppMensajeRecibidoPostulante;
		PostulanteRepositorio _repPostulante;
		PersonalRepositorio _repPersonal;
		PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion;
		TokenPostulanteProcesoSeleccionRepositorio _repTokenPostulanteProcesoSeleccion;
		public PostulanteWhatsAppController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repWhatsAppMensajeEnviadoPostulante = new WhatsAppMensajeEnviadoPostulanteRepositorio(_integraDBContext);
			_repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
			_repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);
			_repWhatsAppMensajeRecibidoPostulante = new WhatsAppMensajeRecibidoPostulanteRepositorio(_integraDBContext);
			_repPostulante = new PostulanteRepositorio(_integraDBContext);
			_repPersonal = new PersonalRepositorio(_integraDBContext);
			_repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
			_repTokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccionRepositorio(_integraDBContext);
		}

		[Route("[action]/{Numero}/{IdPais}")]
		[HttpGet]
		public ActionResult ObtenerConversacionPorNumero(string Numero, int IdPais)
		{
			try
			{
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

				var DatosConversacion = _repWhatsAppMensajeEnviadoPostulante.ObtenerConversacionNumero(Numero);
				if (DatosConversacion == null) // si no tine conversaciones buscar si existe alumno
				{
					return Ok(DatosConversacion);
				}
				return Ok(DatosConversacion);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}


		[Route("[action]")]
		[HttpGet]
		public ActionResult ObtenerAsesorGPConMenorChatsOffLine()
		{
			try
			{
				var respuesta = _repWhatsAppMensajeEnviadoPostulante.ObtenerAsesorGPConMenorChat();
				return Ok(respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[Route("[action]/{IdPersonal}/{Numero}/{Area}")]
		[HttpGet]
		public ActionResult WhatsAppHistorialMensajeChat(int IdPersonal, string Numero, string Area)
		{

			if (IdPersonal != null)
			{
				try
				{
					var _restultado = _repWhatsAppMensajeEnviadoPostulante.ListaHistorialMensajeChatPostulante(IdPersonal, Numero, Area);

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

		[Route("[action]/{IdPersonal}/{Numero}/{Area}")]
		[HttpGet]
		public ActionResult HistorialMensajeRecibidosChat(int IdPersonal, string Numero, string Area)
		{

			if (IdPersonal != null)
			{
				try
				{
					var _restultado = _repWhatsAppMensajeEnviadoPostulante.HistorialChatsRecibidoPostulante(IdPersonal, Numero, Area);

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


		[Route("[action]/{IdPersonal}")]
		[HttpGet]
		public ActionResult WhatsAppUltimoMensajeRecibidosChat(int IdPersonal)
		{

			if (IdPersonal != null)
			{
				try
				{
					var _restultado = _repWhatsAppMensajeEnviadoPostulante.ListaUltimoMensajeChatsPostulante(IdPersonal);

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

		[Route("[action]/{WaId}")]
		[HttpGet]
		public ActionResult WhatsAppObtenerMensajeMultimedia(string WaId)
		{

			if (WaId != null)
			{
				try
				{
					var _restultado = _repWhatsAppMensajeEnviadoPostulante.ObtenerMensajeMultimedia(WaId);

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

		[Route("[action]")]
		[HttpPost]
		public ActionResult WhatsAppMensaje([FromBody] WhatsAppMensajeEnviadoPostulanteDTO DTO)
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


						var _credencialesHost = _repCredenciales.ObtenerCredencialHost(DTO.IdPais);
						var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(DTO.IdPersonal, DTO.IdPais);

						string urlToPost = _credencialesHost.UrlWhatsApp;

						string resultado = string.Empty, _waType = string.Empty;

						WhatsAppMensajeEnviadoPostulanteBO mensajeEnviado = new WhatsAppMensajeEnviadoPostulanteBO();

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
									WhatsAppUsuarioCredencialBO modelCredencial = new WhatsAppUsuarioCredencialBO();

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
									_waType = "hsm";

									MensajePlantillaWhatsAppEnvio _mensajePlantilla = new MensajePlantillaWhatsAppEnvio();

									_mensajePlantilla.to = DTO.WaTo;
									_mensajePlantilla.type = DTO.WaType;
									_mensajePlantilla.hsm = new hsm();

									_mensajePlantilla.hsm.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
									_mensajePlantilla.hsm.element_name = DTO.WaBody;

									_mensajePlantilla.hsm.language = new language();
									_mensajePlantilla.hsm.language.policy = "deterministic";
									_mensajePlantilla.hsm.language.code = "es";

									if (DTO.datosPlantillaWhatsApp != null)
									{
										_mensajePlantilla.hsm.localizable_params = new List<localizable_params>();
										foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
										{
											localizable_params _dato = new localizable_params();
											_dato.@default = listaDatos.texto;

											_mensajePlantilla.hsm.localizable_params.Add(_dato);
										}
									}

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
								mensajeEnviado.WaId = itemGuardar.id;
								mensajeEnviado.WaTo = DTO.WaTo;
								mensajeEnviado.WaType = _waType;
								mensajeEnviado.WaRecipientType = DTO.WaRecipientType;
								mensajeEnviado.WaBody = DTO.WaBody;
								mensajeEnviado.WaCaption = DTO.WaCaption;
								mensajeEnviado.WaLink = DTO.WaLink;
								mensajeEnviado.WaFileName = DTO.WaFileName;
								mensajeEnviado.IdPais = DTO.IdPais;
								if (DTO.IdPostulante != 0)
								{
									mensajeEnviado.IdPostulante = DTO.IdPostulante;
								}
								else
								{
									mensajeEnviado.IdPostulante = null;
								}

								mensajeEnviado.IdPersonal = DTO.IdPersonal;
								mensajeEnviado.Estado = true;
								mensajeEnviado.FechaCreacion = DateTime.Now;
								mensajeEnviado.FechaModificacion = DateTime.Now;
								mensajeEnviado.UsuarioCreacion = DTO.usuario;
								mensajeEnviado.UsuarioModificacion = DTO.usuario;

								_repWhatsAppMensajeEnviadoPostulante.Insert(mensajeEnviado);
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

				//var _rptMensajeRecibido = _repWhatsAppMensajeRecibidoPostulante.GetBy(x => x.WaId == WaId).FirstOrDefault();
				var _rptMensajeRecibido = _repWhatsAppMensajeRecibidoPostulante.ObtenerWhatsAppMensajeRecibidoPostulantePorWaId(WaId);
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
							WhatsAppUsuarioCredencialBO modelCredencial = new WhatsAppUsuarioCredencialBO();

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

					string respuesta = string.Empty;

					if (_rptMensajeRecibido.WaType.Contains("image"))
					{
						respuesta = _repWhatsAppMensajeRecibidoPostulante.guardarArchivos(response.RawBytes, _rptMensajeRecibido.WaType, response.ContentType, _rptMensajeRecibido.WaIdTypeMensaje + ".jpeg", _rptMensajeRecibido.IdPais);
					}
					else if (_rptMensajeRecibido.WaType.Contains("voice"))
					{
						respuesta = _repWhatsAppMensajeRecibidoPostulante.guardarArchivos(response.RawBytes, _rptMensajeRecibido.WaType, response.ContentType, _rptMensajeRecibido.WaIdTypeMensaje + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("HHmmss") + ".ogg", _rptMensajeRecibido.IdPais);
					}
					else if (_rptMensajeRecibido.WaType.Contains("video"))
					{
						respuesta = _repWhatsAppMensajeRecibidoPostulante.guardarArchivos(response.RawBytes, _rptMensajeRecibido.WaType, response.ContentType, _rptMensajeRecibido.WaIdTypeMensaje + ".mp4", _rptMensajeRecibido.IdPais);
					}
					else if (_rptMensajeRecibido.WaType.Contains("audio"))
					{
						respuesta = _repWhatsAppMensajeRecibidoPostulante.guardarArchivos(response.RawBytes, _rptMensajeRecibido.WaType, response.ContentType, _rptMensajeRecibido.WaIdTypeMensaje + ".mp4", _rptMensajeRecibido.IdPais);
					}
					else
					{
						respuesta = _repWhatsAppMensajeRecibidoPostulante.guardarArchivos(response.RawBytes, _rptMensajeRecibido.WaType, response.ContentType, _rptMensajeRecibido.WaFileName, _rptMensajeRecibido.IdPais);
					}

					_rptMensajeRecibido.WaFile = respuesta;
					_repWhatsAppMensajeRecibidoPostulante.Update(_rptMensajeRecibido);


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

		[Route("[Action]")]
		[HttpPost]
		public ActionResult GenerarPlantillaGPWhatsapp([FromBody] GestionPersonasPlantillaWhatsAppDTO Plantilla)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var personal = _repPersonal.FirstById(Plantilla.IdPersonal);
				var postulanteProcesoSeleccion = _repPostulanteProcesoSeleccion.ObtenerPostulanteProcesoSeleccionPorIdPostulante(Plantilla.IdPostulante);
				if(Plantilla.IdPlantilla == 1300)
				{
					if (postulanteProcesoSeleccion.Token == null)
					{
						TokenPostulanteProcesoSeleccionBO tokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccionBO();
						var token = tokenPostulanteProcesoSeleccion.GenerarClave(8);
						var tokenHash = Crypto.HashPassword(token);
						var tokenPostulante = _repTokenPostulanteProcesoSeleccion.ObtenerUltimoTokenPorPostulanteProcesoSeleccion(postulanteProcesoSeleccion.Id);

						TokenPostulanteProcesoSeleccionBO tokenNueva = new TokenPostulanteProcesoSeleccionBO()
						{
							IdPostulanteProcesoSeleccion = postulanteProcesoSeleccion.Id,
							Token = token,
							TokenHash = tokenHash,
							GuidAccess = Guid.NewGuid(),
							Activo = true,
							Estado = true,
							UsuarioCreacion = Plantilla.Usuario,
							UsuarioModificacion = Plantilla.Usuario,
							FechaCreacion = DateTime.Now,
							FechaModificacion = DateTime.Now,
							FechaEnvioAccesos = DateTime.Now
						};

						_repTokenPostulanteProcesoSeleccion.Insert(tokenNueva);
						postulanteProcesoSeleccion.Token = token;
						postulanteProcesoSeleccion.GuidAccess = tokenNueva.GuidAccess;
					}
				}
				var postulante = _repPostulante.FirstById(postulanteProcesoSeleccion.IdPostulante);
				if (postulanteProcesoSeleccion.Id > 0 && postulanteProcesoSeleccion.Id != null)
				{
					DateTime? fecha = null;
					if (Plantilla.Fecha.HasValue)
					{
						fecha = Plantilla.Fecha.Value.AddHours(-5);
					}
					var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
					{
						IdPlantilla = Plantilla.IdPlantilla,
						IdPostulanteProcesoSeleccion = postulanteProcesoSeleccion.Id,
						Personal = personal,
						FechaGP = fecha
					};
					_reemplazoEtiquetaPlantilla.ReemplazarEtiquetasProcesoSeleccion();
					return Ok(new { plantilla =  _reemplazoEtiquetaPlantilla.WhatsAppReemplazado.Plantilla, objetoplantilla = _reemplazoEtiquetaPlantilla.WhatsAppReemplazado.ListaEtiquetas });
				}

				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
