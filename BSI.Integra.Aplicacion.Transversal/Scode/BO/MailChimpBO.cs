using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: MailChimp
    /// Autor: Gian Miranda
    /// Fecha: 23/07/2021
    /// <summary>
    /// BO para la logica de peticiones a MailChimp
    /// </summary>
    public class MailChimpBO
    {
        private string urlBaseMailchimpUs12V3 = "https://us12.api.mailchimp.com/3.0/";
        private string username = string.Empty;
        private string token = string.Empty;

        private readonly integraDBContext _integraDBContext;
        private readonly AutenticacionServicioExternoRepositorio _repAutenticacionServicioExterno;

        public MailChimpBO()
        {
            _repAutenticacionServicioExterno = new AutenticacionServicioExternoRepositorio();

            var objUsername = _repAutenticacionServicioExterno.FirstById(ValorEstatico.IdUsernameMailChimpAPIMarketing);
            username = objUsername != null ? objUsername.Valor : string.Empty;

            var objToken = _repAutenticacionServicioExterno.FirstById(ValorEstatico.IdTokenMailChimpAPIMarketing);
            token = objToken != null ? objToken.Valor : string.Empty;
        }

        public MailChimpBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repAutenticacionServicioExterno = new AutenticacionServicioExternoRepositorio(_integraDBContext);

            var objUsername = _repAutenticacionServicioExterno.FirstById(ValorEstatico.IdUsernameMailChimpAPIMarketing);
            username = objUsername != null ? objUsername.Valor : string.Empty;

            var objToken = _repAutenticacionServicioExterno.FirstById(ValorEstatico.IdTokenMailChimpAPIMarketing);
            token = objToken != null ? objToken.Valor : string.Empty;
        }

        /// <summary>
        /// Descarga todas las campanias de Mailchimp segun el intervalo de fechas de creacion
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio de campanias</param>
        /// <param name="fechaFin">Fecha de fin de campanias</param>
        /// <returns>Objeto de clase DescargarCampaniaMailChimpPorIntervaloFecha</returns>
        public InformacionCampaniaMailchimpFormatoDTO DescargarCampaniaMailChimpPorIntervaloFecha(string fechaInicio, string fechaFin)
        {
            try
            {
                var resultado = new InformacionCampaniaMailchimpFormatoDTO();
                var respuestaMailChimp = string.Empty;

                int cantidadSegmento = 1000;
                int? totalRepeticiones = null;
                int cantidadSegmentoActual = 0;

                do
                {
                    using (WebClient client = new WebClient())
                    {
                        string urlGet = $"{urlBaseMailchimpUs12V3}campaigns?count={cantidadSegmento}{(totalRepeticiones == null ? string.Empty : string.Concat("&offset=", cantidadSegmentoActual)) }{(string.IsNullOrEmpty(fechaInicio) ? string.Empty : string.Concat("&since_create_time=", fechaInicio))}{(string.IsNullOrEmpty(fechaFin) ? string.Empty : string.Concat("&before_create_time=", fechaFin))}";
                        string credenciales = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + token));

                        client.Encoding = Encoding.UTF8;
                        client.Headers[HttpRequestHeader.Authorization] = $"Basic {credenciales}";
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        respuestaMailChimp = client.DownloadString(urlGet);
                    }

                    var resultadoSinProcesar = JsonConvert.DeserializeObject<InformacionCampaniaMailchimpFormatoDTO>(respuestaMailChimp);

                    if (resultado.campaigns == null)
                        resultado = resultadoSinProcesar;
                    else
                        resultado.campaigns.AddRange(resultadoSinProcesar.campaigns);

                    totalRepeticiones = totalRepeticiones ?? (resultado.total_items / cantidadSegmento) + 1;
                    cantidadSegmentoActual += cantidadSegmento;

                    totalRepeticiones--;
                } while (totalRepeticiones.Value > 0 && resultado.total_items > 0);

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descarga todas las listas de Mailchimp segun el intervalo de fechas de creacion
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio de campanias</param>
        /// <param name="fechaFin">Fecha de fin de campanias</param>
        /// <returns>Objeto de clase InformacionListaMailchimpFormatoDTO</returns>
        public InformacionListaMailchimpFormatoDTO DescargarListaMailChimpPorIntervaloFecha(string fechaInicio, string fechaFin)
        {
            try
            {
                var resultado = new InformacionListaMailchimpFormatoDTO();
                var respuestaMailChimp = string.Empty;

                int cantidadSegmento = 1000;
                int? totalRepeticiones = null;
                int cantidadSegmentoActual = 0;

                do
                {
                    using (WebClient client = new WebClient())
                    {
                        string urlGet = $"{urlBaseMailchimpUs12V3}lists?count={cantidadSegmento}{(totalRepeticiones == null ? string.Empty : string.Concat("&offset=", cantidadSegmentoActual)) }{(string.IsNullOrEmpty(fechaInicio) ? string.Empty : string.Concat("&since_date_created=", fechaInicio))}{(string.IsNullOrEmpty(fechaFin) ? string.Empty : string.Concat("&before_date_created=", fechaFin))}";
                        string credenciales = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + token));

                        client.Encoding = Encoding.UTF8;
                        client.Headers[HttpRequestHeader.Authorization] = $"Basic {credenciales}";
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        respuestaMailChimp = client.DownloadString(urlGet);
                    }

                    var resultadoSinProcesar = JsonConvert.DeserializeObject<InformacionListaMailchimpFormatoDTO>(respuestaMailChimp);

                    if (resultado.lists == null)
                        resultado = resultadoSinProcesar;
                    else
                        resultado.lists.AddRange(resultadoSinProcesar.lists);

                    totalRepeticiones = totalRepeticiones ?? (resultado.total_items / cantidadSegmento) + 1;
                    cantidadSegmentoActual += cantidadSegmento;

                    totalRepeticiones--;
                } while (totalRepeticiones.Value > 0 && resultado.total_items > 0);

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descarga todos los miembros de una lista de Mailchimp
        /// </summary>
        /// <param name="mailchimpIdLista">Id de una lista de mailchimp (Formato Mailchimp)</param>
        /// <returns>Objeto de clase InformacionListaMailchimpFormatoDTO</returns>
        public InformacionMiembroMailchimpFormatoDTO DescargarMiembroMailChimpPorListaMailchimpId(string mailchimpIdLista)
        {
            try
            {
                var resultado = new InformacionMiembroMailchimpFormatoDTO();
                var respuestaMailChimp = string.Empty;

                int cantidadSegmento = 1000;
                int? totalRepeticiones = null;
                int cantidadSegmentoActual = 0;

                do
                {
                    using (WebClient client = new WebClient())
                    {
                        string urlGet = $"{urlBaseMailchimpUs12V3}lists/{mailchimpIdLista}/members?count={cantidadSegmento}{(totalRepeticiones == null ? string.Empty : string.Concat("&offset=", cantidadSegmentoActual)) }";
                        string credenciales = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + token));

                        client.Encoding = Encoding.UTF8;
                        client.Headers[HttpRequestHeader.Authorization] = $"Basic {credenciales}";
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        respuestaMailChimp = client.DownloadString(urlGet);
                    }

                    var resultadoSinProcesar = JsonConvert.DeserializeObject<InformacionMiembroMailchimpFormatoDTO>(respuestaMailChimp);

                    if (resultado.members == null)
                        resultado = resultadoSinProcesar;
                    else
                        resultado.members.AddRange(resultadoSinProcesar.members);

                    totalRepeticiones = totalRepeticiones ?? (resultado.total_items / cantidadSegmento) + 1;
                    cantidadSegmentoActual += cantidadSegmento;

                    totalRepeticiones--;
                } while (totalRepeticiones.Value > 0 && resultado.total_items > 0);

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descarga metrica de las campanias de MailChimp
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio de la descarga de metrica</param>
        /// <param name="fechaFin">Fecha de fin de la descarga de metrica</param>
        /// <returns>Objeto de clase ListaReporteCampaniaFacebookResultadoCrudoDTO</returns>
        public ListaReporteCampaniaMailchimpResultadoCrudoDTO DescargarReporteMailChimpPorIntervaloFecha(string fechaInicio, string fechaFin)
        {
            try
            {
                ListaReporteCampaniaMailchimpResultadoCrudoDTO resultado = new ListaReporteCampaniaMailchimpResultadoCrudoDTO();

                string respuestaMailChimp = string.Empty;

                int cantidadSegmento = 1000;
                int? totalRepeticiones = null;
                int cantidadSegmentoActual = 0;

                do
                {
                    using (WebClient client = new WebClient())
                    {
                        string urlGet = $"{urlBaseMailchimpUs12V3}reports?count={cantidadSegmento}{(totalRepeticiones == null ? string.Empty : string.Concat("&offset=", cantidadSegmentoActual)) }{(string.IsNullOrEmpty(fechaInicio) ? string.Empty : string.Concat("&since_send_time=", fechaInicio))}{(string.IsNullOrEmpty(fechaFin) ? string.Empty : string.Concat("&before_send_time=", fechaFin))}";
                        string credenciales = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + token));

                        client.Encoding = Encoding.UTF8;
                        client.Headers[HttpRequestHeader.Authorization] = $"Basic {credenciales}";
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        respuestaMailChimp = client.DownloadString(urlGet);
                    }

                    var resultadoSinProcesar = JsonConvert.DeserializeObject<ListaReporteCampaniaMailchimpResultadoCrudoDTO>(respuestaMailChimp);

                    if (resultado.reports == null)
                        resultado = resultadoSinProcesar;
                    else
                        resultado.reports.AddRange(resultadoSinProcesar.reports);

                    totalRepeticiones = totalRepeticiones ?? (resultado.total_items / cantidadSegmento) + 1;
                    cantidadSegmentoActual += cantidadSegmento;

                    totalRepeticiones--;
                } while (totalRepeticiones.Value > 0 && resultado.total_items > 0);

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descarga metrica de los reportes diarios de mailchimp
        /// </summary>
        /// <param name="idMailChimpCampania">Id de las listas de MailChimp</param>
        /// <param name="FechaAnalizada">Fecha para analizar la fecha</param>
        /// <returns>Objeto de clase ListaActividadDiariaMailChimpCrudoDTO</returns>
        public ListaActividadDiariaMailChimpCrudoDTO DescargarReporteDiarioMailChimpPorIntervaloFecha(string idMailChimpCampania, DateTime? FechaAnalizada)
        {
            try
            {
                ListaActividadDiariaMailChimpCrudoDTO resultadoPorCampania = new ListaActividadDiariaMailChimpCrudoDTO();

                int cantidadSegmento = 1000;

                string respuestaMailChimp = string.Empty;

                int? totalRepeticiones = null;
                int cantidadSegmentoActual = 0;

                do
                {
                    using (WebClient client = new WebClient())
                    {
                        string urlGet = $"{urlBaseMailchimpUs12V3}reports/{idMailChimpCampania}/open-details?count={cantidadSegmento}{(totalRepeticiones == null ? string.Empty : string.Concat("&offset=", cantidadSegmentoActual)) }";
                        string credenciales = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + token));

                        client.Encoding = Encoding.UTF8;
                        client.Headers[HttpRequestHeader.Authorization] = $"Basic {credenciales}";
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        respuestaMailChimp = client.DownloadString(urlGet);
                    }

                    var resultadoSinProcesar = JsonConvert.DeserializeObject<ListaActividadDiariaMailChimpCrudoDTO>(respuestaMailChimp);

                    if (resultadoPorCampania.members == null)
                        resultadoPorCampania = resultadoSinProcesar;
                    else
                        resultadoPorCampania.members.AddRange(resultadoSinProcesar.members);

                    totalRepeticiones = totalRepeticiones ?? (resultadoPorCampania.total_items / cantidadSegmento) + 1;
                    cantidadSegmentoActual += cantidadSegmento;

                    totalRepeticiones--;
                } while (totalRepeticiones.Value > 0 && resultadoPorCampania.total_items > 0);

                return resultadoPorCampania;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
