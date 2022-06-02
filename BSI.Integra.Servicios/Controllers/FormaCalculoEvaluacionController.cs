using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FormaCalculoEvaluacion")]
    [ApiController]
    public class FormaCalculoEvaluacionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly FormaCalculoEvaluacionRepositorio _repoEsquema;

        public FormaCalculoEvaluacionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repoEsquema = new FormaCalculoEvaluacionRepositorio(_integraDBContext);
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
                return Ok(_repoEsquema.ObtenerCombo());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
