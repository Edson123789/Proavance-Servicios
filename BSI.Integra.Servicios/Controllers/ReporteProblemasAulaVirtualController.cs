using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Reportes.Comercial;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteProblemasAulaVirtual")]
    public class ReporteProblemasAulaVirtualController : Controller
    {
        private readonly integraDBContext _integraDBContex;
        public ReporteProblemasAulaVirtualController(integraDBContext integraDBContext)
        {
            _integraDBContex = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContex);
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(_integraDBContex);
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContex);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContex);

                var listaCombos = new
                {
                    MatriculaCabecera = _repMatriculaCabecera.ObtenerCodigoMatriculaParaCombo(),
                    coordinadores = _repPersonal.ObtenerCoordinadorasOperaciones(),
                    centroCosto = _repCentroCosto.ObtenerCentroCostoParaFiltro(),
                    tipoCategoriaError = _repAlumno.ObtenerTodoFiltroTipoCategoriaError()
                };

                return Ok(listaCombos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteProblemasAulaVirtualFiltroDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reporte = new Reportes();

                var result = reporte.ReporteProblemasAulaVirtual(Filtros).OrderByDescending(w=>w.Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
