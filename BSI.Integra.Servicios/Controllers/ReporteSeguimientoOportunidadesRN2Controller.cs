using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteSeguimientoOportunidadesRN2")]
    [ApiController]
    public class ReporteSeguimientoOportunidadesRN2Controller : ControllerBase
    {

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            try
            {
                PersonalRepositorio repPersonal = new PersonalRepositorio();

                var asesores = repPersonal.ObtenerPersonalVentas();

                return Ok(asesores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteSeguimientoOportunidadesRN2FiltrosDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reporte = new Reportes();
                List<ReporteSeguimientoOportunidadesRN2DTO> result = new List<ReporteSeguimientoOportunidadesRN2DTO>();
                result = reporte.ObtenerReporteSeguimientoOportunidadRN2(Filtros);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

      
    }
}
