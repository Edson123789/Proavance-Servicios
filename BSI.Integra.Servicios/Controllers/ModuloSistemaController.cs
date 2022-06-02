using System;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ModuloSistema")]
    public class ModuloSistemaController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly ModuloSistemaRepositorio _repModuloSistema;
        public ModuloSistemaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repModuloSistema = new ModuloSistemaRepositorio(_integraDBContext);
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                return Ok(_repModuloSistema.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
