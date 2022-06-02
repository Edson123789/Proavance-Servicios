using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteContactabilidadPeriodo")]
    [ApiController]
    public class ReporteContactabilidadPeriodoController : ControllerBase
    {

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                PeriodoRepositorio repPeriodo = new PeriodoRepositorio();

                ReporteContactabilidadPeriodoComboDTO result = new ReporteContactabilidadPeriodoComboDTO();

                result.Periodos = repPeriodo.ObtenerPeriodos();
                result.Asesores = _repPersonal.ObtenerAsesoresVentasOficial();

                return Ok(result);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteContactabilidadPeriodoFiltroDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reporte = new Reportes();
                List<ReporteContactabilidadPeriodoAgrupadoDTO> result = new List<ReporteContactabilidadPeriodoAgrupadoDTO>();
                result = reporte.ReporteContactabilidadPeriodo(Filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
