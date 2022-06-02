using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FormaCalificacionEvaluacion")]
    [ApiController]
    public class FormaCalificacionEvaluacionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly FormaCalificacionEvaluacionRepositorio _repoForma;

        public FormaCalificacionEvaluacionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repoForma = new FormaCalificacionEvaluacionRepositorio(_integraDBContext);
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repoForma.ObtenerCombo());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
