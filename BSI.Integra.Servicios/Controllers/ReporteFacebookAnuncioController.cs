using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteFacebookAnuncio
    /// <summary>
    /// Autor: Gian Miranda
    /// Fecha: 12/06/2021
    /// <summary>
    /// Gestión de Reporte de Facebook Anuncio
    /// </summary>
    [Route("api/ReporteFacebookAnuncio")]
    public class ReporteFacebookAnuncioController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly AnuncioFacebookMetricaBO AnuncioFacebookMetrica;
        private readonly AnuncioFacebookMetricaRepositorio _repAnuncioFacebookMetrica;

        public ReporteFacebookAnuncioController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            AnuncioFacebookMetrica = new AnuncioFacebookMetricaBO(_integraDBContext);
            _repAnuncioFacebookMetrica = new AnuncioFacebookMetricaRepositorio(_integraDBContext);
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 15/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza las metricas de Facebook por intervalo de fecha
        /// </summary>
        /// <param name="FechaFiltroDescarga">Objeto de clase AnuncioFacebookMetricaFechaDTO</param>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarMetricaFacebookAnuncioPorIntervaloFecha([FromBody] AnuncioFacebookMetricaFechaDTO FechaFiltroDescarga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
                if (!_repIntegraAspNetUsers.ExistePorNombreUsuario(FechaFiltroDescarga.Usuario))
                {
                    return BadRequest("El usuario no existe");
                }

                string usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(FechaFiltroDescarga.Usuario);

                List<string> correosCopia = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };

                FacebookBO facebook = new FacebookBO();
                DateTime fechaInicio = DateTime.Now;
                string cadenaFechaInicio = FechaFiltroDescarga.FechaInicio.ToString("yyyy-MM-dd");
                string cadenaFechaFinal = FechaFiltroDescarga.FechaFin.ToString("yyyy-MM-dd");

                List<AnuncioFacebookMetricaDTO> resultado = facebook.DescargarMetricaFacebookAnuncio(cadenaFechaInicio, cadenaFechaFinal);
                bool resultadoActualizacion = AnuncioFacebookMetrica.ActualizarMetricaIntegra(resultado, FechaFiltroDescarga.Usuario);

                DateTime fechaFin = DateTime.Now;

                TMK_MailServiceImpl mailservicePersonalizado = new TMK_MailServiceImpl();
                TMKMailDataDTO contenidoMailing = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = $"Actualización de datos de la fecha {cadenaFechaInicio}",
                    Message = AnuncioFacebookMetrica.EstructurarMensajeAnuncioFacebook(cadenaFechaInicio, fechaInicio, fechaFin),
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
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 15/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de anuncio de Facebook Metrica
        /// </summary>
        /// <param name="IdAreaCapacitacion">Id del grupo de filtro de programa critico (PK de la tabla pla.T_GrupoFiltroProgramaCritico)</param>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]/{IdAreaCapacitacion?}")]
        [HttpGet]
        public ActionResult ObtenerReporteAnuncioFacebookMetrica(int? IdAreaCapacitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //List<ReporteAnuncioFacebookMetricaDTO> listaReporteAnuncioFacebookMetrica = _repAnuncioFacebookMetrica.ObtenerReporteAnuncioFacebookMetrica(IdAreaCapacitacion);
                List<ReporteAnuncioFacebookMetricaDiasDTO> listaReporteAnuncioFacebookMetrica = AnuncioFacebookMetrica.EstructurarReporteAnuncioFacebook(IdAreaCapacitacion);

                return Ok(JsonConvert.SerializeObject(listaReporteAnuncioFacebookMetrica));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 15/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos de los anuncios del modulo de Facebook Metrica
        /// </summary>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombosAnuncioFacebookMetrica()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = new
                {
                    ListaArea = _repAnuncioFacebookMetrica.ObtenerComboAreaAnuncioFacebookMetrica()
                };

                return Ok(JsonConvert.SerializeObject(resultado));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 15/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene informacion basica para alimentar el modulo
        /// </summary>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerInformacionBasica()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = new
                {
                    UltimaModificacion = _repAnuncioFacebookMetrica.ObtenerUltimaModificacion()
                };

                return Ok(JsonConvert.SerializeObject(resultado));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
