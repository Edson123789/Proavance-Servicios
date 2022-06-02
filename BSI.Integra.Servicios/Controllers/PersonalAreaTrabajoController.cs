using System;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PersonalAreaTrabajo")]
    public class PersonalAreaTrabajoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo;
        public PersonalAreaTrabajoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                return Ok(_repPersonalAreaTrabajo.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
