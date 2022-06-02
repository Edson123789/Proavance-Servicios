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
    [Route("api/ReportePagosDiaPeriodo")]
    public class ReportePagosDiaPeriodoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReportePagosDiaPeriodoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosPagosDiaPeriodo(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PeriodoRepositorio repPeriodo = new PeriodoRepositorio(_integraDBContext);
                PersonalRepositorio repPersonal = new PersonalRepositorio(_integraDBContext);

                CombosPagosDiaPeriodoDTO comboPendiente = new CombosPagosDiaPeriodoDTO();
                comboPendiente.ListaPeriodo = repPeriodo.ObtenerPeriodosPendiente();
                //comboPendiente.ListaCoordinador = repPersonal.ObtenerCoordinadoresOperaciones();

                List<PersonalAsignadoDTO> asistentes = repPersonal.ObtenerPersonalAsignadoOperacionesUsuarioTotal(IdPersonal);
                //activos
                comboPendiente.AsistentesActivos = asistentes.Where(w => w.Activo == true).ToList();
                //todos
                comboPendiente.AsistentesTotales = asistentes;
                //inactivo
                comboPendiente.AsistentesInactivos = asistentes.Where(w => w.Activo == false).ToList();


                return Ok(comboPendiente);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReportePagosDiaPeriodoFiltroDTO FiltroReportePagosDiaPeriodo)
        {
            try
            {
                PersonalRepositorio repPersonal = new PersonalRepositorio(_integraDBContext);
                ModalidadCursoRepositorio repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);
                IntegraAspNetUsersRepositorio repIntegraUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);

                //if (FiltroPendiente.Coordinadora.Count() == 0)
                //{
                //    var sincoordinador = "0";
                //    FiltroPendiente.Coordinadora = repPersonal.ObtenerCoordinadoresOperaciones().Select(w => w.Usuario).ToList();
                //    FiltroPendiente.Coordinadora.Add(sincoordinador);
                //}

                CronogramaPagoDetalleFinalBO reporteCronogramaGeneral = new CronogramaPagoDetalleFinalBO();
                var respuestaGeneral = reporteCronogramaGeneral.GenerarReportePagosDiaPeriodoGeneral(FiltroReportePagosDiaPeriodo);

                CronogramaPagoDetalleFinalBO reportePagosPorDia = new CronogramaPagoDetalleFinalBO();
                var reportePagosPorDiaFinal = reporteCronogramaGeneral.GenerarReportePagosPorDia(respuestaGeneral);


                CronogramaPagoDetalleFinalBO reportePagosPorPeriodo = new CronogramaPagoDetalleFinalBO();
                var reportePagosPorPeriodoFinal = reporteCronogramaGeneral.GenerarReportePagosPorPeriodo(respuestaGeneral);

                ReportePagosDiaPeriodoCompuestoDTO reporte = new ReportePagosDiaPeriodoCompuestoDTO();
                reporte.ReportePagosPorDia = reportePagosPorDiaFinal.ToList();
                reporte.ReportePagosPorPeriodo = reportePagosPorPeriodoFinal.ToList();

                return Ok(reporte);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
                
    }
}