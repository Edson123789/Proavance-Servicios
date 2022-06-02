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
    [Route("api/ReporteRN2")]
    [ApiController]
    public class ReporteRN2Controller : ControllerBase
    {
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerRN2()
        {
            try
            {
                return Ok();
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

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboFasesOportunidad()
        {
            try
            {
                FaseOportunidadRepositorio repFaseOportunidad = new FaseOportunidadRepositorio();
                var result = repFaseOportunidad.ObtenerFaseOportunidadTodoFiltro();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboCategoriaDato()
        {
            try
            {
                CategoriaOrigenRepositorio categoriaOrigenRepositorio = new CategoriaOrigenRepositorio();

                var result = categoriaOrigenRepositorio.ObtenerCategoriaFiltro();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte(ReporteRN2FiltroDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                string _area = ListIntToString(Filtros.areas);
                string _subarea = ListIntToString(Filtros.subareas);
                string _pgeneral = ListIntToString(Filtros.pgeneral);
                string _pespecifico = ListIntToString(Filtros.pespecifico);
                string _modalidades = ListStringToString(Filtros.modalidades);
                string _ciudades = ListStringToString(Filtros.ciudades);
                string _categoriaOrigen = ListIntToString(Filtros.categoriaOrigen);
                string _faseMaxima = ListStringToString(Filtros.faseMaxima);
                Filtros.FechaInicio = Convert.ToDateTime(Filtros.FechaInicio).Date;
                Filtros.FechaFin = Convert.ToDateTime(Filtros.FechaFin).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                return Ok(reportesRepositorio.ObtenerReporteRN2(_area, _subarea, _pgeneral, _pespecifico, _modalidades, _ciudades, _categoriaOrigen, Filtros.anio, _faseMaxima, Filtros.FechaInicio.Value, Filtros.FechaFin.Value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
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
