using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs.Reportes;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteParticipacionExpositor")]
    [ApiController]
    public class ReporteParticipacionExpositorController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteParticipacionExpositorController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte()
        {
            PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
            var listado = pespecificoRepositorio.ObtenerHistorialParticipacionV3();
            return Ok(listado);
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteFiltrado([FromBody] ParticipacionExpositorFiltroDTO filtro)
        {
            PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
            var listado = pespecificoRepositorio.ObtenerHistorialParticipacionV3_Filtrado(filtro);
            return Ok(listado);
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteFiltradoPortal([FromBody] ParticipacionExpositorFiltroDTO filtro)
        {
            PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
            var listado = pespecificoRepositorio.ObtenerHistorialParticipacionV3Portal_Filtrado(filtro);
            return Ok(listado);
        }
    }
}
