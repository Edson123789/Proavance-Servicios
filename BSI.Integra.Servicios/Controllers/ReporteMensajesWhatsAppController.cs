using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Reportes.Comercial;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteMensajesWhatsApp")]
    public class ReporteMensajesWhatsAppController : ControllerBase
    {
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteMensajesWhatsAppFiltrosDTO Json)
        {
            try
            {
                Reportes reporte = new Reportes();
                var result = reporte.ObtenerReporteMensajesWhatsApp(Json);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteMensajesMasivos([FromBody] ReporteMensajesWhatsAppFiltrosDTO Json)
        {
            try
            {
                Reportes reporte = new Reportes();
                var result = reporte.GenerarReporteMensajesMasivos(Json);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// General reporte de mensajes WhatsApp Masivos por area
        /// </summary>
        /// <param name="Json">Objeto de clase ReporteMensajesWhatsAppPorAreaFiltrosDTO</param>
        /// <returns>Response 200 con lista de objetos de clase ReporteWhatsAppEnvioMasivoDTO, response 400</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarReporteMensajesMasivosPorArea([FromBody] ReporteMensajesWhatsAppPorAreaFiltrosDTO Json)
        {
            try
            {
                Reportes reporte = new Reportes();
                var resultado = reporte.GenerarReporteMensajesMasivosPorArea(Json);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// General reporte de mensajes WhatsApp Masivos por area
        /// </summary>
        /// <param name="Json">Objeto de clase ReporteWhatsAppMasivoFiltrosDTO</param>
        /// <returns>Response 200 con lista de objetos de clase ReporteWhatsAppEnvioMasivoDTO, response 400</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarReporteMensajesMasivosConjuntoLista([FromBody] ReporteWhatsAppMasivoFiltrosDTO Json)
        {
            try
            {
                Reportes reporte = new Reportes();
                var resultado = reporte.GenerarReporteMensajesMasivosConjuntoLista(Json);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
