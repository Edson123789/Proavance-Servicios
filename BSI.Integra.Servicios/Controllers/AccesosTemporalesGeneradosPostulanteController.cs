using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas.PostulanteAccesoTemporalAulaVirtualDTO;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AccesosTemporalesGeneradosPostulante
    /// Autor: Edgar Serruto
    /// Fecha: 22/06/2021
    /// <summary>
    /// Gestiona la funcionalidad del modulo (R) Accesos Temporales Generados
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccesosTemporalesGeneradosPostulanteController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly ProcesoSeleccionRepositorio _repProcesoSeleccion;
        private readonly ProcesoSeleccionEtapaRepositorio _repProcesoSeleccionEtapa;

        public AccesosTemporalesGeneradosPostulanteController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repProcesoSeleccion = new ProcesoSeleccionRepositorio(_integraDBContext);
            _repProcesoSeleccionEtapa = new ProcesoSeleccionEtapaRepositorio(_integraDBContext);
        }

        /// TipoFuncion: POST
        /// Autor: Edgar Serruto
        /// Fecha: 17/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combos para interfaz
        /// </summary>
        /// <returns> Objeto Agrupado </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var proceso = _repProcesoSeleccion.ObtenerCodigoNombreProcesoSeleccion();
                var etapa = _repProcesoSeleccionEtapa.ObtenerProcesoSeleccionEtapa();
                var estadoPostulante = _repProcesoSeleccion.ObtenerEstadoProcesoSeleccion();
                var estadoEtapaProcesoSeleccion = _repProcesoSeleccion.ObtenerEstadoEtapaProcesoSeleccion();
                return Ok(new {ListaProcesoSeleccion = proceso, ListaEtapa = etapa, EstadoProceso = estadoPostulante, ListaEstadoEtapa = estadoEtapaProcesoSeleccion });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto
        /// Fecha: 17/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene convocatorias Registradas
        /// </summary>
        /// <returns> Objeto Agrupado </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerAccesosTemporalesRegistrados(FiltroReporteAccesosTemporalesDTO InformacionAccesos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var mensaje = "";
                PostulanteAccesoTemporalAulaVirtualBO accesosPostulante = new PostulanteAccesoTemporalAulaVirtualBO();
                var listaAccesosTemporalesRegistrados = accesosPostulante.GenerarReporteAccesosTemporales(InformacionAccesos);
                if (listaAccesosTemporalesRegistrados.Count > 0)
                {
                    mensaje = "Se cargaron los postulantes filtrados";
                    return Ok(new{ Respuesta = true, Mensaje = mensaje, Datos = listaAccesosTemporalesRegistrados });
                }
                else
                {
                    mensaje = "No se encontraros postulantes con los filtros selecciones";
                    return Ok(new { Respuesta = false, Mensaje = mensaje, Datos = listaAccesosTemporalesRegistrados });
                }                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }        
    }
}
