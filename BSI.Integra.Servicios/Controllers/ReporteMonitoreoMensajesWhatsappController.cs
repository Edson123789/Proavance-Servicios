using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteMonitoreoMensajesWhatsapp")]
    public class ReporteMonitoreoMensajesWhatsappController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteMonitoreoMensajesWhatsappController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerReportePorConjuntoLista([FromBody] FiltroFechasReporteMonitoreoMensajesWhatsappDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                ReportesRepositorio _repReportesRepositorio = new ReportesRepositorio();
                return Ok(_repReportesRepositorio.ObtenerReporteMonitoreoMensajesWhatsappPorConjuntoLista(
                    Filtro.IdConjuntoLista,
                    Filtro.FechaInicio,
                    Filtro.FechaFin
                ));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerConjuntoListas([FromBody] FiltroFechasReporteMonitoreoMensajesWhatsappDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ConjuntoListaRepositorio _repConjuntoLista = new ConjuntoListaRepositorio();

                DateTime FechaActual = DateTime.Now;
                DateTime FechaInicio = new DateTime(Filtro.FechaInicio.Year, Filtro.FechaInicio.Month, Filtro.FechaInicio.Day, 0, 0, 0, 0);

                if (FechaInicio.Day      == FechaActual.Day
                    && FechaInicio.Month == FechaActual.Month
                    && FechaInicio.Year  == FechaActual.Year
                   )
                {
                    FechaInicio = FechaActual;
                }

                return Ok(_repConjuntoLista.ObtenerInformacionBasicaConjuntoLista(
                    FechaInicio,
                    new DateTime(Filtro.FechaFin.Year, Filtro.FechaFin.Month, Filtro.FechaFin.Day, 23, 59, 59, 999)
                ));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
