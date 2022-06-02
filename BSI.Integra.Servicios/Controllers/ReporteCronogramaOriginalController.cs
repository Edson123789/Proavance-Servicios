using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteCronogramaOriginalController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteCronogramaOriginalController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// Tipo Función: Get
        /// Autor: Miguel Angel Mora Frisancho
        /// Fecha: 28/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Funcion que trae los datos de los alumnos para el reporte de cronogramas originales
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult GenerarReporte()
        {
            try
            {
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                return Ok(reportesRepositorio.ObtenerReporteCronogramaOriginales(DateTime.UtcNow.Date, DateTime.UtcNow.Date, null,null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
