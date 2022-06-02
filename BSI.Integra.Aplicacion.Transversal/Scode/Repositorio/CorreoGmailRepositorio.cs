using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using AngleSharp;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using AngleSharp.Dom;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Comercial/Oportunidad
    /// Autor: Jose Villena - Priscila Pacsi - Wilber Choque - Jorge Rivera - Gian Miranda - Edgar Serruto - Jashin Salazar
    /// Fecha: 09/04/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_CorreoGmail
    /// </summary>
    public class CorreoGmailRepositorio : BaseRepository<TCorreoGmail, CorreoGmailBO>
    {
        /// <summary>
        /// Expresión regular compilada para rendimiento..
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        #region Metodos Base
        public CorreoGmailRepositorio() : base()
        {
        }
        public CorreoGmailRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CorreoGmailBO> GetBy(Expression<Func<TCorreoGmail, bool>> filter)
        {
            IEnumerable<TCorreoGmail> listado = base.GetBy(filter);
            List<CorreoGmailBO> listadoBO = new List<CorreoGmailBO>();
            foreach (var itemEntidad in listado)
            {
                CorreoGmailBO objetoBO = Mapper.Map<TCorreoGmail, CorreoGmailBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public IEnumerable<CorreoGmailBO> GetBy(Expression<Func<TCorreoGmail, bool>> filter, int skip, int take)
        {
            IEnumerable<TCorreoGmail> listado = base.GetBy(filter).Skip(skip).Take(take);
            List<CorreoGmailBO> listadoBO = new List<CorreoGmailBO>();
            foreach (var itemEntidad in listado)
            {
                CorreoGmailBO objetoBO = Mapper.Map<TCorreoGmail, CorreoGmailBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public CorreoGmailBO FirstById(int id)
        {
            try
            {
                TCorreoGmail entidad = base.FirstById(id);
                CorreoGmailBO objetoBO = new CorreoGmailBO();
                Mapper.Map<TCorreoGmail, CorreoGmailBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CorreoGmailBO FirstBy(Expression<Func<TCorreoGmail, bool>> filter)
        {
            try
            {
                TCorreoGmail entidad = base.FirstBy(filter);
                CorreoGmailBO objetoBO = Mapper.Map<TCorreoGmail, CorreoGmailBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CorreoGmailBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCorreoGmail entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<CorreoGmailBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(CorreoGmailBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCorreoGmail entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<CorreoGmailBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TCorreoGmail entidad, CorreoGmailBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TCorreoGmail MapeoEntidad(CorreoGmailBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCorreoGmail entidad = new TCorreoGmail();
                entidad = Mapper.Map<CorreoGmailBO, TCorreoGmail>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.CorreoGmailArchivoAdjunto != null && objetoBO.CorreoGmailArchivoAdjunto.Count > 0)
                {
                    foreach (var hijo in objetoBO.CorreoGmailArchivoAdjunto)
                    {
                        TCorreoGmailArchivoAdjunto entidadHijo = new TCorreoGmailArchivoAdjunto();
                        entidadHijo = Mapper.Map<CorreoGmailArchivoAdjuntoBO, TCorreoGmailArchivoAdjunto>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TCorreoGmailArchivoAdjunto.Add(entidadHijo);
                    }
                }
                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene los contactos que crearan oportunidad
        /// </summary>
        /// <returns>Lista de objetos (CorreoGmailBO)</returns>
        public List<CorreoGmailBO> ObtenerCumplenCriterioAplicaCrearOportunidad()
        {
            try
            {
                return this.GetBy(x => x.AplicaCrearOportunidad && x.CumpleCriterioCrearOportunidad && !x.SeCreoOportunidad & x.IdPrioridadMailChimpListaCorreo != null).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene los correos que pueden ser validados
        /// </summary>
        /// <returns>Lista de objetos (CorreoGmailBO) </returns>
        public List<CorreoGmailBO> ObtenerValidarCumplenCriterio()
        {
            try
            {
                return this.GetBy(x => !x.CumpleCriterioCrearOportunidad && x.IdPersonal == null).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene la ultima fecha de descarga por folder, si no existe trae un dia anterior a la fecha actual
        /// </summary>
        /// <param name="idGmailFolder"> Folder del correo Gmail.</param>
        /// <returns> DateTime </returns>
        public DateTime ObtenerUltimaFechaDescarga(int idGmailFolder)
        {
            try
            {
                var fechaUltimoCorreoDescargado = new Dictionary<string, DateTime?>();
                string _query = @"
                        SELECT FechaCreacion
                        FROM mkt.T_CorreoGmail
                        WHERE IdGmailFolder = @idGmailFolder
                              AND IdPersonal IS NULL
                        ORDER BY FechaCreacion DESC;
                        ";
                var resultado = _dapper.FirstOrDefault(_query, new { idGmailFolder });
                if (resultado != "null" && !string.IsNullOrEmpty(resultado))
                {
                    fechaUltimoCorreoDescargado = JsonConvert.DeserializeObject<Dictionary<string, DateTime?>>(resultado);
                }
                //var ultimaCorreoDescargado = this.GetBy(x => x.IdGmailFolder == idGmailFolder).OrderByDescending(x => x.FechaCreacion).FirstOrDefault();
                if (fechaUltimoCorreoDescargado != null)
                {
                    return fechaUltimoCorreoDescargado.Select(x => x.Value).FirstOrDefault().Value;
                }
                return DateTime.Now.AddDays(-1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene informacion de correo.
        /// </summary>
        /// <param name="id"> Id de correo Gmail.</param>
        /// <returns> InformacionCorreoGmailDTO </returns>
        public InformacionCorreoGmailDTO ObtenerInformacionCorreo(int id)
        {
            try
            {
                var listaCorreoGmail = new List<CorreoGmailDTO>();
                var listaInformacionDetalleCorreoGmail = new List<InformacionDetalleCorreoGmailDTO>();
                string query = @"
                        SELECT GmailCorreoId, 
                               CuerpoHTML
                        FROM mkt.V_ObtenerInformacionCorreoGmail
                        WHERE IdCorreoGmail = @id
                              AND EstadoGmailFolder = 1
                              AND EstadoCorreoGmail = 1;
                            ";

                var queryResultado = _dapper.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(queryResultado) && !queryResultado.Contains("[]"))
                {
                    listaCorreoGmail = JsonConvert.DeserializeObject<List<CorreoGmailDTO>>(queryResultado);
                    var queryArchivosAdjuntos = @"
                        SELECT IdCorreoGmailArchivoAdjunto, 
                               NombreCorreoGmailArchivoAdjunto
                        FROM mkt.V_ObtenerInformacionCorreoGmailArchivoAdjunto
                        WHERE IdCorreoGmail = @id
                              AND EstadoCorreoGmail = 1
                              AND EstadoCorreoGmailArchivoAdjunto = 1;
                        ";
                    var _queryResultadoArchivosAdjuntos = _dapper.QueryDapper(queryArchivosAdjuntos, new { id });
                    if (!string.IsNullOrEmpty(queryArchivosAdjuntos) && !queryArchivosAdjuntos.Contains("[]"))
                    {
                        listaInformacionDetalleCorreoGmail = JsonConvert.DeserializeObject<List<InformacionDetalleCorreoGmailDTO>>(_queryResultadoArchivosAdjuntos);
                    }
                }
                return listaCorreoGmail.Select(x => new InformacionCorreoGmailDTO
                {
                    CuerpoHTML = x.CuerpoHTML,
                    GmailCorreoId = x.GmailCorreoId,
                    ListaInformacionDetalleCorreoGmail = listaInformacionDetalleCorreoGmail
                }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene los correos filtrados.
        /// </summary>
        /// <param name="filtroBandejaCorreo">Filtro para bandeja.</param>
        /// <returns>BandejaCorreoGmailDTO</returns>
        public BandejaCorreoGmailDTO ObtenerCorreos(FiltroBandejaCorreoGmailDTO filtroBandejaCorreo)
        {
            try
            {
                string _asunto = string.Empty;
                string _destinatarios = string.Empty;
                string _remitente = string.Empty;
                DateTime? _fecha = null;

                if (filtroBandejaCorreo.FiltroKendo != null)
                {
                    foreach (var item in filtroBandejaCorreo.FiltroKendo.Filters)
                    {
                        switch (item.Field)
                        {
                            case "Asunto":
                                _asunto = item.Value;
                                break;
                            case "Destinatarios":
                                _destinatarios = item.Value;
                                break;
                            case "Remitente":
                                _remitente = item.Value;
                                break;
                            case "Fecha":
                                _fecha = Convert.ToDateTime(item.Value);
                                break;
                        }
                    }
                }

                var _correosGmail = _dapper.QuerySPDapper("mkt.SP_ObtenerCorreoGmail",
                    new
                    {
                        ListaEstadoCreacionOportunidad = string.Join(",", filtroBandejaCorreo.ListaEstadoCreacionOportunidad.Select(x => x.Valor)),
                        IdGmailFolder = filtroBandejaCorreo.IdGmailFolder,
                        Skip = filtroBandejaCorreo.Skip,
                        Take = filtroBandejaCorreo.Take,
                        Asunto = _asunto,
                        Destinatarios = _destinatarios,
                        Remitente = _remitente,
                        Fecha = _fecha
                    });

                BandejaCorreoGmailDTO bandejaSalidaDTO = new BandejaCorreoGmailDTO();
                if (!string.IsNullOrEmpty(_correosGmail) && _correosGmail != "[]")
                {
                    bandejaSalidaDTO.ListaCorreoGmail = JsonConvert.DeserializeObject<List<ResumenCorreoGmailDTO>>(_correosGmail);
                    bandejaSalidaDTO.TotalCorreoGmail = bandejaSalidaDTO.ListaCorreoGmail.FirstOrDefault().TotalCorreos ?? 0;
                    foreach (var item in bandejaSalidaDTO.ListaCorreoGmail)
                    {
                        item.CuerpoHtml = this.EliminarHtmlCssTags(item.CuerpoHtml);
                    }
                    return bandejaSalidaDTO;
                }
                bandejaSalidaDTO.TotalCorreoGmail = 0;
                return bandejaSalidaDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene los correos por oportunidad creada.
        /// </summary>
        /// <param name="filtroBandejaCorreo">Filtro para bandeja.</param>
        /// <returns>BandejaCorreoGmailDTO</returns>
        public BandejaCorreoGmailDTO ObtenerCorreosOportunidadCreada(FiltroBandejaCorreoGmailDTO filtroBandejaCorreo)
        {
            try
            {
                string _asunto = string.Empty;
                string _destinatarios = string.Empty;
                string _remitente = string.Empty;
                DateTime? _fecha = null;

                if (filtroBandejaCorreo.FiltroKendo != null)
                {
                    foreach (var item in filtroBandejaCorreo.FiltroKendo.Filters)
                    {
                        switch (item.Field)
                        {
                            case "Asunto":
                                _asunto = item.Value;
                                break;
                            case "Destinatarios":
                                _destinatarios = item.Value;
                                break;
                            case "Remitente":
                                _remitente = item.Value;
                                break;
                            case "Fecha":
                                _fecha = Convert.ToDateTime(item.Value);
                                break;
                        }
                    }
                }

                var _correosGmail = _dapper.QuerySPDapper("mkt.SP_ObtenerCorreoGmailOportunidadCreada",
                    new
                    {
                        ListaEstadoCreacionOportunidad = string.Join(",", filtroBandejaCorreo.ListaEstadoCreacionOportunidad.Select(x => x.Valor)),
                        //IdGmailFolder = filtroBandejaCorreo.IdGmailFolder,
                        Skip = filtroBandejaCorreo.Skip,
                        Take = filtroBandejaCorreo.Take,
                        Asunto = _asunto,
                        Destinatarios = _destinatarios,
                        Remitente = _remitente,
                        Fecha = _fecha
                    });

                BandejaCorreoGmailDTO bandejaSalidaDTO = new BandejaCorreoGmailDTO();
                if (!string.IsNullOrEmpty(_correosGmail) && _correosGmail != "[]")
                {
                    bandejaSalidaDTO.ListaCorreoGmail = JsonConvert.DeserializeObject<List<ResumenCorreoGmailDTO>>(_correosGmail);
                    bandejaSalidaDTO.TotalCorreoGmail = bandejaSalidaDTO.ListaCorreoGmail.FirstOrDefault().TotalCorreos ?? 0;
                    foreach (var item in bandejaSalidaDTO.ListaCorreoGmail)
                    {
                        item.CuerpoHtml = this.EliminarHtmlCssTags(item.CuerpoHtml);
                    }
                    return bandejaSalidaDTO;
                }
                bandejaSalidaDTO.TotalCorreoGmail = 0;
                return bandejaSalidaDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene los uids descargados para la fecha enviada.
        /// </summary>
        /// <param name="fecha">Fecha</param>
        /// <returns>Lista (Long)</returns>
        public List<long> ObtenerUidsPorFecha(DateTime fecha)
        {
            try
            {
                //return this.GetBy(x => x.Fecha.Date == fecha.Date).Select(x => x.GmailCorreoId).ToList();
                return this.GetBy(x => x.Fecha.Date == fecha.Date && x.IdPersonal == null).Select(x => x.GmailCorreoId).ToList();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Eliminar tags de HTML y CSS
        /// </summary>
        /// <param name="texto"> Texto</param>
        /// <returns> string </returns>
        private string EliminarHtmlCssTags(string texto)
        {
            try
            {
                string Pat = "<(script|style)\\b[^>]*?>.*?</\\1>";
                texto = Regex.Replace(texto, Pat, "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

                String input = texto;
                StringBuilder sb = new StringBuilder();
                bool inside = false;
                bool delete = false;
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i].Equals('<'))
                    {
                        inside = true;
                        delete = false;
                    }
                    else if (input[i].Equals('>'))
                    {
                        inside = false;
                        delete = false;
                    }
                    else if (inside)
                    {
                        //if (input[i].Equals(' '))
                        delete = true;
                    }
                    if (!delete)
                        sb.Append(input[i]);
                }
                var resultadoFinal = sb.ToString();
                resultadoFinal = resultadoFinal.Replace("<>", "");
                return resultadoFinal;
            }
            catch (Exception e)
            {
                return "";
                //throw e;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene los correos con contactos a desuscribir
        /// </summary>
        /// <returns>Lista de objetos(CorreoGmailBO)</returns>
        public List<CorreoGmailBO> ObtenerMarcadosDesuscribir()
        {
            try
            {
                return this.GetBy(x => x.EsMarcadoDesuscrito == true & x.EsDesuscritoCorrectamente == false && x.EsDescartado == false).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene datas de un correo
        /// </summary>
        /// <returns>Lista de objetos(DatosGmailDTO)</returns>
        public List<DatosGmailDTO> ObtenerDatosCorreo()
        {
            try
            {
                var listaCorreoGmail = new List<DatosGmailDTO>();
                string _query = @"
                        SELECT Id, 
                               Email,
                               AliasCorreo,
                               Clave
                        FROM mkt.V_ObtenerDatosCorreo
                            ";
                var _queryResultado = _dapper.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(_queryResultado) && !_queryResultado.Contains("[]"))
                {
                    listaCorreoGmail = JsonConvert.DeserializeObject<List<DatosGmailDTO>>(_queryResultado);
                }

                return listaCorreoGmail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene datas de un correo por Id de personal
        /// </summary>
        /// <param name="IdPersonal">Id de personal</param>
        /// <returns>Lista de objetos(DatosGmailDTO)</returns>
        public List<DatosGmailDTO> ObtenerDatosCorreoPorIdPersonal(int IdPersonal)
        {
            try
            {
                var listaCorreoGmail = new List<DatosGmailDTO>();
                string _query = @"
                        SELECT Id, 
                               Email,
                               AliasCorreo,
                               Clave
                        FROM mkt.V_ObtenerDatosCorreo
                        WHERE Id=@IdPersonal
                            ";
                var _queryResultado = _dapper.QueryDapper(_query, new { IdPersonal });

                if (!string.IsNullOrEmpty(_queryResultado) && !_queryResultado.Contains("[]"))
                {
                    listaCorreoGmail = JsonConvert.DeserializeObject<List<DatosGmailDTO>>(_queryResultado);
                }

                return listaCorreoGmail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Cuenta correos por persona.
        /// </summary>
        /// <param name="IdPersonal">Id de personal</param>
        /// <param name="IdFolder">Id de folder de correo</param>
        /// <returns>int</returns>
        public int ContadorCorreosPorPersona(int IdPersonal, int IdFolder)
        {
            try
            {
                var listaCorreoGmail = new Dictionary<string, int>();
                int TotalCorreos = 0;

                string _query = @"
                        SELECT COUNT(*) Cantidad
                        FROM mkt.T_CorreoGmail
                        WHERE IdPersonal is not null and IdPersonal=@IdPersonal and IdGmailFolder=@IdFolder
                            ";
                var _queryResultado = _dapper.FirstOrDefault(_query, new { IdPersonal, IdFolder });

                if (!string.IsNullOrEmpty(_queryResultado) && !_queryResultado.Contains("[]"))
                {
                    listaCorreoGmail = JsonConvert.DeserializeObject<Dictionary<string, int>>(_queryResultado);
                }

                if (listaCorreoGmail != null)
                {
                    var repuesta = listaCorreoGmail.Select(x => x.Value).FirstOrDefault();
                    if (repuesta != null)
                    {
                        TotalCorreos = repuesta;
                    }
                    else
                    {
                        TotalCorreos = 0;
                    }
                }
                else
                {
                    TotalCorreos = 0;
                }


                return TotalCorreos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Filtro de correos por persona.
        /// </summary>
        /// <param name="IdPersonal">Id de personal</param>
        /// <param name="IdFolder">Id de folder de correo</param>
        /// <param name="QueryFiltro">Filtro para query</param>
        /// <returns>Lista de objetos (CorreoDTO)</returns>
        public List<CorreoDTO> FiltroCorreosPorPersona(int IdPersonal, int IdFolder, string QueryFiltro)
        {
            try
            {
                var listaCorreoGmail = new List<CorreoDTO>();
                int TotalCorreos = 0;

                string _query = string.Empty;

                _query = @"
                        SELECT GmailCorreoId AS Id, Asunto, CuerpoHTML AS EmailBody, Fecha, EmailRemitente AS Remitente, Destinatarios
                        FROM mkt.T_CorreoGmail
                        WHERE IdPersonal is not null and IdPersonal=@IdPersonal and IdGmailFolder=@IdFolder ";

                _query = _query + QueryFiltro + " GROUP BY GmailCorreoId,Asunto,CuerpoHTML,Fecha,EmailRemitente,Destinatarios";

                var _queryResultado = _dapper.QueryDapper(_query, new { IdPersonal, IdFolder });

                if (!string.IsNullOrEmpty(_queryResultado) && !_queryResultado.Contains("[]"))
                {
                    listaCorreoGmail = JsonConvert.DeserializeObject<List<CorreoDTO>>(_queryResultado);
                }

                return listaCorreoGmail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Filtro de correos por persona.
        /// </summary>
        /// <param name="IdFolder">Id de folder de correo</param>
        /// <param name="QueryFiltro">Filtro para query</param>
        /// <returns>Lista de objetos (CorreoDTO)</returns>
        public List<CorreoDTO> FiltroCorreosPorPersona(int IdFolder, string QueryFiltro)
        {
            try
            {
                var listaCorreoGmail = new List<CorreoDTO>();
                int TotalCorreos = 0;

                string _query = string.Empty;

                _query = @"
                        SELECT GmailCorreoId AS Id, Asunto, Fecha, EmailRemitente AS Remitente, Destinatarios, IdPersonal, EmailConCopia AS ConCopia
                        FROM mkt.T_CorreoGmail WITH(NOLOCK)
                        WHERE IdPersonal is not null AND IdGmailFolder=@IdFolder ";

                _query = _query + QueryFiltro + " GROUP BY GmailCorreoId,Asunto,Fecha,EmailRemitente,Destinatarios,IdPersonal,EmailConCopia ORDER BY Fecha DESC";

                var _queryResultado = _dapper.QueryDapper(_query, new { IdFolder });

                if (!string.IsNullOrEmpty(_queryResultado) && !_queryResultado.Contains("[]"))
                {
                    listaCorreoGmail = JsonConvert.DeserializeObject<List<CorreoDTO>>(_queryResultado);
                }

                return listaCorreoGmail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene data modelada de mkt.T_GmailCorreo para casos webinar y aonline.
        /// </summary>
        /// <param name="QueryFiltro">Filtro para query</param>
        /// <returns>Lista de objetos (CorreoDTO)</returns>
        public List<CorreoDTO> FiltroCorreosPorPersonaGmailCorreo(string QueryFiltro)
        {
            try
            {
                var listaCorreoGmail = new List<CorreoDTO>();
                int TotalCorreos = 0;

                string _query = string.Empty;

                _query = @"
                        SELECT Id, Asunto, EmailBody,  Fecha, Remitente, Destinatarios, IdPersonal, ConCopia
                        from mkt.V_GmailCorreo_ObtenerMensajesEnviados
                        WHERE IdPersonal is not null";

                _query = _query + QueryFiltro + " GROUP BY Id, Asunto, EmailBody, Fecha, Remitente, Destinatarios, IdPersonal, ConCopia ORDER BY Fecha DESC";

                var _queryResultado = _dapper.QueryDapper(_query, new { });

                if (!string.IsNullOrEmpty(_queryResultado) && !_queryResultado.Contains("[]"))
                {
                    listaCorreoGmail = JsonConvert.DeserializeObject<List<CorreoDTO>>(_queryResultado);
                }

                return listaCorreoGmail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene correos por grupos con version.
        /// </summary>
        /// <param name="IdCentroCosto">Id centro de costo</param>
        /// <param name="IdPaquete">Id de paquete</param>
        /// <param name="Estados">Estado</param>
        /// <param name="SubEstados">Sub estado</param>
        /// <returns>ListaCorreosGrupoBO</returns>
        public ListaCorreosGrupoBO obtenerCorreosGruposConVersion(int IdCentroCosto, int IdPaquete, List<int> Estados, List<int> SubEstados)
        {
            try
            {
                var _queryResultado = "";//inicializando variable _queryResultado
                var listaCorreoGmail = new List<ValorStringDTO>();
                ListaCorreosGrupoBO _rptQuery = new ListaCorreosGrupoBO();
                int TotalCorreos = 0;

                string _query = string.Empty;
                if (Estados.Count == 0 && SubEstados.Count == 0) //Si solo hay Centro de costo y paquete
                {
                    _query = @"
                        SELECT Emails as Valor
                        FROM ope.V_CorreosGruposEnvio
                        WHERE IdCentroCosto=@IdCentroCosto and IdPaquete=@IdPaquete";

                    _queryResultado = _dapper.QueryDapper(_query, new { IdCentroCosto, IdPaquete });
                }
                if (Estados.Count != 0 && SubEstados.Count == 0) //si hay centro de costo,paquete y estado pero no SubEstado
                {
                    _query = @"
                        SELECT Emails as Valor
                        FROM ope.V_CorreosGruposEnvio
                        WHERE IdCentroCosto=@IdCentroCosto and IdPaquete=@IdPaquete and EstadoMatricula in @Estados";

                    _queryResultado = _dapper.QueryDapper(_query, new { IdCentroCosto, IdPaquete, Estados });
                }
                if (Estados.Count != 0 && SubEstados.Count != 0)//Si hay  los 4 campos
                {
                    _query = @"
                        SELECT Emails as Valor
                        FROM ope.V_CorreosGruposEnvio
                        WHERE IdCentroCosto=@IdCentroCosto and IdPaquete=@IdPaquete and EstadoMatricula in @Estados and SubEstadoMatricula in @SubEstados";

                    _queryResultado = _dapper.QueryDapper(_query, new { IdCentroCosto, IdPaquete, Estados, SubEstados });
                }


                if (!string.IsNullOrEmpty(_queryResultado) && !_queryResultado.Contains("[]"))
                {
                    listaCorreoGmail = JsonConvert.DeserializeObject<List<ValorStringDTO>>(_queryResultado);
                    var repuesta = listaCorreoGmail.Select(x => x.Valor).ToList();

                    _rptQuery.ListaCorreos = string.Join(",", repuesta);
                    _rptQuery.TotalCorreos = repuesta.Count;
                    _rptQuery.Errores = false;
                }
                else
                {
                    _rptQuery.ListaCorreos = "";
                    _rptQuery.TotalCorreos = 0;
                    _rptQuery.Errores = true;
                }

                return _rptQuery;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene correos por grupos sin version.
        /// </summary>
        /// <param name="IdCentroCosto">Id centro de costo</param>
        /// <param name="IdPaquete">Id de paquete</param>
        /// <param name="Estados">Estado</param>
        /// <param name="SubEstados">Sub estado</param>
        /// <returns>ListaCorreosGrupoBO</returns>
        public ListaCorreosGrupoBO obtenerCorreosGruposSinVersion(int IdCentroCosto, List<int> Estados, List<int> SubEstados)
        {
            try
            {
                var _queryResultado = "";//inicializando variable _queryResultado
                var listaCorreoGmail = new List<ValorStringDTO>();
                ListaCorreosGrupoBO _rptQuery = new ListaCorreosGrupoBO();
                int TotalCorreos = 0;

                string _query = string.Empty;
                if (Estados.Count == 0 && SubEstados.Count == 0) //Si solo hay Centro de costo
                {
                    _query = @"
                        SELECT Emails as Valor
                        FROM ope.V_CorreosGruposEnvio
                        WHERE IdCentroCosto=@IdCentroCosto";
                    _queryResultado = _dapper.QueryDapper(_query, new { IdCentroCosto });
                }
                if (Estados.Count != 0 && SubEstados.Count == 0) //si hay centro de costo, estado pero no SubEstado
                {
                    _query = @"
                        SELECT Emails as Valor
                        FROM ope.V_CorreosGruposEnvio
                        WHERE IdCentroCosto=@IdCentroCosto and EstadoMatricula in @Estados";
                    _queryResultado = _dapper.QueryDapper(_query, new { IdCentroCosto, Estados });
                }
                if (Estados.Count != 0 && SubEstados.Count != 0)//Si hay  los 3 campos
                {
                    _query = @"
                        SELECT Emails as Valor
                        FROM ope.V_CorreosGruposEnvio
                        WHERE IdCentroCosto=@IdCentroCosto and EstadoMatricula in @Estados and SubEstadoMatricula in @SubEstados";
                    _queryResultado = _dapper.QueryDapper(_query, new { IdCentroCosto, Estados, SubEstados });
                }


                if (!string.IsNullOrEmpty(_queryResultado) && !_queryResultado.Contains("[]"))
                {
                    listaCorreoGmail = JsonConvert.DeserializeObject<List<ValorStringDTO>>(_queryResultado);
                    var repuesta = listaCorreoGmail.Select(x => x.Valor).ToList();

                    _rptQuery.ListaCorreos = string.Join(",", repuesta);
                    _rptQuery.TotalCorreos = repuesta.Count;
                    _rptQuery.Errores = false;
                }
                else
                {
                    _rptQuery.ListaCorreos = "";
                    _rptQuery.TotalCorreos = 0;
                    _rptQuery.Errores = true;
                }

                return _rptQuery;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns>string</returns>
        public string SubirArchivoRepositorio(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {

                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"operaciones/comprobantes/";
                    //string _direccionBlob = @"correos/individuales/";

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = tipo;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = tipo;
                    Stream stream = new MemoryStream(archivo);
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    if (correcto)
                    {
                        _nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + nombreArchivo.Replace(" ", "%20");
                    }
                    else
                    {
                        _nombreLink = "";
                    }
                    return _nombreLink;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                return "";
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Indica si debe seguir descargando los cuerpo html
        /// </summary>
        /// <returns>Booleano</returns>
        public bool ContinuarDescargando()
        {
            try
            {
                var _resultado = new ValorBoolDTO();
                var query = $@"mkt.SP_ContinuarDescargando";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { });
                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorBoolDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene correo de Personal Asignado
        /// </summary>
        /// <param name="correoAlumno">Correo del alumno</param>
        /// <returns>Lista de objeto(ValorStringDTO)</returns>
        public List<ValorStringDTO> ObtenerEmailPersonalAsignado(string correoAlumno)
        {
            var _resultado = new List<ValorStringDTO>();
            try
            {

                var query = $@"mkt.SP_ObtenerEmailPersonalAsignado";
                var resultado = _dapper.QuerySPDapper(query, new { correoAlumno });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    _resultado = JsonConvert.DeserializeObject<List<ValorStringDTO>>(resultado);
                }
                return _resultado;
            }
            catch (Exception)
            {
                return _resultado;
            }
        }
    }
}
