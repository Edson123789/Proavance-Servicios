using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/CentroCostoEstado")]
    [ApiController]
    public class CentroCostoEstadoOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltro()
        {
            try
            {
                RaCentroCostoEstadoRepositorio _repCentroCostoEstado = new RaCentroCostoEstadoRepositorio();
                return Ok(_repCentroCostoEstado.GetBy(x => x.Estado, x => new { x.Id, x.Nombre }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltroActivo()
        {
            try
            {
                RaCentroCostoEstadoRepositorio _repCentroCostoEstado = new RaCentroCostoEstadoRepositorio();
                return Ok(_repCentroCostoEstado.GetBy(x => x.Activo, x => new { x.Id, x.Nombre }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
