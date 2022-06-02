using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Facebook
    /// Autor: Gian Miranda
    /// Fecha: 12/06/2021
    /// <summary>
    /// BO para la logica de peticiones hacia Facebook
    /// </summary>
    public class FacebookBO
    {
        private string urlBaseV12 = "https://graph.facebook.com/v12.0/";
        private string token = string.Empty;

        private readonly integraDBContext _integraDBContext;
        private readonly FacebookCuentaPublicitariaRepositorio _repFacebookCuentaPublicitaria;
        private readonly AutenticacionServicioExternoRepositorio _repAutenticacionServicioExterno;

        public FacebookBO()
        {
            _repFacebookCuentaPublicitaria = new FacebookCuentaPublicitariaRepositorio();
            _repAutenticacionServicioExterno = new AutenticacionServicioExternoRepositorio();

            var objToken = _repAutenticacionServicioExterno.FirstById(ValorEstatico.IdAutenticacionFacebookLeadsReportes);
            token = objToken != null ? objToken.Valor : string.Empty;
        }

        public FacebookBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repFacebookCuentaPublicitaria = new FacebookCuentaPublicitariaRepositorio(_integraDBContext);
            _repAutenticacionServicioExterno = new AutenticacionServicioExternoRepositorio(_integraDBContext);

            var objToken = _repAutenticacionServicioExterno.FirstById(ValorEstatico.IdAutenticacionFacebookLeadsReportes);
            token = objToken != null ? objToken.Valor : string.Empty;
        }

        /// <summary>
        /// Descarga metrica de los anuncios de Facebook
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio de la descarga de metrica</param>
        /// <param name="fechaFin">Fecha de fin de la descarga de metrica</param>
        /// <returns>Lista de objetos de clase AnuncioFacebookMetricaDTO</returns>
        public List<AnuncioFacebookMetricaDTO> DescargarMetricaFacebookAnuncio(string fechaInicio, string fechaFin)
        {
            try
            {
                List<string> listaFacebookIdCuenta = _repFacebookCuentaPublicitaria.GetAll().Select(s => s.FacebookIdCuentaPublicitaria).ToList();

                List<AnuncioFacebookMetricaDTO> resultado = new List<AnuncioFacebookMetricaDTO>();

                foreach (var facebookIdCuenta in listaFacebookIdCuenta)
                {
                    AnuncioFacebookMetricaSinProcesarDTO resultadoSinProcesar = new AnuncioFacebookMetricaSinProcesarDTO();
                    string respuestaFacebook = string.Empty;
                    string urlPaginacion = null;

                    do
                    {
                        try
                        {
                            using (WebClient client = new WebClient())
                            {
                                string urlGet = urlPaginacion ?? $"{urlBaseV12}{facebookIdCuenta}/insights?fields=ad_id,adset_id,campaign_id,account_id,spend,impressions,unique_clicks,clicks,inline_link_clicks,reach&level=ad&time_range={{\"since\":\"{fechaInicio}\",\"until\":\"{fechaFin}\"}}";
                                client.Encoding = Encoding.UTF8;
                                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                                client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                respuestaFacebook = client.DownloadString(urlGet);
                            }

                            resultadoSinProcesar = JsonConvert.DeserializeObject<AnuncioFacebookMetricaSinProcesarDTO>(respuestaFacebook);
                            urlPaginacion = resultadoSinProcesar.paging == null ? null : resultadoSinProcesar.paging.next;

                            resultado.AddRange(resultadoSinProcesar.data.ToList());
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    } while (urlPaginacion != null);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descarga los anuncios Facebook y su padre desde la plataforma de Facebook
        /// </summary>
        /// <param name="listaAnuncio">Lista de string con el id de la plataforma de Facebook</param>
        /// <returns>string</returns>
        public List<AnuncioFacebookDTO> DescargarAnuncioyPadre(List<string> listaAnuncio)
        {
            try
            {
                List<AnuncioFacebookDTO> listaJerarquiaFacebook = new List<AnuncioFacebookDTO>();
                List<FacebookRespuestaBatchDTO> resultadoSinProcesar = new List<FacebookRespuestaBatchDTO>();
                string respuestaFacebook = string.Empty;

                List<List<string>> bloqueListaAnuncio =
                                listaAnuncio.Select((x, i) => new { Index = i, Value = x })
                                .GroupBy(x => x.Index / 25)
                                .Select(x => x.Select(v => v.Value).ToList())
                                .ToList();

                foreach (var segmentoListaAnuncio in bloqueListaAnuncio)
                {
                    List<FacebookFormatoPostDTO> anuncioFacebookFormatoPost = new List<FacebookFormatoPostDTO>();

                    foreach (var urlRelativa in segmentoListaAnuncio)
                    {
                        anuncioFacebookFormatoPost.Add(new FacebookFormatoPostDTO()
                        {
                            method = "GET",
                            relative_url = $"{urlRelativa}?fields=id,name,adset_id"
                        });
                    }

                    using (WebClient client = new WebClient())
                    {
                        string cuerpoPost = JsonConvert.SerializeObject(new BatchFacebookFormatoPostDTO { batch = anuncioFacebookFormatoPost.ToArray() });

                        client.Encoding = Encoding.UTF8;
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        client.Headers[HttpRequestHeader.ContentLength] = cuerpoPost.Length.ToString();
                        respuestaFacebook = client.UploadString(urlBaseV12, cuerpoPost);
                    }

                    resultadoSinProcesar = JsonConvert.DeserializeObject<List<FacebookRespuestaBatchDTO>>(respuestaFacebook);

                    foreach (var datoSinProcesar in resultadoSinProcesar)
                        listaJerarquiaFacebook.Add(JsonConvert.DeserializeObject<AnuncioFacebookDTO>(datoSinProcesar.body));
                }

                return listaJerarquiaFacebook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descarga los conjuntos anuncios Facebook y su padre desde la plataforma de Facebook
        /// </summary>
        /// <param name="listaConjuntoAnuncio">Lista de string con el id de la plataforma de Facebook</param>
        /// <returns>string</returns>
        public List<ConjuntoAnuncioFacebookJerarquiaDTO> DescargarConjuntoAnuncioyPadre(List<string> listaConjuntoAnuncio)
        {
            try
            {
                List<ConjuntoAnuncioFacebookJerarquiaDTO> listaJerarquiaFacebook = new List<ConjuntoAnuncioFacebookJerarquiaDTO>();
                List<FacebookRespuestaBatchDTO> resultadoSinProcesar = new List<FacebookRespuestaBatchDTO>();
                string respuestaFacebook = string.Empty;

                List<List<string>> bloqueListaConjuntoAnuncio =
                                listaConjuntoAnuncio.Select((x, i) => new { Index = i, Value = x })
                                .GroupBy(x => x.Index / 25)
                                .Select(x => x.Select(v => v.Value).ToList())
                                .ToList();

                foreach (var segmentoListaConjuntoAnuncio in bloqueListaConjuntoAnuncio)
                {
                    List<FacebookFormatoPostDTO> conjuntoAnuncioFacebookFormatoPost = new List<FacebookFormatoPostDTO>();

                    foreach (var urlRelativa in segmentoListaConjuntoAnuncio)
                    {
                        conjuntoAnuncioFacebookFormatoPost.Add(new FacebookFormatoPostDTO()
                        {
                            method = "GET",
                            relative_url = $"{urlRelativa}?fields=id,name,optimization_goal,billing_event,daily_budget,campaign_id,start_time,budget_remaining,created_time,effective_status,configured_status,status,updated_time"
                        });
                    }

                    using (WebClient client = new WebClient())
                    {
                        string cuerpoPost = JsonConvert.SerializeObject(new BatchFacebookFormatoPostDTO { batch = conjuntoAnuncioFacebookFormatoPost.ToArray() });

                        client.Encoding = Encoding.UTF8;
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        client.Headers[HttpRequestHeader.ContentLength] = cuerpoPost.Length.ToString();
                        respuestaFacebook = client.UploadString(urlBaseV12, cuerpoPost);
                    }

                    resultadoSinProcesar = JsonConvert.DeserializeObject<List<FacebookRespuestaBatchDTO>>(respuestaFacebook);

                    foreach (var datoSinProcesar in resultadoSinProcesar)
                        listaJerarquiaFacebook.Add(JsonConvert.DeserializeObject<ConjuntoAnuncioFacebookJerarquiaDTO>(datoSinProcesar.body));
                }

                return listaJerarquiaFacebook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descarga las campanias Facebook y su padre desde la plataforma de Facebook
        /// </summary>
        /// <param name="listaCampania">Lista de string con el id de la plataforma de Facebook</param>
        /// <returns>string</returns>
        public List<CampaniaFacebookDTO> DescargarCampaniayPadre(List<string> listaCampania)
        {
            try
            {
                List<CampaniaFacebookDTO> listaJerarquiaFacebook = new List<CampaniaFacebookDTO>();
                List<FacebookRespuestaBatchDTO> resultadoSinProcesar = new List<FacebookRespuestaBatchDTO>();
                string respuestaFacebook = string.Empty;

                List<List<string>> bloqueListaCampania =
                                listaCampania.Select((x, i) => new { Index = i, Value = x })
                                .GroupBy(x => x.Index / 25)
                                .Select(x => x.Select(v => v.Value).ToList())
                                .ToList();

                foreach (var segmentoListaCampania in bloqueListaCampania)
                {
                    List<FacebookFormatoPostDTO> campaniaFacebookFormatoPost = new List<FacebookFormatoPostDTO>();

                    foreach (var urlRelativa in segmentoListaCampania)
                    {
                        campaniaFacebookFormatoPost.Add(new FacebookFormatoPostDTO()
                        {
                            method = "GET",
                            relative_url = $"{urlRelativa}?fields=id,name,account_id"
                        });
                    }

                    using (WebClient client = new WebClient())
                    {
                        string cuerpoPost = JsonConvert.SerializeObject(new BatchFacebookFormatoPostDTO { batch = campaniaFacebookFormatoPost.ToArray() });

                        client.Encoding = Encoding.UTF8;
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        client.Headers[HttpRequestHeader.ContentLength] = cuerpoPost.Length.ToString();
                        respuestaFacebook = client.UploadString(urlBaseV12, cuerpoPost);
                    }

                    resultadoSinProcesar = JsonConvert.DeserializeObject<List<FacebookRespuestaBatchDTO>>(respuestaFacebook);

                    foreach (var datoSinProcesar in resultadoSinProcesar)
                        listaJerarquiaFacebook.Add(JsonConvert.DeserializeObject<CampaniaFacebookDTO>(datoSinProcesar.body));
                }

                return listaJerarquiaFacebook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
