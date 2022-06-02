using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteIndicadoresProductividadVentas")]
    public class ReporteIndicadoresProductividadVentasController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteIndicadoresProductividadVentasController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReportePendienteFiltroDTO FiltroPendiente)
        {
            try
            {
                PersonalRepositorio repPersonal = new PersonalRepositorio(_integraDBContext);
                ModalidadCursoRepositorio repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);
                IntegraAspNetUsersRepositorio repIntegraUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);

                RegistroMarcadorFechaBO reporteCronogramaGeneral = new RegistroMarcadorFechaBO();
                var respuestaGeneral = reporteCronogramaGeneral.GenerarReporteIndicadoresProductividadVentasGeneral(FiltroPendiente);

                RegistroMarcadorFechaBO reporteIndicadoresProductividad = new RegistroMarcadorFechaBO();                
                var agrupado = reporteIndicadoresProductividad.GenerarReporteIndicadoresHorasTrabajadasVentas(respuestaGeneral);

                //RegistroMarcadorFechaBO reporteIndicadoresProductividad2 = new RegistroMarcadorFechaBO();
                //var agrupado2 = reporteIndicadoresProductividad2.GenerarReporteIndicadoresProductividadVentas(respuestaGeneral);

                RegistroMarcadorFechaBO reporteIndicadoresProductividad3 = new RegistroMarcadorFechaBO();
                var agrupado3 = reporteIndicadoresProductividad3.GenerarReporteHorasTrabajadasProductividadEquipoVentas(respuestaGeneral);

                RegistroMarcadorFechaBO reporteIndicadoresProductividad4 = new RegistroMarcadorFechaBO();
                var listRpta = reporteIndicadoresProductividad4.GenerarReporteIndicadoresProductividadIndicadoresVentas(respuestaGeneral);
                var agrupado4 = (from p in listRpta
                                group p by p.Periodo into grupo
                                select new ReporteProductividadVentas { g = grupo.Key, l = grupo.ToList() });


                ReporteIndicadoresProductividadVentasCompuestoDTO reporte = new ReporteIndicadoresProductividadVentasCompuestoDTO();
                reporte.ReporteProductividadHorasTrabajadas = agrupado.ToList();
                //reporte.ReporteProductividadToTalVendido = agrupado2.ToList();
                reporte.ReporteHorasTrabajadasProductividadEquipo = agrupado3.ToList();
                reporte.ReporteIndicadoresProductividad = agrupado4.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}