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
    [Route("api/ReporteTasaConversionHistorica")]
    [ApiController]
    public class ReporteTasaConversionHistoricaController : ControllerBase
    {
        public string ReporteRepositorio { get; private set; }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosReporteTasaConversionHistorica()
        {
            try
            {
                PersonalRepositorio repPersonal = new PersonalRepositorio();

                ReporteTasaConversionConsolidadaDTO result = new ReporteTasaConversionConsolidadaDTO();
                result.Asesores = repPersonal.ObtenerPersonalAsesoresFiltro();
                result.Coordinadores = repPersonal.ObtenerPersonalCoordinadoresFiltro();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboAreas()
        {
            try
            {
                AreaCapacitacionRepositorio repAreaCapacitacion = new AreaCapacitacionRepositorio();
                var result = repAreaCapacitacion.ObtenerTodoFiltro().Select(o => new {id = o.Id, name = o.Nombre }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboSubAreas()
        {
            try
            {
                SubAreaCapacitacionRepositorio repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio();
                var result = repSubAreaCapacitacion.ObtenerTodoFiltro().Select(o => new {id = o.Id, name = o.Nombre,area=o.IdAreaCapacitacion }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboPGenerales()
        {
            try
            {
                PgeneralRepositorio repPGeneral = new PgeneralRepositorio();
                var result = repPGeneral.ObtenerTodoGrid().Select(o => new { id = o.IdPgeneral, name = o.Nombre, subarea=o.IdSubArea }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboPEspecificos()
        {
            try
            {
                PespecificoRepositorio repPEspecifico = new PespecificoRepositorio();
                var result = repPEspecifico.ObtenerDatosProgramaEspecifico().ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[Route("[action]")]
        //[HttpPost]
        //public ActionResult GenerarReporte([FromBody] ReporteTasaConversionHistoricaFiltroDTO Filtros)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {

        //        string _area = ListIntToString(Filtros.areas);
        //        string _subarea = ListIntToString(Filtros.subareas);
        //        string _pgeneral = ListIntToString(Filtros.pgeneral);
        //        string _pespecifico = ListIntToString(Filtros.pespecifico);
        //        string _probabilidad = ListStringToString(Filtros.probabilidad);
        //        string _categoriaDato = ListStringToString(Filtros.categoriaDato);

        //        Filtros.fechaInicio = Convert.ToDateTime(Filtros.fechaInicio).Date;
        //        Filtros.fechaFin = Convert.ToDateTime(Filtros.fechaFin).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        //        Filtros.fechaInicioTCH = Convert.ToDateTime(Filtros.fechaInicioTCH).Date;
        //        Filtros.fechaFinTCH = Convert.ToDateTime(Filtros.fechaFinTCH).Date;

                
        //        try
        //        {
        //            var listRpta = reportes.ReporteTasaConversionConsolidadoAsesores(_area, _subarea, _pgeneral, _pespecifico, _modalidades, _ciudades, Filtros.fecha, _coordinadores, _asesores, Filtros.FechaInicio.Value, Filtros.FechaFin.Value);

        //            return Ok(new { Records = agrupado, Total = agrupado2 });
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
        private string ListIntToString(IList<int> datos)
        {
            if (datos == null)
                datos = new List<int>();
            int NumberElements = datos.Count;
            string rptaCadena = string.Empty;
            for (int i = 0; i < NumberElements - 1; i++)
                rptaCadena += Convert.ToString(datos[i]) + ",";
            if (NumberElements > 0)
                rptaCadena += Convert.ToString(datos[NumberElements - 1]);
            return rptaCadena;
        }
        private string ListStringToString(IList<string> datos)
        {
            if (datos == null)
                datos = new List<string>();
            int NumberElements = datos.Count;
            string rptaCadena = string.Empty;
            for (int i = 0; i < NumberElements - 1; i++)
                rptaCadena += Convert.ToString(datos[i]) + ",";
            if (NumberElements > 0)
                rptaCadena += Convert.ToString(datos[NumberElements - 1]);
            return rptaCadena;
        }
    }
}
