using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Marketing/ReporteProgramasCriticos
    /// Autor: Joao Benavente - Gian Miranda
    /// Fecha: 26/04/2021
    /// <summary>
    /// Configura las actividades automaticas de la interfaz de los reportes de programas criticos
    /// </summary>
    [Route("api/ReporteProgramasCriticos")]
    public class ReporteProgramasCriticosController : ControllerBase
    {
        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista para un combo y seleccionar los grupos de los grupos de filtro de programas criticos
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase FiltroIdNombreDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboGrupos()
        {
            try
            {
                GrupoFiltroProgramaCriticoRepositorio grupoFiltroProgramaCriticoRepositorio = new GrupoFiltroProgramaCriticoRepositorio();
                return Ok(grupoFiltroProgramaCriticoRepositorio.ObtenerFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el periodo actual para el reporte de asignacion diaria
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase FiltroIdNombreDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboUltimoPeriodo()
        {
            try
            {
                PeriodoRepositorio periodoRepositorio = new PeriodoRepositorio();

                return Ok(periodoRepositorio.ObtenerUltimoPeriodo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista para llenar los combos de areas de capacitacion
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase FiltroDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboAreas()
        {
            try
            {
                AreaCapacitacionRepositorio repAreaCapacitacion = new AreaCapacitacionRepositorio();
                var result = repAreaCapacitacion.ObtenerTodoFiltro().Select(o => new { id = o.Id, name = o.Nombre }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista para llenar los combos de subareas de capacitacion
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase FiltroDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboSubAreas()
        {
            try
            {
                SubAreaCapacitacionRepositorio repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio();
                var result = repSubAreaCapacitacion.ObtenerTodoFiltro().Select(o => new { id = o.Id, name = o.Nombre, area = o.IdAreaCapacitacion }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera el reporte de indicadores de ventas
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase ReporteProgramasCriticosDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteProgramasCriticosFiltroDTO Filtros)
        {
            try
            {
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                return Ok(reportesRepositorio.ObtenerReporteProgramasCriticos(Filtros));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera el reporte de asignacion diaria
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase ReporteAsignacionDiariaDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteAsignacion([FromBody] ReporteProgramasCriticosFiltroDTO Filtros)
        {
            try
            {
                var _repReportes = new ReportesRepositorio();

                return Ok(_repReportes.ObtenerReporteProgramasCriticosAsignacion(Filtros));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}