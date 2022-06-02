using System;

using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Persistencia.Models;

using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteFlujo")]
    public class ReporteFlujoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteFlujoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// Tipo Función: POST
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 14/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Funcion que trae los datos de los alumnos para el reporte de Flujos de Finanzas
        /// </summary>
        /// <param name="FiltroFlujos">ReportePagoFiltroDTO: Filtros de fechas</param>
        /// <returns>List<ReporteFlujoDTO></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReportePagoFiltroDTO FiltroFlujos)
        {
            try
            {
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                return Ok(reportesRepositorio.ObtenerReporteFlujos(FiltroFlujos.FechaInicio, FiltroFlujos.FechaFin));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}