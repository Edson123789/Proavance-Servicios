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
    [Route("api/ReporteDevoluciones")]
    public class ReporteDevolucionesController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteDevolucionesController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEstadoPagoMatricula()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EstadoPagoMatriculaRepositorio repEstadoPagoMatricula = new EstadoPagoMatriculaRepositorio(_integraDBContext);   
                return Ok(repEstadoPagoMatricula.ObtenerEstadoPagoMatriculaDevoluciones());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
        public ActionResult GenerarReporte([FromBody] ReporteDevolucionesFiltroDTO FiltroDevoluciones)
        {
            try
            {
                EstadoPagoMatriculaRepositorio repEstadoPagoMatricula = new EstadoPagoMatriculaRepositorio(_integraDBContext);
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                var EstadoPagoMatricula = FiltroDevoluciones.IdEstadoPagoMatricula;

                var ConMontos = true;
                if (FiltroDevoluciones.FechaInicioCronograma == null)
                {
                    ConMontos = false;
                }
                if (FiltroDevoluciones.FechaFinCronograma == null)
                {
                    ConMontos = false;
                }
                if (FiltroDevoluciones.IdEstadoPagoMatricula == null)
                {
                    FiltroDevoluciones.IdEstadoPagoMatricula = repEstadoPagoMatricula.ObtenerEstadoPagoMatriculaDevoluciones().Select(w => w.Id).ToList();
                }
                var reporteControlDocumentos = reportesRepositorio.ObtenerReporteDevoluciones(FiltroDevoluciones);

                if (EstadoPagoMatricula != null)
                {
                    reporteControlDocumentos = (from r in reporteControlDocumentos
                                    where r.TipoRetiro.Trim().ToUpper() == (FiltroDevoluciones.IdEstadoPagoMatricula[0] == 4 ? "CONDEVOLUCION" : "SINDEVOLUCION")
                    select r).ToList();
                    
                }

                var agrupado = (from p in reporteControlDocumentos
                                group p by p.PeriodoPorFechaVencimiento into grupo
                                select new ReporteDevolucion { g = grupo.Key, l = grupo.ToList() });

                ReporteDevolucionesCompuestoDTO reporte = new ReporteDevolucionesCompuestoDTO();
                reporte.ReporteDevolucionAgrupado = agrupado.ToList(); 
                reporte.Cronograma = ConMontos;
                reporte.ReporteDevoluciones = reporteControlDocumentos;

                return Ok(reporte);
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
        /// Fecha: 14/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Congela los datos de la tabla T_ReporteDevolucionCongelado  base a una fecha fecha 
        /// </summary>
        /// <returns>Objeto</returns>
        public ActionResult GenerarCongelamientoReporteDeDevoluciones(string Fecha, string Usuario)
        {
            try
            {

                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                var congelamientoReporteCambios = reportesRepositorio.CongelarReporteDeDevoluciones(Fecha, Usuario);

                return Ok(congelamientoReporteCambios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}