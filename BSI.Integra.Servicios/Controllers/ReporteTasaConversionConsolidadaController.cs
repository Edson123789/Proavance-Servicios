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
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteTasaConversionConsolidadaController
    /// Autor: Edgar S.
    /// Fecha: 15/03/2021
    /// <summary>
    /// Gestión de Reporte de Tasa de Conversion Consolidada
    /// </summary>
    [Route("api/ReporteTasaConversionConsolidada")]
    [ApiController]
    public class ReporteTasaConversionConsolidadaController : ControllerBase
    {
        public string ReporteRepositorio { get; private set; }
        private readonly integraDBContext _integraDBContext;

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 15/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos para reporte de Tasas de Conversión Consolidada
        /// </summary>
        /// <returns> Objeto DTO: ReporteTasaConversionConsolidadaGeneralDTO </returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosReporteTasaConversionConsolidada(int IdPersonal)
        {
            try
            {
                PersonalRepositorio repPersonal = new PersonalRepositorio();

                ReporteTasaConversionConsolidadaGeneralDTO result = new ReporteTasaConversionConsolidadaGeneralDTO();
                result.Asesores = repPersonal.ObtenerAsesoresVentasOficial();
                result.Coordinadores = repPersonal.ObtenerCoordinadoresVentasOficial();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 15/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos para Área
        /// </summary>
        /// <returns> Lista de Objetos: int,string </returns>
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

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 15/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos para SubArea
        /// </summary>
        /// <returns> Lista de Objetos: int,string,int </returns>
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

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 15/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos Programa General
        /// </summary>
        /// <returns> Objeto: int,string,int </returns>
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

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 15/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos Programa Especifico
        /// </summary>
        /// <returns> Lista de ObjetoBO : PespecificoBO </returns>
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

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 15/03/2021
        /// Versión: 1.0
        /// <summary>
        /// /*Agregado Lis para buscar asesores correspondientes por coordinadores*/
        /// </summary>
        /// <returns> Objeto DTO: List<FiltroDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult DarPeriodoActual()
        {
            return Ok((new PeriodoRepositorio()).ObtenerIdPeriodoFechaActual());
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 15/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combos para reporte
        /// </summary>
        /// <returns> Objeto DTO: ReporteTasaConversionConsolidadaGeneralDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosReporte()
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();

                ReporteTasaConversionConsolidadaGeneralDTO resultado = new ReporteTasaConversionConsolidadaGeneralDTO();
                resultado.Coordinadores = _repPersonal.ObtenerCoordinadoresVentasOficial();
                resultado.Asesores = _repPersonal.ObtenerAsesoresVentasOficial();
                resultado.Periodos = _repPeriodo.ObtenerPeriodos();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 15/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del personal segun el id enviado
        /// </summary>
        /// <returns> objeto : PersonalInformacionAgendaDTO </returns>
        [Route("[action]/{IdPersonal}")]
        [HttpPost]
        public IActionResult GenerarAsesoresCoordinadores( int idPersonal)
        {
            return (new PersonalController(_integraDBContext)).ObtenerDatosPersonal(idPersonal);
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 15/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Gráficas
        /// </summary>
        /// <returns> objeto : ReporteTasaConversionConsolidadaGraficasVistaDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteGraficas([FromBody] ReporteTasaConversionConsolidadaGraficaFiltroDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                string coordinadores = ListIntToString(Filtros.Coordinadores);
                string asesores = ListIntToString(Filtros.Asesores);
                string periodoInicio = Filtros.PeriodoInicio;
                string periodoFin = Filtros.PeriodoFin;
                try
                {
                    if(Filtros.TipoPeriodo == 2)
                    {
                        Reportes reportes = new Reportes();
                        var listRpta = reportes.ReporteTasaConversionConsolidadoAsesoresGraficaMensual(coordinadores, asesores, periodoInicio, periodoFin);
                        listRpta.Consolidado = listRpta.Consolidado.OrderBy(x => x.Ano).ThenBy(x => x.MesNumero).ToList();
                        return Ok(new { Records = listRpta });
                    }
                    else
                    {
                        Reportes reportes = new Reportes();
                        var listRpta = reportes.ReporteTasaConversionConsolidadoAsesoresGrafica(coordinadores, asesores, periodoInicio, periodoFin);

                        return Ok(new { Records = listRpta });
                    }                    
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

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 15/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte /*fin agregados para graficas*/
        /// </summary>
        /// <returns> Objeto Agrupado </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteTasaConversionConsolidadaFiltroDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                string _area = ListIntToString(Filtros.areas);
                string _subarea = ListIntToString(Filtros.subareas);
                string _pgeneral = ListIntToString(Filtros.pgeneral);
                string _pespecifico = ListIntToString(Filtros.pespecifico);
                string _modalidades = ListStringToString(Filtros.modalidades);
                string _ciudades = ListStringToString(Filtros.ciudades);
                string _coordinadores = ListIntToString(Filtros.coordinadores);
                string _asesores = ListIntToString(Filtros.asesores);
                Filtros.FechaInicio = Convert.ToDateTime(Filtros.FechaInicio).Date;
                Filtros.FechaFin = Convert.ToDateTime(Filtros.FechaFin).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                try
                {
                    Reportes reportes = new Reportes();
                    var listRpta = reportes.ReporteTasaConversionConsolidadoAsesores(_area, _subarea, _pgeneral, _pespecifico, _modalidades, _ciudades, Filtros.fecha, _coordinadores, _asesores, Filtros.FechaInicio.Value, Filtros.FechaFin.Value);
                    var centroCostoOportunidades = reportes.ObtenerCentroCostoPorAsesor(_area, _subarea, _pgeneral, _pespecifico, _modalidades, _ciudades, Filtros.fecha, _coordinadores, _asesores, Filtros.FechaInicio.Value, Filtros.FechaFin.Value);
                    var agrupado = (from p in listRpta.Consolidado
                                    group p by p.probabilidadDesc into grupo
                                    select new { g = grupo.Key, l = grupo }).ToList();
                    var agrupado2 = (from p in listRpta.Desagregado
                                     group p by p.probabilidadDesc into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();
                    var agrupado3 = (from p in centroCostoOportunidades
                                     group p by new
                                     {
                                         p.idasesor
                                     }
                                     into grupo
                                     select new TCRM_CentroCostoByAsesorgrupadoDTO
                                     {
                                         idasesor = grupo.Key.idasesor,
                                         ingresoReal = grupo.Sum(w => w.ingresoReal),
                                         ingresoMes = grupo.Sum(w => w.ingresoMes),
                                         DescuentoPromedio = grupo.Sum(w => w.oportunidadesOCAnyIS) == 0 ? 0 : grupo.Sum(w => w.Descuento) / grupo.Sum(w => w.oportunidadesOCAnyIS),
                                         precioPromedio = grupo.Sum(w => w.precioListaFinal) / grupo.Sum(w => w.oportunidadesOCTotal),
                                         oportunidadesOCAnyIS = grupo.Sum(w => w.oportunidadesOCAnyIS),
                                         oportunidadesOCTotal = grupo.Sum(w => w.oportunidadesOCTotal),
                                         estadoAsesor = grupo.Max(w => w.estadoAsesor)
                                     }).ToList();


                    return Ok(new { Records = agrupado, Records2 = agrupado2, Records3 = agrupado3, Records5= centroCostoOportunidades });
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

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 15/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Reporte de Tasas , es una version igual al reporte de tasa de conversion consolidada , con la diferencia que muestra  desde la grilla de categoria dato consolidado
        /// </summary>
        /// <returns> Objeto Agrupado </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteTasas([FromBody] ReporteTasaConversionConsolidadaFiltroDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                string _area = ListIntToString(Filtros.areas);
                string _subarea = ListIntToString(Filtros.subareas);
                string _pgeneral = ListIntToString(Filtros.pgeneral);
                string _pespecifico = ListIntToString(Filtros.pespecifico);
                string _modalidades = ListStringToString(Filtros.modalidades);
                string _ciudades = ListStringToString(Filtros.ciudades);
                string _coordinadores = ListIntToString(Filtros.coordinadores);
                string _asesores = ListIntToString(Filtros.asesores);
                Filtros.FechaInicio = Convert.ToDateTime(Filtros.FechaInicio).Date;
                Filtros.FechaFin = Convert.ToDateTime(Filtros.FechaFin).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                try
                {
                    Reportes reportes = new Reportes();
                    var listRpta = reportes.ReporteTasaConversionConsolidadoAsesores(_area, _subarea, _pgeneral, _pespecifico, _modalidades, _ciudades, Filtros.fecha, _coordinadores, _asesores, Filtros.FechaInicio.Value, Filtros.FechaFin.Value);
                    var centroCostoOportunidades = reportes.ObtenerCentroCostoPorAsesor(_area, _subarea, _pgeneral, _pespecifico, _modalidades, _ciudades, Filtros.fecha, _coordinadores, _asesores, Filtros.FechaInicio.Value, Filtros.FechaFin.Value);

                    var agrupado = (from p in listRpta.Consolidado
                                    group p by p.probabilidadDesc into grupo
                                    select new { g = grupo.Key, l = grupo }).ToList();
                    var agrupado2 = (from p in listRpta.Desagregado
                                     group p by p.probabilidadDesc into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();
                    var agrupado3 = (from p in centroCostoOportunidades
                                     group p by new
                                     {
                                         p.idasesor
                                     }
                                     into grupo
                                     select new TCRM_CentroCostoByAsesorgrupadoDTO
                                     {
                                         idasesor = grupo.Key.idasesor,
                                         ingresoReal = grupo.Sum(w => w.ingresoReal),
                                         ingresoMes = grupo.Sum(w => w.ingresoMes),
                                         DescuentoPromedio = grupo.Sum(w => w.oportunidadesOCAnyIS) == 0 ? 0 : grupo.Sum(w => w.Descuento) / grupo.Sum(w => w.oportunidadesOCAnyIS),
                                         precioPromedio = grupo.Sum(w => w.precioListaFinal) / grupo.Sum(w => w.oportunidadesOCTotal),
                                         oportunidadesOCAnyIS = grupo.Sum(w => w.oportunidadesOCAnyIS),
                                         oportunidadesOCTotal = grupo.Sum(w => w.oportunidadesOCTotal),
                                         estadoAsesor = grupo.Max(w => w.estadoAsesor)
                                     }).ToList();

                    return Ok(new { Records = agrupado, Records2 = agrupado2, Records3 = agrupado3, Records5 = centroCostoOportunidades });
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

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 15/03/2021
        /// Versión: 1.0
        /// <summary>
        ///Genera Detalle de Reporte
        /// </summary>
        /// <returns> Lista de ObjetoDTO : List<TCRM_CentroCostoByAsesorDetallesDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteDetalles([FromBody] ReporteTasaConversionConsolidadaFiltroDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                string _area = ListIntToString(Filtros.areas);
                string _subarea = ListIntToString(Filtros.subareas);
                string _pgeneral = ListIntToString(Filtros.pgeneral);
                string _pespecifico = ListIntToString(Filtros.pespecifico);
                string _modalidades = ListStringToString(Filtros.modalidades);
                string _ciudades = ListStringToString(Filtros.ciudades);
                string _coordinadores = ListIntToString(Filtros.coordinadores);
                string _asesores = ListIntToString(Filtros.asesores);
                Filtros.FechaInicio = Convert.ToDateTime(Filtros.FechaInicio).Date;
                Filtros.FechaFin = Convert.ToDateTime(Filtros.FechaFin).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                try
                {
                    Reportes reportes = new Reportes();
                    var centroCostoOportunidadesdetalles = reportes.ObtenerCentroCostoPorAsesorDetalles(_area, _subarea, _pgeneral, _pespecifico, _modalidades, _ciudades, Filtros.fecha, _coordinadores, _asesores, Filtros.FechaInicio.Value, Filtros.FechaFin.Value);

                    return Ok(new { Records = centroCostoOportunidadesdetalles });
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
        /// Controlador: ProcesarReporte
        /// <summary> 
        /// Autor: Jashin Salazar 
        /// Fecha: 22/04/2021 
        /// Descripción: Procesa el calculo de Reporte Tasa de Conversion Consolidada  
        /// </summary> 
        [Route("[action]")]
        [HttpGet]
        public ActionResult ProcesarReporte()
        {
            try
            {
                DateTime horaInicio = DateTime.Now;
                Reportes reporte = new Reportes();
                var resultado = reporte.CalculoReporteTasaConversionConsolidada();
                if (resultado.Valor == 1)
                {
                    //enviarCorreo
                    var correosPersonalizados = new List<string>
                    {
                        "jsalazart@bsginstitute.com"
                    };
                    var MailservicePersonalizado = new TMK_MailServiceImpl();
                    var mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = "jsalazart@bsginstitute.com",
                        Recipient = string.Join(",", correosPersonalizados),
                        Subject = string.Concat("Exito al Procesar Calculo de Reporte Tasa de Conversion Consolidada"),
                        Message = $@"
                        <p style='color: red;'><strong>----Servicio de confirmación de Reporte Tasa de Conversion Consolidada----</strong></p>
                        <p>Se proceso correctamente el calculo de Reporte Tasa de Conversion Consolidada</span></strong></p>
                        <p><strong>Hora de Ejecucion:</strong></p>
                        <p><span style='color: orange;'>{horaInicio}</span></p>
                        <p><strong>Hora de Finalizacion:</strong></p>
                        <p><span style='color: orange;'>{DateTime.Now}</span></p> 
                        ",
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };
                    MailservicePersonalizado.SetData(mailDataPersonalizado);
                    MailservicePersonalizado.SendMessageTask();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                List<string> correos = new List<string>
                {
                    "sistemas@bsginstitute.com"
                };
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "sistemas@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Error al Procesar Calculo de Reporte Tasa de Conversion Consolidada"),
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(ex),
                    $@"
                        <p style='color: red;'><strong>----Servicio de confirmación de de Reporte Tasa de Conversion Consolidada----</strong></p>
                    "
                    ),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();

                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: 
        /// Autor: _ _ _ _ _ _ _.
        /// Fecha: 17/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Convierte lista de int en string
        /// </summary>
        /// <returns> string </returns>
        private string ListIntToString(IList<int> Datos)
        {
            if (Datos == null)
                Datos = new List<int>();
            int numeroElementos = Datos.Count;
            string rptaCadena = string.Empty;
            for (int i = 0; i < numeroElementos - 1; i++)
                rptaCadena += Convert.ToString(Datos[i]) + ",";
            if (numeroElementos > 0)
                rptaCadena += Convert.ToString(Datos[numeroElementos - 1]);
            return rptaCadena;
        }

        /// <summary>
        /// Convierte lista de Id string en una sola cadena string
        /// </summary>
        /// <returns> string </returns>
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
