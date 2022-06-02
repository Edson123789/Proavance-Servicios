using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/TipoCapacitacionMoodle")]
    [ApiController]
    public class TipoCapacitacionMoodleOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltro()
        {
            try
            {
                TipoCapacitacionMoodleRepositorio _repTipoCapacitacionMoodle = new TipoCapacitacionMoodleRepositorio();
                return Ok(_repTipoCapacitacionMoodle.GetBy(x => x.Estado, x => new { x.Id, x.Nombre }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
