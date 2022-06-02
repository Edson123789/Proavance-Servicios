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
    /// Controlador: QuejaSugerenciaController
    /// Autor: Abelson Quiñones Gutiérrez
    /// Fecha: 2/06/2021
    /// <summary>
    /// Contiene los controladores necesarios para los filtros y la consulta del reporte de quejas y sugerencias 
    /// </summary>
    [Route("api/QuejaSugerencia")]
    public class QuejaSugerenciaController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public QuejaSugerenciaController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
        }

        /// Tipo Función: POST
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Generar el reporte de quejas y sugerencias segun el filtro ingresado
        /// </summary>
        /// <param name="FiltroReporte">filtro para la seleccion del reporte de quejas y sugerencias</param>
        /// <returns>Lista del reporte quejas y sugerencias en un List<QuejaSugerenciaDTO></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] QuejaSugerenciaFiltroDTO FiltroReporte)
        {
            try
            {
                QuejaSugerenciaRepositorio _repoQuejasSugerencias = new QuejaSugerenciaRepositorio();
                var lista = _repoQuejasSugerencias.GenerarReporteQuejaSugerencia(FiltroReporte);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
