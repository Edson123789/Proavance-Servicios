using System;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaterialTipoEntrega")]
    public class MaterialTipoEntregaController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MaterialTipoEntregaRepositorio _repMaterialTipoEntrega;
        public MaterialTipoEntregaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repMaterialTipoEntrega = new MaterialTipoEntregaRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                return Ok(_repMaterialTipoEntrega.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}