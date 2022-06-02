using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteIndicadoresCampaniasMailing
    /// <summary>
    /// Autor: Gian Miranda
    /// Fecha: 20/07/2021
    /// <summary>
    /// Gestión de Reporte de Indicadores de CampaniasMailing
    /// </summary>
    [Route("api/ReporteIndicadoresCampaniasMailing")]
    public class ReporteIndicadoresCampaniasMailingController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PrioridadMailChimpListaBO PrioridadMailChimpLista;
        private readonly CampaniaMailingBO CampaniaMailing;
        private readonly PrioridadMailChimpListaCorreoRepositorio _repPrioridadMailChimpListaCorreo;
        private readonly IntegraAspNetUsersRepositorio _repIntegraAspNetUsers;

        public ReporteIndicadoresCampaniasMailingController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            PrioridadMailChimpLista = new PrioridadMailChimpListaBO(_integraDBContext);
            CampaniaMailing = new CampaniaMailingBO(_integraDBContext);
            _repPrioridadMailChimpListaCorreo = new PrioridadMailChimpListaCorreoRepositorio(_integraDBContext);
            _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarInformacionCompletaMailchimpPorIntervaloFecha([FromBody] InformacionCompletaMailchimpIntervaloFechaDTO FechaFiltroDescarga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var fechaInicioProceso = DateTime.Now;
                var cadenaFechaInicio = FechaFiltroDescarga.FechaInicio.ToString("yyyy-MM-dd");
                var cadenaFechaFin = FechaFiltroDescarga.FechaFin.ToString("yyyy-MM-dd");

                if (!_repIntegraAspNetUsers.ExistePorNombreUsuario(FechaFiltroDescarga.Usuario))
                {
                    return BadRequest("El usuario no existe");
                }

                var usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(FechaFiltroDescarga.Usuario);

                var correosCopia = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };

                // Campania
                var resultadoCampania = PrioridadMailChimpLista.DescargarCampaniaMailchimpPorIntervaloFecha(FechaFiltroDescarga.FechaInicio, FechaFiltroDescarga.FechaFin, FechaFiltroDescarga.Usuario);

                // Lista
                var resultadoLista = PrioridadMailChimpLista.DescargarListaMailchimpPorIntervaloFecha(FechaFiltroDescarga.FechaInicio, FechaFiltroDescarga.FechaFin, true, FechaFiltroDescarga.Usuario);

                var mailservicePersonalizado = new TMK_MailServiceImpl();
                var contenidoMailing = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = $"Actualización de datos Reporte de la fecha {cadenaFechaInicio} - {cadenaFechaFin}",
                    Message = $"Finalizo la actualizacion.<br>Campanias erroneas: {JsonConvert.SerializeObject(resultadoCampania)}<br>Listas erroneas: {JsonConvert.SerializeObject(resultadoLista)}",
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };

                mailservicePersonalizado.SetData(contenidoMailing);
                mailservicePersonalizado.SendMessageTask();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 18/02/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la lista completa de campaña Mailchimp por intervalo de fechas
        /// </summary>
        /// <param name="FechaFiltroDescarga">Objeto de clase InformacionCompletaMailchimpIntervaloFechaDTO</param>
        /// <returns>Response 200 con booleano, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarCampaniaCompletaMailchimpPorIntervaloFecha([FromBody] InformacionCompletaMailchimpIntervaloFechaDTO FechaFiltroDescarga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var fechaInicioProceso = DateTime.Now;
                var cadenaFechaInicio = FechaFiltroDescarga.FechaInicio.ToString("yyyy-MM-dd");
                var cadenaFechaFin = FechaFiltroDescarga.FechaFin.ToString("yyyy-MM-dd");

                if (!_repIntegraAspNetUsers.ExistePorNombreUsuario(FechaFiltroDescarga.Usuario))
                {
                    return BadRequest("El usuario no existe");
                }

                var usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(FechaFiltroDescarga.Usuario);

                var correosCopia = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };

                // Campania
                var resultadoCampania = PrioridadMailChimpLista.DescargarCampaniaMailchimpPorIntervaloFecha(FechaFiltroDescarga.FechaInicio, FechaFiltroDescarga.FechaFin, FechaFiltroDescarga.Usuario);

                var mailservicePersonalizado = new TMK_MailServiceImpl();
                var contenidoMailing = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = $"Actualización de datos Reporte de la fecha {cadenaFechaInicio} - {cadenaFechaFin}",
                    Message = $"Finalizo la actualizacion.<br>Campanias erroneas: {JsonConvert.SerializeObject(resultadoCampania)}",
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };

                mailservicePersonalizado.SetData(contenidoMailing);
                mailservicePersonalizado.SendMessageTask();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 18/02/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la lista completa de campaña Mailchimp por intervalo de fechas
        /// </summary>
        /// <param name="FechaFiltroDescarga">Objeto de clase InformacionCompletaMailchimpIntervaloFechaDTO</param>
        /// <returns>Response 200 con booleano, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarListaCompletaMailchimpPorIntervaloFecha([FromBody] InformacionCompletaMailchimpIntervaloFechaDTO FechaFiltroDescarga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var fechaInicioProceso = DateTime.Now;
                var cadenaFechaInicio = FechaFiltroDescarga.FechaInicio.ToString("yyyy-MM-dd");
                var cadenaFechaFin = FechaFiltroDescarga.FechaFin.ToString("yyyy-MM-dd");

                if (!_repIntegraAspNetUsers.ExistePorNombreUsuario(FechaFiltroDescarga.Usuario))
                {
                    return BadRequest("El usuario no existe");
                }

                var usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(FechaFiltroDescarga.Usuario);

                var correosCopia = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };

                // Lista
                var resultadoLista = PrioridadMailChimpLista.DescargarListaMailchimpPorIntervaloFecha(FechaFiltroDescarga.FechaInicio, FechaFiltroDescarga.FechaFin, false, FechaFiltroDescarga.Usuario);

                var mailservicePersonalizado = new TMK_MailServiceImpl();
                var contenidoMailing = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = $"Actualización de datos Reporte de la fecha {cadenaFechaInicio} - {cadenaFechaFin}",
                    Message = $"Finalizo la actualizacion.Listas erroneas: {JsonConvert.SerializeObject(resultadoLista)}",
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };

                mailservicePersonalizado.SetData(contenidoMailing);
                mailservicePersonalizado.SendMessageTask();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{Usuario}")]
        [HttpGet]
        public ActionResult ActualizarMiembroReboteDesuscrito(string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var mailchimpController = new MailChimpController();

                ActualizarMiembroCompletaMailchimpPorProcedimientoAlmacenado(Usuario);

                mailchimpController.RegularizarDesuscritosProcedimientoAlmacenado();

                return Ok(true);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{Usuario}")]
        [HttpGet]
        public ActionResult ActualizarMiembroCompletaMailchimpPorProcedimientoAlmacenado(string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var fechaInicioProceso = DateTime.Now;

                if (!_repIntegraAspNetUsers.ExistePorNombreUsuario(Usuario))
                {
                    return BadRequest("El usuario no existe");
                }

                var usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(Usuario);

                var correosCopia = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };

                var resultadoMiembro = new List<string>();

                // Lista
                var listaIdMailchimpParaMiembro = PrioridadMailChimpLista.ObtenerListaMiembroPorProcedimiento();

                foreach (var listaMailchimp in listaIdMailchimpParaMiembro)
                {
                    resultadoMiembro.AddRange(PrioridadMailChimpLista.DescargarMiembroMailchimpPorListaMailchimpId(listaMailchimp, Usuario));
                }

                var mailservicePersonalizado = new TMK_MailServiceImpl();
                var contenidoMailing = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = $"Actualización de datos Reporte de miembros Mailchimp",
                    Message = $"Finalizo la actualizacion.Listas erroneas: {JsonConvert.SerializeObject(resultadoMiembro)}",
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };

                mailservicePersonalizado.SetData(contenidoMailing);
                mailservicePersonalizado.SendMessageTask();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult RegularizarActualizarMetricaCampaniaReporteMailchimp([FromBody] InformacionCompletaMailchimpIntervaloFechaDTO FechaFiltroDescarga)
        {
            // Se recomienda solo barrer con la ultima semana a ultimo mes
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var fechaInicio = DateTime.Now;
                var fechaInicioConsulta = FechaFiltroDescarga.FechaInicio.ToString("yyyy-MM-dd");
                string fechaFinConsulta = FechaFiltroDescarga.FechaInicio.ToString("yyyy-MM-dd");

                bool falloConsultaListaMailChimp;

                if (!_repIntegraAspNetUsers.ExistePorNombreUsuario(FechaFiltroDescarga.Usuario))
                {
                    return BadRequest("El usuario no existe");
                }

                string usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(FechaFiltroDescarga.Usuario);

                List<string> correosCopia = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };

                #region Actualizacion de Campanias y Listas creadas en Mailchimp y combinacion
                ActualizarCampaniaCompletaMailchimpPorIntervaloFecha(FechaFiltroDescarga);

                try
                {
                    // Normalmente falla porque la consulta demoro demasiado tiempo en Mailchimp
                    ActualizarListaCompletaMailchimpPorIntervaloFecha(FechaFiltroDescarga);
                }
                catch (Exception e)
                {
                    falloConsultaListaMailChimp = true;
                }

                _repPrioridadMailChimpListaCorreo.RegularizarIdMailchimpFaltantePorActualizacion(FechaFiltroDescarga.FechaInicio.Date, FechaFiltroDescarga.FechaFin.Date);
                #endregion

                #region Actualizacion metrica
                ActualizarMetricaIndicadoresCampaniasMailingPorIntervaloFecha(new ReporteIndicadoresMailingFechaDTO { FechaConsulta = FechaFiltroDescarga.FechaFin, MapeoCompleto = false, Usuario = FechaFiltroDescarga.Usuario });
                #endregion

                var fechaFin = DateTime.Now;

                var mailservicePersonalizado = new TMK_MailServiceImpl();
                var contenidoMailing = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = $"Actualización de datos Reporte de la fecha {fechaInicioConsulta} - {fechaFinConsulta} MailChimp",
                    Message = CampaniaMailing.EstructurarMensajeRegularizacionMailChimp(fechaInicioConsulta, fechaFinConsulta, fechaInicio, fechaFin),
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };

                mailservicePersonalizado.SetData(contenidoMailing);
                mailservicePersonalizado.SendMessageTask();

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 05/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la metrica de los indicados de campania mailing por intervalo por fecha
        /// </summary>
        /// <param name="FechaFiltroDescarga">Objeto de clase ReporteIndicadoresMailingFechaDTO</param>
        /// <returns>Response 200 con booleano, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarMetricaIndicadoresCampaniasMailingPorIntervaloFecha([FromBody] ReporteIndicadoresMailingFechaDTO FechaFiltroDescarga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DateTime fechaInicioProceso = DateTime.Now;
                string cadenaFechaConsulta = FechaFiltroDescarga.FechaConsulta.ToString("yyyy-MM-dd");                

                if (!_repIntegraAspNetUsers.ExistePorNombreUsuario(FechaFiltroDescarga.Usuario))
                {
                    return BadRequest("El usuario no existe");
                }

                string usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(FechaFiltroDescarga.Usuario);

                List<string> correosCopia = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };

                DateTime fechaFin = FechaFiltroDescarga.FechaConsulta;
                DateTime fechaInicio = fechaFin.AddDays(-180)/*Tiempo maximo que Mailchimp almacena los datos*/;

                var resultado = PrioridadMailChimpLista.DescargarIndicadorMailChimpPorIntervaloFecha(fechaInicio, fechaFin, FechaFiltroDescarga.MapeoCompleto);

                string fechaFinConsulta = DateTime.Now.ToString("yyyy-MM-dd");

                TMK_MailServiceImpl mailservicePersonalizado = new TMK_MailServiceImpl();
                TMKMailDataDTO contenidoMailing = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = $"Actualización de datos Reporte de la fecha {cadenaFechaConsulta} MailChimp - Barrido Completo: {FechaFiltroDescarga.MapeoCompleto}",
                    Message = CampaniaMailing.EstructurarMensajeRegularizacionMailChimp(cadenaFechaConsulta, cadenaFechaConsulta, fechaInicio, fechaFin),
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };

                mailservicePersonalizado.SetData(contenidoMailing);
                mailservicePersonalizado.SendMessageTask();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 05/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de metrica general de mailchimp
        /// </summary>
        /// <param name="FiltroFechaReporteCampaniaMailing">Objeto de clase FiltroReporteMailingMetricaDTO</param>
        /// <returns>Response 200 con lista de objetos de clase ReporteCampaniaMailchimpMetricaDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerReporteIndicadoresCampaniasMailing([FromBody] FiltroReporteMailingMetricaDTO FiltroFechaReporteCampaniaMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<ReporteCampaniaMailchimpMetricaDTO> listaReporteMailingMetrica = CampaniaMailing.EstructurarReporteCampaniaMailChimpGeneral(FiltroFechaReporteCampaniaMailing.FechaInicio, FiltroFechaReporteCampaniaMailing.FechaFin);

                return Ok(listaReporteMailingMetrica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 05/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de metrica general de mailchimp para exportacion
        /// </summary>
        /// <param name="FiltroFechaReporteCampaniaMailing">Objeto de clase FiltroReporteMailingMetricaDTO</param>
        /// <returns>Response 200 con lista de objetos de clase ReporteCampaniaMailchimpMetricaDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerReporteIndicadoresCampaniasMailingExportacion([FromBody] FiltroReporteMailingMetricaDTO FiltroFechaReporteCampaniaMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<ReporteCampaniaMailchimpMetricaDTO> listaReporteMailingMetrica = CampaniaMailing.EstructurarReporteCampaniaMailChimpGeneralExportacion(FiltroFechaReporteCampaniaMailing.FechaInicio, FiltroFechaReporteCampaniaMailing.FechaFin);

                return Ok(listaReporteMailingMetrica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 03/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de la campania mailing
        /// </summary>
        /// <param name="FiltroFechaReporteDetalle">Objeto de clase FiltroReporteMailingMetricaDetalleDTO</param>
        /// <returns>Response 200 con lista de objetos de clase ReporteCampaniaMailchimpMetricaRegistrosDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerDetalleFilaCampaniaMailing([FromBody] FiltroReporteMailingMetricaDetalleDTO FiltroFechaReporteDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<ReporteCampaniaMailchimpMetricaDTO> listaReporteMailingMetrica = CampaniaMailing.EstructurarReporteDetalleCampaniaMailChimpGeneral(FiltroFechaReporteDetalle.IdCampaniaMailing, FiltroFechaReporteDetalle.VersionMailing, FiltroFechaReporteDetalle.FechaInicio, FiltroFechaReporteDetalle.FechaFin);

                return Ok(listaReporteMailingMetrica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 05/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de metrica registros de mailchimp
        /// </summary>
        /// <param name="FiltroFechaReporteCampaniaMailing">Objeto de clase FiltroReporteMailingMetricaDTO</param>
        /// <returns>Response 200 con lista de objetos de clase ReporteCampaniaMailchimpMetricaRegistrosDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerReporteIndicadoresCampaniasMailingRegistros([FromBody] FiltroReporteMailingMetricaDTO FiltroFechaReporteCampaniaMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<ReporteCampaniaMailchimpMetricaRegistrosDTO> listaReporteAnuncioFacebookMetrica = CampaniaMailing.EstructurarReporteCampaniaMailChimpRegistros(FiltroFechaReporteCampaniaMailing.FechaInicio, FiltroFechaReporteCampaniaMailing.FechaFin);

                return Ok(listaReporteAnuncioFacebookMetrica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 05/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de metrica de registros de mailchimp para exportacion
        /// </summary>
        /// <param name="FiltroFechaReporteCampaniaMailing">Objeto de clase FiltroReporteMailingMetricaDTO</param>
        /// <returns>Response 200 con lista de objetos de clase ReporteCampaniaMailchimpMetricaDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerReporteIndicadoresCampaniasMailingRegistrosExportacion([FromBody] FiltroReporteMailingMetricaDTO FiltroFechaReporteCampaniaMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<ReporteCampaniaMailchimpMetricaRegistrosDTO> listaReporteMailingMetrica = CampaniaMailing.EstructurarReporteCampaniaMailChimpRegistrosExportacion(FiltroFechaReporteCampaniaMailing.FechaInicio, FiltroFechaReporteCampaniaMailing.FechaFin);

                return Ok(listaReporteMailingMetrica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 03/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de la campania mailing de registros
        /// </summary>
        /// <param name="FiltroFechaReporteDetalle">Objeto de clase FiltroReporteMailingMetricaDetalleDTO</param>
        /// <returns>Response 200 con lista de objetos de clase ReporteCampaniaMailchimpMetricaRegistrosDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerDetalleFilaCampaniaMailingRegistros([FromBody] FiltroReporteMailingMetricaDetalleDTO FiltroFechaReporteDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<ReporteCampaniaMailchimpMetricaRegistrosDTO> listaReporteMailingMetrica = CampaniaMailing.EstructurarReporteDetalleCampaniaMailChimpRegistros(FiltroFechaReporteDetalle.IdCampaniaMailing, FiltroFechaReporteDetalle.VersionMailing, FiltroFechaReporteDetalle.FechaInicio, FiltroFechaReporteDetalle.FechaFin);

                return Ok(listaReporteMailingMetrica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 05/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de metrica registros de mailchimp
        /// </summary>
        /// <param name="FiltroFechaReporteCampaniaMailing">Objeto de clase FiltroReporteMailingMetricaDTO</param>
        /// <returns>Response 200 con lista de objetos de clase ReporteCampaniaMailchimpMetricaRegistrosDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerReporteIndicadoresCampaniasMailingOportunidades([FromBody] FiltroReporteMailingMetricaDTO FiltroFechaReporteCampaniaMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<ReporteCampaniaMailchimpMetricaOportunidadesDTO> listaReporteAnuncioFacebookMetrica = CampaniaMailing.EstructurarReporteCampaniaMailChimpOportunidades(FiltroFechaReporteCampaniaMailing.FechaInicio, FiltroFechaReporteCampaniaMailing.FechaFin);

                return Ok(listaReporteAnuncioFacebookMetrica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 05/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de metrica de oportunidades de mailchimp para exportacion
        /// </summary>
        /// <param name="FiltroFechaReporteCampaniaMailing">Objeto de clase FiltroReporteMailingMetricaDTO</param>
        /// <returns>Response 200 con lista de objetos de clase ReporteCampaniaMailchimpMetricaDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerReporteIndicadoresCampaniasMailingOportunidadesExportacion([FromBody] FiltroReporteMailingMetricaDTO FiltroFechaReporteCampaniaMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<ReporteCampaniaMailchimpMetricaOportunidadesDTO> listaReporteMailingMetrica = CampaniaMailing.EstructurarReporteCampaniaMailChimpOportunidadesExportacion(FiltroFechaReporteCampaniaMailing.FechaInicio, FiltroFechaReporteCampaniaMailing.FechaFin);

                return Ok(listaReporteMailingMetrica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 03/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de la campania mailing de registros
        /// </summary>
        /// <param name="FiltroFechaReporteDetalle">Objeto de clase FiltroReporteMailingMetricaDetalleDTO</param>
        /// <returns>Response 200 con lista de objetos de clase ReporteCampaniaMailchimpMetricaRegistrosDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerDetalleFilaCampaniaMailingOportunidades([FromBody] FiltroReporteMailingMetricaDetalleDTO FiltroFechaReporteDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<ReporteCampaniaMailchimpMetricaOportunidadesDTO> listaReporteMailingMetrica = CampaniaMailing.EstructurarReporteDetalleCampaniaMailChimpOportunidades(FiltroFechaReporteDetalle.IdCampaniaMailing, FiltroFechaReporteDetalle.VersionMailing, FiltroFechaReporteDetalle.FechaInicio, FiltroFechaReporteDetalle.FechaFin);

                return Ok(listaReporteMailingMetrica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
