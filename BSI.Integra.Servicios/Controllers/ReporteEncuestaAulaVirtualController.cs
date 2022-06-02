using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Reportes.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteEncuestaAulaVirtual
    /// Autor: Abeson Quiñones
    /// Fecha: 07/12/2021
    /// <summary>
    /// Contiene los controladores necesarios para los filtros y la consulta del reporte de encuesta Aula virtual
    /// </summary>
    [Route("api/ReporteEncuestaAulaVirtual")]
    public class ReporteEncuestaAulaVirtualController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteEncuestaAulaVirtualController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
        }


        /// Tipo Función: GET
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 16/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Listar los nombres de los docentes para el filtro
        /// </summary>
        /// <returns>Lista de los nombres en un List<ItemComboAutocompleDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerDocentes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToString());
            }
            try
            {
                ReporteEncuestasInicialesRepositorio _repoLibroReclamacion = new ReporteEncuestasInicialesRepositorio();
                var listaDocentes = _repoLibroReclamacion.ObtenerListaDocentes();
                return Ok(listaDocentes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 16/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Listar los nombres de los docentes para el filtro
        /// </summary>
        /// <returns>Lista de los nombres en un List<ItemComboAutocompleDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerAsistenteAC()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToString());
            }
            try
            {
                ReporteEncuestaAulaVirtualRepositorio _repoEncuestaAulaVirtual = new ReporteEncuestaAulaVirtualRepositorio();
                var listaAsistente = _repoEncuestaAulaVirtual.ObtenerListaAsistenteAC();
                return Ok(listaAsistente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        

        /// Tipo Función: POST
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 16/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Generar el reporte de encuestas del aula virtual segun el filtro ingresado
        /// </summary>
        /// <param name="FiltroReporte">filtro para la seleccion del reporte por fechainicio, fechafin, nombre y dni</param>
        /// <returns>Lista del reporte encuestas aula virtual en un List<ReporteEncuestaAulaVirtualDTO></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteEncuestaAulaVirtual([FromBody] ReporteEncuestaAulaVirtualFiltroDTO FiltroReporte)
        {
            try
            {
                ReporteEncuestaAulaVirtualRepositorio _repoEncuestaAulaVirtual = new ReporteEncuestaAulaVirtualRepositorio();
                var lista = _repoEncuestaAulaVirtual.GenerarReporteEncuestaAulaVirtual(FiltroReporte);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
