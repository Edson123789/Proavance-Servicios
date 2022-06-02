using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: RegistroLandingPageController
    /// Autor: , Jashin Salazar
    /// Fecha: 14/05/2021
    /// <summary>
    /// Registro Landing Page
    /// </summary>
    [Route("api/RegistroLandingPage")]
    public class RegistroLandingPageController : Controller
    {

        /// TipoFuncion: GET
        /// Autor: --, Jashin Salazar
        /// Fecha: 14/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera el Reporte Landing Page
        /// </summary>
        /// <param name="FechaInicial"> Fecha inicial. </param>
        /// <param name="FechaFinal"> Fecha final. </param>
        /// <returns> Lista de objetos: List<ReporteLandingPagePortalDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerLandingPage(DateTime? FechaInicial, DateTime? FechaFinal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _filtro = new FiltroLandingPagePortalDTO()
                {
                    FechaInicial = FechaInicial,
                    FechaFinal = FechaFinal
                };
                AsignacionAutomaticaRepositorio _repAsignacionAutomatica = new AsignacionAutomaticaRepositorio();
                var listado = _repAsignacionAutomatica.ObtenerReporteLandingPagePortal(_filtro);
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

        /// TipoFuncion: GET
        /// Autor: --, Jashin Salazar
        /// Fecha: 14/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera el Reporte Landing Page Facebook
        /// </summary>
        /// <param name="FechaInicial"> Fecha inicial. </param>
        /// <param name="FechaFinal"> Fecha final. </param>
        /// <returns> Lista de objetos: List<ReporteLandingPagePortalFacebookDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerLandingPageFacebook(DateTime? FechaInicial, DateTime? FechaFinal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _filtro = new FiltroLandingPagePortalFacebookDTO()
                {
                    FechaInicial = FechaInicial,
                    FechaFinal = FechaFinal
                };
                AsignacionAutomaticaRepositorio _repAsignacionAutomatica = new AsignacionAutomaticaRepositorio();
                var listado = _repAsignacionAutomatica.ObtenerReporteLandingPagePortalFacebook(_filtro);
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
    }
}
