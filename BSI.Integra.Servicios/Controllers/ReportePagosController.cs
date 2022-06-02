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
    [Route("api/ReportePagos")]
    public class ReportePagosController : ControllerBase
    {
        
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReportePagoFiltroDTO Filtro)
        {
            try
            {
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                return Ok(reportesRepositorio.ObtenerReportePagos(Filtro.FechaInicio,Filtro.FechaFin));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}