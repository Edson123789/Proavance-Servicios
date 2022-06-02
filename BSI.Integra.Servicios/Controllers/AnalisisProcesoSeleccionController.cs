using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/AnalisisProcesoSeleccion")]
    public class AnalisisProcesoSeleccionController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PuestoTrabajoRepositorio _repPuestoTrabajo;
        private readonly ProcesoSeleccionRepositorio _repProcesoSeleccion;

        public AnalisisProcesoSeleccionController()
        {
            _integraDBContext = new integraDBContext();
            _repPuestoTrabajo = new PuestoTrabajoRepositorio(_integraDBContext);
            _repProcesoSeleccion = new ProcesoSeleccionRepositorio(_integraDBContext);
            
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaPuestoTrabajo = _repPuestoTrabajo.GetBy(x => x.Estado == true).Select(x => new { IdPuestoTrabajo = x.Id, Nombre = x.Nombre });
                var listaProcesoSeleccion = _repProcesoSeleccion.GetBy(x => x.Estado == true && x.Activo == true).Select(x => new { Id = x.Id, Nombre = x.Nombre, IdPuestoTrabajo = x.IdPuestoTrabajo });

                return Ok(new { listaPuestoTrabajo, listaProcesoSeleccion });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] FiltroAnalisisProcesoSeleccionDTO Filtro)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (Filtro.FechaInicio == null || Filtro.FechaFin == null)
                {
                    Filtro.FechaFin = DateTime.Now;
                    Filtro.FechaInicio = new DateTime(1900, 12, 31);
                }

                var fechaInicioChange = Filtro.FechaInicio?.ToString("yyyy-MM-dd");
                var fechaInicioChange2 = Convert.ToDateTime(fechaInicioChange);
                TimeSpan fechaInicioChange3 = new TimeSpan(00,00,00);
                fechaInicioChange2 = fechaInicioChange2 + fechaInicioChange3;
                Filtro.FechaInicio = fechaInicioChange2;

                var fechaFinChange = Filtro.FechaFin?.ToString("yyyy-MM-dd");
                var fechaFinChange2 = Convert.ToDateTime(fechaFinChange);
                TimeSpan fechaFinChange3 = new TimeSpan(23, 59, 59);
                fechaFinChange2 = fechaFinChange2 + fechaFinChange3;
                Filtro.FechaFin = fechaFinChange2;


                var listaEtapas=_repProcesoSeleccion.ObtenerReporteAnalisisProcesoSeleccion(Filtro).OrderBy(x=>x.OrdenEtapa).ToList();
                List<ReporteAnalisisProcesoSeleccionPorcentajeDTO> listaPorcentaje = new List<ReporteAnalisisProcesoSeleccionPorcentajeDTO>();

                for (var i = 0; i < listaEtapas.Count; i++) {
                    if (i != 0)
                    {
                        listaEtapas[i].NumeroPostulante = listaEtapas[i - 1].Aprobados;
                    }

                    ReporteAnalisisProcesoSeleccionPorcentajeDTO PorcentajeEtapaReporte = new ReporteAnalisisProcesoSeleccionPorcentajeDTO();
                    PorcentajeEtapaReporte.IdEtapa = listaEtapas[i].IdEtapa;
                    PorcentajeEtapaReporte.IdProcesoSeleccion = listaEtapas[i].IdProcesoSeleccion;
                    PorcentajeEtapaReporte.IdProveedor = listaEtapas[i].IdProveedor;
                    PorcentajeEtapaReporte.NombreEtapa = listaEtapas[i].NombreEtapa;
                    PorcentajeEtapaReporte.Proveedor = listaEtapas[i].Proveedor;
                    PorcentajeEtapaReporte.OrdenEtapa = listaEtapas[i].OrdenEtapa;
                    PorcentajeEtapaReporte.Contactados = Math.Round((listaEtapas[i].Contactados * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.Aprobados = Math.Round((listaEtapas[i].Aprobados * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.Desaprobados = Math.Round((listaEtapas[i].Desaprobados * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.EnProceso = Math.Round((listaEtapas[i].EnProceso * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.Abandonados = Math.Round((listaEtapas[i].Abandonados* 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.SinRendir = Math.Round((listaEtapas[i].SinRendir* 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    if (listaEtapas[i].OrdenEtapa == 1)
                    {
                        PorcentajeEtapaReporte.NumeroPostulante = Math.Round((listaEtapas[i].NumeroPostulante * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    }
                    else
                    {
                        PorcentajeEtapaReporte.NumeroPostulante = Math.Round((listaEtapas[i].NumeroPostulante* 100.0M) / (listaEtapas[i - 1].NumeroPostulante == 0 ? 1.0M : listaEtapas[i - 1].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    }
                    listaPorcentaje.Add(PorcentajeEtapaReporte);
                }
                return Ok(new { listaEtapas,listaEtapasPorcentaje= listaPorcentaje });
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte_V2([FromBody] FiltroAnalisisProcesoSeleccionDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (Filtro.FechaInicio == null || Filtro.FechaFin == null)
                {
                    Filtro.FechaFin = DateTime.Now;
                    Filtro.FechaInicio = new DateTime(1900, 12, 31);
                }
                var listaEtapas = _repProcesoSeleccion.ObtenerReporteAnalisisProcesoSeleccion_V2(Filtro).ToList();

                List<ReporteAnalisisProcesoSeleccionPorcentajeDTO> listaPorcentaje = new List<ReporteAnalisisProcesoSeleccionPorcentajeDTO>();
                for (var i = 0; i < listaEtapas.Count; i++)
                {
                    if (listaEtapas[i].OrdenEtapa!=1)
                    {
                        listaEtapas[i].NumeroPostulante = listaEtapas[i - 1].Aprobados;
                    }
                    ReporteAnalisisProcesoSeleccionPorcentajeDTO PorcentajeEtapaReporte = new ReporteAnalisisProcesoSeleccionPorcentajeDTO();
                    PorcentajeEtapaReporte.IdEtapa = listaEtapas[i].IdEtapa;
                    PorcentajeEtapaReporte.IdProcesoSeleccion = listaEtapas[i].IdProcesoSeleccion;
                    PorcentajeEtapaReporte.IdProveedor = listaEtapas[i].IdProveedor;
                    PorcentajeEtapaReporte.NombreEtapa = listaEtapas[i].NombreEtapa;
                    PorcentajeEtapaReporte.Proveedor = listaEtapas[i].Proveedor;
                    PorcentajeEtapaReporte.OrdenEtapa = listaEtapas[i].OrdenEtapa;
                    PorcentajeEtapaReporte.Contactados = Math.Round((listaEtapas[i].Contactados*100.0M ) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.Aprobados = Math.Round((listaEtapas[i].Aprobados* 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.Desaprobados = Math.Round((listaEtapas[i].Desaprobados * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.EnProceso = Math.Round((listaEtapas[i].EnProceso*100) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.Abandonados = Math.Round((listaEtapas[i].Abandonados* 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.SinRendir = Math.Round((listaEtapas[i].SinRendir* 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    if (listaEtapas[i].OrdenEtapa == 1)
                    {
                        PorcentajeEtapaReporte.NumeroPostulante = Math.Round((listaEtapas[i].NumeroPostulante* 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    }
                    else
                    {
                        PorcentajeEtapaReporte.NumeroPostulante = Math.Round((listaEtapas[i].NumeroPostulante * 100.0M) / (listaEtapas[i - 1].NumeroPostulante == 0 ? 1.0M : listaEtapas[i - 1].NumeroPostulante*1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    }
                    listaPorcentaje.Add(PorcentajeEtapaReporte);

                }
                return Ok( new { listaEtapasAgrupadas = listaEtapas , listaEtapasAgrupadasPorcentaje= listaPorcentaje });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}