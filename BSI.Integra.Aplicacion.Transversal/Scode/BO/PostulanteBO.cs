using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Newtonsoft.Json.NetCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
	public class PostulanteBO : BaseBO
	{
		public string Nombre { get; set; }
		public string ApellidoPaterno { get; set; }
		public string ApellidoMaterno { get; set; }
		public string NroDocumento { get; set; }
		public string Telefono { get; set; }
		public string Celular { get; set; }
		public string Email { get; set; }
		public string Telefono2 { get; set; }
		public string Celular2 { get; set; }
		public string Celular3 { get; set; }
		public string Email2 { get; set; }
		public string Email3 { get; set; }
		public DateTime? FechaNacimiento { get; set; }
		public int? IdPais { get; set; }
		public int? IdCiudad { get; set; }
		public int? IdTipoDocumento { get; set; }
		public int? IdSexo { get; set; }
		public int? IdMigracion { get; set; }
		public string UrlPerfilFacebook { get; set; }
		public string UrlPerfilLinkedin { get; set; }
		public bool? EsProcesoAnterior { get; set; }
		public int? Edad { get; set; }
		public bool? TieneHijo { get; set; }
		public int? CantidadHijo { get; set; }

		public string ObtenerNumeroWhatsApp(int codigoPais, string celular)
		{
			try
			{
				if (celular.Contains("-"))
				{
					var index = celular.IndexOf("-");
					celular = celular.Substring(index+1);
				}
				if (codigoPais == 51)
				{
					if (celular.Length == 9)
					{
						celular = "51" + celular;
					}
				}
				else if (codigoPais == 57)
				{
					if (celular.StartsWith("00"))
					{
						celular = celular.Substring(2, celular.Length - 2);
					}
					if (celular.Length < 12)
					{
						celular = "57" + celular;
					}
				}
				else if (codigoPais == 591)
				{
					if (celular.StartsWith("00"))
					{
						celular = celular.Substring(2, celular.Length - 2);
					}
					if (celular.Length < 11)
					{
						celular = "591" + celular;
					}
				}
				return celular;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public bool ValidarNumeroEnvioWhatsApp(int IdPersonal, int IdPais, ValidarNumerosWhatsAppAsyncDTO DTO)
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

					WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio();
					WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio();

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
							banderaLogin = true;
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
						if (datoRespuesta.contacts[0].status == "invalid")
						{
							return false;
						}
						else
						{
							return true;
						}
					}
					else
					{
						return false;
					}

				}
				catch (Exception ex)
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
	}
}
