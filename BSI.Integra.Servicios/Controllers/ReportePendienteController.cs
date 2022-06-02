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
    [Route("api/ReportePendiente")]
    public class ReportePendienteController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReportePendienteController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosPendientes(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModalidadCursoRepositorio repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);
                PeriodoRepositorio repPeriodo = new PeriodoRepositorio(_integraDBContext);
                PersonalRepositorio repPersonal = new PersonalRepositorio(_integraDBContext);

                CombosPendienteDTO comboPendiente = new CombosPendienteDTO();
                
                List<PersonalAsignadoDTO> asistentes = repPersonal.ObtenerPersonalAsignadoOperacionesUsuarioTotal(IdPersonal);
                
                //activos

                comboPendiente.ListaModalidades = repModalidadCurso.ObtenerModalidadCursoFiltro();
                comboPendiente.ListaPeriodo = repPeriodo.ObtenerPeriodosPendiente();

                //comboPendiente.ListaCoordinador = repPersonal.ObtenerCoordinadoresOperaciones();
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
        public ActionResult GenerarReporte([FromBody] ReportePendienteFiltroDTO FiltroPendiente)
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
                if (FiltroPendiente.Modalidad.Count() == 0)
                {
                    FiltroPendiente.Modalidad = repModalidadCurso.ObtenerModalidadCursoFiltro().Select(w => w.Id).ToList();
                }

                CronogramaPagoDetalleFinalBO reporteCronogramaGeneral = new CronogramaPagoDetalleFinalBO();
                var respuestaGeneral = reporteCronogramaGeneral.GenerarReportePendienteOperacionesGeneral(FiltroPendiente);

                CronogramaPagoDetalleFinalBO reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta = reporteCronograma.GenerarReportePendientePorPeriodoOperaciones(respuestaGeneral);
                var agrupado = (from p in listRpta
                                group p by p.Periodo into grupo
                                select new ReportePendiente { g = grupo.Key, l = grupo.ToList() });

                CronogramaPagoDetalleFinalBO reporteCronograma2 = new CronogramaPagoDetalleFinalBO();
                var listRpta2 = reporteCronograma2.GenerarReportePendienteIngresoVentasPorPeriodoOperacionesAnterior(respuestaGeneral);
                var agrupado2 = (from p in listRpta2
                                 group p by p.Periodo into grupo
                                 select new ReportePendiente { g = grupo.Key, l = grupo.ToList() }).ToList();

                CronogramaPagoDetalleFinalBO reporteCronograma3 = new CronogramaPagoDetalleFinalBO();
                var listRpta3 = reporteCronograma3.GenerarReportePendientePorCoordinadoraOperaciones(respuestaGeneral);
                var agrupado3 = (from p in listRpta3
                                 group p by p.Periodo into grupo
                                 select new ReportePendiente { g = grupo.Key, l = grupo.ToList() }).ToList();

                CronogramaPagoDetalleFinalBO reporteCronograma4 = new CronogramaPagoDetalleFinalBO();
                var listRpta4 = reporteCronograma4.GenerarReportePendientePeriodoyCoordinadorOperaciones(respuestaGeneral);
                var agrupado4 = (from p in listRpta4
                                 group p by p.Periodo into grupo
                                 select new ReportePendientePorCoordinador { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReportePendienteCompuestoDTO reporte = new ReportePendienteCompuestoDTO();
                reporte.ReportePendientePorPeriodo = agrupado.ToList();
                reporte.ReportePendienteIngresoVentasPorPeriodo = agrupado2.ToList();
                reporte.ReportePendientePorCoordinador = agrupado3.ToList();
                reporte.ReportePendientePeriodoyCoordinador = agrupado4.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteDetalles([FromBody] ReportePendienteFiltroDTO FiltroPendiente)
        {
            try
            {
                PersonalRepositorio repPersonal = new PersonalRepositorio(_integraDBContext);
                ModalidadCursoRepositorio repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);
                IntegraAspNetUsersRepositorio repIntegraUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);

                if (FiltroPendiente.Modalidad.Count() == 0)
                {
                    FiltroPendiente.Modalidad = repModalidadCurso.ObtenerModalidadCursoFiltro().Select(w => w.Id).ToList();
                }

                CronogramaPagoDetalleFinalBO reporteCronogramaGeneral = new CronogramaPagoDetalleFinalBO();
                var respuestaGeneral = reporteCronogramaGeneral.GenerarReportePendienteOperacionesDetalles(FiltroPendiente);
                ReportePendienteCompuestoDTO reporte = new ReportePendienteCompuestoDTO();
                List<PersonalAsignadoReportePendienteDTO> Coordinadoras = new List<PersonalAsignadoReportePendienteDTO>();
                foreach (var personal in FiltroPendiente.Coordinadora)
                {
                    if (personal != "0") {
                        var ResultadoCoordinadora = repPersonal.ObtenerDatosUsuariosReportePendiente(personal);
                        Coordinadoras.Add(ResultadoCoordinadora);
                        //IdSesiones.Add(sesion);
                    }
                }

                reporte.ListaCoordinadoras = Coordinadoras;
                reporte.ReportePendienteDetalles = respuestaGeneral.ToList();
                reporte.EstadoPersonal = FiltroPendiente.EstadoPersonal;

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}