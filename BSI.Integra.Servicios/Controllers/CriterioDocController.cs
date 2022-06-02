using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CriterioDoc")]
    public class CriterioDocController : Controller
    {

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoSeleccionar()
        {
            try
            {
                CriterioDocRepositorio _repCriterioDoc = new CriterioDocRepositorio();
                return Ok(_repCriterioDoc.ObtenerTodoSeleccionar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
