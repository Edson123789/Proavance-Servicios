using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TipoEvaluacionTrabajo")]
    public class TipoEvaluacionTrabajoController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public TipoEvaluacionTrabajoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult listaTipoEvaluacionTrabajo()
        {
            try
            {
                TipoEvaluacionTrabajoRepositorio _tipoEvaluacionTrabajo = new TipoEvaluacionTrabajoRepositorio();
                var respuesta = _tipoEvaluacionTrabajo.listaTipoEvaluacionTrabajo();
                
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
