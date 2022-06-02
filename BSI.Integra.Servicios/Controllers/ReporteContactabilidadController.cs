using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteContactabilidadController
    /// <summary>
    /// Autor: , Jashin Salazar
    /// Fecha: 07/04/2021
    /// <summary>
    /// Gestión Reporte de Contactabilidad
    /// </summary>
    [Route("api/ReporteContactabilidad")]
    [ApiController]
    public class ReporteContactabilidadController : ControllerBase
    {
        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar
        /// Fecha: 07/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combo de asesor para Módulo de Reporte de Contactabilidad
        /// </summary>
        /// <returns> objeto DTO : ReporteContactabilidadCombosDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosReporte()
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();

                ReporteContactabilidadCombosDTO result = new ReporteContactabilidadCombosDTO();
                result.Asesores = _repPersonal.ObtenerAsesoresVentasOficial();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        /// TipoFuncion: GET
        /// Autor: , Jashin Salazar
        /// Fecha: 07/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos de asesores para Módulo de Reporte de Contactabilidad para Operaciones
        /// </summary>
        /// <returns> objeto DTO : ReporteContactabilidadCombosDTO </returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosReporteOperaciones(int IdPersonal)
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();

                ReporteContactabilidadCombosDTO result = new ReporteContactabilidadCombosDTO();

                List<PersonalAsignadoDTO> asistentes = _repPersonal.ObtenerPersonalAsignadoOperacionesTotal(IdPersonal);
                //activos
                result.AsistentesActivos = asistentes.Where(w => w.Activo == true).ToList();
                //todos
                result.AsistentesTotales = asistentes;
                //inactivo
                result.AsistentesInactivos = asistentes.Where(w => w.Activo == false).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        /// TipoFuncion: POST
        /// Autor: , Jashin Salazar
        /// Fecha: 07/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Generar Reporte de Contactabilidad para Operaciones
        /// </summary>
        /// <returns> objeto : Reportes</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteContactabilidadFiltroDTO Json)
        {
            try
            {
                PersonalRepositorio _repo = new PersonalRepositorio();
                Reportes reporte = new Reportes();
                var result = reporte.ReporteContactabilidad(Json);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        /// TipoFuncion: POST
        /// Autor: , Jashin Salazar
        /// Fecha: 07/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Generar Reporte de contactabilidad para Comercial
        /// </summary>
        /// <returns> objeto : Reportes</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReportev2([FromBody] ReporteContactabilidadFiltroDTO Json)
        {
            try
            {
                PersonalRepositorio _repo = new PersonalRepositorio();
                Reportes reporte = new Reportes();
                var result = reporte.ReporteContactabilidadV2(Json);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        /// TipoFuncion: POST
        /// Autor: , Jashin Salazar
        /// Fecha: 07/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Generar Reporte de contactabilidad Desagregado (Deprecated)
        /// </summary>
        /// <returns> objeto : Reportes</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteDesagregado([FromBody] ReporteContactabilidadFiltroDTO Json)
        {
            try
            {
                PersonalRepositorio _repo = new PersonalRepositorio();
                Reportes reporte = new Reportes();
                var result = reporte.ReporteContactabilidadDesagregado(Json);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


    }
}
