using System;
using System.Linq;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/LogFiltroSegmentoEjecutado")]
    public class LogFiltroSegmentoEjecutadoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public LogFiltroSegmentoEjecutadoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]/{IdFiltroSegmento}")]
        [HttpGet]
        public ActionResult ObtenerTodoPorIdFiltroSegmento(int IdFiltroSegmento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repLogFiltroSegmentoEjecutado = new LogFiltroSegmentoEjecutadoRepositorio(_integraDBContext);
                return Ok(_repLogFiltroSegmentoEjecutado.ObtenerPorIdFiltroSegmento(IdFiltroSegmento));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}