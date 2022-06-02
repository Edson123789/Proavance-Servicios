using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriterioEvaluacionTipoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private CriterioEvaluacionTipoRepositorio _repocriterioTipo;

        public CriterioEvaluacionTipoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repocriterioTipo = new CriterioEvaluacionTipoRepositorio(integraDBContext);
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult Listado()
        {
            try
            {
                return Ok(_repocriterioTipo.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}