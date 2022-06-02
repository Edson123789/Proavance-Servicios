using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CongelamientoPeriodoReporteFlujo")]
    public class CongelamientoPeriodoReporteFlujoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public CongelamientoPeriodoReporteFlujoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarCongelamiento([FromBody] List<FlujoCongelamientoPeriodoDTO> FlujoCongelamientoPeriodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CongelamientoPeriodoReporteFlujoRepositorio congelamientoPeriodoReporteFlujoRepositorio = new CongelamientoPeriodoReporteFlujoRepositorio();
                string resultado;
                var valor = congelamientoPeriodoReporteFlujoRepositorio.GenerarCongelamientoReporte(FlujoCongelamientoPeriodo);

                if (valor == false)
                {
                    resultado = "Se guardo correctamente ";
                }
                else
                {
                    resultado = "ERROR: No se pudo guardar";
                }

                return Ok(new { resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}