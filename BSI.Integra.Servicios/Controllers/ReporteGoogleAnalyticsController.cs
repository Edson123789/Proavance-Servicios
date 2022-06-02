using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Servicios.DTOs;
using BSI.Integra.Servicios.DTOs.Reportes;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201809;
using Google.Apis.AnalyticsReporting.v4;
using Google.Apis.AnalyticsReporting.v4.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteGoogleAnalytics")]
    [ApiController]
    public class ReporteGoogleAnalyticsController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public ReporteGoogleAnalyticsController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]/{IdModulo}")]
        [HttpGet]
        public ActionResult ObtenerComboOperadorComparacion(int IdModulo)
        {
            try
            {
                OperadorComparacionRepositorio operadorComparacionRepositorio = new OperadorComparacionRepositorio(_integraDBContext);
                var result = operadorComparacionRepositorio.ObtenerListaOperadorComparacionPorNombreModulo(IdModulo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboReporteAnalyticsFiltro()
        {
            try
            {
                ReporteAnalyticsFiltroRepositorio reporteAnalyticsFiltroRepositorio = new ReporteAnalyticsFiltroRepositorio(_integraDBContext);

                ReporteAnalyticsFiltroDTO nuevo = new ReporteAnalyticsFiltroDTO
                {
                    Id = 0,
                    Nombre = "Nuevo"
                };
                List<ReporteAnalyticsFiltroDTO> lista = new List<ReporteAnalyticsFiltroDTO>();
                lista.Add(nuevo);

                List<ReporteAnalyticsFiltroDTO> listaBase = (reporteAnalyticsFiltroRepositorio.GetBy(x => true, y => new ReporteAnalyticsFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre
                }).ToList());

                lista.AddRange(listaBase);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdReporteAnalyticsFiltro}")]
        [HttpGet]
        public ActionResult ObtenerDetalle(int IdReporteAnalyticsFiltro)
        {
            try
            {
                ReporteAnalyticsFiltroDetalleRepositorio reporteAnalyticsFiltroDetalleRepositorio = new ReporteAnalyticsFiltroDetalleRepositorio(_integraDBContext);
                List<ReporteAnalyticsFiltroDetalleDTO> lista = reporteAnalyticsFiltroDetalleRepositorio.GetBy(x => x.IdReporteAnalyticsFiltro == IdReporteAnalyticsFiltro, y => new ReporteAnalyticsFiltroDetalleDTO
                {
                    Id = y.Id,
                    Texto = y.Texto,
                    Excluir = y.Excluir,
                    IdOperadorComparacion = y.IdOperadorComparacion
                }).ToList();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarFiltro([FromBody] ReporteAnalyticsFiltroDTO reporteAnalyticsFiltroDTO)
        {
            try
            {
                ReporteAnalyticsFiltroRepositorio reporteAnalyticsFiltroRepositorio = new ReporteAnalyticsFiltroRepositorio(_integraDBContext);
                ReporteAnalyticsFiltroDetalleRepositorio reporteAnalyticsFiltroDetalleRepositorio = new ReporteAnalyticsFiltroDetalleRepositorio(_integraDBContext);
                ReporteAnalyticsFiltroBO reporteAnalyticsFiltroBO;
                ReporteAnalyticsFiltroDetalleBO reporteAnalyticsFiltroDetalleBO;

                using (TransactionScope scope = new TransactionScope())
                {
                    if (reporteAnalyticsFiltroDTO.Id == 0)
                    {
                        reporteAnalyticsFiltroBO = new ReporteAnalyticsFiltroBO();
                        reporteAnalyticsFiltroBO.Nombre = reporteAnalyticsFiltroDTO.Nombre;
                        reporteAnalyticsFiltroBO.Estado = true;
                        reporteAnalyticsFiltroBO.UsuarioCreacion = reporteAnalyticsFiltroDTO.Usuario;
                        reporteAnalyticsFiltroBO.UsuarioModificacion = reporteAnalyticsFiltroDTO.Usuario;
                        reporteAnalyticsFiltroBO.FechaCreacion = DateTime.Now;
                        reporteAnalyticsFiltroBO.FechaModificacion = DateTime.Now;

                        reporteAnalyticsFiltroRepositorio.Insert(reporteAnalyticsFiltroBO);
                    }
                    else
                    {
                        reporteAnalyticsFiltroBO = reporteAnalyticsFiltroRepositorio.FirstById(reporteAnalyticsFiltroDTO.Id);
                        reporteAnalyticsFiltroBO.Nombre = reporteAnalyticsFiltroDTO.Nombre;
                        reporteAnalyticsFiltroBO.UsuarioModificacion = reporteAnalyticsFiltroDTO.Usuario;
                        reporteAnalyticsFiltroBO.FechaModificacion = DateTime.Now;

                        reporteAnalyticsFiltroRepositorio.Update(reporteAnalyticsFiltroBO);

                        reporteAnalyticsFiltroDetalleRepositorio.DeleteLogico(reporteAnalyticsFiltroBO.Id, reporteAnalyticsFiltroDTO.Usuario, reporteAnalyticsFiltroDTO.Detalle);
                    }

                    foreach (var detalle in reporteAnalyticsFiltroDTO.Detalle)
                    {
                        if (detalle.Id == 0)
                        {
                            reporteAnalyticsFiltroDetalleBO = new ReporteAnalyticsFiltroDetalleBO();
                            reporteAnalyticsFiltroDetalleBO.Texto = detalle.Texto;
                            reporteAnalyticsFiltroDetalleBO.Excluir = detalle.Excluir;
                            reporteAnalyticsFiltroDetalleBO.IdReporteAnalyticsFiltro = reporteAnalyticsFiltroBO.Id;
                            reporteAnalyticsFiltroDetalleBO.IdOperadorComparacion = detalle.IdOperadorComparacion;
                            reporteAnalyticsFiltroDetalleBO.Estado = true;
                            reporteAnalyticsFiltroDetalleBO.UsuarioCreacion = reporteAnalyticsFiltroDTO.Usuario;
                            reporteAnalyticsFiltroDetalleBO.UsuarioModificacion = reporteAnalyticsFiltroDTO.Usuario;
                            reporteAnalyticsFiltroDetalleBO.FechaCreacion = DateTime.Now;
                            reporteAnalyticsFiltroDetalleBO.FechaModificacion = DateTime.Now;

                            reporteAnalyticsFiltroDetalleRepositorio.Insert(reporteAnalyticsFiltroDetalleBO);
                        }

                        else
                        {
                            reporteAnalyticsFiltroDetalleBO = reporteAnalyticsFiltroDetalleRepositorio.FirstById(detalle.Id);
                            reporteAnalyticsFiltroDetalleBO.Texto = detalle.Texto;
                            reporteAnalyticsFiltroDetalleBO.Excluir = detalle.Excluir;
                            reporteAnalyticsFiltroDetalleBO.IdOperadorComparacion = detalle.IdOperadorComparacion;
                            reporteAnalyticsFiltroDetalleBO.Estado = true;
                            reporteAnalyticsFiltroDetalleBO.UsuarioModificacion = reporteAnalyticsFiltroDTO.Usuario;
                            reporteAnalyticsFiltroDetalleBO.FechaModificacion = DateTime.Now;

                            reporteAnalyticsFiltroDetalleRepositorio.Update(reporteAnalyticsFiltroDetalleBO);
                        }
                    }
                    scope.Complete();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarFiltro([FromBody] ReporteAnalyticsFiltroDTO reporteAnalyticsFiltroDTO)
        {
            try
            {
                ReporteAnalyticsFiltroRepositorio reporteAnalyticsFiltroRepositorio = new ReporteAnalyticsFiltroRepositorio(_integraDBContext);
                ReporteAnalyticsFiltroDetalleRepositorio reporteAnalyticsFiltroDetalleRepositorio = new ReporteAnalyticsFiltroDetalleRepositorio(_integraDBContext);

                reporteAnalyticsFiltroRepositorio.Delete(reporteAnalyticsFiltroDTO.Id, reporteAnalyticsFiltroDTO.Usuario);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] FiltroReporteGoogleAnalyticsDTO filtroReporteGoogleAnalyticsDTO)
        {

            try
            {
                ReporteAnalyticsFiltroDetalleRepositorio reporteAnalyticsFiltroDetalleRepositorio = new ReporteAnalyticsFiltroDetalleRepositorio(_integraDBContext);
                AutenticacionPlataformaRepositorio autenticacionPlataformaRepositorio = new AutenticacionPlataformaRepositorio(_integraDBContext);

                //Obtener Autenticacion
                string autenticacion = autenticacionPlataformaRepositorio.FirstBy(x => x.Producto == "GoogleAPI").Autenticacion;

                //Filtro del Usuario
                List<ReporteAnalyticsFiltroDetalleSimboloDTO> filtroDetalle = reporteAnalyticsFiltroDetalleRepositorio.ObtenerDetallePorIdReporteAnalyticsFiltro(filtroReporteGoogleAnalyticsDTO.IdReporteAnalyticsFiltro);
                DimensionFilter dimensionFilter;
                List<DimensionFilter> dimensionFilters = new List<DimensionFilter>();
                foreach (var detalle in filtroDetalle)
                {
                    dimensionFilter = new DimensionFilter { DimensionName = "ga:pagePath", Expressions = new List<string>() { detalle.Texto }, Operator__ = detalle.Simbolo, Not = detalle.Excluir };
                    dimensionFilters.Add(dimensionFilter);
                }
                DimensionFilterClause dimensionFilterClause = new DimensionFilterClause { Operator__ = "AND", Filters = dimensionFilters };

                var service = GetService(autenticacion);

                using (service)
                {
                    // Create the Metrics object.
                    Metric metricaAuxiliar;
                    List<Metric> metricas = new List<Metric>();
                    foreach (var metrica in filtroReporteGoogleAnalyticsDTO.Metricas)
                    {
                        metricaAuxiliar = new Metric { Expression = "ga:" + metrica.Id, Alias = metrica.Nombre.Replace(" ", "") };
                        metricas.Add(metricaAuxiliar);
                    }

                    //Create the Dimensions object.
                    Dimension url = new Dimension { Name = "ga:pagePath" };
                    Dimension segmentos = new Dimension { Name = "ga:segment" };


                    //Create the Segment object.
                    Segment segmentoAuxiliar;
                    List<Segment> listaSegmentos = new List<Segment>();
                    foreach (var metrica in filtroReporteGoogleAnalyticsDTO.Segmentos)
                    {
                        segmentoAuxiliar = new Segment { SegmentId = "gaid::" + metrica };
                        listaSegmentos.Add(segmentoAuxiliar);
                    }

                    Pivot pivot = new Pivot
                    {
                        Dimensions = new List<Dimension>() { segmentos },
                        Metrics = metricas,
                    };

                    if (filtroReporteGoogleAnalyticsDTO.Mensual)
                    {
                        List<ReporteAnalyticsPorMesDTO> listaReportes = new List<ReporteAnalyticsPorMesDTO>();
                        int AnhoInicio = filtroReporteGoogleAnalyticsDTO.FechaInicio.Year;
                        int AnhoFin = filtroReporteGoogleAnalyticsDTO.FechaFin.Year;
                        int MesInicio = filtroReporteGoogleAnalyticsDTO.FechaInicio.Month;
                        int MesFin = filtroReporteGoogleAnalyticsDTO.FechaFin.Month;

                        bool primeranho = true;
                        int mesActual = 0;
                        int MesFinInteractivo = 0;

                        for (int anhoactual = AnhoInicio; anhoactual <= AnhoFin; anhoactual++)
                        {
                            if (primeranho) { mesActual = MesInicio; primeranho = false; }
                            else mesActual = 1;

                            if (anhoactual == AnhoFin) MesFinInteractivo = MesFin;
                            else MesFinInteractivo = 12;

                            for (int mesActualInteractivo = mesActual; mesActualInteractivo <= MesFinInteractivo; mesActualInteractivo++)
                            {
                                string mesActualFiltro = (mesActualInteractivo < 10 ? "0" + mesActualInteractivo : mesActualInteractivo + "");
                                Google.Apis.AnalyticsReporting.v4.Data.DateRange dateRange = new Google.Apis.AnalyticsReporting.v4.Data.DateRange() { StartDate = anhoactual + "-" + mesActualFiltro + "-" + "01", EndDate = anhoactual + "-" + mesActualFiltro + "-" + DateTime.DaysInMonth(anhoactual, mesActualInteractivo) };

                                // Create the ReportRequest object.
                                ReportRequest reportRequest = new ReportRequest
                                {
                                    ViewId = "68328353",
                                    DateRanges = new List<Google.Apis.AnalyticsReporting.v4.Data.DateRange>() { dateRange },
                                    Dimensions = new List<Dimension>() { url },
                                    Segments = listaSegmentos,
                                    DimensionFilterClauses = new List<DimensionFilterClause>() { dimensionFilterClause },
                                    Pivots = new List<Pivot>() { pivot },
                                    PageSize = 10000
                                };

                                List<ReportRequest> requests = new List<ReportRequest>();
                                requests.Add(reportRequest);

                                // Create the GetReportsRequest object.
                                GetReportsRequest getReport = new GetReportsRequest() { ReportRequests = requests };

                                // Call the batchGet method.
                                GetReportsResponse response = service.Reports.BatchGet(getReport).Execute();

                                listaReportes.Add(new ReporteAnalyticsPorMesDTO
                                {
                                    Mes = mesActualInteractivo,
                                    Anho = anhoactual,
                                    Reporte = response.Reports.FirstOrDefault()
                                });
                            }
                        }

                        var tasks = new[]
                        {
                            Task.Run(() => GuardarReporte(listaReportes, filtroReporteGoogleAnalyticsDTO.Usuario, filtroReporteGoogleAnalyticsDTO.IdReporteAnalyticsFiltro))
                        };

                        return Ok(listaReportes);
                    }

                    else
                    {
                        Google.Apis.AnalyticsReporting.v4.Data.DateRange dateRange = new Google.Apis.AnalyticsReporting.v4.Data.DateRange() { StartDate = filtroReporteGoogleAnalyticsDTO.FechaInicio.ToString("yyyy-MM-dd"), EndDate = filtroReporteGoogleAnalyticsDTO.FechaFin.ToString("yyyy-MM-dd") };

                        // Create the ReportRequest object.
                        ReportRequest reportRequest = new ReportRequest
                        {
                            ViewId = "68328353",
                            DateRanges = new List<Google.Apis.AnalyticsReporting.v4.Data.DateRange>() { dateRange },
                            Dimensions = new List<Dimension>() { url },
                            Segments = listaSegmentos,
                            DimensionFilterClauses = new List<DimensionFilterClause>() { dimensionFilterClause },
                            Pivots = new List<Pivot>() { pivot },
                            PageSize = 10000
                        };

                        List<ReportRequest> requests = new List<ReportRequest>();
                        requests.Add(reportRequest);

                        // Create the GetReportsRequest object.
                        GetReportsRequest getReport = new GetReportsRequest() { ReportRequests = requests };

                        // Call the batchGet method.
                        GetReportsResponse response = service.Reports.BatchGet(getReport).Execute();

                        return Ok(response.Reports.FirstOrDefault());
                    }

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        static async Task<UserCredential> GetCredential(string autenticacion)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(autenticacion);
            MemoryStream stream = new MemoryStream(byteArray);

            const string loginEmailAddress = "abenavente@bsginstitute.com";


            return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                new[] { AnalyticsReportingService.Scope.Analytics },
                loginEmailAddress, CancellationToken.None);
        }

        private AnalyticsReportingService GetService(string autenticacion)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(autenticacion);
            MemoryStream stream = new MemoryStream(byteArray);

            // These are the scopes of permissions you need. It is best to request only what you need and not all of them
            string[] scopes = new string[] { AnalyticsReportingService.Scope.Analytics };             // View your Google Analytics data

            GoogleCredential credential;
            using (stream)
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(scopes);
            }

            // Create the  Analytics service.
            return new AnalyticsReportingService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "MyApp" // not important
            });
        }

        private void GuardarReporte(List<ReporteAnalyticsPorMesDTO> listaReportes, string usuario, int IdReporteAnalyticsFiltro)
        {
            string nombreFiltro = "";
            try
            {

                GoogleAnalyticsReportePaginaRepositorio googleAnalyticsReportePaginaRepositorio = new GoogleAnalyticsReportePaginaRepositorio();
                GoogleAnalyticsReporteDetalleRepositorio googleAnalyticsReporteDetalleRepositorio = new GoogleAnalyticsReporteDetalleRepositorio();
                GoogleAnalyticsSegmentoRepositorio googleAnalyticsSegmentoRepositorio = new GoogleAnalyticsSegmentoRepositorio();
                GoogleAnalyticsMetricaRepositorio googleAnalyticsMetricaRepositorio = new GoogleAnalyticsMetricaRepositorio();
                ReporteAnalyticsFiltroRepositorio reporteAnalyticsFiltroRepositorio = new ReporteAnalyticsFiltroRepositorio();

                nombreFiltro = reporteAnalyticsFiltroRepositorio.FirstById(IdReporteAnalyticsFiltro).Nombre;
                List<GoogleAnalyticsSegmentoBO> segmentos = googleAnalyticsSegmentoRepositorio.GetBy(x => true).ToList();
                List<GoogleAnalyticsMetricaDTO> metricas = googleAnalyticsMetricaRepositorio.GetBy(x => true, y => new GoogleAnalyticsMetricaDTO()
                {
                    Id = y.Id,
                    Nombre = y.Nombre.Replace(" ", "")
                }).ToList();

                GoogleAnalyticsReportePaginaBO googleAnalyticsReportePaginaBO;
                GoogleAnalyticsReporteDetalleBO googleAnalyticsReporteDetalleBO;

                int mes, anho, mesInicial, anhoInicial;
                bool primero = true;
                mes = anho = mesInicial = anhoInicial= 0;
                foreach (var reporte in listaReportes)
                {
                    mes = reporte.Mes;
                    anho = reporte.Anho;

                    if (primero)
                    {
                        mesInicial = reporte.Mes;
                        anhoInicial = reporte.Anho;
                        primero = false;
                    }
                    var columnas = reporte.Reporte.ColumnHeader.MetricHeader.PivotHeaders[0].PivotHeaderEntries;
                    var data = reporte.Reporte.Data.Rows;

                    for(int i = 0; i < columnas.Count; i++)
                    {
                        var segmento = segmentos.Where(x => x.NombreIngles == (columnas[i].DimensionValues[0])).First();
                        var metrica = metricas.Where(x => x.Nombre == (columnas[i].Metric.Name)).First(); ;

                        foreach (var row in data)
                        {
                            string pagina = row.Dimensions[0];
                            string valor = row.Metrics[0].PivotValueRegions[0].Values[i];

                            List<GoogleAnalyticsReportePaginaBO> googleAnalyticsReportePaginaBOs = googleAnalyticsReportePaginaRepositorio.GetBy(x => x.Nombre == pagina).ToList();
                            if (googleAnalyticsReportePaginaBOs.Count == 0) googleAnalyticsReportePaginaBO = null;
                            else googleAnalyticsReportePaginaBO = googleAnalyticsReportePaginaBOs.Where(x => x.Nombre == pagina).FirstOrDefault();
                            if (googleAnalyticsReportePaginaBO == null)
                            {
                                googleAnalyticsReportePaginaBO = new GoogleAnalyticsReportePaginaBO();
                                googleAnalyticsReportePaginaBO.Nombre = pagina;
                                googleAnalyticsReportePaginaBO.Estado = true;
                                googleAnalyticsReportePaginaBO.FechaCreacion = DateTime.Now;
                                googleAnalyticsReportePaginaBO.FechaModificacion = DateTime.Now;
                                googleAnalyticsReportePaginaBO.UsuarioCreacion = usuario;
                                googleAnalyticsReportePaginaBO.UsuarioModificacion = usuario;

                                googleAnalyticsReportePaginaRepositorio.Insert(googleAnalyticsReportePaginaBO);
                            }

                            googleAnalyticsReporteDetalleBO = googleAnalyticsReporteDetalleRepositorio.FirstBy(x => x.IdGoogleAnalyticsReportePagina == googleAnalyticsReportePaginaBO.Id && x.IdGoogleAnalyticsSegmento == segmento.Id && x.IdGoogleAnalyticsMetrica == metrica.Id && x.Mes == mes && x.Anho == anho);
                            if(googleAnalyticsReporteDetalleBO == null)
                            {
                                googleAnalyticsReporteDetalleBO = new GoogleAnalyticsReporteDetalleBO();
                                googleAnalyticsReporteDetalleBO.IdGoogleAnalyticsReportePagina = googleAnalyticsReportePaginaBO.Id;
                                googleAnalyticsReporteDetalleBO.IdGoogleAnalyticsSegmento = segmento.Id;
                                googleAnalyticsReporteDetalleBO.IdGoogleAnalyticsMetrica = metrica.Id;
                                googleAnalyticsReporteDetalleBO.Mes = mes;
                                googleAnalyticsReporteDetalleBO.Anho = anho;
                                googleAnalyticsReporteDetalleBO.Valor = valor;
                                googleAnalyticsReporteDetalleBO.Estado = true;
                                googleAnalyticsReporteDetalleBO.FechaCreacion = DateTime.Now;
                                googleAnalyticsReporteDetalleBO.FechaModificacion = DateTime.Now;
                                googleAnalyticsReporteDetalleBO.UsuarioCreacion = usuario;
                                googleAnalyticsReporteDetalleBO.UsuarioModificacion = usuario;

                                googleAnalyticsReporteDetalleRepositorio.Insert(googleAnalyticsReporteDetalleBO);
                            }
                        }
                    }
                }

                List<string> correos = new List<string>();
                correos.Add("wvalencia@bsginstitute.com");

                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                TMKMailDataDTO mailData = new TMKMailDataDTO();
                mailData.Sender = "abenavente@bsginstitute.com";
                mailData.Recipient = string.Join(",", correos);
                mailData.Subject = "Reporte Google Analytics - Guardado EXITOSO";
                mailData.Message = "Usuario: " + usuario + "<br> Periodo: " + mesInicial + "/" + anhoInicial + " - " + mes + "/" + anho + " <br>Categoría de URL: " + nombreFiltro;
                mailData.Cc = "abenavente@bsginstitute.com";
                mailData.Bcc = "";
                mailData.AttachedFiles = null;

                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
            }
            catch (Exception ex)
            {
                List<string> correos = new List<string>();
                correos.Add("abenavente@bsginstitute.com");

                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                TMKMailDataDTO mailData = new TMKMailDataDTO();
                mailData.Sender = "abenavente@bsginstitute.com";
                mailData.Recipient = string.Join(",", correos);
                mailData.Subject = "Reporte Google Analytics - Guardado ERROR";
                mailData.Message = "Usuario: " + usuario + " <br>Categoría de URL: " + nombreFiltro + "<br>Error: " + ex.Message + "<br> Especifico: " + ex.ToString();
                mailData.Cc = "abenavente@bsginstitute.com";
                mailData.Bcc = "";
                mailData.AttachedFiles = null;

                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
            }
        }
    }
}