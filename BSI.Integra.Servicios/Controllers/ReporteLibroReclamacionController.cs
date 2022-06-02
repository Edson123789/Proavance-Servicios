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
    /// Controlador: ReporteLibroReclamacionController
    /// Autor: Abelson Quiñones Gutiérrez
    /// Fecha: 2/06/2021
    /// <summary>
    /// Contiene los controladores necesarios para los filtros y la consulta del reporte de libro de reclamaciones
    /// </summary>

    [Route("api/ReporteLibroReclamacion")]
    public class ReporteLibroReclamacionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public ReporteLibroReclamacionController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
        }

        /// Tipo Función: GET
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Listar los nombres de la personas segun la coincidencia del parametro recibido
        /// </summary>
        /// <param name="Nombre">parte del nombre para buscar coincidencias</param>
        /// <returns>Lista de los nombres en un List<ItemComboAutocompleDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaNombreReclamo(string Nombre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReporteLibroReclamacionRepositorio _repoLibroReclamacion = new ReporteLibroReclamacionRepositorio();
                var listaElementos = _repoLibroReclamacion.ObtenerListaNombreReclamo(Nombre);
                return Ok(listaElementos );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Listar los dni de las personas segun la coincidencia del parametro recibido
        /// </summary>
        /// <param name="Dni">parte del dni para buscar coincidencias</param>
        /// <returns>Lista de los dnis en un List<ItemComboAutocompleDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaDniReclamo(string Dni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReporteLibroReclamacionRepositorio _repoLibroReclamacion = new ReporteLibroReclamacionRepositorio();
                var listaElementos = _repoLibroReclamacion.ObtenerListaDniReclamo(Dni);
                return Ok(listaElementos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Generar el reporte de libro de reclamaciones segun el filtro ingresado
        /// </summary>
        /// <param name="FiltroReporte">filtro para la seleccion del reporte por fechainicio, fechafin, nombre y dni</param>
        /// <returns>Lista del reporte libro de reclamaciones en un List<ReporteLibroReclamacionDTO></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteLibroReclamacion([FromBody] ReporteLibroReclamacionFiltroDTO FiltroReporte)
        {
            try
            {
                ReporteLibroReclamacionRepositorio _repoLibroReclamacion = new ReporteLibroReclamacionRepositorio();
                var lista = _repoLibroReclamacion.GenerarReporteLibroReclamacion(FiltroReporte);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
