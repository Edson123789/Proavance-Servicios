using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteCambioDeFaseController
    /// Autor: Edgar S.
    /// Fecha: 22/02/2021
    /// <summary>
    /// Gestión Reporte de Cambio de Fase
    /// </summary>
    [Route("api/ReporteCambioDeFase")]
    [ApiController]
    public class ReporteCambioDeFaseController : ControllerBase
    {
        public string ReporteRepositorio { get; private set; }

        /// TipoFuncion: GET
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// Versión: 1.1
        /// <summary>
        /// Obtiene Combos para Módulo de Reporte de Cambio de Fase
        /// </summary>
        /// <param></param>
        /// <returns> objeto DTO : ReporteCambioDeFaseCombosGeneralDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosReporteCambioDeFase()
        {
            try
            {
                CentroCostoRepositorio repCentroCosto = new CentroCostoRepositorio();
                PersonalRepositorio repPersonal = new PersonalRepositorio();
                TipoDatoRepositorio repTipoDato = new TipoDatoRepositorio();
                CategoriaOrigenRepositorio repCategoriaOrigen = new CategoriaOrigenRepositorio();

                ReporteCambioDeFaseCombosGeneralDTO result = new ReporteCambioDeFaseCombosGeneralDTO
                {
                    CentroCostos = repCentroCosto.ObtenerCentroCostoParaFiltro(),
                    Asesores = repPersonal.ObtenerAsesoresVentasOficial(),
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        /// TipoFuncion: POST
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// Versión: 1.1
        /// <summary>
        /// Obtiene lista de centro de costos para filtro por Asesores
        /// </summary>
        /// <returns> Lista objeto DTO : List<FiltroDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoPorPersonal([FromBody] ListadoIdDTO IdsAsesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CentroCostoRepositorio repCentroCosto = new CentroCostoRepositorio();
                string asesores = string.Join(",", IdsAsesor.Ids);
                return Ok(repCentroCosto.ObtenerCentroCostoPorAsesores(IdsAsesor.Ids));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// Versión: 1.1
        /// <summary>
        /// Genera Reporte de Cambio de Fase
        /// </summary>
        /// <returns> objeto DTO : ReporteCambioDeFaseDataDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteCambioFaseFiltrosDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reportes = new Reportes();
                ReporteCambioDeFaseDataDTO data = new ReporteCambioDeFaseDataDTO();
                data = reportes.ReporteCambioDeFase(Filtros);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// Versión: 1.1
        /// <summary>
        /// Genera Reporte de Cambio de Fase Versión 2
        /// </summary>
        /// <returns> objeto DTO : ReporteCambioDeFaseDataDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteV2([FromBody] ReporteCambioFaseFiltrosDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reportes = new Reportes();
                ReporteCambioDeFaseDataV2DTO data = new ReporteCambioDeFaseDataV2DTO();
                data = reportes.ReporteCambioDeFaseV2(Filtros);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// Versión: 1.1
        /// <summary>
        /// Genera Reporte de Cambio de Fase en Integra
        /// </summary>
        /// <returns> objeto DTO : ReporteCambioDeFaseDataDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteV2Integra([FromBody] ReporteCambioFaseFiltrosDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reportes = new Reportes();
                ReporteCambioDeFaseDataDTO data = new ReporteCambioDeFaseDataDTO();
                data = reportes.ReporteCambioDeFaseV2Integra(Filtros);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// Versión: 1.1
        /// <summary>
        /// Genera Reporte de Cambio de Fase Prueba
        /// </summary>
        /// <returns> objeto DTO : ReporteCambioDeFaseDataDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteV2_Prueba([FromBody] ReporteCambioFaseFiltrosDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reportes = new Reportes();
                ReporteCambioDeFaseDataDTO data = new ReporteCambioDeFaseDataDTO();
                data = reportes.ReporteCambioDeFaseV2_prueba(Filtros);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// Versión: 1.1
        /// <summary>
        /// Genera Reporte de Calidad
        /// </summary>
        /// <returns> objeto DTO : ReporteCambioDeFaseDataDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteCalidadProcesamiento([FromBody] ReporteCambioFaseFiltrosDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reportes = new Reportes();
                ReporteCalidadCambioDeFaseDTO data = new ReporteCalidadCambioDeFaseDTO();
                data = reportes.ReporteCalidadCambioDeFase(Filtros);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
