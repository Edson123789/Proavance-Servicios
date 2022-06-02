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
    [Route("api/CronogramaPagoDetalleFinalPorDiaCierre")]
    public class CronogramaPagoDetalleFinalPorDiaCierreController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public CronogramaPagoDetalleFinalPorDiaCierreController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarCierreReportePorDia()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FiltroCierrePorDiaDTO FiltroCierrePorDia = new FiltroCierrePorDiaDTO();
                FiltroCierrePorDia.FechaCierre = DateTime.Now.AddDays(-1);
                FiltroCierrePorDia.Usuario = "sistemas";
                CronogramaPagoDetalleFinalPorDiaRepositorio cronogramaPagoDetalleFinalPorDiaRepositorio = new CronogramaPagoDetalleFinalPorDiaRepositorio();
                var existeCierrePeriodo = cronogramaPagoDetalleFinalPorDiaRepositorio.ExisteIdPeriodo(FiltroCierrePorDia.FechaCierre);
                string resultado;
                if (existeCierrePeriodo == false)
                {
                    var valor = cronogramaPagoDetalleFinalPorDiaRepositorio.GenerarCierrePorDia(FiltroCierrePorDia);
                    if (valor == false)
                    {                      
                        resultado = "Se guardo cierre correctamente";
                    }
                    else
                    {
                        resultado = "ERROR: No se pudo guardar el cierre ";
                    }
                }
                else
                {
                    resultado = "Ya hay un cierre guardado con este periodo";
                }

                // return Ok(new { resultado });
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult GenerarCierreDiarioCronograma(DateTime FechaCierre,string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FiltroCierrePorDiaDTO FiltroCierrePorDia = new FiltroCierrePorDiaDTO();
                FiltroCierrePorDia.FechaCierre = FechaCierre;
                FiltroCierrePorDia.Usuario = Usuario;
                CronogramaPagoDetalleFinalPorDiaRepositorio cronogramaPagoDetalleFinalPorDiaRepositorio = new CronogramaPagoDetalleFinalPorDiaRepositorio();
                var existeCierrePeriodo = cronogramaPagoDetalleFinalPorDiaRepositorio.ExisteIdPeriodo(FiltroCierrePorDia.FechaCierre);
                string resultado;
                if (existeCierrePeriodo == false)
                {
                    var valor = cronogramaPagoDetalleFinalPorDiaRepositorio.GenerarCierrePorDia(FiltroCierrePorDia);
                    if (valor == false)
                    {
                        resultado = "Se guardo cierre correctamente";
                        return Ok(true);
                    }
                    else
                    {
                        resultado = "ERROR: No se pudo guardar el cierre ";
                        return Ok(false);
                    }

                }
                else
                {
                    resultado = "Ya hay un cierre guardado con este periodo";
                    return Ok(false);
                }

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}
