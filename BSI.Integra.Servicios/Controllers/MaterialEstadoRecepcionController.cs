using System;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaterialEstadoRecepcion")]
    public class MaterialEstadoRecepcionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MaterialEstadoRecepcionRepositorio _repMaterialEstadoRecepcion;
        public MaterialEstadoRecepcionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repMaterialEstadoRecepcion = new MaterialEstadoRecepcionRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                return Ok(_repMaterialEstadoRecepcion.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}