using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Reportes.Comercial;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteOperaciones")]
    [ApiController]
    public class ReporteOperacionesController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PersonalRepositorio _repPersonal;
        public ReporteOperacionesController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repPersonal = new PersonalRepositorio(_integraDBContext);
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 08/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la informacion para llenar los combos de las personas  y los paises
        /// </summary>
        /// <returns>ActionResult con estado 200, 400</returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombos(int IdPersonal)
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                //var personalAsignado = _repPersonal.ObtenerPersonalAsesoresOperacionesPorIdPersonal(IdPersonal);
                var asistentes = _repPersonal.ObtenerPersonalAsignadoOperacionesTotal(IdPersonal);
                var personalActivo= _repPersonal.ObtenerPersonalAsesoresOperacionesActivos();

                var asistentesActivos = asistentes.Where(w => w.Activo == true).ToList();
                var asistentesInactivos = asistentes.Where(w => w.Activo == false).ToList();


                var listaPais = _repPais.ObtenerListaPais();

                var filtros = new {
                    listaPersonal = asistentes,
                    listaPersonalActivo = asistentesActivos,
                    listaPersonalInactivo = asistentesInactivos,
                    listaPais,
                    personalActivo
                };
                return Ok(filtros);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 08/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la informacion para llenar los combos de las personas  y los paises
        /// </summary>
        /// <returns>ActionResult con estado 200, 400</returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosReporteOperacionesv2(int IdPersonal)
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                //var personalAsignado = _repPersonal.ObtenerPersonalAsesoresOperacionesPorIdPersonal(IdPersonal);
                var asistentes = _repPersonal.ObtenerPersonalAsignadoOperacionesTotalV2(IdPersonal);
                
                var listaPais = _repPais.ObtenerListaPais();
                var filtros = new
                {
                    listaPersonal = asistentes,
                    listaPais
                };
                return Ok(filtros);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 08/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la informacion para llenar los combos de las personas  y los paises
        /// </summary>
        /// <returns>ActionResult con estado 200, 400</returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosEstadoAlumno(int IdPersonal)
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);                                
                var asistentes = _repPersonal.ObtenerPersonalAsignadoOperacionesTotalV2(IdPersonal);
                var personalActivo = _repPersonal.ObtenerPersonalAsesoresOperacionesActivos();

                var filtros = new
                {
                    listaPersonal = asistentes,
                    personalActivo
                };
                return Ok(filtros);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteOperacionesFiltroDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reportes = new Reportes();
                return Ok(reportes.GenerarReporteOperaciones(Filtros));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteDetallesAsignacionCoordinadora([FromBody] ReporteTasaConversionConsolidadaFiltroDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                try
                {
                    Reportes reportes = new Reportes();
                    List<ReporteOperacionesDetallesAsignacionCoordinadoraDTO> centroCostoOportunidadesdetalles = new List<ReporteOperacionesDetallesAsignacionCoordinadoraDTO>();
                    List<ReporteOperacionesDetallesAsignacionCoordinadoraEstadosDTO> centroCostoOportunidadesdetallesEstados = new List<ReporteOperacionesDetallesAsignacionCoordinadoraEstadosDTO>();
                    if (Filtros.coordinadores.Count() == 0)
                    {
                        var asistentesCargo = _repPersonal.ObtenerPersonalAsignadoOperacionesTotalV2(Filtros.Personal);
                        List<int> ListaCoordinadortmp = new List<int>();
                        foreach (var item in asistentesCargo)
                        {
                            ListaCoordinadortmp.Add(item.Id);
                        }
                        Filtros.coordinadores = ListaCoordinadortmp;
                        string _coordinadores = ListIntToString(Filtros.coordinadores);

                        centroCostoOportunidadesdetalles = reportes.GenerarReporteOperacionesDetallesAsignacionCoordinadora(_coordinadores, Filtros);
                        centroCostoOportunidadesdetallesEstados = reportes.GenerarReporteOperacionesDetallesAsignacionCoordinadoraEstados(_coordinadores, Filtros);
                    }
                    else
                    {
                        string _coordinadores = ListIntToString(Filtros.coordinadores);
                        centroCostoOportunidadesdetalles = reportes.GenerarReporteOperacionesDetallesAsignacionCoordinadora(_coordinadores, Filtros);
                        centroCostoOportunidadesdetallesEstados = reportes.GenerarReporteOperacionesDetallesAsignacionCoordinadoraEstados(_coordinadores, Filtros);
                    }

                    

                    return Ok(new { Records = centroCostoOportunidadesdetalles,Records2 = centroCostoOportunidadesdetallesEstados });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteControlContactoTelefonico([FromBody] ReporteTasaConversionConsolidadaFiltroDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string _coordinadores = ListIntToString(Filtros.coordinadores);
                try
                {
                    Reportes reportes = new Reportes();
                    var ControlContactoDetalles = reportes.GenerarReporteControlContactoTelefonico(_coordinadores, Filtros).OrderBy(w => w.Orden).ToList();

                    return Ok(new { Records = ControlContactoDetalles });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteEstadoAlumnos([FromBody] ReporteTasaConversionConsolidadaFiltroDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {                
                try
                {
                    Reportes reportes = new Reportes();
                    ReporteEstadosAlumnosDTO reporte = new ReporteEstadosAlumnosDTO();

                    if (Filtros.coordinadores.Count() == 0)
                    {
                        var asistentesCargo = _repPersonal.ObtenerPersonalAsignadoOperacionesTotalV2(Filtros.Personal);
                        List<int> ListaCoordinadortmp = new List<int>();
                        foreach (var item in asistentesCargo)
                        {
                            ListaCoordinadortmp.Add(item.Id);
                        }
                        Filtros.coordinadores = ListaCoordinadortmp;
                        string _coordinadores = ListIntToString(Filtros.coordinadores);
                        //PAGOS//PRESENCIAL-ONLINE-AONLINE
                        var RespuestaEstadoAlumnosGeneral = reportes.GenerarReporteEstadoAlumnosPagos(_coordinadores, Filtros).ToList();
                        var RespuestaEstadoAlumnosAgrupado = reportes.GenerarReporteEstadoAlumnoAgrupadoPagos(RespuestaEstadoAlumnosGeneral);

                        //AONLINE
                        var RespuestaEstadoAlumnosAonlineGeneral = reportes.GenerarReporteEstadoAlumnos2(_coordinadores, Filtros).ToList();
                        var RespuestaEstadoAlumnosAonlineAgrupado = reportes.GenerarReporteEstadoAlumnoAgrupadoAonline(RespuestaEstadoAlumnosAonlineGeneral);

                        //AONLINE//ACADEMICOVSPAGOS
                        var RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosGeneral = reportes.GenerarReporteEstadoAlumnosAvanceAcademicoVSPagos(_coordinadores, Filtros).ToList();
                        var RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosAgrupado = reportes.GenerarReporteEstadoAlumnoAgrupadoAonlineAvanceAcademicoVSPagos(RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosGeneral);

                        //PRESENCIAL-ONLINE-AONLINE//ALUMNOSPAGOSATRASADOS
                        var RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosGeneral = reportes.GenerarReporteEstadoAlumnosPagosAtrasados(_coordinadores, Filtros).ToList();
                        var RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosAgrupado = reportes.GenerarReporteEstadoAlumnoAgrupadoAonlineAlumnosPagosAtrasados(RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosGeneral);
                                               
                        reporte.ReporteAvanceAcademicoPagos = RespuestaEstadoAlumnosAgrupado;
                        reporte.ReporteAvanceAcademicoAonline = RespuestaEstadoAlumnosAonlineAgrupado;
                        reporte.ReporteAvanceAcademicoVSPagosAonline = RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosAgrupado;
                        reporte.ReporteAvanceAcademicoAlumnosPagoAtrasado = RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosAgrupado;
                    }
                    else
                    {
                        string _coordinadores = ListIntToString(Filtros.coordinadores);
                        //PAGOS//PRESENCIAL-ONLINE-AONLINE
                        var RespuestaEstadoAlumnosGeneral = reportes.GenerarReporteEstadoAlumnosPagos(_coordinadores, Filtros).ToList();
                        var RespuestaEstadoAlumnosAgrupado = reportes.GenerarReporteEstadoAlumnoAgrupadoPagos(RespuestaEstadoAlumnosGeneral);

                        //AONLINE
                        var RespuestaEstadoAlumnosAonlineGeneral = reportes.GenerarReporteEstadoAlumnos2(_coordinadores, Filtros).ToList();
                        var RespuestaEstadoAlumnosAonlineAgrupado = reportes.GenerarReporteEstadoAlumnoAgrupadoAonline(RespuestaEstadoAlumnosAonlineGeneral);

                        //AONLINE//ACADEMICOVSPAGOS
                        var RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosGeneral = reportes.GenerarReporteEstadoAlumnosAvanceAcademicoVSPagos(_coordinadores, Filtros).ToList();
                        var RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosAgrupado = reportes.GenerarReporteEstadoAlumnoAgrupadoAonlineAvanceAcademicoVSPagos(RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosGeneral);

                        //PRESENCIAL-ONLINE-AONLINE//ALUMNOSPAGOSATRASADOS
                        var RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosGeneral = reportes.GenerarReporteEstadoAlumnosPagosAtrasados(_coordinadores, Filtros).ToList();
                        var RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosAgrupado = reportes.GenerarReporteEstadoAlumnoAgrupadoAonlineAlumnosPagosAtrasados(RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosGeneral);


                        
                        reporte.ReporteAvanceAcademicoPagos = RespuestaEstadoAlumnosAgrupado;
                        reporte.ReporteAvanceAcademicoAonline = RespuestaEstadoAlumnosAonlineAgrupado;
                        reporte.ReporteAvanceAcademicoVSPagosAonline = RespuestaEstadoAlumnosAonlineAvanceAcademicoVSPagosAgrupado;
                        reporte.ReporteAvanceAcademicoAlumnosPagoAtrasado = RespuestaEstadoAlumnosAonlineAlumnosPagosAtrasadosAgrupado;
                    }
                    

                    return Ok(new { Records = reporte });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteIndicadoresOperativos([FromBody] ReporteTasaConversionConsolidadaFiltroDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {                
                try
                {
                    Reportes reportes = new Reportes();
                    ReporteIndicadoresOperativosFinalDTO reporte = new ReporteIndicadoresOperativosFinalDTO();

                    if (Filtros.coordinadores.Count() == 0)
                    {
                        var asistentesCargo = _repPersonal.ObtenerPersonalAsignadoOperacionesTotal(Filtros.Personal);
                        List<int> ListaCoordinadortmp = new List<int>();
                        foreach (var item in asistentesCargo)
                        {
                            ListaCoordinadortmp.Add(item.Id);
                        }
                        Filtros.coordinadores = ListaCoordinadortmp;
                        string _coordinadores = ListIntToString(Filtros.coordinadores);

                        //General
                        var RespuestaIndicadoresOperativos = reportes.GenerarReporteIndicadoresOperativos(_coordinadores, Filtros).ToList();
                        var RespuestaIndicadoresOperativosAgrupado = reportes.GenerarReporteIndicadoresOperativosAgrupado(RespuestaIndicadoresOperativos);

                        //Por Dia Coordinadora
                        var RespuestaIndicadoresOperativosPorDiaCoordinadora = reportes.GenerarReporteIndicadoresOperativosPorDiaCoordinadora(_coordinadores, Filtros).ToList();

                        var coordinadores = RespuestaIndicadoresOperativosPorDiaCoordinadora.GroupBy(x => x.Coordinadora);
                        var RespuestaIndicadoresOperativosAgrupadoPorDia = new List<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoNuevoDTO>();
                        var ListaCoordinadoras = new List<String>();
                        foreach (var item in coordinadores)
                        {
                            RespuestaIndicadoresOperativosAgrupadoPorDia.AddRange(reportes.GenerarReporteIndicadoresOperativosPorDiaCoordinadoraAgrupadoVersion2(item));
                            ListaCoordinadoras.Add(item.Key);
                        }
                        reporte.ReporteIndicadoresOperativos = RespuestaIndicadoresOperativosAgrupado;
                        reporte.ReporteIndicadoresOperativosAgrupadoPorDia = RespuestaIndicadoresOperativosAgrupadoPorDia;
                        reporte.Coordinadoras = ListaCoordinadoras;
                    }
                    else
                    {
                        string _coordinadores = ListIntToString(Filtros.coordinadores);
                        //General
                        var RespuestaIndicadoresOperativos = reportes.GenerarReporteIndicadoresOperativos(_coordinadores, Filtros).ToList();
                        var RespuestaIndicadoresOperativosAgrupado = reportes.GenerarReporteIndicadoresOperativosAgrupado(RespuestaIndicadoresOperativos);

                        //Por Dia Coordinadora
                        var RespuestaIndicadoresOperativosPorDiaCoordinadora = reportes.GenerarReporteIndicadoresOperativosPorDiaCoordinadora(_coordinadores, Filtros).ToList();

                        var coordinadores = RespuestaIndicadoresOperativosPorDiaCoordinadora.GroupBy(x => x.Coordinadora);
                        var RespuestaIndicadoresOperativosAgrupadoPorDia = new List<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoNuevoDTO>();
                        var ListaCoordinadoras = new List<String>();
                        foreach (var item in coordinadores)
                        {
                            RespuestaIndicadoresOperativosAgrupadoPorDia.AddRange(reportes.GenerarReporteIndicadoresOperativosPorDiaCoordinadoraAgrupadoVersion2(item));
                            ListaCoordinadoras.Add(item.Key);
                        }
                        reporte.ReporteIndicadoresOperativos = RespuestaIndicadoresOperativosAgrupado;
                        reporte.ReporteIndicadoresOperativosAgrupadoPorDia = RespuestaIndicadoresOperativosAgrupadoPorDia;
                        reporte.Coordinadoras = ListaCoordinadoras;
                    }
                    

                    return Ok(new { Records = reporte });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
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
    }
}
