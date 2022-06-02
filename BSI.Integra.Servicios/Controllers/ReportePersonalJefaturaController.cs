using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReportePersonalJefaturaController
    /// Autor: Edgar Serruto
    /// Fecha: 12/17/2021
    /// <summary>
    /// Gestión de Reporte de Personal a Cargo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReportePersonalJefaturaController : ControllerBase
    {
		private readonly integraDBContext _integraDBContext;
		private readonly PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo;
		private readonly PersonalRepositorio _repPersonal;
		public ReportePersonalJefaturaController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
			_repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
			_repPersonal = new PersonalRepositorio(_integraDBContext);
		}

		/// TipoFuncion: POST
		/// Autor: Edgar Serruto
		/// Fecha: 12/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Información de combos para módulo
		/// </summary>
		/// <returns>List<FiltroIdNombreDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerCombos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var personalAreaTrabajo = _repPersonalAreaTrabajo.GetFiltroIdNombre().OrderBy(x => x.Nombre);
				return Ok(personalAreaTrabajo);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Edgar Serruto
		/// Fecha: 12/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Información de Personal Activo de Personal
		/// </summary>
		/// <returns>PersonalJefaturaIteradorDTO</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult GenerarReportePersonalActivo()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalBO personal = new PersonalBO();
				var personalEncargado = personal.ObtenerPersonalEncargadoJefatura();
				return Ok(personalEncargado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Edgar Serruto
		/// Fecha: 13/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Información de encargados Personal por Filtro
		/// </summary>
		/// <param name="Filtros">Filtros de búsqueda</param>
		/// <returns>Objeto Agrupado (bool,List<FiltroPersonalJefaturaFiltroDTO>,string) </returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult GenerarReporteTodoPersonal(FiltroPersonalJefaturaDTO Filtros)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalBO personal = new PersonalBO();
				string mensaje = string.Empty;
				var personalEncargado = personal.ObtenerReporteTodoPersonal(Filtros);
				mensaje = "Datos cargados correctamente";
				return Ok(new { Respuesta = true, Datos = personalEncargado, Mensaje = mensaje });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
