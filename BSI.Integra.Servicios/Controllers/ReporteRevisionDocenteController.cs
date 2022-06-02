using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	/// Controlador: ReporteRevisionDocente
	/// Autor: Edgar Serruto
	/// Fecha: 07/07/2021
	/// <summary>
	/// Gestión de Reporte de Revisión de Docentes en Foros y Proyectos
	/// </summary>
	[Route("api/[controller]")]
    [ApiController]
    public class ReporteRevisionDocenteController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
		private readonly SubAreaCapacitacionRepositorio _repSubAreaCapacitacion;
		private readonly AreaCapacitacionRepositorio _repAreaCapacitacion;
		private readonly PgeneralRepositorio _repPgeneral;
		private readonly ProveedorRepositorio _repProveedor;

		public ReporteRevisionDocenteController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
			_repAreaCapacitacion = new AreaCapacitacionRepositorio(_integraDBContext);
			_repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio(_integraDBContext);
			_repPgeneral = new PgeneralRepositorio(_integraDBContext);			
			_repProveedor = new ProveedorRepositorio(_integraDBContext);
		}

		/// TipoFuncion: POST
		/// Autor: Edgar Serruto.
		/// Fecha: 28/06/2021
		/// Versión: 1.0
		/// <summary>
		/// Genera Reporte de Accesos
		/// </summary>
		/// <returns>Objeto Agrupado</returns>
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
				var listaArea = _repAreaCapacitacion.GetBy(x => x.Estado == true).Select(x => new FiltroBasicoDTO { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre).ToList();
				var listaSubArea = _repSubAreaCapacitacion.ObtenerSubAreaAreaParaCombo().OrderBy(x => x.Nombre).ToList();
				var listaPGeneral = _repPgeneral.ObtenerSubAreaPGeneralParaCombo().OrderBy(x => x.Nombre).ToList();
				var listaProveedor = _repProveedor.ObtenerProveedorParaFiltro();

				return Ok( new { ListaArea = listaArea, ListaSubArea = listaSubArea, ListaPGeneral = listaPGeneral, ListaProveedor = listaProveedor });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Edgar Serruto.
		/// Fecha: 28/06/2021
		/// Versión: 1.0
		/// <summary>
		/// Genera Reporte de Accesos
		/// </summary>
		/// <param name="Filtro">Filtros de Búsqueda</param>
		/// <returns>Confirmación Bool y DTO de Tipo: List<RespuestaReporteRevisionDocenteDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult GenerarReporte(ReporteRevisionDocenteDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				string mensaje = string.Empty;				
				List<RespuestaReporteRevisionDocenteDTO> resultado = new List<RespuestaReporteRevisionDocenteDTO>();
				ProveedorBO proveedor = new ProveedorBO();
				var condicion = proveedor.GenerarCondicionReporteForo(Filtro);
				if (Filtro.IdCategoriaRevision == 1)
                {
					var listaGeneradaForo = _repProveedor.GenerarReporteRevisionForo(condicion);
					resultado.AddRange(listaGeneradaForo);
				}
				else if (Filtro.IdCategoriaRevision == 2)
                {
					var listaGeneradaProyecto = _repProveedor.GenerarReporteProyecto(condicion); 
					resultado.AddRange(listaGeneradaProyecto);
				}
                else
                {
					var listaGeneradaForo = _repProveedor.GenerarReporteRevisionForo(condicion);
					resultado.AddRange(listaGeneradaForo);
					var listaGeneradaProyecto = _repProveedor.GenerarReporteProyecto(condicion);
					resultado.AddRange(listaGeneradaProyecto);
				}
                if (resultado.Count > 0)
                {
					mensaje = "Se cargaron los datos correctamente";
					return Ok(new { Respuesta = true, Datos = resultado});
				}
                else
                {
					mensaje = "No se encontraron resultados con los filtros seleccionados";
					return Ok(new { Respuesta = false, Datos = resultado});
				}				
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
