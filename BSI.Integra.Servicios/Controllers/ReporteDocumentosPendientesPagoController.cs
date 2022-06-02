using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteDocumentosPendientesPago")]
    public class ReporteDocumentosPendientesPagoController : ControllerBase
    {
        
        [Route("[action]")]
        [HttpGet]
        public ActionResult GenerarReporte()
        {
            try
            {
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                return Ok(reportesRepositorio.ObtenerReporteDocumentosPendientesPago());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}