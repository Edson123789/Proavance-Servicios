using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    /// Controlador: ReportePendiente
    /// Autor: Lisbeth Ortogorin Condori
    /// Fecha: 04/02/2021
    /// <summary>
    /// Contiene los controladores necesarios para los reporte porcentaje pendientes periodo y mes coordinadora
    /// </summary>

    [Route("api/ReportePendienteV2")] 
    public class ReportePendienteV2Controller : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReportePendienteV2Controller(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        /// Tipo Función: GET
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 03/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos de periodos y coordinadoras para los fltros del reporte
        /// </summary>
        /// <returns>ReportePendienteCompuestoDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosPendientes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio(_integraDBContext);
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);

                CombosPendienteDTO comboPendiente = new CombosPendienteDTO();

                comboPendiente.ListaModalidades = _repModalidadCurso.ObtenerModalidadCursoFiltro();
                comboPendiente.ListaPeriodo = _repPeriodo.ObtenerPeriodosPendiente();
                comboPendiente.ListaCoordinador = _repPersonal.ObtenerCoordinadoresOperaciones();

                return Ok(comboPendiente);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 21/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera el reporte de porcentaje pendiente mes y coordinadora
        /// </summary>
        /// <returns>ReportePendienteCompuestoDTO</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReportePendienteMesCoordinadorFiltroDTO FiltroPendiente)
        {
            try
            {
                ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);

                if (FiltroPendiente.Modalidad.Count() == 0)
                {
                    FiltroPendiente.Modalidad = _repModalidadCurso.ObtenerModalidadCursoFiltro().Select(w => w.Id).ToList();
                }

                ReportePendientePeriodoMesCoordinadoraBO reporteCronogramaGeneral = new ReportePendientePeriodoMesCoordinadoraBO();
                var respuestaGeneral = reporteCronogramaGeneral.GenerarReportePendienteMesCoordinadorOperacionesGeneral(FiltroPendiente);

                ReportePendientePeriodoMesCoordinadoraBO reporteCronograma = new ReportePendientePeriodoMesCoordinadoraBO();
                var listRpta = reporteCronograma.GenerarReportePendientePorPeriodoOperacionesMesCoordinador(respuestaGeneral);
                var agrupado = (from p in listRpta
                                group p by p.Periodo into grupo
                                select new ReportePendiente { g = grupo.Key, l = grupo.ToList() });

                ReportePendientePeriodoMesCoordinadoraBO reporteCronograma4 = new ReportePendientePeriodoMesCoordinadoraBO();
                var listRpta4 = reporteCronograma4.GenerarReportePendientePeriodoyCoordinadorOperacionesMesCoordinador(respuestaGeneral);
                var agrupado4 = (from p in listRpta4
                                 group p by p.Periodo into grupo
                                 select new ReportePendientePorCoordinador { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReportePendienteCompuestoDTO reporte = new ReportePendienteCompuestoDTO();
                reporte.ReportePendientePorPeriodo = agrupado.ToList();
                reporte.ReportePendientePeriodoyCoordinador = agrupado4.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 25/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera el reporte de porcentaje pendiente periodo
        /// </summary>
        /// <returns>ReportePendienteCompuestoDTO</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReportePeriodo([FromBody] ReportePendientePeriodoFiltroDTO FiltroPendiente)
        {
            try
            {
                ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);

                if (FiltroPendiente.Modalidad.Count() == 0)
                {
                    FiltroPendiente.Modalidad = _repModalidadCurso.ObtenerModalidadCursoFiltro().Select(w => w.Id).ToList();
                }
                
                ReportePendientePeriodoBO reporteCronogramaGeneral = new ReportePendientePeriodoBO();
                var respuestaGeneral = reporteCronogramaGeneral.GenerarReportePendientePorPeriodoOperacionesGeneral(FiltroPendiente);


                ReportePendientePeriodoBO reporteCronograma2 = new ReportePendientePeriodoBO();
                var listRpta2 = reporteCronograma2.GenerarReportePendienteIngresoVentasPorPeriodoOperaciones(respuestaGeneral);
                var agrupado2 = (from p in listRpta2
                                 group p by p.Periodo into grupo
                                 select new ReportePendiente { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReportePendientePeriodoBO reporteCronograma3 = new ReportePendientePeriodoBO();
                var listRpta3 = reporteCronograma3.GenerarReportePendientePorCoordinadoraOperacionesPorPeriodo(respuestaGeneral);
                var agrupado3 = (from p in listRpta3
                                 group p by p.Periodo into grupo
                                 select new ReportePendiente { g = grupo.Key, l = grupo.ToList() }).ToList();

               
                ReportePendienteCompuestoDTO reporte = new ReportePendienteCompuestoDTO();
                reporte.ReportePendienteIngresoVentasPorPeriodo = agrupado2.ToList();
                reporte.ReportePendientePorCoordinador = agrupado3.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}