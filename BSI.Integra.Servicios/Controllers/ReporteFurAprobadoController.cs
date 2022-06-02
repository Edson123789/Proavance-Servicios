using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteFurAprobado")]
    public class ReporteFurAprobadoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteFurAprobadoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerReporteFurAprobado(DateTime? FechaInicial, DateTime? FechaFinal, int? IdPersonalAreaTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportesRepositorio _repReportes = new ReportesRepositorio();
                var listado = _repReportes.ObtenerReporteFurAprobado(FechaInicial, FechaFinal, IdPersonalAreaTrabajo);
                if (listado != null)
                {

                }
                return Ok(listado);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}