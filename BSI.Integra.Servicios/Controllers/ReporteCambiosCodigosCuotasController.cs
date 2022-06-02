using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteCambiosCodigosCuotas")]
    public class ReporteCambiosCodigosCuotasController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteCambiosCodigosCuotasController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
            
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoFiltroAutoComplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
                    return Ok(_repCentroCosto.ObtenerTodoFiltroAutoComplete(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerAlumnoFiltroAutoComplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
                    return Ok(_repAlumno.ObtenerTodoFiltroAutoComplete(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerMatriculaFiltroAutoComplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                    return Ok(_repMatriculaCabecera.ObtenerCodigoMatriculaAutocompleto(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteCambios([FromBody] ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {
                
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                var reporteCambios = reportesRepositorio.ObtenerReporteCambios(FiltroCambios);
                
                return Ok(reporteCambios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteCodigos([FromBody] ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {

                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                var reporteCodigos = reportesRepositorio.ObtenerReporteCodigos(FiltroCambios);
               
                return Ok(reporteCodigos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteCuotas([FromBody] ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {

                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                var reporteCuotas = reportesRepositorio.ObtenerReporteCuotas(FiltroCambios);
                
                return Ok(reporteCuotas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteTraslados([FromBody] ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {

                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                var reporteCuotas = reportesRepositorio.ObtenerReporteTraslados(FiltroCambios);

                return Ok(reporteCuotas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]/{Fecha}/{Usuario}")]
        [HttpGet]
        /// Tipo Función: GET/
        /// Autor: Miguel
        /// Fecha: 07/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Congela los datos de la tabla fin.T_ReporteCambiosCongelado en base a una fecha fecha 
        /// </summary>
        /// <returns>Objeto</returns>
        public ActionResult GenerarCongelamientoReporteDeCambios(string Fecha, string Usuario)
        { 
            try
            {

                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                var congelamientoReporteCambios = reportesRepositorio.CongelarReporteDeCambios(Fecha, Usuario);

                return Ok(congelamientoReporteCambios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}