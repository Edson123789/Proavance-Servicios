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
    [Route("api/ReporteControlOperativo")]
    [ApiController]
    public class ReporteControlOperativoController : ControllerBase
    {

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosReporte()
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                TipoCategoriaOrigenRepositorio _repTipoCategoria = new TipoCategoriaOrigenRepositorio();

                ReporteControlOperativoCombosDTO result = new ReporteControlOperativoCombosDTO();
                result.Coordinadores = _repPersonal.ObtenerPersonalCoordinadoresFiltro();
                result.Asesores = _repPersonal.ObtenerAsesoresVentas();
                result.Grupos = _repTipoCategoria.ObtenerTodoFiltro();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [Route("[action]/{Coordinadores}")]
        [HttpGet]
        public ActionResult ObtenerAsesoresPorCoordinadores(string Coordinadores)
        {
            try
            {
                PersonalRepositorio _repo = new PersonalRepositorio();
                List<AsesorNombreFiltroDTO> asesores = new List<AsesorNombreFiltroDTO>();
                if (!Coordinadores.Equals("null"))
                {
                    var arrCoordinadores = Coordinadores.Split(',');
                    foreach (var ac in arrCoordinadores)
                    {
                        int idAsesor = int.Parse(ac);
                        var asesoresTemp = _repo.ObtenerSubordinadosCoordinador(idAsesor);
                        asesores = asesores.Union(asesoresTemp).ToList();
                    }
                }
                else
                {
                    asesores = _repo.ObtenerAsesoresVentas();
                }
                return Ok(asesores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteControlCalidadProcesamiento([FromBody] ReporteControlCalidadProcesamientoFiltrosDTO Json)
        {
            try
            {
               PersonalRepositorio _repo = new PersonalRepositorio();
               Reportes reporte = new Reportes();
               var result = reporte.ReporteControlCalidadProcesamiento(Json);
               return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteCalidadProspecto([FromBody] ReporteCalidadProspectoFiltroDTO Json)
        {
            try
            {
                PersonalRepositorio _repo = new PersonalRepositorio();
                Reportes reporte = new Reportes();
                var result = reporte.ReporteCalidadProspecto(Json);
                var agrupado = (from p in result
                                group p by p.IdAsesor into grupo
                                select new { g = grupo.Key, l = grupo.OrderBy(a => a.Orden) }).ToList();

                return Ok(agrupado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteTasaContactoConsolidado([FromBody] ReporteTasaContactoConsolidadoFiltroDTO Json)
        {
            try
            {
                PersonalRepositorio _repo = new PersonalRepositorio();
                Reportes reporte = new Reportes();
                var result = reporte.GenerarReporteTasaContactoConsolidado(Json);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteTasaContactoAsesor([FromBody] ReporteTasaContactoAsesorFiltroDTO Json)
        {
            try
            {
                PersonalRepositorio _repo = new PersonalRepositorio();
                Reportes reporte = new Reportes();
                var result = reporte.GenerarReporteTasaContactoAsesor(Json);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoPorPersonal([FromBody]ListadoIdDTO IdsAsesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CentroCostoRepositorio repCentroCosto = new CentroCostoRepositorio();
                string asesores = string.Join(",", IdsAsesor.Ids);
                return Ok(repCentroCosto.ObtenerCentroCostoPorAsesores(IdsAsesor.Ids));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        //public ActionResult GenerarReporte([FromBody] ReporteControlOperativoFiltrosDTO Filtros)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        Reportes reporte = new Reportes();
        //        List<ReporteControlOperativoDTO> result = new List<ReporteControlOperativoDTO>();
        //        result = reporte.ObtenerReporteSeguimientoOportunidad(Filtros);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerListaOportunidadLog(int IdOportunidad)
        {
            try
            {
                Reportes reporte = new Reportes();
                return Ok(reporte.ObtenerOportunidadesLog(IdOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

      
        
    }
}
