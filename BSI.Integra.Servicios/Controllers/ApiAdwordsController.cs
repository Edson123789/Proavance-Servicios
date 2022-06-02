using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using Google.Api.Ads.AdWords.Lib;
using BSI.Integra.Aplicacion.DTOs;
using Google.Api.Ads.AdWords.Util.Reports;
using Google.Api.Ads.AdWords.v201809;
using Google.Api.Ads.Common.Util.Reports;
using System.IO.Compression;
using System.Xml;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/ApiAdword")]
	public class ApiAdwordsController : Controller
	{

		[Route("[Action]")]
		[HttpGet]
		public ActionResult ObtenerRegistroLog()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				AdwordsLogRepositorio _repAdwordsLog = new AdwordsLogRepositorio();
				var AdwordsLog = _repAdwordsLog.ObtenerLogs();
				return Ok(AdwordsLog);
			}
			catch (Exception Ex)
			{
				return BadRequest(Ex.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ApiAdword([FromBody]Dictionary<string,string> infoUsuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			
			PersonalRepositorio _repPersonal = new PersonalRepositorio();
			IntegraAspNetUsersRepositorio _repAspNetUsers = new IntegraAspNetUsersRepositorio();
			string usuarioNombre = infoUsuario["Usuario"];
			try
			{
				// Credecianles de acceso para api adwords de google


				Dictionary<string, string> Cabecera = new Dictionary<string, string>() {
					{ "DeveloperToken", "BUdPVfmTtKHPJz4ZektGfQ" },
					{"ClientCustomerId", "574-320-7825" },
					{ "OAuth2ClientId", "392997978069-ohmt625g9l3htvr97drcqkc6eg7alkch.apps.googleusercontent.com" },
					{"OAuth2ClientSecret", "PyidhDlTZgZAQybHzzRGREOo" },
					{"OAuth2RefreshToken", "1/9QhDeYqBibzg6rbTtsF2kNMZdIEJLLzRKGm4uJbatdA" }
				};


				ConjuntoAnuncioAdwordRepositorio _repConjuntoAnuncioAdword = new ConjuntoAnuncioAdwordRepositorio();
				var mensajeLog = Ejecutar(new AdWordsUser(Cabecera), usuarioNombre);
				return Ok(mensajeLog);
			}
			catch (Exception ex)
			{
				AdwordsLogRepositorio _repAdwordsLog = new AdwordsLogRepositorio();
				AdwordsLogBO mensajeLog = new AdwordsLogBO()
				{
					Estado = true,
					UsuarioCreacion = usuarioNombre,
					UsuarioModificacion = usuarioNombre,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now,
					Mensaje = ex.Message
				};
				_repAdwordsLog.Insert(mensajeLog);
				return BadRequest(ex.Message);
			}
		}


		/// <summary>
		/// Descarga los reportes de adwords a la base de datos mediante adwordUser y el asesor que esta realizando la operacion.
		/// </summary>
		/// <param name="User"></param>
		/// <param name="UsuarioAsesor"></param>
		/// <param name="NombresAsesor"></param>
		/// <returns></returns>
		private List<String> Ejecutar(AdWordsUser User, string Usuario)
		{
			AdwordsLogRepositorio _adsLog = new AdwordsLogRepositorio();
			string query = "SELECT AdGroupId, AdGroupName, CampaignId, CampaignName, AdNetworkType1, Date, AdGroupStatus, Cost, Ctr, AverageCpc, AveragePosition, Clicks, Impressions FROM AD_PERFORMANCE_REPORT DURING YESTERDAY";
			ReportUtilities reportUtilities = null;

			try
			{
				reportUtilities = new ReportUtilities(User, "v201809", query,
				DownloadFormat.GZIPPED_XML.ToString());
			}
			catch (Exception e)
			{
				var mensaje = "Fallo en descargar el reporte " + e.Message + " - " + e.InnerException != null ? e.InnerException.Message : "";
				AdwordsLogBO mensajeLog = new AdwordsLogBO()
				{
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now,
					Mensaje = mensaje,
					UsuarioCreacion = Usuario,
					UsuarioModificacion = Usuario
				};
				_adsLog.Insert(mensajeLog);

				throw new Exception(mensaje);
			}
			var reporte = new List<ReporteDTO>();

			try
			{
				using (ReportResponse response = reportUtilities.GetResponse())
				{
					using (GZipStream gzipStream = new GZipStream(response.Stream,
						CompressionMode.Decompress))
					{
						using (XmlTextReader reader = new XmlTextReader(gzipStream))
						{
							string documento = reader.ReadInnerXml();
							while (reader.Read())
							{
								var r = new ReporteDTO();
								while (reader.MoveToNextAttribute())
								{
									switch (reader.Name)
									{
										case "adGroupID":
											r.AdGroupId = reader.Value;
											break;
										case "adGroup":
											r.AdGroup = reader.Value;
											break;
										case "campaignID":
											r.CampaignId = reader.Value;
											break;
										case "campaign":
											r.Campaign = reader.Value;
											break;
										case "network":
											r.Network = reader.Value;
											break;
										case "day":
											r.Day = reader.Value;
											break;
										case "adGroupState":
											r.AdGroupState = reader.Value;
											break;
										case "cost":
											r.Cost = long.Parse(reader.Value);
											break;
										case "avgPosition":
											r.AvgPosition = Convert.ToDouble(reader.Value);
											break;
										case "clicks":
											r.Clicks = long.Parse(reader.Value);
											break;
										case "impressions":
											r.Impressions = int.Parse(reader.Value);
											break;
									}
								}
								if (!String.IsNullOrEmpty(r.AdGroupId) && reader.Name == "impressions")
								{
									//flag para ver si va existir repetidos
									bool flag = false;

									foreach (var item in reporte)
									{
										if (item.AdGroupId == r.AdGroupId && item.CampaignId == r.CampaignId)
										{
											item.Cost = item.Cost + r.Cost;
											item.Clicks = item.Clicks + r.Clicks;
											item.Impressions = item.Impressions + r.Impressions;
											item.AvgPosition = (item.AvgPosition + r.AvgPosition) / 2;
											//como ya existe repetido entonces ya no se ejecuta  reporte.Add(r)
											flag = true;
										}
									}
									if (flag == false)
									{
										reporte.Add(r);
									}
								}
							}

							var reporteActualizado = CalcularPorcentajes(reporte);
							var guardar = Guardar(reporteActualizado, Usuario);
							string mensaje = "";
							foreach (var item in guardar)
							{
								mensaje = mensaje + item + " ";
							}
							AdwordsLogBO mensajeLog = new AdwordsLogBO()
							{
								Estado = true,
								UsuarioCreacion = Usuario,
								UsuarioModificacion = Usuario,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now,
								Mensaje = mensaje
							};
							_adsLog.Insert(mensajeLog);
							return guardar;
						}
					}
				}

			}
			catch (Exception e)
			{
				throw new System.ApplicationException("Fallo en descargar el reporte." + e.Message + " - " + (e.InnerException != null ? e.InnerException.Message : ""), e);
			}
		}

		private List<string> Guardar(List<ReporteDTO> reporte, string UsuarioAsesor)
		{
			AdwordsInsigthRepositorio _repAdwordsInsigth = new AdwordsInsigthRepositorio();
			ConjuntoAnuncioRepositorio _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio();
			ConjuntoAnuncioAdwordRepositorio _repConjuntoAnuncioAdword = new ConjuntoAnuncioAdwordRepositorio();
			var Mensaje = new List<string>();
			if (reporte.Count == 0)
			{
				Mensaje.Add("El reporte esta vacio - no cargo adwords");
				return Mensaje;
			}
			string idError = "";
			integraDBContext context = new integraDBContext();

			foreach (var item in reporte)
			{
				using (var contextoTransaccion = context.Database.BeginTransaction())
				{
					try
					{
						if (!_repConjuntoAnuncioAdword.Contiene(item.AdGroup))
						{
							var caintegra = _repConjuntoAnuncio.obtenerAnuncio(item.AdGroup);
							idError = item.AdGroupId;
							ConjuntoAnuncioAdwordBO conjuntoAnuncioAdwords = new ConjuntoAnuncioAdwordBO()
							{
								IdF = item.AdGroupId,
								CreatedTime = DateTime.Now,
								EffectiveStatus = item.AdGroupState,
								Name = item.AdGroup,
								OptimizationGoal = "CONVERSION",
								StartTime = DateTime.Parse(item.Day),
								Status = item.AdGroupState,
								UpdatedTime = DateTime.Now,
								TieneInsights = true,
								EsValidado = false,
								EsIntegra = false,
								EsPublicado = false,
								ActivoActualizado = false,
								FkCampaniaIntegra = caintegra == null ? 0 : caintegra.Id,
								EsRelacionado = caintegra == null ? false : true,
								EsOtros = false,
								Estado = true,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now,
								UsuarioCreacion = UsuarioAsesor,
								UsuarioModificacion = UsuarioAsesor,
								CuentaPublicitaria = 1,
								NombreCampania = item.Campaign,
								CentroCosto = "",
								TipoCampania = item.Network == "Search Network" ? 1 : 2
							};
							_repConjuntoAnuncioAdword.Insert(conjuntoAnuncioAdwords);
							var procesarInsights = ProcesarInsightsAdwords(item, conjuntoAnuncioAdwords);
							if (!procesarInsights)
							{
								Mensaje.Add("Error ProcesarInsightsAdwords");
							}
							if (caintegra != null)
							{
								var actualizarCA = ActualizarConjuntoAnuncios(conjuntoAnuncioAdwords);
								if (!actualizarCA)
								{
									Mensaje.Add("Error ActualizarConjuntoAnuncios");
								}
							}
						}
						else
						{
							var padre = _repConjuntoAnuncioAdword.ObtenerAnuncioAdword(item.AdGroup);
							AdwordInsigthBO adwordsInsigth = new AdwordInsigthBO()
							{
								AdsetId = padre.id_f,
								Age = null,
								DateStart = DateTime.Parse(item.Day),
								DateStop = DateTime.Parse(item.Day),
								Gender = "",
								Impressions = item.Impressions,
								Objective = "CONVERSIONS",
								Reach = null,
								Spend = Convert.ToDouble(item.Cost) / 1000000,
								IdTipo = 1,
								UltimaActualizacion = DateTime.Now,
								Frequency = null,
								FCCosto = 0,
								FPostLike = 0,
								FComment = 0,
								FLinkClick = Convert.ToInt32(item.Clicks),
								FLike = 0,
								FPhotoView = 0,
								FPost = null,
								FPageEngagement = 0,
								FPostEngagement = 0,
								FLeadgenOther = 0,
								Estado = true,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now,
								UsuarioCreacion = UsuarioAsesor,
								UsuarioModificacion = UsuarioAsesor,
								CpcMedio = item.AvgCPC.ToString(),
								PosicionMedia = Convert.ToDecimal(item.AvgPosition)
							};
							_repAdwordsInsigth.Insert(adwordsInsigth);
							var actualizar = ActualizarEstadoConjuntoAnunciosAdwords(padre, item.AdGroupState, UsuarioAsesor);
							if (!actualizar)
							{
								Mensaje.Add("Error Actualizar Estado ConjuntoAnunciosAdwords");
							}
						}
					}
					catch (Exception ex)
					{
						Mensaje.Add($"Error al  guardar en id {idError} rollback - " + ex.Message);
						return Mensaje;
					}

					contextoTransaccion.Commit();
				}
			}
			Mensaje.Add("El reporte se guardo correctamente");
			return Mensaje;
		}

		/// <summary>
		/// Actualiza el Estado de los ConjuntoAnunciosAdwords
		/// </summary>
		/// <param name="padre"></param>
		/// <param name="estado"></param>
		/// <param name="UsuarioAsesor"></param>
		/// <returns></returns>
		private bool ActualizarEstadoConjuntoAnunciosAdwords(ConjuntoAnuncioAdwordDTO padre, string estado, string UsuarioAsesor)
		{
			try
			{
				ConjuntoAnuncioAdwordRepositorio _repConjuntoAnuncioAdword = new ConjuntoAnuncioAdwordRepositorio();
				ConjuntoAnuncioAdwordDTO anuncio = _repConjuntoAnuncioAdword.ObtenerAnuncioAdword(padre.name);
				ConjuntoAnuncioAdwordBO conjuntoAnuncioAdword = new ConjuntoAnuncioAdwordBO(anuncio.Id)
				{
					IdF = anuncio.id_f,
					CampaignId = anuncio.campaign_id,
					CreatedTime = anuncio.created_time,
					EffectiveStatus = estado,
					Name = anuncio.name,
					OptimizationGoal = anuncio.optimization_goal,
					StartTime = anuncio.start_time,
					Status = estado,
					UpdatedTime = anuncio.updated_time,
					TieneInsights = anuncio.tiene_insights,
					EsValidado = anuncio.es_validado,
					EsIntegra = anuncio.es_integra,
					EsPublicado = anuncio.es_publicado,
					ActivoActualizado = anuncio.activo_actualizado,
					FkCampaniaIntegra = anuncio.FK_CampaniaIntegra,
					EsRelacionado = anuncio.es_relacionado,
					EsOtros = anuncio.es_otros,
					CuentaPublicitaria = anuncio.CuentaPublicitaria,
					NombreCampania = anuncio.NombreCampania,
					CentroCosto = anuncio.CentroCosto,
					TipoCampania = anuncio.tipo_campania,
					FechaModificacion = DateTime.Now,
					UsuarioModificacion = UsuarioAsesor
				};
				conjuntoAnuncioAdword.EffectiveStatus = estado;
				conjuntoAnuncioAdword.Status = estado;
				_repConjuntoAnuncioAdword.Update(conjuntoAnuncioAdword);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// Actualiza datos de los conjunto de anuncios
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		private bool ActualizarConjuntoAnuncios(ConjuntoAnuncioAdwordBO item)
		{
			try
			{
				ConjuntoAnuncioRepositorio _repCA = new ConjuntoAnuncioRepositorio();
				ConjuntoAnuncioDTO _anuncio = _repCA.ObtenerAnuncioPorIdCampaniaIntegra(item.FkCampaniaIntegra);
				if (_repCA.Exist(item.Id))
				{


					ConjuntoAnuncioBO ca_original = new ConjuntoAnuncioBO();
					ca_original = _repCA.FirstById(item.Id);
					ca_original.Nombre = _anuncio.Nombre;
					ca_original.IdCategoriaOrigen = _anuncio.IdCategoriaOrigen;
					ca_original.Origen = _anuncio.Origen;
					ca_original.IdConjuntoAnuncioFacebook = item.IdF;
					ca_original.FechaCreacionCampania = _anuncio.FechaCreacionCampania;
					ca_original.FechaModificacion = DateTime.Now;
					ca_original.UsuarioModificacion = item.UsuarioCreacion;

					_repCA.Update(ca_original);
				}


				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// Procesa todos los insigths de los atwords
		/// </summary>
		/// <param name="item"></param>
		/// <param name="padre"></param>
		/// <returns></returns>
		private bool ProcesarInsightsAdwords(ReporteDTO item, ConjuntoAnuncioAdwordBO padre)
		{
			ConjuntoAnuncioAdwordRepositorio _repConjuntoAnunciosAdwords = new ConjuntoAnuncioAdwordRepositorio();
			AdwordsInsigthRepositorio _repAdwordsInsigth = new AdwordsInsigthRepositorio();
			var padreAdwords = _repConjuntoAnunciosAdwords.ObtenerAnuncioAdwordPorId(padre.Id);
			AdwordInsigthBO adwordsInsigth = new AdwordInsigthBO()
			{
				AdsetId = padreAdwords.id_f,
				Age = null,
				DateStart = DateTime.Parse(item.Day),
				DateStop = DateTime.Parse(item.Day),
				Gender = "",
				Impressions = item.Impressions,
				Objective = "CONVERSION",
				Reach = null,
				Spend = Convert.ToDouble(item.Cost) / 1000000,
				IdTipo = 1,
				UltimaActualizacion = DateTime.Now,
				Frequency = null,
				FCCosto = 0,
				FPostLike = 0,
				FComment = 0,
				FLinkClick = Convert.ToInt32(item.Clicks),
				FLike = 0,
				FPhotoView = 0,
				FPost = null,
				FPageEngagement = 0,
				FPostEngagement = 0,
				FLeadgenOther = 0,
				Estado = true,
				FechaCreacion = DateTime.Now,
				FechaModificacion = DateTime.Now,
				UsuarioCreacion = padre.UsuarioCreacion,
				UsuarioModificacion = padre.UsuarioCreacion,
				CpcMedio = item.AvgCPC.ToString(),
				PosicionMedia = Convert.ToDecimal(item.AvgPosition)
			};
			_repAdwordsInsigth.Insert(adwordsInsigth);

			return true;
		}

		/// <summary>
		/// Calcula porcentajes respecto a la cantidad de clicks que se realizaron en las publicidades
		/// </summary>
		/// <param name="reporte"></param>
		/// <returns></returns>
		private List<ReporteDTO> CalcularPorcentajes(List<ReporteDTO> reporte)
		{
			foreach (var item in reporte)
			{
				var clics = Convert.ToDouble(item.Clicks);
				var nroVistas = Convert.ToDouble(item.Impressions);
				item.CTR = (nroVistas != 0) ? (clics / nroVistas) * 100 : 0;
				item.AvgCPC = (clics != 0) ? item.Cost / item.Clicks : 0;
			}
			return reporte;
		}
	}
}
