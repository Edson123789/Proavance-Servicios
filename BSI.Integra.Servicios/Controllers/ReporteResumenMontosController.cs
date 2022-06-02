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
    [Route("api/ReporteResumenMontos")]
    public class ReporteResumenMontosController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteResumenMontosController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosResumenMontos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModalidadCursoRepositorio repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);
                PeriodoRepositorio repPeriodo = new PeriodoRepositorio(_integraDBContext);
                PaisRepositorio repPais = new PaisRepositorio(_integraDBContext);

                CombosResumenMontosDTO comboResumenMontos = new CombosResumenMontosDTO();

                comboResumenMontos.ListaModalidades = repModalidadCurso.ObtenerModalidadCursoFiltro();
                comboResumenMontos.ListaPeriodo = repPeriodo.ObtenerPeriodosPendiente();
                comboResumenMontos.ListaPais = repPais.ObtenerPaisesResumenMontosCombo();

                return Ok(comboResumenMontos);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoPeriodoActual([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;
                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta3 = reporteCronograma.GenerarReporteResumenMontosTotalizadoPeriodoActual(RespuestaGeneral);
                var agrupado3 = (from p in listRpta3
                                 group p by p.Periodo into grupo
                                 select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoPeriodoActual = agrupado3.ToList();
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoPeriodoCierre([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta4 = reporteCronograma.GenerarReporteResumenMontosTotalizadoPeriodoCierre(RespuestaGeneral);
                var agrupado4 = (from p in listRpta4
                                 group p by p.Periodo into grupo
                                 select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoPeriodoCierre = agrupado4.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoPeru([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta5 = reporteCronograma.GenerarReporteResumenMontosTotalizadoPeru(RespuestaGeneral);
                var agrupado5 = (from p in listRpta5
                                 group p by p.Periodo into grupo
                                 select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();



                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();

                reporte.ReporteResumenMontosTotalizadoPeru = agrupado5.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoColombia([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta6 = reporteCronograma.GenerarReporteResumenMontosTotalizadoColombia(RespuestaGeneral);
                var agrupado6 = (from p in listRpta6
                                 group p by p.Periodo into grupo
                                 select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoColombia = agrupado6.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoBolivia([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;
                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta7 = reporteCronograma.GenerarReporteResumenMontosTotalizadoBolivia(RespuestaGeneral);
                var agrupado7 = (from p in listRpta7
                                 group p by p.Periodo into grupo
                                 select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();
                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoBolivia = agrupado7.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoOtrosPaises([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta8 = reporteCronograma.GenerarReporteResumenMontosTotalizadoOtrosPaises(RespuestaGeneral);
                var agrupado8 = (from p in listRpta8
                                 group p by p.Periodo into grupo
                                 select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoOtrosPaises = agrupado8.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadPresencialPeru([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta9 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadPresencialPeru(RespuestaGeneral);
                var agrupado9 = (from p in listRpta9
                                 group p by p.Periodo into grupo
                                 select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

              

                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();

                reporte.ReporteResumenMontosTotalizadoModalidadPresencialPeru = agrupado9.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadOnlinePeru([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta10 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadOnlinePeru(RespuestaGeneral);
                var agrupado10 = (from p in listRpta10
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();


                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadOnlinePeru = agrupado10.ToList();
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadAonlinePeru([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta11 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadAonlinePeru(RespuestaGeneral);
                var agrupado11 = (from p in listRpta11
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadAonlinePeru = agrupado11.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadPresencialColombia([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta12 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadPresencialColombia(RespuestaGeneral);
                var agrupado12 = (from p in listRpta12
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();


                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadPresencialColombia = agrupado12.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadOnlineColombia([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;
             
                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta13 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadOnlineColombia(RespuestaGeneral);
                var agrupado13 = (from p in listRpta13
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();


                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadOnlineColombia = agrupado13.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadAonlineColombia([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;
                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta14 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadAonlineColombia(RespuestaGeneral);
                var agrupado14 = (from p in listRpta14
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();


                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadAonlineColombia = agrupado14.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadPresencialBolivia([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta15 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadPresencialBolivia(RespuestaGeneral);
                var agrupado15 = (from p in listRpta15
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();


                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadPresencialBolivia = agrupado15.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadOnlineBolivia([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta16 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadOnlineBolivia(RespuestaGeneral);
                var agrupado16 = (from p in listRpta16
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();


                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadOnlineBolivia = agrupado16.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadAonlineBolivia([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta17 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadAonlineBolivia(RespuestaGeneral);
                var agrupado17 = (from p in listRpta17
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadAonlineBolivia = agrupado17.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadPresencialOtrosPaises([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta18 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadPresencialOtrosPaises(RespuestaGeneral);
                var agrupado18 = (from p in listRpta18
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();


                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadPresencialOtrosPaises = agrupado18.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadOnlineOtrosPaises([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta19 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadOnlineOtrosPaises(RespuestaGeneral);
                var agrupado19 = (from p in listRpta19
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();


                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadOnlineOtrosPaises = agrupado19.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadAonlineOtrosPaises([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta20 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadAonlineOtrosPaises(RespuestaGeneral);
                var agrupado20 = (from p in listRpta20
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();


                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadAonlineOtrosPaises = agrupado20.ToList();
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadInHousePeru([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta21 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadInHousePeru(RespuestaGeneral);
                var agrupado21 = (from p in listRpta21
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadInHousePeru = agrupado21.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadInHouseBolivia([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta22 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadInHouseBolivia(RespuestaGeneral);
                var agrupado22 = (from p in listRpta22
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();


                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadInHouseBolivia = agrupado22.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadInHouseColombia([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta23 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadInHouseColombia(RespuestaGeneral);
                var agrupado23 = (from p in listRpta23
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();


                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadInHouseColombia = agrupado23.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosTotalizadoModalidadInHouseOtrosPaises([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta24 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadInHouseOtrosPaises(RespuestaGeneral);
                var agrupado24 = (from p in listRpta24
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoModalidadInHouseOtrosPaises = agrupado24.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosVariacionMensual([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;
                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta25 = reporteCronograma.GenerarReporteResumenMontosVariacionMensual(RespuestaGeneral);
                var agrupado25 = (from p in listRpta25
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();


                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();

                reporte.ReporteResumenMontosVariacionMensual = agrupado25.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarResumenMontosNuevosMatriculados([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta26 = reporteCronograma.GenerarReporteResumenMontosNuevosMatriculados(RespuestaGeneral);
                var agrupado26 = (from p in listRpta26
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosNuevosMatriculados = agrupado26.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteResumenMontosGeneralTotalDTO RespuestaGeneral)
        {
            try
            {
                CronogramaPagoDetalleFinalBO reporteCronograma;
                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta3 = reporteCronograma.GenerarReporteResumenMontosTotalizadoPeriodoActual(RespuestaGeneral);
                var agrupado3 = (from p in listRpta3
                                 group p by p.Periodo into grupo
                                 select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta4 = reporteCronograma.GenerarReporteResumenMontosTotalizadoPeriodoCierre(RespuestaGeneral);
                var agrupado4 = (from p in listRpta4
                                 group p by p.Periodo into grupo
                                 select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta5 = reporteCronograma.GenerarReporteResumenMontosTotalizadoPeru(RespuestaGeneral);
                var agrupado5 = (from p in listRpta5
                                 group p by p.Periodo into grupo
                                 select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta6 = reporteCronograma.GenerarReporteResumenMontosTotalizadoColombia(RespuestaGeneral);
                var agrupado6 = (from p in listRpta6
                                 group p by p.Periodo into grupo
                                 select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta7 = reporteCronograma.GenerarReporteResumenMontosTotalizadoBolivia(RespuestaGeneral);
                var agrupado7 = (from p in listRpta7
                                 group p by p.Periodo into grupo
                                 select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta8 = reporteCronograma.GenerarReporteResumenMontosTotalizadoOtrosPaises(RespuestaGeneral);
                var agrupado8 = (from p in listRpta8
                                 group p by p.Periodo into grupo
                                 select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta9 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadPresencialPeru(RespuestaGeneral);
                var agrupado9 = (from p in listRpta9
                                 group p by p.Periodo into grupo
                                 select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta10 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadOnlinePeru(RespuestaGeneral);
                var agrupado10 = (from p in listRpta10
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta11 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadAonlinePeru(RespuestaGeneral);
                var agrupado11 = (from p in listRpta11
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();


                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta12 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadPresencialColombia(RespuestaGeneral);
                var agrupado12 = (from p in listRpta12
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta13 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadOnlineColombia(RespuestaGeneral);
                var agrupado13 = (from p in listRpta13
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta14 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadAonlineColombia(RespuestaGeneral);
                var agrupado14 = (from p in listRpta14
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta15 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadPresencialBolivia(RespuestaGeneral);
                var agrupado15 = (from p in listRpta15
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta16 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadOnlineBolivia(RespuestaGeneral);
                var agrupado16 = (from p in listRpta16
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta17 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadAonlineBolivia(RespuestaGeneral);
                var agrupado17 = (from p in listRpta17
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta18 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadPresencialOtrosPaises(RespuestaGeneral);
                var agrupado18 = (from p in listRpta18
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta19 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadOnlineOtrosPaises(RespuestaGeneral);
                var agrupado19 = (from p in listRpta19
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta20 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadAonlineOtrosPaises(RespuestaGeneral);
                var agrupado20 = (from p in listRpta20
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta21 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadInHousePeru(RespuestaGeneral);
                var agrupado21 = (from p in listRpta21
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta22 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadInHouseBolivia(RespuestaGeneral);
                var agrupado22 = (from p in listRpta22
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta23 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadInHouseColombia(RespuestaGeneral);
                var agrupado23 = (from p in listRpta23
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta24 = reporteCronograma.GenerarReporteResumenMontosTotalizadoModalidadInHouseOtrosPaises(RespuestaGeneral);
                var agrupado24 = (from p in listRpta24
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta25 = reporteCronograma.GenerarReporteResumenMontosVariacionMensual(RespuestaGeneral);
                var agrupado25 = (from p in listRpta25
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                reporteCronograma = new CronogramaPagoDetalleFinalBO();
                var listRpta26 = reporteCronograma.GenerarReporteResumenMontosNuevosMatriculados(RespuestaGeneral);
                var agrupado26 = (from p in listRpta26
                                  group p by p.Periodo into grupo
                                  select new ReporteResumenMontos { g = grupo.Key, l = grupo.ToList() }).ToList();

                ReporteResumenMontosCompuestoDTO reporte = new ReporteResumenMontosCompuestoDTO();
                reporte.ReporteResumenMontosTotalizadoPeriodoActual = agrupado3.ToList();
                reporte.ReporteResumenMontosTotalizadoPeriodoCierre = agrupado4.ToList();

                reporte.ReporteResumenMontosTotalizadoPeru = agrupado5.ToList();
                reporte.ReporteResumenMontosTotalizadoColombia = agrupado6.ToList();
                reporte.ReporteResumenMontosTotalizadoBolivia = agrupado7.ToList();
                reporte.ReporteResumenMontosTotalizadoOtrosPaises = agrupado8.ToList();

                reporte.ReporteResumenMontosTotalizadoModalidadPresencialPeru = agrupado9.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadOnlinePeru = agrupado10.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadAonlinePeru = agrupado11.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadPresencialColombia = agrupado12.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadOnlineColombia = agrupado13.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadAonlineColombia = agrupado14.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadPresencialBolivia = agrupado15.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadOnlineBolivia = agrupado16.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadAonlineBolivia = agrupado17.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadPresencialOtrosPaises = agrupado18.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadOnlineOtrosPaises = agrupado19.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadAonlineOtrosPaises = agrupado20.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadInHousePeru = agrupado21.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadInHouseBolivia = agrupado22.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadInHouseColombia = agrupado23.ToList();
                reporte.ReporteResumenMontosTotalizadoModalidadInHouseOtrosPaises = agrupado24.ToList();

                reporte.ReporteResumenMontosVariacionMensual = agrupado25.ToList();
                reporte.ReporteResumenMontosNuevosMatriculados = agrupado26.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteResumenMontosGeneral([FromBody] ReporteResumenMontosFiltroGeneralDTO FiltroPendiente)
        {
            try
            {
                PaisRepositorio repPais = new PaisRepositorio(_integraDBContext);
                ModalidadCursoRepositorio repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);

                CronogramaPagoDetalleFinalBO reporteCronogramaGeneral = new CronogramaPagoDetalleFinalBO();
                var respuestaGeneral = reporteCronogramaGeneral.GenerarReporteResumenMontosGeneral(FiltroPendiente);

                return Ok(respuestaGeneral);    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteResumenMontosCierre([FromBody] ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                PaisRepositorio repPais = new PaisRepositorio(_integraDBContext);
                ModalidadCursoRepositorio repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);

                CronogramaPagoDetalleFinalBO reporteCronogramaGeneral = new CronogramaPagoDetalleFinalBO();
                var respuestaGeneral = reporteCronogramaGeneral.GenerarReporteResumenMontosCierre(FiltroPendiente);

                return Ok(respuestaGeneral);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteResumenMontosCambios([FromBody] ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                PaisRepositorio repPais = new PaisRepositorio(_integraDBContext);
                ModalidadCursoRepositorio repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);

                CronogramaPagoDetalleFinalBO reporteCronogramaGeneral = new CronogramaPagoDetalleFinalBO();
                var respuestaGeneral = reporteCronogramaGeneral.GenerarReporteResumenMontosCambiosPais(FiltroPendiente);

                return Ok(respuestaGeneral);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteResumenMontosDiferencias([FromBody] ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                PaisRepositorio repPais = new PaisRepositorio(_integraDBContext);
                ModalidadCursoRepositorio repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);

                CronogramaPagoDetalleFinalBO reporteCronogramaGeneral = new CronogramaPagoDetalleFinalBO();
                var respuestaGeneral = reporteCronogramaGeneral.GenerarReporteResumenMontosDiferencias(FiltroPendiente);

                return Ok(respuestaGeneral);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}