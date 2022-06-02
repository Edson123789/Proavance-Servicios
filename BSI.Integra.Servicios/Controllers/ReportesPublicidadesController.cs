using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReportesPublicidades")]
    public class ReportesPublicidadesController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ReportesPublicidadesController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerEnlacesPublicidadFacebook([FromBody] FiltroFechasDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportesRepositorio _repReportesRepositorio = new ReportesRepositorio();
                return Ok(_repReportesRepositorio.ObtenerListaEnlacesPublicidadFacebook(Filtro.FechaInicio, Filtro.FechaFin));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerEnlacesLandingPage([FromBody] FiltroFechasDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportesRepositorio _repReportesRepositorio = new ReportesRepositorio();
                return Ok(_repReportesRepositorio.ObtenerListaEnlacesLandingPage(Filtro.FechaInicio, Filtro.FechaFin));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerEnlacesLandingPageWeb([FromBody] FiltroFechasDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportesRepositorio _repReportesRepositorio = new ReportesRepositorio();
                return Ok(_repReportesRepositorio.ObtenerListaEnlacesLandingPageWeb(Filtro.FechaInicio, Filtro.FechaFin));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerEnlacesLandingPageWebEstandar([FromBody] FiltroFechasDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportesRepositorio _repReportesRepositorio = new ReportesRepositorio();
                return Ok(_repReportesRepositorio.ObtenerListaEnlacesLandingPageWebEstandar(Filtro.FechaInicio, Filtro.FechaFin));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPaisesWhatsapp()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PaisRepositorio _repoPais = new PaisRepositorio();
                var Paises = _repoPais.ObtenerPaisesParaEnlacesPublicidadWhatsapp();
                return Ok(Paises);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerEnlacesWhatsapp([FromBody] ValorFiltroDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportesRepositorio _repReportesRepositorio = new ReportesRepositorio();
                return Ok(_repReportesRepositorio.ObtenerListaEnlacesWhatsapp(Filtro.Valor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
