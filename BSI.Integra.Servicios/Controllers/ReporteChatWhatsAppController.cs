using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/ReporteChatWhatsApp")]
	[ApiController]
	public class ReporteChatWhatsAppController : Controller
	{
		private readonly integraDBContext _integraDBContext;
		public ReporteChatWhatsAppController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCombosReporteChat()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
				PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);

				var personal = _repPersonal.CargarPersonalParaFiltro();
				var pais = _repPais.ObtenerPaisesCombo();

				return Ok(new { Personal = personal, Pais = pais });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		
		[Route("[Action]")]
		[HttpPost]
		public ActionResult CalculoChatsWhatsAppDiario([FromBody]ParametrosCalculoReporteChatDTO Fecha)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				InteraccionChatWhatsAppRepositorio _repInteraccionChatWhatsApp = new InteraccionChatWhatsAppRepositorio(_integraDBContext);
				var interaccion = _repInteraccionChatWhatsApp.GetBy(x => x.FechaInicio.Date == Fecha.Fecha.Date).ToList();
				List<InteraccionChatWhatsAppBO> listaInteraccionChatWhatsApp = new List<InteraccionChatWhatsAppBO>();
				if (interaccion.Count == 0)
				{
					var chats = _repInteraccionChatWhatsApp.ObtenerCalculoChatWhatsAppDiario(Fecha.Fecha);

					var agrupado = chats.GroupBy(x => x.Numero).Select(g => new DesagregadoWhatsAppDiarioDTO
					{
						Numero = g.Key,
						DetalleNumero = g.GroupBy(x => x.IdOportunidad).Select(x => new DesagregadoPorOportunidad
						{
							IdOportunidad = x.Key,
							DetalleOportunidad = x.Select(y => new ChatWhatsAppDesagregadoPorNumeroDiarioDTO
							{
								IdAlumno = y.IdAlumno,
								IdPersonal = y.IdPersonal,
								IdFaseOportunidad = y.IdFaseOportunidad,
								IdPais = y.IdPais,
								TipoOportunidad = y.TipoOportunidad,
								FechaCreacionOportunidad = y.FechaCreacionOportunidad,
								FechaMensaje = y.FechaMensaje,
								Remitente = y.Remitente
							}).ToList()
						}).ToList()
					}).ToList();
					foreach (var numeros in agrupado)
					{
						InteraccionChatWhatsAppBO interaccionChatWhatsAppBO = new InteraccionChatWhatsAppBO();
						interaccionChatWhatsAppBO.Numero = numeros.Numero;
						var remitente = "";
						DateTime fechaMensajeAnterior = DateTime.Now;
						DateTime fechaInicio = DateTime.Now;
						bool esPrimero = true;
						double suma = 0;
						int count = 0;
						int mensajesVisitante = 0;
						int? IdAlumno = null; // null
						int? idPais = null;
						int? idPersonal = null;
						int totalMensajesChat = 0;
						bool esAtendido = false;
						int countOpoSeg = 0;
						int countOpoGen = 0;

						bool primeroportunidad = true;
						foreach (var oportunidades in numeros.DetalleNumero)
						{
							var detalleOportunidad = oportunidades.DetalleOportunidad.OrderBy(x => x.FechaMensaje).ToList();
							if (primeroportunidad)
							{
								foreach (var item in detalleOportunidad)
								{
									if (item.Remitente.Equals("Visitante"))
									{
										mensajesVisitante++;
									}
									if (esPrimero)
									{
										fechaInicio = item.FechaMensaje;
										esPrimero = false;
									}
									else
									{
										if (item.Remitente.Equals("Asesor") && remitente.Equals("Visitante"))
										{
											TimeSpan diferencia = item.FechaMensaje.Subtract(fechaMensajeAnterior);
											suma += diferencia.TotalSeconds;
											count++;
										}
									}
									IdAlumno = item.IdAlumno;
									idPais = item.IdPais;
									idPersonal = item.IdPersonal;
									fechaMensajeAnterior = item.FechaMensaje;
									remitente = item.Remitente;
								}
								if (detalleOportunidad.Where(x => x.Remitente.Equals("Asesor")).ToList().Count > 0)
								{
									esAtendido = true;
								}
								totalMensajesChat += detalleOportunidad.Count;

								primeroportunidad = false;
							}


							countOpoGen += detalleOportunidad.Where(x => x.TipoOportunidad == 1).ToList().Count();
							countOpoSeg += detalleOportunidad.Where(x => x.TipoOportunidad == 2).ToList().Count();
						}
						if (count == 0)
						{
							count = 1;
						}
						interaccionChatWhatsAppBO.TieneOportunidadNueva = countOpoGen > 0 ? true : false;
						interaccionChatWhatsAppBO.TieneOportunidadSeguimiento = countOpoSeg > 0 ? true : false;
						interaccionChatWhatsAppBO.IdPais = idPais;
						interaccionChatWhatsAppBO.IdAlumno = IdAlumno;
						interaccionChatWhatsAppBO.IdPersonal = idPersonal == null ? 0 : idPersonal.Value;
						interaccionChatWhatsAppBO.EsAtendido = esAtendido;
						interaccionChatWhatsAppBO.FechaInicio = fechaInicio;
						interaccionChatWhatsAppBO.FechaFin = fechaMensajeAnterior;
						interaccionChatWhatsAppBO.TotalMensajesVisitante = mensajesVisitante;
						interaccionChatWhatsAppBO.TotalMensajesChat = totalMensajesChat;

						interaccionChatWhatsAppBO.PromedioRespuestaUsuario = Convert.ToDecimal(suma / count);
						interaccionChatWhatsAppBO.Estado = true;
						interaccionChatWhatsAppBO.UsuarioCreacion = "SYSTEM";
						interaccionChatWhatsAppBO.UsuarioModificacion = "SYSTEM";
						interaccionChatWhatsAppBO.FechaCreacion = DateTime.Now;
						interaccionChatWhatsAppBO.FechaModificacion = DateTime.Now;

						listaInteraccionChatWhatsApp.Add(interaccionChatWhatsAppBO);
					}
					_repInteraccionChatWhatsApp.Insert(listaInteraccionChatWhatsApp);
				}
				
				return Ok(listaInteraccionChatWhatsApp);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		[Route("[Action]")]
		[HttpPost]
		public ActionResult ReporteChatWhatsApp([FromBody]FiltrosChatWhatsAppReporteDTO data)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				InteraccionChatWhatsAppRepositorio _repInteraccionChatWhatsApp = new InteraccionChatWhatsAppRepositorio(_integraDBContext);
				var chats = _repInteraccionChatWhatsApp.obtenerReporteChatWhatsApp(data).OrderByDescending(x=>x.Fecha);
				IEnumerable<ReporteChatWhatsAppAgrupadoDTO> agrupado = null;
				if (data.Desglose == 1)
				{
					agrupado = chats.GroupBy(x => x.Pais)
					.Select(g => new ReporteChatWhatsAppAgrupadoDTO
					{
						Pais = g.Key,
						DetallePais = g.GroupBy(x => x.Fecha).Select(x => new ReporteChatWhatsAppAgrupadoDetallePaisDTO
						{
							Fecha = x.Key.ToString("yyyyMMdd"),
							DetalleFecha = x.Select(y => new ReporteChatWhatsAppAgrupadoDetalleFechaDTO
							{
								Asesor = y.Asesor,
								NumeroChats = y.NumeroChats,
								NumeroChatsAtendidoOportunidadGenerada = y.NumeroChatsAtendidoOportunidadGenerada,
								NumeroChatsAtendidoOportunidadSeguimiento = y.NumeroChatsAtendidoOportunidadSeguimiento,
								NumeroChatsAtendidoSinOportunidadSeguimiento = y.NumeroChatsAtendidoSinOportunidadSeguimiento,
								NumeroOportunidadesGeneradas = y.NumeroOportunidadesGeneradas,
								NumeroOportunidadesSeguimiento = y.NumeroOportunidadesSeguimiento,
								PalabrasVisitante = y.PalabrasVisitante,
								PromedioRespuestaUsuario = y.PromedioRespuestaUsuario,
								NumeroChatsAtendidos = y.NumeroChatsAtendidos,
								NumeroChatsNuevosSinOportunidad = y.NumeroChatsNuevosSinOportunidad

							}).ToList()
						}).ToList()
					});
				}
				if (data.Desglose == 2)
				{
					agrupado = chats.GroupBy(x => x.Pais)
					.Select(g => new ReporteChatWhatsAppAgrupadoDTO
					{
						Pais = g.Key,
						DetallePais = g.GroupBy(x => _repInteraccionChatWhatsApp.ObtenerNumeroSemana(x.Fecha)).Select(x => new ReporteChatWhatsAppAgrupadoDetallePaisDTO
						{
							Fecha = "Semana_" + x.Key,
							DetalleFecha = x.Select(y => new ReporteChatWhatsAppAgrupadoDetalleFechaDTO
							{
								Asesor = y.Asesor,
								NumeroChats = y.NumeroChats,
								NumeroChatsAtendidoOportunidadGenerada = y.NumeroChatsAtendidoOportunidadGenerada,
								NumeroChatsAtendidoOportunidadSeguimiento = y.NumeroChatsAtendidoOportunidadSeguimiento,
								NumeroChatsAtendidoSinOportunidadSeguimiento = y.NumeroChatsAtendidoSinOportunidadSeguimiento,
								NumeroOportunidadesGeneradas = y.NumeroOportunidadesGeneradas,
								NumeroOportunidadesSeguimiento = y.NumeroOportunidadesSeguimiento,
								PalabrasVisitante = y.PalabrasVisitante,
								PromedioRespuestaUsuario = y.PromedioRespuestaUsuario,
								NumeroChatsAtendidos = y.NumeroChatsAtendidos,
								NumeroChatsNuevosSinOportunidad = y.NumeroChatsNuevosSinOportunidad

							}).ToList()
						}).ToList()
					});
				}
				if (data.Desglose == 3)
				{
					agrupado = chats.GroupBy(x => x.Pais)
					.Select(g => new ReporteChatWhatsAppAgrupadoDTO
					{
						Pais = g.Key,
						DetallePais = g.GroupBy(x => x.Fecha.Month).Select(x => new ReporteChatWhatsAppAgrupadoDetallePaisDTO
						{
							Fecha = _repInteraccionChatWhatsApp.ObtenerNombreMes(x.Key),
							DetalleFecha = x.Select(y => new ReporteChatWhatsAppAgrupadoDetalleFechaDTO
							{
								Asesor = y.Asesor,
								NumeroChats = y.NumeroChats,
								NumeroChatsAtendidoOportunidadGenerada = y.NumeroChatsAtendidoOportunidadGenerada,
								NumeroChatsAtendidoOportunidadSeguimiento = y.NumeroChatsAtendidoOportunidadSeguimiento,
								NumeroChatsAtendidoSinOportunidadSeguimiento = y.NumeroChatsAtendidoSinOportunidadSeguimiento,
								NumeroOportunidadesGeneradas = y.NumeroOportunidadesGeneradas,
								NumeroOportunidadesSeguimiento = y.NumeroOportunidadesSeguimiento,
								PalabrasVisitante = y.PalabrasVisitante,
								PromedioRespuestaUsuario = y.PromedioRespuestaUsuario,
								NumeroChatsAtendidos = y.NumeroChatsAtendidos,
								NumeroChatsNuevosSinOportunidad = y.NumeroChatsNuevosSinOportunidad
							}).ToList()
						}).ToList()
					});
				}
				return Ok(agrupado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
