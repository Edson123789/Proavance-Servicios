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
    /// Controlador: ReporteEncuestasInicialesController
    /// Autor: Abelson Quiñones Gutiérrez
    /// Fecha: 16/06/2021
    /// <summary>
    /// Contiene los controladores necesarios para los filtros y la consulta del reporte de encuestas iniciales
    /// </summary>

    [Route("api/ReporteEncuestasIniciales")]
    public class ReporteEncuestasInicialesController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteEncuestasInicialesController(integraDBContext IntegraDBContext)
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

        /// Tipo Función: POST
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 16/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Generar el reporte de encuestas iniciales segun el filtro ingresado
        /// </summary>
        /// <param name="FiltroReporte">filtro para la seleccion del reporte por fechainicio, fechafin, nombre y dni</param>
        /// <returns>Lista del reporte encuestas iniciales en un List<ReporteEncuestasInicialesDTO></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteEncuestasIniciales([FromBody] ReporteEncuestasInicialesFiltroDTO FiltroReporte)
        {
            try
            {
                ReporteEncuestasInicialesRepositorio _repoLibroReclamacion = new ReporteEncuestasInicialesRepositorio();
                var lista = _repoLibroReclamacion.GenerarReporteEncuestasIniciales(FiltroReporte);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
