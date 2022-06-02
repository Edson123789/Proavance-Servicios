using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/GrabacionesClasesOnline")]
	public class GrabacionesClasesOnlineController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;

		public GrabacionesClasesOnlineController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
		}


		/// TipoFuncion: GET
		/// Autor: Cesar Santillana
		/// Fecha: 30/07/2021
		/// Version: 2.0
		/// <summary>
		/// Función que trae data para llenar los combos de PGeneral ,Partner, AreaCapacitacion, DisponibilidadPrograma
		/// </summary>
		/// <returns>Retorma una lista</returns>
		[Route("[Action]")]
		[HttpGet]
		public ActionResult ObtenerDataCombos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				AreaCapacitacionRepositorio _repoAreaCapacitacion = new AreaCapacitacionRepositorio();
				PgeneralRepositorio repPGeneral = new PgeneralRepositorio();
				PartnerRepositorio repPartner = new PartnerRepositorio();
				GrabacionesClasesOnlineRepositorio repClasesOnline = new GrabacionesClasesOnlineRepositorio();
				var Detalles = new
				{
					ProgramaGeneral = repPGeneral.ObtenerProgramasFiltro(),
					Area = _repoAreaCapacitacion.ObtenerTodoFiltro(),
					Partner = repPartner.ObtenerPartner(),
					DisponibilidadPrograma = repClasesOnline.ObtenerDisponibilidadPrograma()
				};

				return Ok(Detalles);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: Post
		/// Autor: Cesar Santillana
		/// Fecha: 09/07/2021
		/// Version: 1.0
		/// <summary>
		/// Funcion que nos trae el nombre y el id de los programas especificos segun el IdPgeneral
		/// </summary>
		/// <param name="IdPGeneral">Id del Programa General</param>
		/// <returns>Retorma una lista del tipo PEspecificoFiltroPGeneralDTO </returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerDataCurso([FromBody] FiltroPGeneral IdPgeneral)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
				List<PEspecificoFiltroPGeneralDTO> listaPespecifico = new List<PEspecificoFiltroPGeneralDTO>();
				foreach (var p in IdPgeneral.Pgeneral)
				{
					var lista = _repPEspecifico.ObtenerProgramaEspecificoPorIdPGeneral(p);
					listaPespecifico.AddRange(lista);
				}
				return Ok(listaPespecifico);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// Tipo Función: POST
		/// Autor: Cesar Santillana
		/// Fecha: 02/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Generar la vista para la grilla secundaria (modal) de la interfaz Grabaciones clases Online
		/// </summary>
		/// <param name="filtroReporte">Filtro para el modal de Grabaciones clases Online</param>
		/// <returns>El reporte retorna una Lista List<SesionesClasesOnlineDTO></returns>
		/// 
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerSesiones([FromBody] SesionesFiltroDTO FiltroReporte)
		{
			try
			{
				GrabacionesClasesOnlineRepositorio repoGrabacionesClasesOnline = new GrabacionesClasesOnlineRepositorio();
				var lista = repoGrabacionesClasesOnline.GenerarVistaSesiones(FiltroReporte);
				return Ok(lista);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// Tipo Función: POST
		/// Autor: Cesar Santillana
		/// Fecha: 08/06/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza o crea la nueva data de sesiones para las grabaciones online
		/// </summary>
		/// <param name="filtroReporte">Filtro para la tabla de grabaciones online</param>
		/// <returns>El reporte retorna un bool</returns>

		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarSesiones([FromBody] SesionesClasesOnlineModificarFiltroDTO FiltroReporte)
		{
			try
			{
				GrabacionesClasesOnlineRepositorio repoGrabacionesClasesOnline = new GrabacionesClasesOnlineRepositorio();
				var consulta = repoGrabacionesClasesOnline.ActualizarSesiones(FiltroReporte);
				return Ok(consulta);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ModificarDisponibilidadProgramaDefecto([FromBody] DataDisponibilidadProgramaDefectoDTO FiltroReporte)
		{
			try
			{
				GrabacionesClasesOnlineRepositorio repoGrabacionesClasesOnline = new GrabacionesClasesOnlineRepositorio();
				var consulta = repoGrabacionesClasesOnline.ModificarDisponibilidadProgramaDefecto(FiltroReporte);
				return Ok(consulta);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// Tipo Función: POST
		/// Autor: Cesar Santillana
		/// Fecha: 02/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Generar la vista para la grilla principal de la interfaz Grabaciones clases Online
		/// </summary>
		/// <param name="filtroReporte">Filtro para la vista de Grabaciones clases Online</param>
		/// <returns>El reporte retorna una Lista List<GrabacionesClasesOnlineDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult GenerarVistaProgramasOnline([FromBody] GrabacionesClasesOnlineFiltroDTO FiltroReporte)
		{
			try
			{
				GrabacionesClasesOnlineRepositorio repoGrabacionesClasesOnline = new GrabacionesClasesOnlineRepositorio();
				var lista = repoGrabacionesClasesOnline.GenerarVistaProgramasOnline(FiltroReporte);
				return Ok(lista);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
