using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/InformacionProgramacionCronograma")]
    public class InformacionProgramacionCronogramaOpeController : Controller
    {

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerProgramacionCronograma(int? Periodo, Paginador Paginador, GridFilters Filter = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerTotalesPorModalidad(Paginador Paginador, GridFilters Filter = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //var listRpta = cronogramaSupervisionBLL.ObtenerTotales_PorModalidad(paginador.page, paginador.pageSize, paginador.skip, paginador.take, filter);
                //var counter = listRpta.Count();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}