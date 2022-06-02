using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReportePagoPorCuenta")]
    public class ReportePagoPorCuentaContableController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ReportePagoPorCuentaContableController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPagoPorCuentaContable(string Año, string Empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportesRepositorio _repReportes = new ReportesRepositorio();
                var listado = _repReportes.ObtenerReportePagoPorCuentaContable(Año, Empresa);
                if (listado != null)
                {

                }
                return Ok(listado);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFiltrosPagoPorCuentas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurRepositorio _repFurRep = new FurRepositorio();
                var listadoAños = _repFurRep.ObtenerFiltroAñoReporteCuentasContables();
                var listadoEmpresa= _repFurRep.ObtenerFiltroEmpresaReporteCuentasContables();
                return Ok(new { listadoAños, listadoEmpresa });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

    }
}