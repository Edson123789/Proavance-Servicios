using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Servicios.DTOs;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201809;
using Google.Apis.AnalyticsReporting.v4;
using Google.Apis.AnalyticsReporting.v4.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteAdwordsApiVolumenBusqueda")]
    [ApiController]
    public class ReporteAdwordsApiVolumenBusquedaController : ControllerBase
    {
        Dictionary<string, string> Cabecera = new Dictionary<string, string>() {
                    {"DeveloperToken", "yj96WeNiguTTxbTNJSvTHw" },
                    {"ClientCustomerId", "574-320-7825" },
                    {"OAuth2ClientId", "908308661940-5dqqc42726ubesati7t21ie9geshbf6s.apps.googleusercontent.com" },
                    {"OAuth2ClientSecret", "AOPy5MD5YMZFb8HRpq8YdZW_" },
                    {"OAuth2RefreshToken", "1//0hrRURuTN3yAGCgYIARAAGBESNwF-L9IrxsLtupV0_Mv1JoYvePffjwT9fdxRyVDjT1wY8U9U70mvH4bKXqPGyhPzLrTYxwH1vCo" }
                };

        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] FiltroReporteAdwordsApiVolumenBusquedaDTO FiltroReporteAdwordsApiVolumenBusquedaDTO)
        {
            try
            {
                List<ReporteAdwordsApiPalabrasClaveRespuestaDTO> reporteAdwordsApiPalabrasClaveRespuestas = new List<ReporteAdwordsApiPalabrasClaveRespuestaDTO>();
                List<ReporteAdwordsApiPalabrasClaveRespuestaDTO> reporteAdwordsApiPalabrasClaveRespuestasGuardar = new List<ReporteAdwordsApiPalabrasClaveRespuestaDTO>();
                AdwordsApiPalabraClaveRepositorio adwordsApiPalabraClaveRepositorio = new AdwordsApiPalabraClaveRepositorio();
                AdwordsApiVolumenBusquedaRepositorio adwordsApiVolumenBusquedaRepositorio = new AdwordsApiVolumenBusquedaRepositorio();
                AdwordsApiPalabraClaveBO adwordsApiPalabraClaveBO = new AdwordsApiPalabraClaveBO();
                AdwordsApiVolumenBusquedaBO adwordsApiVolumenBusquedaBO = new AdwordsApiVolumenBusquedaBO();
                PaisRepositorio paisRepositorio = new PaisRepositorio();

                AdWordsUser user = new AdWordsUser(Cabecera);
                //************************************************************************************************************************************************************************************************************************
                //**************************************************************************     funcion de  agregaci[on   ***************************************************************************************************************
                //************************************************************************************************************************************************************************************************************************
                List<FiltroReporteGrupoPalabrasTipoPalabra> ReporteAdwordsTemporal = new List<FiltroReporteGrupoPalabrasTipoPalabra>();
                foreach (var InterarListaPalabras in FiltroReporteAdwordsApiVolumenBusquedaDTO.ListaPalabras)
                {
                    char[] charsToTrim = { '*', ' ', '\t' };
                    string ListaPalabras = InterarListaPalabras.CadenaTexto;
                    string[] separado = ListaPalabras.Split(new string[] { "\n" }, StringSplitOptions.None);
                    foreach (string palabra in separado)
                    {
                        string almacenar = palabra;
                        almacenar = almacenar.Replace("\t", "");
                        string result = almacenar.Trim(charsToTrim);
                        FiltroReporteGrupoPalabrasTipoPalabra VarReporteAdwordsTemporal = new FiltroReporteGrupoPalabrasTipoPalabra()
                        {
                            TipoTexto = InterarListaPalabras.TipoTexto,
                            CadenaTexto = result,
                        };
                        ReporteAdwordsTemporal.Add(VarReporteAdwordsTemporal);
                    }
                }
                FiltroReporteAdwordsApiVolumenBusquedaDTO.ListaPalabras.Clear();
                FiltroReporteAdwordsApiVolumenBusquedaDTO.ListaPalabras.AddRange(ReporteAdwordsTemporal);
                //************************************************************************************************************************************************************************************************************************
                //**************************************************************************     funcion de  agregaci[on   ***************************************************************************************************************
                //************************************************************************************************************************************************************************************************************************

                var listaFrases = FiltroReporteAdwordsApiVolumenBusquedaDTO.ListaPalabras.Where(x => x.TipoTexto == 1/*Frase*/).ToList();
                var listaPalabras = FiltroReporteAdwordsApiVolumenBusquedaDTO.ListaPalabras.Where(x => x.TipoTexto == 2/*Palabra*/).ToList();

                List<string> listaPalabrasAProcesar = new List<string>();
                string[] palabrasABuscar = Array.Empty<string>();



                listaPalabrasAProcesar.AddRange(listaPalabras.Select(s => s.CadenaTexto.Trim().Split(" ")[0]).Where(x => x.Length > 0).ToList());
                listaPalabrasAProcesar.AddRange(listaFrases.Select(s => s.CadenaTexto).Where(x => x.Length > 0).ToList());


                listaPalabrasAProcesar = listaPalabrasAProcesar.Distinct().ToList();

                using (TargetingIdeaService targetingIdeaService =
                        (TargetingIdeaService)user.GetService(AdWordsService.v201809.TargetingIdeaService))
                {

                    foreach (int codigoPais in FiltroReporteAdwordsApiVolumenBusquedaDTO.Paises)
                    {
                        int idPais = paisRepositorio.FirstBy(x => x.CodigoGoogleId == codigoPais).Id;

                        List<PalabraClaveVolumenDTO> palabraClaveVolumenDTOs = new List<PalabraClaveVolumenDTO>();

                        if (listaPalabrasAProcesar.Any())
                            palabrasABuscar = listaPalabrasAProcesar.ToArray();

                        for (int contPalabaras = 0; contPalabaras < palabrasABuscar.Length; contPalabaras = contPalabaras + 700)
                        {

                            // Create selector.
                            TargetingIdeaSelector selector = new TargetingIdeaSelector
                            {
                                requestType = FiltroReporteAdwordsApiVolumenBusquedaDTO.TipoPalabra == 1 ? RequestType.STATS : RequestType.IDEAS,
                                ideaType = IdeaType.KEYWORD,
                                requestedAttributeTypes = new AttributeType[]
                                {
                                AttributeType.KEYWORD_TEXT,
                                AttributeType.TARGETED_MONTHLY_SEARCHES,
                                }
                            };

                            List<SearchParameter> searchParameters = new List<SearchParameter>();

                            // Create related to query search parameter.
                            RelatedToQuerySearchParameter relatedToQuerySearchParameter =
                                new RelatedToQuerySearchParameter
                                {
                                    queries = palabrasABuscar.Skip(contPalabaras).Take(700).ToArray()
                                };
                            searchParameters.Add(relatedToQuerySearchParameter);

                            LocationSearchParameter locationSearchParameter = new LocationSearchParameter();
                            Location location = new Location { id = codigoPais };
                            locationSearchParameter.locations = new Location[]
                            {
                            location
                            };
                            searchParameters.Add(locationSearchParameter);

                            LanguageSearchParameter languageParameter = new LanguageSearchParameter();
                            Language language = new Language
                            {
                                id = FiltroReporteAdwordsApiVolumenBusquedaDTO.IdIdioma
                            };
                            languageParameter.languages = new Language[]
                            {
                            language
                            };
                            if (FiltroReporteAdwordsApiVolumenBusquedaDTO.TipoPalabra == 2)
                            {
                                searchParameters.Add(languageParameter);
                            }

                            // Add network search parameter (optional).
                            NetworkSetting networkSetting = new NetworkSetting
                            {
                                targetGoogleSearch = true,
                                targetSearchNetwork = false,
                                targetContentNetwork = false,
                                targetPartnerSearchNetwork = false
                            };

                            NetworkSearchParameter networkSearchParameter = new NetworkSearchParameter
                            {
                                networkSetting = networkSetting
                            };
                            searchParameters.Add(networkSearchParameter);

                            // Set the search parameters.
                            selector.searchParameters = searchParameters.ToArray();

                            // Set selector paging (required for targeting idea service).

                            selector.paging = Paging.Default;

                            TargetingIdeaPage page = new TargetingIdeaPage();

                            try
                            {
                                int i = 0;
                                do
                                {
                                    // Get related keywords.
                                    page = targetingIdeaService.get(selector);

                                    // Display related keywords.
                                    if (page.entries != null && page.entries.Length > 0)
                                    {
                                        foreach (TargetingIdea targetingIdea in page.entries)
                                        {
                                            Dictionary<AttributeType, Google.Api.Ads.AdWords.v201809.Attribute>
                                                ideas = targetingIdea.data.ToDict();


                                            PalabraClaveVolumenDTO palabraClaveVolumenDTO = new PalabraClaveVolumenDTO();

                                            palabraClaveVolumenDTO.PalabraClave = (ideas[AttributeType.KEYWORD_TEXT] as StringAttribute).value;

                                            palabraClaveVolumenDTO.PromedioPorMes = (ideas[AttributeType.TARGETED_MONTHLY_SEARCHES] as MonthlySearchVolumeAttribute).value;
                                            //palabraClaveVolumenDTO.PromedioPorMes = palabraClaveVolumenDTO.PromedioPorMes.OrderBy(x => x.year).ThenBy(x => x.month).ToArray();
                                            palabraClaveVolumenDTOs.Add(palabraClaveVolumenDTO);

                                            i++;
                                        }
                                    }

                                    selector.paging.IncreaseOffset();
                                } while (selector.paging.startIndex < page.totalNumEntries);

                            }
                            catch (Exception e)
                            {
                                return BadRequest(e);
                            }

                        }

                        string Pais = "";
                        if (codigoPais == 2604) Pais = "Peru";
                        else if (codigoPais == 2170) Pais = "Colombia";
                        else if (codigoPais == 2484) Pais = "Mexico";
                        else if (codigoPais == 2152) Pais = "Chile";
                        else if (codigoPais == 2862) Pais = "Venezuela";
                        else if (codigoPais == 2591) Pais = "Panama";
                        else if (codigoPais == 2032) Pais = "Argentina";
                        else if (codigoPais == 2218) Pais = "Ecuador";
                        else if (codigoPais == 2188) Pais = "CostaRica";
                        else if (codigoPais == 2068) Pais = "Bolivia";
                        else if (codigoPais == 2600) Pais = "Paraguay";

                        reporteAdwordsApiPalabrasClaveRespuestasGuardar.Add(new ReporteAdwordsApiPalabrasClaveRespuestaDTO { Pais = Pais, IdPais = idPais, Detalle = palabraClaveVolumenDTOs });

                        if (FiltroReporteAdwordsApiVolumenBusquedaDTO.TipoPalabra == 1)
                        {
                            var historico = adwordsApiVolumenBusquedaRepositorio.ObtenerHistorico(FiltroReporteAdwordsApiVolumenBusquedaDTO.FechaInicio, DateTime.Now, palabrasABuscar, idPais);
                            List<PalabraClaveVolumenDTO> palabraClaveVolumenDTOsHistorico = new List<PalabraClaveVolumenDTO>();

                            palabraClaveVolumenDTOsHistorico = (from p in historico
                                                                group p by new
                                                                {
                                                                    p.PalabraClave
                                                                } into g
                                                                select new PalabraClaveVolumenDTO
                                                                {
                                                                    PalabraClave = g.Key.PalabraClave,
                                                                    PromedioPorMes = g.Select(o => new MonthlySearchVolume
                                                                    {
                                                                        year = o.Anho,
                                                                        month = o.Mes,
                                                                        count = o.PromedioBusqueda
                                                                    }).ToArray(),
                                                                }).ToList();

                            palabraClaveVolumenDTOs = palabraClaveVolumenDTOs.Concat(palabraClaveVolumenDTOsHistorico).ToList();
                        }

                        reporteAdwordsApiPalabrasClaveRespuestas.Add(new ReporteAdwordsApiPalabrasClaveRespuestaDTO { Pais = Pais, IdPais = idPais, Detalle = palabraClaveVolumenDTOs });
                    }
                }
                if (FiltroReporteAdwordsApiVolumenBusquedaDTO.IdIdioma == 1003)
                {
                    var tasks = new[]
                    {
                    Task.Run(() => GuardarHistorico(reporteAdwordsApiPalabrasClaveRespuestasGuardar, FiltroReporteAdwordsApiVolumenBusquedaDTO.Usuario, string.Join(",", listaPalabras)))
                    };
                }

                return Ok(reporteAdwordsApiPalabrasClaveRespuestas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private void GuardarHistorico(List<ReporteAdwordsApiPalabrasClaveRespuestaDTO> reporteAdwordsApiPalabrasClaveRespuestas, string Usuario, string Palabras)
        {
            try
            {
                AdwordsApiPalabraClaveRepositorio adwordsApiPalabraClaveRepositorio = new AdwordsApiPalabraClaveRepositorio();
                AdwordsApiVolumenBusquedaRepositorio adwordsApiVolumenBusquedaRepositorio = new AdwordsApiVolumenBusquedaRepositorio();
                AdwordsApiPalabraClaveBO adwordsApiPalabraClaveBO = new AdwordsApiPalabraClaveBO();
                AdwordsApiVolumenBusquedaBO adwordsApiVolumenBusquedaBO = new AdwordsApiVolumenBusquedaBO();
                PaisRepositorio paisRepositorio = new PaisRepositorio();

                foreach (var item in reporteAdwordsApiPalabrasClaveRespuestas)
                {
                    foreach (var detalles in item.Detalle)
                    {
                        adwordsApiPalabraClaveBO = adwordsApiPalabraClaveRepositorio.FirstBy(x => x.PalabraClave == detalles.PalabraClave);
                        if (adwordsApiPalabraClaveBO == null)
                        {
                            adwordsApiPalabraClaveBO = new AdwordsApiPalabraClaveBO();
                            adwordsApiPalabraClaveBO.PalabraClave = detalles.PalabraClave;
                            adwordsApiPalabraClaveBO.Estado = true;
                            adwordsApiPalabraClaveBO.UsuarioCreacion = Usuario;
                            adwordsApiPalabraClaveBO.UsuarioModificacion = Usuario;
                            adwordsApiPalabraClaveBO.FechaCreacion = DateTime.Now;
                            adwordsApiPalabraClaveBO.FechaModificacion = DateTime.Now;

                            adwordsApiPalabraClaveRepositorio.Insert(adwordsApiPalabraClaveBO);
                        }

                        foreach (var detalle in detalles.PromedioPorMes)
                        {
                            adwordsApiVolumenBusquedaBO = adwordsApiVolumenBusquedaRepositorio.FirstBy(x => x.IdAdwordsApiPalabraClave == adwordsApiPalabraClaveBO.Id && x.Mes == detalle.month && x.Anho == detalle.year && x.IdPais == item.IdPais);
                            if (adwordsApiVolumenBusquedaBO == null)
                            {
                                adwordsApiVolumenBusquedaBO = new AdwordsApiVolumenBusquedaBO();
                                adwordsApiVolumenBusquedaBO.IdAdwordsApiPalabraClave = adwordsApiPalabraClaveBO.Id;
                                adwordsApiVolumenBusquedaBO.PromedioBusqueda = unchecked((int)detalle.count);
                                adwordsApiVolumenBusquedaBO.Mes = detalle.month;
                                adwordsApiVolumenBusquedaBO.Anho = detalle.year;
                                adwordsApiVolumenBusquedaBO.IdPais = item.IdPais;
                                adwordsApiVolumenBusquedaBO.Estado = true;
                                adwordsApiVolumenBusquedaBO.UsuarioCreacion = Usuario;
                                adwordsApiVolumenBusquedaBO.UsuarioModificacion = Usuario;
                                adwordsApiVolumenBusquedaBO.FechaCreacion = DateTime.Now;
                                adwordsApiVolumenBusquedaBO.FechaModificacion = DateTime.Now;

                                adwordsApiVolumenBusquedaRepositorio.Insert(adwordsApiVolumenBusquedaBO);
                            }
                        }
                    }
                }
                List<string> correos = new List<string>();
                correos.Add("abenavente@bsginstitute.com");

                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                TMKMailDataDTO mailData = new TMKMailDataDTO();
                mailData.Sender = "abenavente@bsginstitute.com";
                mailData.Recipient = string.Join(",", correos);
                mailData.Subject = "Reporte Volumen Busqueda - Guardado EXITOSO";
                mailData.Message = "Usuario: " + Usuario + "<br> Palabras: " + Palabras;
                mailData.Cc = "wvalencia@bsginstitute.com";
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
                mailData.Subject = "Reporte Volumen Busqueda - Guardado ERROR";
                mailData.Message = "Usuario: " + Usuario + "<br> Palabras: " + Palabras + "<br>Error: " + ex.Message + "<br> Especifico: " + ex.ToString();
                mailData.Cc = "wvalencia@bsginstitute.com";
                mailData.Bcc = "";
                mailData.AttachedFiles = null;

                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
            }
        }


        [Route("[Action]/{Usuario}")]
        [HttpGet]
        public ActionResult ActualizarBusquedaPalabras(string Usuario)
        {
            try
            {
                AdwordsApiPalabraClaveRepositorio adwordsApiPalabraClaveRepositorio = new AdwordsApiPalabraClaveRepositorio();


                var palabrasABuscar = adwordsApiPalabraClaveRepositorio.ObtenerPalabraAdwordsDesactualizado();

                var tasks = new[]
                    {
                    Task.Run(() => ActualizarHistorico(Usuario))
                    };

                return Ok(new { Palabras = palabrasABuscar.Count });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private void ActualizarHistorico(string Usuario)
        {
            string nombrePais = "";
            try
            {
                List<ReporteAdwordsApiPalabrasClaveRespuestaDTO> reporteAdwordsApiPalabrasClaveRespuestas = new List<ReporteAdwordsApiPalabrasClaveRespuestaDTO>();
                List<ReporteAdwordsApiPalabrasClaveRespuestaDTO> reporteAdwordsApiPalabrasClaveRespuestasGuardar = new List<ReporteAdwordsApiPalabrasClaveRespuestaDTO>();
                AdwordsApiPalabraClaveRepositorio adwordsApiPalabraClaveRepositorio = new AdwordsApiPalabraClaveRepositorio();
                AdwordsApiVolumenBusquedaRepositorio adwordsApiVolumenBusquedaRepositorio = new AdwordsApiVolumenBusquedaRepositorio();
                AdwordsApiPalabraClaveDTO adwordsApiPalabraClaveBO = new AdwordsApiPalabraClaveDTO();
                AdwordsApiVolumenBusquedaBO adwordsApiVolumenBusquedaBO = new AdwordsApiVolumenBusquedaBO();
                PaisRepositorio paisRepositorio = new PaisRepositorio();

                var Paises = paisRepositorio.GetBy(x => x.CodigoGoogleId != null);
                string inicio = "";
                string fin = "";
                bool calcularFecha = true;

                AdWordsUser user = new AdWordsUser(Cabecera);

                using (TargetingIdeaService targetingIdeaService =
                        (TargetingIdeaService)user.GetService(AdWordsService.v201809.TargetingIdeaService))
                {

                    foreach (var pais in Paises)
                    {
                        int idPais = pais.Id;
                        nombrePais = pais.NombrePais;
                        calcularFecha = true;
                        inicio = "";
                        fin = "";

                        var palabrasABuscar = adwordsApiPalabraClaveRepositorio.ObtenerPalabraAdwordsDesactualizadoPais(pais.Id);

                        List<PalabraClaveVolumenDTO> palabraClaveVolumenDTOs;

                        for (int contPalabaras = 0; contPalabaras < palabrasABuscar.Count; contPalabaras = contPalabaras + 700)
                        {
                            palabraClaveVolumenDTOs = new List<PalabraClaveVolumenDTO>();
                            // Create selector.
                            TargetingIdeaSelector selector = new TargetingIdeaSelector
                            {
                                requestType = RequestType.STATS,
                                ideaType = IdeaType.KEYWORD,
                                requestedAttributeTypes = new AttributeType[]
                                {
                                AttributeType.KEYWORD_TEXT,
                                AttributeType.TARGETED_MONTHLY_SEARCHES,
                                }
                            };

                            List<SearchParameter> searchParameters = new List<SearchParameter>();

                            // Create related to query search parameter.
                            RelatedToQuerySearchParameter relatedToQuerySearchParameter =
                                new RelatedToQuerySearchParameter
                                {
                                    queries = palabrasABuscar.Skip(contPalabaras).Take(700).Select(x => x.PalabraClave).ToArray()
                                };
                            searchParameters.Add(relatedToQuerySearchParameter);

                            LocationSearchParameter locationSearchParameter = new LocationSearchParameter();
                            Location location = new Location { id = Convert.ToInt64(pais.CodigoGoogleId) };
                            locationSearchParameter.locations = new Location[]
                            {
                            location
                            };
                            searchParameters.Add(locationSearchParameter);

                            LanguageSearchParameter languageParameter = new LanguageSearchParameter();
                            Language language = new Language
                            {
                                id = 1003
                            };
                            languageParameter.languages = new Language[]
                            {
                                language
                            };

                            // Add network search parameter (optional).
                            NetworkSetting networkSetting = new NetworkSetting
                            {
                                targetGoogleSearch = true,
                                targetSearchNetwork = false,
                                targetContentNetwork = false,
                                targetPartnerSearchNetwork = false
                            };

                            NetworkSearchParameter networkSearchParameter = new NetworkSearchParameter
                            {
                                networkSetting = networkSetting
                            };
                            searchParameters.Add(networkSearchParameter);

                            // Set the search parameters.
                            selector.searchParameters = searchParameters.ToArray();

                            // Set selector paging (required for targeting idea service).

                            selector.paging = Paging.Default;

                            TargetingIdeaPage page = new TargetingIdeaPage();

                            try
                            {
                                int i = 0;
                                do
                                {
                                    // Get related keywords.
                                    page = targetingIdeaService.get(selector);

                                    // Display related keywords.
                                    if (page.entries != null && page.entries.Length > 0)
                                    {
                                        foreach (TargetingIdea targetingIdea in page.entries)
                                        {
                                            Dictionary<AttributeType, Google.Api.Ads.AdWords.v201809.Attribute>
                                                ideas = targetingIdea.data.ToDict();


                                            PalabraClaveVolumenDTO palabraClaveVolumenDTO = new PalabraClaveVolumenDTO();

                                            palabraClaveVolumenDTO.PalabraClave = (ideas[AttributeType.KEYWORD_TEXT] as StringAttribute).value;

                                            palabraClaveVolumenDTO.PromedioPorMes = (ideas[AttributeType.TARGETED_MONTHLY_SEARCHES] as MonthlySearchVolumeAttribute).value;
                                            //palabraClaveVolumenDTO.PromedioPorMes = palabraClaveVolumenDTO.PromedioPorMes.OrderBy(x => x.year).ThenBy(x => x.month).ToArray();
                                            palabraClaveVolumenDTOs.Add(palabraClaveVolumenDTO);

                                            i++;
                                        }
                                    }

                                    selector.paging.IncreaseOffset();
                                } while (selector.paging.startIndex < page.totalNumEntries);


                                //Guardado en BD


                                foreach (var detalles in palabraClaveVolumenDTOs)
                                {
                                    adwordsApiPalabraClaveBO = palabrasABuscar.Where(x => x.PalabraClave == detalles.PalabraClave).First();
                                    if (calcularFecha)
                                    {
                                        fin = detalles.PromedioPorMes[0].month + "-" + detalles.PromedioPorMes[0].year;
                                        inicio = detalles.PromedioPorMes[11].month + "-" + detalles.PromedioPorMes[11].year;
                                        calcularFecha = false;
                                    }

                                    foreach (var detalle in detalles.PromedioPorMes)
                                    {
                                        adwordsApiVolumenBusquedaBO = adwordsApiVolumenBusquedaRepositorio.FirstBy(x => x.IdAdwordsApiPalabraClave == adwordsApiPalabraClaveBO.Id && x.Mes == detalle.month && x.Anho == detalle.year && x.IdPais == idPais);
                                        if (adwordsApiVolumenBusquedaBO == null)
                                        {
                                            adwordsApiVolumenBusquedaBO = new AdwordsApiVolumenBusquedaBO();
                                            adwordsApiVolumenBusquedaBO.IdAdwordsApiPalabraClave = adwordsApiPalabraClaveBO.Id ?? 0;
                                            adwordsApiVolumenBusquedaBO.PromedioBusqueda = unchecked((int)detalle.count);
                                            adwordsApiVolumenBusquedaBO.Mes = detalle.month;
                                            adwordsApiVolumenBusquedaBO.Anho = detalle.year;
                                            adwordsApiVolumenBusquedaBO.IdPais = idPais;
                                            adwordsApiVolumenBusquedaBO.Estado = true;
                                            adwordsApiVolumenBusquedaBO.UsuarioCreacion = Usuario;
                                            adwordsApiVolumenBusquedaBO.UsuarioModificacion = Usuario;
                                            adwordsApiVolumenBusquedaBO.FechaCreacion = DateTime.Now;
                                            adwordsApiVolumenBusquedaBO.FechaModificacion = DateTime.Now;

                                            adwordsApiVolumenBusquedaRepositorio.Insert(adwordsApiVolumenBusquedaBO);
                                        }
                                    }
                                }


                            }
                            catch (Exception e)
                            {
                                continue;
                            }

                        }

                        List<string> correos = new List<string>();
                        correos.Add("abenavente@bsginstitute.com");

                        TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                        TMKMailDataDTO mailData = new TMKMailDataDTO();
                        mailData.Sender = "abenavente@bsginstitute.com";
                        mailData.Recipient = string.Join(",", correos);
                        mailData.Subject = "Reporte Volumen Busqueda - Actualización de Base";
                        mailData.Message = "Usuario: " + Usuario + "<br> Se actualizo exitosamente toda la base para el país: " + pais.NombrePais + "<br> Desde: " + inicio + ", Hasta: " + fin;
                        mailData.Cc = "wvalencia@bsginstitute.com";
                        mailData.Bcc = "";
                        mailData.AttachedFiles = null;

                        Mailservice.SetData(mailData);
                        Mailservice.SendMessageTask();

                    }
                }

            }
            catch (Exception ex)
            {
                List<string> correos = new List<string>();
                correos.Add("abenavente@bsginstitute.com");

                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                TMKMailDataDTO mailData = new TMKMailDataDTO();
                mailData.Sender = "abenavente@bsginstitute.com";
                mailData.Recipient = string.Join(",", correos);
                mailData.Subject = "Reporte Volumen Busqueda - Actualización de Base";
                mailData.Message = "Usuario: " + Usuario + "<br> Error al actualizar la base para el país: " + nombrePais + "<br>Error: " + ex.Message + "<br> Especifico: " + ex.ToString();
                mailData.Cc = "wvalencia@bsginstitute.com";
                mailData.Bcc = "";
                mailData.AttachedFiles = null;

                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
            }
        }
    }
}