using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteEncuestasIntermedias
    /// Autor: Cesar Santillana
    /// Fecha: 17/06/2021
    /// <summary>
    /// Contiene los controladores necesarios para los filtros y la consulta del reporte de encuestas intermedias
    /// </summary>

    [Route("api/ReporteEncuestasIntermedias")]
    public class ReporteEncuestasIntermedias : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public ReporteEncuestasIntermedias(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
        }

		/// TipoFuncion: GET
		/// Autor: Cesar Santillana
		/// Fecha: 17/06/2021
		/// Version: 1.0
		/// <summary>
		/// Función que trae data para llenar los combos Programa general y Docente 
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
				PgeneralRepositorio repPGeneral = new PgeneralRepositorio();
				ProveedorRepositorio repProveedor = new ProveedorRepositorio();
				var Detalles = new
				{
					ProgramaGeneral = repPGeneral.ObtenerProgramasFiltro(),
					Docente = repProveedor.ObtenerNombreProveedorParaHonorario(),
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
		/// Fecha: 17/06/2021
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
		/// Fecha: 17/06/2021
		/// Versión: 1.0
		/// <summary>
		/// Generar el reporte de encuestas intermedias
		/// </summary>
		/// <param name="filtroReporte">Filtro para el reporte de encuestas intermedias  </param>
		/// <returns>El reporte retorna una Lista List<ReporteEncuestasIntermediasDTO></returns>
		[Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteEncuestasIntermedias([FromBody] ReporteEncuestasIntermediasFiltroDTO FiltroReporte)
        {
            try
            {
                ReporteEncuestasIntermediasRepositorio _repoDescargaMaterial = new ReporteEncuestasIntermediasRepositorio();
                var lista = _repoDescargaMaterial.GenerarReporteEncuestasIntermedias(FiltroReporte);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
