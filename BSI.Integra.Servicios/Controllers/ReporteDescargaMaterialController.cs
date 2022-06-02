using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteDescargaMaterial
    /// Autor: Abelson Quiñones Gutiérrez
    /// Fecha: 1/06/2021
    /// <summary>
    /// Contiene los controladores necesarios para los filtros y la consulta del reporte de descarga de material Webinars y Whitepapers 
    /// </summary>

    [Route("api/ReporteDescargaMaterial")]
    public class ReporteDescargaMaterialController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public ReporteDescargaMaterialController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
        }

        /// Tipo Función: GET
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Listar los paises para cargar el liltro del reporte de descargas
        /// </summary>
        /// <returns>Lista de los paises en un List<FiltroDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPais()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaisRepositorio _repPais = new PaisRepositorio();
                return Ok(_repPais.ObtenerListaPais());

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// Tipo Función: GET
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Listar las areas de capacitacion para cargar el liltro del reporte de descargas
        /// </summary>
        /// <returns>Lista de las areas de capacitacion en un List<FiltroDTO></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaAreaCapacitacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                AreaCapacitacionRepositorio _repoAreaCapacitacion = new AreaCapacitacionRepositorio();
                var listaAreaCapacitacion = _repoAreaCapacitacion.ObtenerTodoFiltro();
                return Ok(listaAreaCapacitacion );
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
        /// Listar las subareas de capacitacion para cargar el liltro del reporte de descargas
        /// </summary>
        /// <param name="IdAreaCapacitacion">Id del area de capacitacion</param>
        /// <returns>Lista de las subareas en un List<FiltroDTO></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaSubAreaCapacitacion(int IdAreaCapacitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ReporteDescargaMaterialRepositorio _repoDescargaMaterial = new ReporteDescargaMaterialRepositorio();
                var listaSubAreaCapacitacion = _repoDescargaMaterial.ObtenerSubAreasParaFiltro(IdAreaCapacitacion);
                return Ok(listaSubAreaCapacitacion );
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
        /// Listar los nombres del material de webinar y whitepaper para cargar el liltro del reporte de descargas
        /// </summary>
        /// <param name="filtroMaterial">Filtro para la seleccion de nombres de material segun el tipoArticulo,area y subarea </param>
        /// <returns>Lista de los articulos el un List<ListaMatarial></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerListaMaterial([FromBody] ReporteMaterialFiltroDTO FiltroMaterial)
        {
            try
            {

                ReporteDescargaMaterialRepositorio _repoDescargaMaterial = new ReporteDescargaMaterialRepositorio();
                var listaSubAreaCapacitacion = _repoDescargaMaterial.ObtenerListaMateriales(FiltroMaterial);
                return Ok(listaSubAreaCapacitacion);
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
        /// Generar el reporte de Material descargado segun el liltro enviado
        /// </summary>
        /// <param name="filtroReporte">Filtro para la seleccion del reporte de material descargado  </param>
        /// <returns>El reporte de Material descargado en una Lista List<ReporteDescargaMaterialDTO></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteDescargas([FromBody] ReporteDescargaMaterialFiltroDTO FiltroReporte)
        {
            try
            {
                ReporteDescargaMaterialRepositorio _repoDescargaMaterial = new ReporteDescargaMaterialRepositorio();
                var lista = _repoDescargaMaterial.GenerarReporteDescargaContenido(FiltroReporte);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
