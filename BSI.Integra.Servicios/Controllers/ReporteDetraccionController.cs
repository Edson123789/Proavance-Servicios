using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteDetraccion")]
    public class ReporteDetraccionController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public ReporteDetraccionController(integraDBContext integraDBContext) {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaSedesConComprobanteDetraccion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SedeRepositorio _repoSede = new SedeRepositorio(_integraDBContext);
                return Ok(_repoSede.ObtenerListaSedesConComprobanteDetraccion());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult VizualizarReporteDetraccion([FromBody] ReporteDetraccionFiltroDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DetraccionRepositorio _repoDetraccion = new DetraccionRepositorio(_integraDBContext);
                return Ok(_repoDetraccion.ObtenerReporteDetraccion(Filtro.IdsPaisEmision, Filtro.FechaEmision, Filtro.FechaVencimiento));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
