using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/RevertirFase")]
	[ApiController]
	public class RevertirFaseController : Controller
	{
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerOportunidades([FromBody]RevertirFaseFiltroGrillaDTO obj)
		{
			try
			{
				OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();

				var OportunidadManual = _repOportunidad.ObtenerPorFiltroRevertirFase(obj.filtro, obj.paginador);

				return Ok(OportunidadManual);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// Tipo Función: POST 
		/// Autor: Luis Huallpa - Jose Villena
		/// Fecha: 21/01/2021
		/// Versión: 2.0
		/// <summary>
		/// Revierte la oportunidad(cambio de fase anterior) de un alumno
		/// </summary>
		/// <returns></returns>

		[Route("[Action]")]
		[HttpPost]
		public ActionResult RevertirOportunidad([FromBody]RevertirFaseOportunidadFiltroDTO Obj)
		{
			try
			{
				integraDBContext contexto = new integraDBContext();
				OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(contexto);
				OportunidadLogRepositorio _repOportunidadLog = new OportunidadLogRepositorio(contexto);
				ActividadDetalleRepositorio _repActividadDetalle = new ActividadDetalleRepositorio(contexto);
				var oportunidadLog = _repOportunidadLog.RevertirFaseOportunidad(Obj.IdOportunidad, Obj.FechaProgramada,Obj.Usuario);
				if (oportunidadLog == null)
				{
					throw new Exception("Oportunidad no tiene un cambio de fase");
				}
				var actividadDetalle = _repActividadDetalle.GetBy(x => x.IdOportunidad == Obj.IdOportunidad);
				foreach(var item in actividadDetalle)
				{
					item.IdEstadoActividadDetalle = ValorEstatico.IdEstadoActividadDetalleEjecutado; //2
					item.UsuarioModificacion = Obj.Usuario;
					item.FechaModificacion = DateTime.Now;
					item.LlamadaActividad = null;

				}
				_repActividadDetalle.Update(actividadDetalle);



				int idEstadoOportunidad = 0;

				if (Obj.FechaProgramada != null)
				{
					idEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada; //6
				}
				else
				{
					idEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada; //2
				}
				int actividadCabecera = ObtenerIdActividadCabecera(oportunidadLog);

				ActividadDetalleBO actDetalle = new ActividadDetalleBO()
				{
					IdActividadCabecera = actividadCabecera,
					FechaProgramada = Obj.FechaProgramada,
					IdEstadoActividadDetalle = ValorEstatico.IdEstadoActividadDetalleNoEjecutado, //1
					Comentario = "Se ha  revertido el ultimo cambio de fase",
					IdAlumno = oportunidadLog.IdContacto,
					Actor = "A",
					IdOportunidad = oportunidadLog.IdOportunidad == null ? 0 : oportunidadLog.IdOportunidad.Value,
					IdClasificacionPersona = oportunidadLog.IdClasificacionPersona,
					Estado = true,
					UsuarioCreacion = Obj.Usuario,
					UsuarioModificacion = Obj.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				_repActividadDetalle.Insert(actDetalle);

				var oportunidad = _repOportunidad.GetBy(x => x.Id == Obj.IdOportunidad && x.Estado == true).FirstOrDefault();
				oportunidad.IdActividadDetalleUltima = actDetalle.Id;
				oportunidad.IdActividadCabeceraUltima = actividadCabecera;
				oportunidad.IdEstadoActividadDetalleUltimoEstado = ValorEstatico.IdEstadoActividadDetalleNoEjecutado; //1
				oportunidad.UltimoComentario = "Se ha revertido el ultimo cambio de fase";
				oportunidad.UltimaFechaProgramada = Obj.FechaProgramada;
				oportunidad.IdEstadoOportunidad = idEstadoOportunidad;
				oportunidad.IdPersonalAsignado = oportunidadLog.IdPersonalAsignado == null ? 0 : oportunidadLog.IdPersonalAsignado.Value;
				oportunidad.IdCentroCosto = oportunidadLog.IdCentroCosto == null ? 0 : oportunidadLog.IdCentroCosto.Value;
				oportunidad.IdFaseOportunidad = oportunidadLog.IdFaseOportunidad == null ? 0 : oportunidadLog.IdFaseOportunidad.Value;
				oportunidad.UsuarioModificacion = Obj.Usuario;
				oportunidad.FechaModificacion = DateTime.Now;

				_repOportunidad.Update(oportunidad);

				return Ok(oportunidad);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		private int ObtenerIdActividadCabecera(OportunidadLogRevertirDTO oportunidadLog)
		{
			int actividadCabecera = 0;
			if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadBNC /*2*/ && oportunidadLog.IdTipoDato == ValorEstatico.IdTipoDatoHistorico /*7*/)
			{
				actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirInteresProHis; //2
			}
			if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadBNC /*2*/ && oportunidadLog.IdTipoDato == ValorEstatico.IdTipoDatoLanzamiento /*8*/)
			{
				actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaContactoInicial; //5
			}
			if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIT) //13
			{
				actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirmacionRevisionInfo; //5
			}
			if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIP) //8
			{
				actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaCierre; //6
			}
			if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadPF) //22
			{
				actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirmacionRegistroPW; //7
			}
			if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIC) //12
			{
				actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirmacionPago; //8
			}
			if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIS) //5
			{
				actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirEnvioDoc; //10;
			}
			if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadRN) //7
			{
				actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirSeguimientoRN; //12
			}
			if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadRN2) //10
			{
				actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirSeguimientoRN; //2
			}
			if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadBNCRN2) //15
			{
				actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaSeguimientoRN2; //13
			}

			return actividadCabecera;
		}

		[HttpGet]
		[Route("[Action]/{IdOportunidad}")]
		public ActionResult ObtenerDetalleOportunidad(int IdOportunidad)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				OportunidadLogRepositorio _repOportunidadLog = new OportunidadLogRepositorio();
				var oportunidadLog = _repOportunidadLog.ObtenerDetalleOportunidad(IdOportunidad);
				return Ok(oportunidadLog);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
