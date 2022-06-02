using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using static BSI.Integra.Servicios.Controllers.GastoFinancieroCronogramaController;
using static BSI.Integra.Servicios.Controllers.PanelControlMetaController;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CierreReporteResumenMontos")]
    public class CierreReporteResumenMontosController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public CierreReporteResumenMontosController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarCierreReporteResumenMontos([FromBody] FiltroCierreResumenMontosDTO FiltroCierreResumenMontos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {                
                CronogramaPagoDetalleFinalPorPeriodoRepositorio cronogramaPagoDetalleFinalPorPeriodoRepositorio = new CronogramaPagoDetalleFinalPorPeriodoRepositorio();
                var existeCierrePeriodo = cronogramaPagoDetalleFinalPorPeriodoRepositorio.ExisteIdPeriodo(FiltroCierreResumenMontos.IdPeriodo);
                string resultado;
                if (existeCierrePeriodo == false)
                {
                    var valor = cronogramaPagoDetalleFinalPorPeriodoRepositorio.GenerarCierreReporteResumenMontos(FiltroCierreResumenMontos);
                    
                    if (valor == false)
                    {
                        resultado = "Se guardo cierre correctamente";
                    }
                    else {
                        resultado = "ERROR: No se pudo guardar el cierre ";
                    }
                }
                else
                {
                    resultado = "Ya hay un cierre guardado con este periodo";
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
