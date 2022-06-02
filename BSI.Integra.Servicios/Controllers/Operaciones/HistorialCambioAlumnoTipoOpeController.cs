using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/HistorialCambioAlumnoTipo")]
    public class HistorialCambioAlumnoTipoOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltro()
        {
            try
            {
                RaHistorialCambioAlumnoTipoRepositorio _repRaHistorialCambioAlumnoTipo = new RaHistorialCambioAlumnoTipoRepositorio();
                return Ok(_repRaHistorialCambioAlumnoTipo.ObtenerFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}