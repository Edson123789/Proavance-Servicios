using BSI.Integra.Persistencia.SCode.Repository;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Servicios.BO;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using MailChimp.Net.Models;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using MailChimp.Net.Core;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using System.Globalization;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    /// BO: Marketing/PrioridadMailChimpLista
    /// Autor: Joao Benavente - Johan Cayo - Wilber Choque - Carlos Crispin - Gian Miranda
    /// Fecha: 26/07/2021
    /// <summary>
    /// BO para la logica de PrioridadMailChimpLista
    /// </summary>
    public class PrioridadMailChimpListaBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdCampaniaMailing                   Id de la campania Mailing (PK de la tabla mkt.T_CampaniaMailing)
        /// IdCampaniaMailingDetalle            Id del detalle de la campania Mailing (PK de la tabla mkt.T_CampaniaMailingDetalle)
        /// AsuntoLista                         Asunto de la lista procesada
        /// Contenido                           Contenido HTML del correo
        /// Asunto                              Asunto HTML del correo
        /// IdPersonal                          Id del personal (PK de la tabla gp.T_Personal)
        /// NombreAsesor                        Nombre del asesor asignado
        /// Alias                               Correo con el dominio personalizado del asesor
        /// Etiquetas                           Etiquetas MailChimp enviadas para su reemplazo
        /// IdCampaniaMailChimp                 Id de la campania segun la API de MailChimp
        /// IdListaMailChimp                    Id de la lista segun la API de MailChimp
        /// Enviado                             Flag para las listas enviadas
        /// FechaEnvio                          Fecha que se realizo el envio
        /// IdMigracion                         Id migracion de V3 (Campo nullable)
        /// NroIntentos                         Nro de Intentos de ejecucion
        /// EsSubidoCorrectamente               Flag para la subida correcta
        /// IdCampaniaGeneralDetalle            Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)
        /// CantidadEnviadosMailChimp           Cantidad enviados desde MailChimp
        /// CantidadAperturaUnica               Cantidad de apertura unica
        /// CantidadReboteSuave                 Cantidad de rebotes suaves
        /// CantidadReboteDuro                  Cantidad de rebotes duros
        /// CantidadReboteSintaxis              Cantidad de rebotes por error de sintaxis
        /// TasaApertura                        Tasa de apertura
        /// CantidadClicUnico                   Cantidad de clics unicos
        /// CantidadTotalClic                   Cantidad total de clics
        /// CantidadReporteAbuso                Cantidad de reportes de abuso
        /// CantidadDesuscritos                 Cantidad de desuscritos
        /// TasaClic                            Tasa de clicks

        public int IdCampaniaMailing { get; set; }
        public int? IdCampaniaMailingDetalle { get; set; }
        public string AsuntoLista { get; set; }
        public string Contenido { get; set; }
        public string Asunto { get; set; }
        public int IdPersonal { get; set; }
        public string NombreAsesor { get; set; }
        public string Alias { get; set; }
        public string Etiquetas { get; set; }
        public string IdCampaniaMailchimp { get; set; }
        public string IdListaMailchimp { get; set; }
        public bool Enviado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? NroIntentos { get; set; }
        public bool? EsSubidoCorrectamente { get; set; }
        public int? IdCampaniaGeneralDetalle { get; set; }
        public int? CantidadEnviadosMailChimp { get; set; }
        public int? CantidadAperturaUnica { get; set; }
        public int? CantidadReboteSuave { get; set; }
        public int? CantidadReboteDuro { get; set; }
        public int? CantidadReboteSintaxis { get; set; }
        public decimal? TasaApertura { get; set; }
        public int? CantidadClicUnico { get; set; }
        public int? CantidadTotalClic { get; set; }
        public int? CantidadReporteAbuso { get; set; }
        public int? CantidadDesuscritos { get; set; }
        public decimal? TasaClic { get; set; }

        public List<PrioridadMailChimpListaCorreoBO> PrioridadMailchimpListaCorreo;
        private readonly integraDBContext _integraDContext;
        private readonly MailChimpBO MailChimp;
        private readonly PrioridadMailChimpListaRepositorio _repPrioridadMailChimpLista;
        private readonly MailchimpEstadisticaPorPrioridadRepositorio _repMailchimpEstadisticaPorPrioridad;

        private readonly CampaniaMailchimpRepositorio _repCampaniaMailchimp;
        private readonly ListaMailchimpRepositorio _repListaMailchimp;
        private readonly MiembroMailchimpRepositorio _repMiembroMailchimp;

        private DapperRepository DapperRepository;
        private TMK_MailchimpServiceImpl _mailchimpServiceImpl;
        public PrioridadMailChimpListaBO()
        {
            _mailchimpServiceImpl = new TMK_MailchimpServiceImpl();
            _integraDContext = new integraDBContext();
            DapperRepository = new DapperRepository(_integraDContext);
            _repPrioridadMailChimpLista = new PrioridadMailChimpListaRepositorio();
            _repMailchimpEstadisticaPorPrioridad = new MailchimpEstadisticaPorPrioridadRepositorio();

            _repCampaniaMailchimp = new CampaniaMailchimpRepositorio();
            _repListaMailchimp = new ListaMailchimpRepositorio();
            _repMiembroMailchimp = new MiembroMailchimpRepositorio();
            MailChimp = new MailChimpBO();
        }

        public PrioridadMailChimpListaBO(integraDBContext integraDBContext)
        {
            _integraDContext = integraDBContext;
            _mailchimpServiceImpl = new TMK_MailchimpServiceImpl();
            DapperRepository = new DapperRepository(_integraDContext);
            _repPrioridadMailChimpLista = new PrioridadMailChimpListaRepositorio(_integraDContext);
            _repMailchimpEstadisticaPorPrioridad = new MailchimpEstadisticaPorPrioridadRepositorio(_integraDContext);
            _repCampaniaMailchimp = new CampaniaMailchimpRepositorio(_integraDContext);
            _repListaMailchimp = new ListaMailchimpRepositorio(_integraDContext);
            _repMiembroMailchimp = new MiembroMailchimpRepositorio(_integraDContext);
            MailChimp = new MailChimpBO();
        }

        public async Task<List> CrearListaMailchimp()
        {
            try
            {
                ListaMailchimpDatosDTO listaM = new ListaMailchimpDatosDTO
                {
                    AsuntoLista = this.AsuntoLista,
                    NombreAsesor = this.NombreAsesor,
                    Alias = this.Alias,
                    Empresa = "BS Grupo",
                    Direccion = "Urb Leon XIII Calle 2 Nº107 Cayma",
                    Ciudad = "Arequipa",
                    EstadoPais = "PE",
                    Zip = "04000",
                    Pais = "Peru",
                    Lenguaje = "es-pe",
                    Permiso = "Información de Capacitación"
                };
                var lista = _mailchimpServiceImpl.CrearEstructuraLista(listaM);
                //var listaResult = new List { };
                //Task.Run(async () => { listaResult = await _mailchimpServiceImpl.CrearLista(lista); }).Wait();
                var listaResult = _mailchimpServiceImpl.CrearLista(lista).Result;

                return listaResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public async Task<List> GetListAsync(string listId)
        {
            try
            {
                return await _mailchimpServiceImpl.GetListAsync(listId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> CrearEtiquetasMailchimp(string lista)
        {
            try
            {
                List<string> ListaEtiquetas = new List<string>();
                ListaEtiquetas = new List<string>(this.Etiquetas.Split(','));

                ListaEtiquetas = ListaEtiquetas.Distinct().ToList();

                foreach (var Etiqueta in ListaEtiquetas)
                {
                    if (Etiqueta != "")
                    {
                        var mergeField = new MergeField
                        {
                            Name = Etiqueta,
                            Tag = Etiqueta,
                            Type = "text",
                            DefaultValue = "",
                            ListId = lista,
                            Public = true
                        };
                        var mergeFieldResult = await _mailchimpServiceImpl.CrearEtiqueta(lista, mergeField);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preciosContado"></param>
        /// <param name="precioCredito"></param>
        /// <returns></returns>
        public List<Member> CrearMiembrosMailchimp(List<CampaniasMailingPrecioDTO> preciosContado, List<CampaniasMailingPrecioDTO> precioCredito)
        {
            try
            {
                List<string> ListaEtiquetas = new List<string>();
                ListaEtiquetas = new List<string>(this.Etiquetas.Split(','));
                var ChimpMembers = new List<Member>();
                foreach (var miembro in PrioridadMailchimpListaCorreo)
                {
                    var member = new Member
                    {
                        EmailAddress = miembro.Email1,
                        StatusIfNew = Status.Subscribed,
                        Status = Status.Subscribed
                    };
                    member.MergeFields.Add("FNAME", miembro.Nombre1);
                    member.MergeFields.Add("LNAME", miembro.ApellidoPaterno);
                    member.MergeFields.Add("IDPMLC", miembro.Id);
                    foreach (var Etiqueta in ListaEtiquetas)
                    {
                        if (Etiqueta != "")
                        {
                            var valor = miembro.ObtenerEtiqueta(Etiquetas, preciosContado, precioCredito);
                            if (valor != null && valor != " " && valor != "-")
                            {
                                member.MergeFields.Add(Etiqueta, valor);
                            }
                        }
                    }
                    ChimpMembers.Add(member);
                    //if (i > 30000) break;
                    //i++;
                }
                return ChimpMembers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="preciosContado"></param>
        /// <param name="precioCredito"></param>
        /// <returns></returns>
        public List<Member> CrearMiembrosMailchimpGeneral(List<CampaniasGeneralPrecioDTO> preciosContado, List<CampaniasGeneralPrecioDTO> precioCredito)
        {
            try
            {
                List<string> ListaEtiquetas = new List<string>();
                ListaEtiquetas = new List<string>(this.Etiquetas.Split(','));
                var ChimpMembers = new List<Member>();
                foreach (var miembro in PrioridadMailchimpListaCorreo)
                {
                    var member = new Member
                    {
                        EmailAddress = miembro.Email1,
                        StatusIfNew = Status.Subscribed,
                        Status = Status.Subscribed
                    };
                    member.MergeFields.Add("FNAME", miembro.Nombre1);
                    member.MergeFields.Add("LNAME", miembro.ApellidoPaterno);
                    member.MergeFields.Add("IDPMLC", miembro.Id);
                    foreach (var Etiqueta in ListaEtiquetas)
                    {
                        if (Etiqueta != "")
                        {
                            var valor = miembro.ObtenerEtiquetaGeneral(Etiquetas, preciosContado, precioCredito);
                            if (valor != null && valor != " " && valor != "-")
                            {
                                member.MergeFields.Add(Etiqueta, valor);
                            }
                        }
                    }
                    ChimpMembers.Add(member);
                }
                return ChimpMembers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> SubirListaConMiembros(List<Member> ChimpMembers, List lista)
        {
            try
            {
                try
                {
                    return _mailchimpServiceImpl.SubirLista(ChimpMembers, lista).Result;
                }
                catch (MailChimpException e)
                {
                    throw e;
                }
            }
            catch (Exception e)
            {
                return false;
                //throw e;
            }
        }

        /// <summary>
        /// Archiva los miembros de la lista indicada
        /// </summary>
        /// <param name="ChimpMembers">Lista de objetos de clase Member</param>
        /// <param name="lista">Objeto de clase lista</param>
        /// <returns>Task (bool)</returns>
        public async Task<bool> ArchivarListaConMiembros(List<Member> ChimpMembers, List lista)
        {
            try
            {
                try
                {
                    bool resultado = false;

                    var subListasBloque =
                                ChimpMembers.Select((x, i) => new { Index = i, Value = x })
                                .GroupBy(x => x.Index / 1000)
                                .Select(x => x.Select(v => v.Value).ToList())
                                .ToList();

                    foreach (var listaMiembro in subListasBloque)
                    {
                        resultado = _mailchimpServiceImpl.ArchivarLista(ChimpMembers, lista).Result;
                    }

                    return resultado;
                }
                catch (MailChimpException e)
                {
                    throw e;
                }
            }
            catch (Exception e)
            {
                return false;
                //throw e;
            }
        }

        /// <summary>
        /// Crea la campania con los datos indicados
        /// </summary>
        /// <param name="campania">Objeto de clase CampaniaMailchimpDatosDTO</param>
        /// <param name="contenido">cadena con el contenido</param>
        /// <returns>Task (campaign)</returns>
        public async Task<Campaign> CrearCampania(CampaniaMailchimpDatosDTO campania, string contenido)
        {
            try
            {
                var campaign = _mailchimpServiceImpl.CrearEstructuraCampania(campania);
                var campaniaInsertada = new Campaign { };
                //Task.Run(async () => { campaniaInsertada = await _mailchimpServiceImpl.CrearCampania(campaign); }).Wait();
                campaniaInsertada = _mailchimpServiceImpl.CrearCampania(campaign).Result;
                var content = new ContentRequest { PlainText = "", Html = contenido };
                var actualizado = _mailchimpServiceImpl.InsertaContenidoACampania(campaniaInsertada, content).Result;

                return campaniaInsertada;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Configura el horario indicado
        /// </summary>
        /// <param name="idCampania">Id de la campania indicada</param>
        /// <param name="contenidoConfiguracion">Objeto de clase CampaignScheduleRequest</param>
        /// <returns>Task</returns>
        public async Task ConfigurarHorario(string idCampania, CampaignScheduleRequest contenidoConfiguracion = null)
        {
            try
            {
                _mailchimpServiceImpl.ConfigurarHorario(idCampania, contenidoConfiguracion).Wait();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Pausa el horario indicado
        /// </summary>
        /// <param name="idCampania">Id de la campania indicada</param>
        /// <param name="contenidoConfiguracion">Objeto de clase CampaignScheduleRequest</param>
        /// <returns>Task</returns>
        public async Task PausarHorario(string idCampania, CampaignScheduleRequest contenidoConfiguracion = null)
        {
            try
            {
                _mailchimpServiceImpl.PausarHorario(idCampania, contenidoConfiguracion).Wait();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<TPrioridadMailChimpLista> PrioridadesMailChimpListaByIdcampaniasmailingDetalle(int IdCampaniaMailingDetalle)
        {
            List<TPrioridadMailChimpLista> lista = new List<TPrioridadMailChimpLista>();
            try
            {
                lista = _integraDContext.TPrioridadMailChimpLista.Where(x => x.IdCampaniaMailingDetalle == IdCampaniaMailingDetalle && x.Enviado != true && x.Estado == true).ToList();
                lista.OrderByDescending(x => x.FechaCreacion);
            }
            catch (Exception e)
            {
            }
            return lista;
        }

        public List<TPrioridadMailChimpLista> PrioridadesMailChimpListaByFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            //List<tpla_PrioridadesMailChimpLista_Temp> lista = new List<tpla_PrioridadesMailChimpLista_Temp>();
            List<TPrioridadMailChimpLista> lista = new List<TPrioridadMailChimpLista>(); ;
            try
            {
                lista = _integraDContext.TPrioridadMailChimpLista.Where(x => x.Enviado == true && x.IdCampaniaMailchimp != null && x.IdListaMailchimp != null
                                                                && x.FechaEnvio != null
                                                                && x.FechaEnvio > fechaInicio
                                                                && x.FechaEnvio <= fechaFin
                                                                ).ToList();
            }
            catch (Exception e)
            {
            }
            return lista;
        }

        public List<TPrioridadMailChimpLista> PrioridadesMailChimpListaByIdcampaniasIntegra(int IdCampaniaMailing)
        {
            List<TPrioridadMailChimpLista> lista = new List<TPrioridadMailChimpLista>();
            try
            {
                lista = _integraDContext.TPrioridadMailChimpLista.Where(x => x.IdCampaniaMailing == IdCampaniaMailing &&
                    x.Enviado != true && x.Estado == true && x.IdCampaniaMailchimp == null && x.IdListaMailchimp == null).ToList();
            }
            catch (Exception e)
            {

            }
            return lista;
        }

        public List<TPrioridadMailChimpLista> PrioridadesMailChimpLista()
        {
            List<TPrioridadMailChimpLista> lista = new List<TPrioridadMailChimpLista>();
            DateTime fecha5dias_antes = DateTime.Now.Date.AddDays(-5);

            try
            {
                lista = _integraDContext.TPrioridadMailChimpLista.Where(x => x.Enviado == true && x.IdCampaniaMailchimp != null && x.IdListaMailchimp != null
                                                                && x.FechaEnvio != null
                                                                && x.FechaEnvio > fecha5dias_antes
                                                                ).ToList();
            }
            catch (Exception e)
            {

            }
            return lista;
        }

        /// <summary>
        /// Descarga toda la informacion de campanias de Mailchimp segun un intervalo de fechas y retorna los Ids que tuvieron error
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio para la descarga</param>
        /// <param name="fechaFin">Fecha de fin para la descarga</param>
        /// <param name="usuario">Usuario que realiza la descarga</param>
        /// <returns>Lista de string</returns>
        public List<string> DescargarCampaniaMailchimpPorIntervaloFecha(DateTime fechaInicio, DateTime fechaFin, string usuario)
        {
            try
            {
                var listaErronea = new List<string>();

                fechaInicio = ConvertirAUtc(fechaInicio.Date).Date;
                fechaFin = ConvertirAUtc(fechaFin.Date.AddDays(1)).Date;

                var resultadoDatosGenerales = MailChimp.DescargarCampaniaMailChimpPorIntervaloFecha(fechaInicio.ToString("o"), fechaFin.ToString("o"));

                if (resultadoDatosGenerales.total_items > 0)
                {
                    listaErronea = ActualizarInformacionCampaniaMailchimp(resultadoDatosGenerales, usuario);
                }

                return listaErronea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descarga toda la informacion de listas de Mailchimp segun un intervalo de fechas y retorna los Ids que tuvieron error
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio para la descarga</param>
        /// <param name="fechaFin">Fecha de fin para la descarga</param>
        /// <param name="usuario">Usuario que realiza la descarga</param>
        /// <returns>Lista de string</returns>
        public List<string> DescargarListaMailchimpPorIntervaloFecha(DateTime fechaInicio, DateTime fechaFin, bool incluirMiembros, string usuario)
        {
            try
            {
                var listaErronea = new List<string>();
                var listaErroneaMiembro = new List<string>();

                fechaInicio = ConvertirAUtc(fechaInicio.Date).Date;
                fechaFin = ConvertirAUtc(fechaFin.Date.AddDays(1)).Date;

                var resultadoDatosGenerales = MailChimp.DescargarListaMailChimpPorIntervaloFecha(fechaInicio.ToString("o"), fechaFin.ToString("o"));

                if (resultadoDatosGenerales.total_items > 0)
                {
                    listaErronea = ActualizarInformacionListaMailchimp(resultadoDatosGenerales, usuario);

                    if (incluirMiembros)
                    {
                        foreach (var listaMailchimpId in resultadoDatosGenerales.lists.Select(s => s.id))
                        {
                            try
                            {
                                // Miembro
                                listaErroneaMiembro.AddRange(DescargarMiembroMailchimpPorListaMailchimpId(listaMailchimpId, usuario));
                            }
                            catch (Exception ex)
                            {
                                listaErronea.Add(string.Concat(listaMailchimpId, " - Insercion Miembros Fallida"));
                            }
                        }
                    }
                }

                listaErronea.AddRange(listaErroneaMiembro);

                return listaErronea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la lista de Id de Mailchimp para miembros
        /// </summary>
        /// <returns>Lista de string</returns>
        public List<string> ObtenerListaMiembroPorProcedimiento()
        {
            try
            {
                var resultadoMiembro = _repPrioridadMailChimpLista.ObtenerListaMailchimpIdParaMiembros();

                return resultadoMiembro.Select(s => s.Valor).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descarga toda la informacion de miembros de Mailchimp segun un Id de Lista de Mailchimp y retorna los Ids que tuvieron error
        /// </summary>
        /// <param name="listaMailchimpId">Id de una lista de Mailchimp</param>
        /// <param name="usuario">Usuario que realiza la descarga</param>
        /// <returns>Lista de string</returns>
        public List<string> DescargarMiembroMailchimpPorListaMailchimpId(string listaMailchimpId, string usuario)
        {
            try
            {
                var listaErronea = new List<string>();

                var resultadoDatosGenerales = MailChimp.DescargarMiembroMailChimpPorListaMailchimpId(listaMailchimpId);

                if (resultadoDatosGenerales.total_items > 0)
                {
                    listaErronea = ActualizarInformacionMiembroMailchimp(resultadoDatosGenerales, listaMailchimpId, usuario);
                }

                return listaErronea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descargar los datos desde la API de mailchimp
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio para la descarga</param>
        /// <param name="fechaFin">Fecha de fin para la descarga</param>
        /// <param name="mapeoCompleto">Flag para habilitar si se realizara un mapeo completo</param>
        /// <returns>Bool</returns>
        public bool DescargarIndicadorMailChimpPorIntervaloFecha(DateTime fechaInicio, DateTime fechaFin, bool mapeoCompleto)
        {
            try
            {
                fechaInicio = ConvertirAUtc(fechaInicio.Date).Date;
                fechaFin = ConvertirAUtc(fechaFin.Date.AddDays(1)).Date;

                var resultadoDatosGenerales = MailChimp.DescargarReporteMailChimpPorIntervaloFecha(fechaInicio.ToString("o"), fechaFin.ToString("o"));

                // Consultar General
                if (!mapeoCompleto)
                    resultadoDatosGenerales.reports = resultadoDatosGenerales.reports.Where(x => !string.IsNullOrEmpty(x.opens.last_open) && DateTime.Parse(x.opens.last_open) >= fechaFin.AddDays(-7)).ToList();

                resultadoDatosGenerales.reports = resultadoDatosGenerales.reports.Where(x => x.id == "d2e1543376").ToList();

                var listaActualizada = ActualizarMetricaDatosGeneralesMailChimp(resultadoDatosGenerales);
                var listaExistenteGeneral = _repMailchimpEstadisticaPorPrioridad.GetBy(x => listaActualizada.Select(s => s.Id).Contains(x.IdPrioridadMailChimpLista.Value)).ToList();

                // Consultar Detalle
                foreach (var campaniaActual in listaActualizada.Select(s => s.CampaniaMailChimpId).Distinct().ToList())
                {
                    var listaExistente = listaExistenteGeneral.Where(x => x.IdPrioridadMailChimpLista == listaActualizada.FirstOrDefault(w => w.CampaniaMailChimpId == campaniaActual).Id).ToList();
                    var prioridadAEvaluar = listaActualizada.FirstOrDefault(x => x.Id == listaActualizada.FirstOrDefault(w => w.CampaniaMailChimpId == campaniaActual).Id);

                    if (prioridadAEvaluar != null)
                    {
                        if (prioridadAEvaluar.CantidadAperturaUnica != listaExistente.Select(s => s.CantidadCorreoAbierto).Sum())
                        {
                            var resultadoDescargaReporteDiario = MailChimp.DescargarReporteDiarioMailChimpPorIntervaloFecha(campaniaActual, fechaInicio);

                            var resultadoActualizacion = ActualizarMetricaDatosDiariosMailChimp(listaActualizada.FirstOrDefault(x => x.CampaniaMailChimpId == campaniaActual), resultadoDescargaReporteDiario);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Verifica y convierte si fuera el caso una fecha a Utc
        /// </summary>
        /// <param name="fecha">Fecha a convertir</param>
        /// <returns>DateTime</returns>
        public DateTime ConvertirAUtc(DateTime fecha)
        {
            try
            {
                return fecha.Kind != DateTimeKind.Utc ? fecha.ToUniversalTime() : fecha;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza la informacion de las campanias
        /// </summary>
        /// <param name="listaCampaniaEncontradaMailChimp">Objeto de clase ListaReporteCampaniaMailchimpResultadoCrudoDTO</param>
        /// <returns>Lista de objetos de clase PrioridadMailChimpCampaniaListaDTO</returns>
        private List<string> ActualizarInformacionCampaniaMailchimp(InformacionCampaniaMailchimpFormatoDTO listaCampaniaEncontradaMailChimp, string usuario)
        {
            try
            {
                // Almacenar erroneos
                var listaIdMailchimpErroneo = new List<string>();

                var formatoDecimal = new CultureInfo("en-US");

                var subListaCampaniaEncontradaMailChimpApi = listaCampaniaEncontradaMailChimp.campaigns
                                                        .Select((x, i) => new { Index = i, Value = x })
                                                        .GroupBy(x => x.Index / 250)
                                                        .Select(x => x.Select(v => v.Value).ToList())
                                                        .ToList();

                foreach (var subLista in subListaCampaniaEncontradaMailChimpApi)
                {
                    // Listas ya almacenadas
                    var listaCampaniaMailchimpIntegra = _repCampaniaMailchimp.GetBy(x => subLista.Select(s => s.id).Contains(x.MailchimpId)).Distinct().ToList();

                    // Se verifican las listas que ya pasaron los 180 dias desde su envio a su fecha de modificacion ya que Mailchimp no almacena mas
                    var listaCampaniaMailchimpIntegraActualizable = listaCampaniaMailchimpIntegra.Where(x => !x.FechaEnvio.HasValue || (x.FechaEnvio.HasValue && x.FechaEnvio.Value.AddDays(180) > FechaModificacion)).ToList();

                    var listaCampaniaAActualizar = new List<CampaniaMailchimpBO>();

                    #region Actualizar Campanias
                    foreach (var campaniaActualizable in listaCampaniaMailchimpIntegraActualizable)
                    {
                        try
                        {
                            var campaniaApi = subLista.FirstOrDefault(x => x.id == campaniaActualizable.MailchimpId);

                            campaniaActualizable.MailchimpTipo = campaniaApi.type;
                            campaniaActualizable.MailchimpEstado = campaniaApi.status;
                            campaniaActualizable.CantidadEnvio = campaniaApi.emails_sent;
                            campaniaActualizable.MailchimpListaId = campaniaApi.recipients != null && campaniaApi.recipients.list_id != null ? campaniaApi.recipients.list_id : string.Empty;
                            campaniaActualizable.MailchimpListaNombre = campaniaApi.recipients != null && campaniaApi.recipients.list_name != null ? campaniaApi.recipients.list_name : string.Empty;
                            campaniaActualizable.CantidadRecipiente = campaniaApi.recipients != null ? campaniaApi.recipients.recipient_count : 0;
                            campaniaActualizable.MailchimpAsunto = campaniaApi.settings != null && campaniaApi.settings.subject_line != null ? campaniaApi.settings.subject_line : string.Empty;
                            campaniaActualizable.MailchimpTitulo = campaniaApi.settings != null && campaniaApi.settings.title != null ? campaniaApi.settings.title : string.Empty;
                            campaniaActualizable.NombreRemitente = campaniaApi.settings != null && campaniaApi.settings.from_name != null ? campaniaApi.settings.from_name : string.Empty;
                            campaniaActualizable.CorreoRemitente = campaniaApi.settings != null && campaniaApi.settings.reply_to != null ? campaniaApi.settings.reply_to : string.Empty;

                            campaniaActualizable.CantidadApertura = campaniaApi.report_summary != null && campaniaApi.report_summary.opens != null ? campaniaApi.report_summary.opens : 0;
                            campaniaActualizable.CantidadAperturaUnica = campaniaApi.report_summary != null ? campaniaApi.report_summary.unique_opens : 0;
                            if (campaniaApi.report_summary != null && !string.IsNullOrEmpty(campaniaApi.report_summary.open_rate)) campaniaActualizable.TasaApertura = decimal.Parse(campaniaApi.report_summary.open_rate, formatoDecimal);
                            campaniaActualizable.CantidadClic = campaniaApi.report_summary != null ? campaniaApi.report_summary.clicks : 0;
                            campaniaActualizable.CantidadClicSuscriptor = campaniaApi.report_summary != null ? campaniaApi.report_summary.subscriber_clicks : 0;
                            if (!string.IsNullOrEmpty(campaniaApi.send_time)) campaniaActualizable.FechaEnvio = DateTime.Parse(campaniaApi.send_time);
                            if (campaniaApi.report_summary != null && !string.IsNullOrEmpty(campaniaApi.report_summary.click_rate)) campaniaActualizable.TasaClic = decimal.Parse(campaniaApi.report_summary.click_rate, formatoDecimal);

                            campaniaActualizable.Estado = true;
                            campaniaActualizable.UsuarioModificacion = usuario;
                            campaniaActualizable.FechaModificacion = DateTime.Now;

                            listaCampaniaAActualizar.Add(campaniaActualizable);
                        }
                        catch (Exception)
                        {
                            listaIdMailchimpErroneo.Add(string.Concat(campaniaActualizable.MailchimpId, " - Actualizacion Fallida"));
                        }
                    }

                    bool resultadoActualizacion = _repCampaniaMailchimp.Update(listaCampaniaAActualizar);
                    #endregion

                    // Listas no almacenadas
                    var listaNuevaCampaniaMailchimp = subLista.Where(x => !listaCampaniaMailchimpIntegra.Select(s => s.MailchimpId).Contains(x.id)).ToList();

                    var listaCampaniaAInsertar = new List<CampaniaMailchimpBO>();

                    #region Insertar Campanias
                    foreach (var campaniaMailchimpIntegra in listaNuevaCampaniaMailchimp)
                    {
                        try
                        {
                            listaCampaniaAInsertar.Add(MapeoCampaniaMailchimpDTOBO(campaniaMailchimpIntegra, usuario));
                        }
                        catch (Exception)
                        {
                            listaIdMailchimpErroneo.Add(string.Concat(campaniaMailchimpIntegra.id, " - Insercion Fallida"));
                        }
                    }

                    bool resultadoInsercion = _repCampaniaMailchimp.Insert(listaCampaniaAInsertar);
                    #endregion
                }

                return listaIdMailchimpErroneo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza la informacion de las campanias
        /// </summary>
        /// <param name="listaCampaniaEncontradaMailChimp">Objeto de clase ListaReporteCampaniaMailchimpResultadoCrudoDTO</param>
        /// <returns>Lista de objetos de clase PrioridadMailChimpCampaniaListaDTO</returns>
        private List<string> ActualizarInformacionListaMailchimp(InformacionListaMailchimpFormatoDTO listaListaEncontradaMailChimp, string usuario)
        {
            try
            {
                // Almacenar erroneos
                var listaIdMailchimpErroneo = new List<string>();

                var formatoDecimal = new CultureInfo("en-US");

                var subListaListaEncontradaMailChimpApi = listaListaEncontradaMailChimp.lists
                                                        .Select((x, i) => new { Index = i, Value = x })
                                                        .GroupBy(x => x.Index / 250)
                                                        .Select(x => x.Select(v => v.Value).ToList())
                                                        .ToList();

                foreach (var subLista in subListaListaEncontradaMailChimpApi)
                {
                    // Listas ya almacenadas
                    var listaListaMailchimpIntegra = _repListaMailchimp.GetBy(x => subLista.Select(s => s.id).Contains(x.MailchimpId)).Distinct().ToList();

                    // Se verifican las listas que ya pasaron los 180 dias desde su envio a su fecha de modificacion ya que Mailchimp no almacena mas
                    var listaListaMailchimpIntegraActualizable = listaListaMailchimpIntegra.Where(x => x.MailchimpFechaCreacion.AddDays(180) > FechaModificacion).ToList();

                    var listaListaAActualizar = new List<ListaMailchimpBO>();

                    #region Actualizar Listas
                    foreach (var listaActualizable in listaListaMailchimpIntegraActualizable)
                    {
                        try
                        {
                            var listaApi = subLista.FirstOrDefault(x => x.id == listaActualizable.MailchimpId);

                            listaActualizable.MailchimpNombre = listaApi.name;
                            listaActualizable.NombreCompania = listaApi.contact.company;
                            listaActualizable.Direccion1 = listaApi.contact.address1;
                            listaActualizable.Direccion2 = listaApi.contact.address2;
                            listaActualizable.DireccionCiudad = listaApi.contact.city;
                            listaActualizable.DireccionEstado = listaApi.contact.state;
                            listaActualizable.CodigoZip = listaApi.contact.zip;
                            listaActualizable.Pais = listaApi.contact.country;
                            listaActualizable.Telefono = listaApi.contact.phone;
                            listaActualizable.NombreRemitente = listaApi.campaign_defaults != null && listaApi.campaign_defaults.from_name != null ? listaApi.campaign_defaults.from_name : string.Empty;
                            listaActualizable.CorreoRemitente = listaApi.campaign_defaults != null && listaApi.campaign_defaults.from_email != null ? listaApi.campaign_defaults.from_email : string.Empty;
                            listaActualizable.MailchimpAsunto = listaApi.campaign_defaults != null && listaApi.campaign_defaults.subject != null ? listaApi.campaign_defaults.subject : string.Empty;
                            listaActualizable.MailchimpLenguaje = listaApi.campaign_defaults != null && listaApi.campaign_defaults.language != null ? listaApi.campaign_defaults.language : string.Empty;
                            if (!string.IsNullOrEmpty(listaApi.date_created)) listaActualizable.MailchimpFechaCreacion = DateTime.Parse(listaApi.date_created);
                            listaActualizable.RatingLista = listaApi.list_rating;

                            listaActualizable.CantidadMiembro = listaApi.stats != null ? listaApi.stats.member_count : 0;
                            listaActualizable.CantidadDesuscrito = listaApi.stats != null ? listaApi.stats.unsubscribe_count : 0;
                            listaActualizable.CantidadLimpiado = listaApi.stats != null ? listaApi.stats.cleaned_count : 0;
                            listaActualizable.CantidadDesuscritoDesdeEnvio = listaApi.stats != null ? listaApi.stats.unsubscribe_count_since_send : 0;
                            listaActualizable.CantidadLimpiadoDesdeEnvio = listaApi.stats != null ? listaApi.stats.cleaned_count_since_send : 0;
                            listaActualizable.CantidadCampania = listaApi.stats != null ? listaApi.stats.campaign_count : 0;
                            if (listaApi.stats != null && !string.IsNullOrEmpty(listaApi.stats.campaign_last_sent)) listaActualizable.FechaUltimoEnvioCampania = DateTime.Parse(listaApi.stats.campaign_last_sent);
                            if (listaApi.stats != null && !string.IsNullOrEmpty(listaApi.stats.last_sub_date)) listaActualizable.FechaUltimoSuscrito = DateTime.Parse(listaApi.stats.last_sub_date);
                            if (listaApi.stats != null && !string.IsNullOrEmpty(listaApi.stats.last_unsub_date)) listaActualizable.FechaUltimoDesuscrito = DateTime.Parse(listaApi.stats.last_unsub_date);

                            if (listaApi.stats != null && !string.IsNullOrEmpty(listaApi.stats.avg_sub_rate)) listaActualizable.PromedioSuscrito = decimal.Parse(listaApi.stats.avg_sub_rate, formatoDecimal);
                            if (listaApi.stats != null && !string.IsNullOrEmpty(listaApi.stats.avg_unsub_rate)) listaActualizable.PromedioDesuscrito = decimal.Parse(listaApi.stats.avg_unsub_rate, formatoDecimal);
                            if (listaApi.stats != null && !string.IsNullOrEmpty(listaApi.stats.target_sub_rate)) listaActualizable.PromedioObjetivoSuscrito = decimal.Parse(listaApi.stats.target_sub_rate, formatoDecimal);
                            if (listaApi.stats != null && !string.IsNullOrEmpty(listaApi.stats.open_rate)) listaActualizable.PromedioApertura = decimal.Parse(listaApi.stats.open_rate, formatoDecimal);
                            if (listaApi.stats != null && !string.IsNullOrEmpty(listaApi.stats.click_rate)) listaActualizable.PromedioClic = decimal.Parse(listaApi.stats.click_rate, formatoDecimal);

                            listaActualizable.Estado = true;
                            listaActualizable.UsuarioModificacion = usuario;
                            listaActualizable.FechaModificacion = DateTime.Now;

                            listaListaAActualizar.Add(listaActualizable);
                        }
                        catch (Exception)
                        {
                            listaIdMailchimpErroneo.Add(string.Concat(listaActualizable.MailchimpId, " - Actualizacion Fallida"));
                        }
                    }

                    bool resultadoActualizacion = _repListaMailchimp.Update(listaListaAActualizar);
                    #endregion

                    // Listas no almacenadas
                    var listaNuevaListaMailchimp = subLista.Where(x => !listaListaMailchimpIntegra.Select(s => s.MailchimpId).Contains(x.id)).ToList();

                    var listaListaAInsertar = new List<ListaMailchimpBO>();

                    #region Insertar Lista
                    foreach (var listaMailchimpMailchimp in listaNuevaListaMailchimp)
                    {
                        try
                        {
                            listaListaAInsertar.Add(MapeoListaMailchimpDTOBO(listaMailchimpMailchimp, usuario));
                        }
                        catch (Exception)
                        {
                            listaIdMailchimpErroneo.Add(string.Concat(listaMailchimpMailchimp.id, " - Insercion Fallida"));
                        }
                    }

                    bool resultadoInsercion = _repListaMailchimp.Insert(listaListaAInsertar);
                    #endregion
                }

                return listaIdMailchimpErroneo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza la informacion de las campanias
        /// </summary>
        /// <param name="listaCampaniaEncontradaMailChimp">Objeto de clase ListaReporteCampaniaMailchimpResultadoCrudoDTO</param>
        /// <returns>Lista de objetos de clase PrioridadMailChimpCampaniaListaDTO</returns>
        private List<string> ActualizarInformacionMiembroMailchimp(InformacionMiembroMailchimpFormatoDTO listaMiembroEncontradaMailChimp, string mailchimpListaId, string usuario)
        {
            try
            {
                var listaMiembroApi = listaMiembroEncontradaMailChimp.members;
                var listaMailchimpIntegra = _repListaMailchimp.FirstBy(x => x.MailchimpId == mailchimpListaId);
                var listaMiembroIntegra = _repMiembroMailchimp.GetBy(x => x.IdListaMailchimp == listaMailchimpIntegra.Id);
                var listaMiembroActualizable = listaMiembroIntegra.Where(x => !x.FechaOptIn.HasValue || (x.FechaOptIn.HasValue && x.FechaOptIn.Value.AddDays(180) > FechaModificacion)).ToList();

                // Almacenar erroneos
                var listaIdMailchimpErroneo = new List<string>();

                var formatoDecimal = new CultureInfo("en-US");

                var listaMiembroAActualizar = new List<MiembroMailchimpBO>();

                // Miembros almacenados
                #region Actualizar Miembros
                foreach (var miembroActualizable in listaMiembroActualizable)
                {
                    try
                    {
                        var miembroApi = listaMiembroApi.FirstOrDefault(x => x.id == miembroActualizable.MailchimpId);

                        miembroActualizable.IdListaMailchimp = listaMailchimpIntegra.Id;
                        miembroActualizable.NombreCompleto = miembroApi.full_name;
                        miembroActualizable.MailchimpEstado = miembroApi.status;
                        miembroActualizable.RatingMiembro = miembroApi.member_rating;

                        if (miembroApi.stats != null && !string.IsNullOrEmpty(miembroApi.stats.avg_open_rate)) miembroActualizable.PromedioApertura = decimal.Parse(miembroApi.stats.avg_open_rate, formatoDecimal);
                        if (miembroApi.stats != null && !string.IsNullOrEmpty(miembroApi.stats.avg_click_rate)) miembroActualizable.PromedioClic = decimal.Parse(miembroApi.stats.avg_click_rate, formatoDecimal);

                        miembroActualizable.Estado = true;
                        miembroActualizable.UsuarioModificacion = usuario;
                        miembroActualizable.FechaModificacion = DateTime.Now;

                        listaMiembroAActualizar.Add(miembroActualizable);
                    }
                    catch (Exception)
                    {
                        listaIdMailchimpErroneo.Add(string.Concat(miembroActualizable.MailchimpId, " - Actualizacion Fallida"));
                    }
                }

                bool resultadoActualizacion = _repMiembroMailchimp.Update(listaMiembroAActualizar);
                #endregion

                // Miembros no almacenados
                var listaNuevoMiembroMailchimp = listaMiembroApi.Where(x => !listaMiembroIntegra.Select(s => s.MailchimpId).Contains(x.id)).ToList();
                var listaMiembroAInsertar = new List<MiembroMailchimpBO>();

                #region Insertar Miembros
                foreach (var nuevoMiembroMailchimp in listaNuevoMiembroMailchimp)
                {
                    try
                    {
                        listaMiembroAInsertar.Add(MapeoMiembroMailchimpDTOBO(nuevoMiembroMailchimp, listaMailchimpIntegra.Id, usuario));
                    }
                    catch (Exception)
                    {
                        listaIdMailchimpErroneo.Add(string.Concat(nuevoMiembroMailchimp.id, " - Insercion Fallida"));
                    }
                }

                bool resultadoInsercion = _repMiembroMailchimp.Insert(listaMiembroAInsertar);
                #endregion

                return listaIdMailchimpErroneo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CampaniaMailchimpBO MapeoCampaniaMailchimpDTOBO(InformacionCampaniaMailchimpCrudoDTO campaniaMailchimp, string usuario)
        {
            try
            {
                var formatoDecimal = new CultureInfo("en-US");

                CampaniaMailchimpBO campaniaMailchimpBO = new CampaniaMailchimpBO()
                {
                    MailchimpId = campaniaMailchimp.id,
                    MailchimpWebId = campaniaMailchimp.web_id,
                    MailchimpTipo = campaniaMailchimp.type,
                    MailchimpEstado = campaniaMailchimp.status,
                    CantidadEnvio = campaniaMailchimp.emails_sent,
                    TipoContenido = campaniaMailchimp.content_type,
                    MailchimpListaId = campaniaMailchimp.recipients != null && campaniaMailchimp.recipients.list_id != null ? campaniaMailchimp.recipients.list_id : string.Empty,
                    MailchimpListaNombre = campaniaMailchimp.recipients != null && campaniaMailchimp.recipients.list_name != null ? campaniaMailchimp.recipients.list_name : string.Empty,
                    CantidadRecipiente = campaniaMailchimp.recipients != null ? campaniaMailchimp.recipients.recipient_count : 0,
                    MailchimpAsunto = campaniaMailchimp.settings.subject_line != null ? campaniaMailchimp.settings.subject_line : string.Empty,
                    MailchimpTitulo = campaniaMailchimp.settings.title != null ? campaniaMailchimp.settings.title : string.Empty,
                    NombreRemitente = campaniaMailchimp.settings.from_name != null ? campaniaMailchimp.settings.from_name : string.Empty,
                    CorreoRemitente = campaniaMailchimp.settings.reply_to != null ? campaniaMailchimp.settings.reply_to : string.Empty,
                    CantidadApertura = campaniaMailchimp.report_summary != null ? campaniaMailchimp.report_summary.opens : 0,
                    CantidadAperturaUnica = campaniaMailchimp.report_summary != null ? campaniaMailchimp.report_summary.unique_opens : 0,
                    CantidadClic = campaniaMailchimp.report_summary != null ? campaniaMailchimp.report_summary.clicks : 0,
                    CantidadClicSuscriptor = campaniaMailchimp.report_summary != null ? campaniaMailchimp.report_summary.subscriber_clicks : 0,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                if (!string.IsNullOrEmpty(campaniaMailchimp.create_time)) campaniaMailchimpBO.MailchimpFechaCreacion = DateTime.Parse(campaniaMailchimp.create_time);
                if (!string.IsNullOrEmpty(campaniaMailchimp.send_time)) campaniaMailchimpBO.FechaEnvio = DateTime.Parse(campaniaMailchimp.send_time);
                if (campaniaMailchimp.report_summary != null && !string.IsNullOrEmpty(campaniaMailchimp.report_summary.open_rate)) campaniaMailchimpBO.TasaApertura = decimal.Parse(campaniaMailchimp.report_summary.open_rate, formatoDecimal);
                if (campaniaMailchimp.report_summary != null && !string.IsNullOrEmpty(campaniaMailchimp.report_summary.click_rate)) campaniaMailchimpBO.TasaClic = decimal.Parse(campaniaMailchimp.report_summary.click_rate, formatoDecimal);

                return campaniaMailchimpBO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ListaMailchimpBO MapeoListaMailchimpDTOBO(InformacionListaMailchimpCrudoDTO listaMailchimp, string usuario)
        {
            try
            {
                var formatoDecimal = new CultureInfo("en-US");

                ListaMailchimpBO listaMailchimpBO = new ListaMailchimpBO()
                {
                    MailchimpId = listaMailchimp.id,
                    MailchimpWebId = listaMailchimp.web_id,
                    MailchimpNombre = listaMailchimp.name,
                    NombreCompania = listaMailchimp.contact.company,
                    Direccion1 = listaMailchimp.contact.address1,
                    Direccion2 = listaMailchimp.contact.address2,
                    DireccionCiudad = listaMailchimp.contact.city,
                    DireccionEstado = listaMailchimp.contact.state,
                    CodigoZip = listaMailchimp.contact.zip,
                    Pais = listaMailchimp.contact.country,
                    Telefono = listaMailchimp.contact.phone,
                    NombreRemitente = listaMailchimp.campaign_defaults.from_name,
                    CorreoRemitente = listaMailchimp.campaign_defaults.from_email,
                    MailchimpAsunto = listaMailchimp.campaign_defaults.subject,
                    MailchimpLenguaje = listaMailchimp.campaign_defaults.language,
                    RatingLista = listaMailchimp.list_rating,
                    CantidadMiembro = listaMailchimp.stats != null ? listaMailchimp.stats.member_count : 0,
                    CantidadDesuscrito = listaMailchimp.stats != null ? listaMailchimp.stats.unsubscribe_count : 0,
                    CantidadLimpiado = listaMailchimp.stats != null ? listaMailchimp.stats.cleaned_count : 0,
                    CantidadDesuscritoDesdeEnvio = listaMailchimp.stats != null ? listaMailchimp.stats.unsubscribe_count_since_send : 0,
                    CantidadLimpiadoDesdeEnvio = listaMailchimp.stats != null ? listaMailchimp.stats.cleaned_count_since_send : 0,
                    CantidadCampania = listaMailchimp.stats != null ? listaMailchimp.stats.campaign_count : 0,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                if (!string.IsNullOrEmpty(listaMailchimp.date_created)) listaMailchimpBO.MailchimpFechaCreacion = DateTime.Parse(listaMailchimp.date_created);
                if (listaMailchimp.stats != null && !string.IsNullOrEmpty(listaMailchimp.stats.campaign_last_sent)) listaMailchimpBO.FechaUltimoEnvioCampania = DateTime.Parse(listaMailchimp.stats.campaign_last_sent);
                if (listaMailchimp.stats != null && !string.IsNullOrEmpty(listaMailchimp.stats.last_sub_date)) listaMailchimpBO.FechaUltimoSuscrito = DateTime.Parse(listaMailchimp.stats.last_sub_date);
                if (listaMailchimp.stats != null && !string.IsNullOrEmpty(listaMailchimp.stats.last_unsub_date)) listaMailchimpBO.FechaUltimoDesuscrito = DateTime.Parse(listaMailchimp.stats.last_unsub_date);
                if (listaMailchimp.stats != null && !string.IsNullOrEmpty(listaMailchimp.stats.avg_sub_rate)) listaMailchimpBO.PromedioSuscrito = decimal.Parse(listaMailchimp.stats.avg_sub_rate, formatoDecimal);
                if (listaMailchimp.stats != null && !string.IsNullOrEmpty(listaMailchimp.stats.avg_unsub_rate)) listaMailchimpBO.PromedioDesuscrito = decimal.Parse(listaMailchimp.stats.avg_unsub_rate, formatoDecimal);
                if (listaMailchimp.stats != null && !string.IsNullOrEmpty(listaMailchimp.stats.target_sub_rate)) listaMailchimpBO.PromedioObjetivoSuscrito = decimal.Parse(listaMailchimp.stats.target_sub_rate, formatoDecimal);
                if (listaMailchimp.stats != null && !string.IsNullOrEmpty(listaMailchimp.stats.open_rate)) listaMailchimpBO.PromedioApertura = decimal.Parse(listaMailchimp.stats.open_rate, formatoDecimal);
                if (listaMailchimp.stats != null && !string.IsNullOrEmpty(listaMailchimp.stats.click_rate)) listaMailchimpBO.PromedioClic = decimal.Parse(listaMailchimp.stats.click_rate, formatoDecimal);

                return listaMailchimpBO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MiembroMailchimpBO MapeoMiembroMailchimpDTOBO(InformacionMiembroMailchimpCrudoDTO miembroMailchimp, int idListaMailchimp, string usuario)
        {
            try
            {
                var formatoDecimal = new CultureInfo("en-US");

                MiembroMailchimpBO miembroMailchimpBO = new MiembroMailchimpBO()
                {
                    IdListaMailchimp = idListaMailchimp,
                    MailchimpId = miembroMailchimp.id,
                    MailchimpCorreo = miembroMailchimp.email_address,
                    MailchimpCorreoId = miembroMailchimp.unique_email_id,
                    MailchimpContactoId = miembroMailchimp.contact_id,
                    NombreCompleto = miembroMailchimp.full_name,
                    MailchimpWebId = miembroMailchimp.web_id,
                    TipoCorreo = miembroMailchimp.email_type,
                    MailchimpEstado = miembroMailchimp.status,
                    RatingMiembro = miembroMailchimp.member_rating,
                    ClienteCorreo = miembroMailchimp.email_client,
                    Fuente = miembroMailchimp.source,
                    MailchimpListaId = miembroMailchimp.list_id,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                if (miembroMailchimp.stats != null && !string.IsNullOrEmpty(miembroMailchimp.stats.avg_open_rate)) miembroMailchimpBO.PromedioApertura = int.Parse(miembroMailchimp.stats.avg_open_rate);
                if (miembroMailchimp.stats != null && !string.IsNullOrEmpty(miembroMailchimp.stats.avg_click_rate)) miembroMailchimpBO.PromedioClic = int.Parse(miembroMailchimp.stats.avg_click_rate);
                if (!string.IsNullOrEmpty(miembroMailchimp.timestamp_opt)) miembroMailchimpBO.FechaOptIn = DateTime.Parse(miembroMailchimp.timestamp_opt);
                if (!string.IsNullOrEmpty(miembroMailchimp.last_changed)) miembroMailchimpBO.FechaUltimoCambio = DateTime.Parse(miembroMailchimp.last_changed);

                return miembroMailchimpBO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza la metrica general de las campanias de MailChimp
        /// </summary>
        /// <param name="listaEncontradaMailChimpApi">Objeto de clase ListaReporteCampaniaMailchimpResultadoCrudoDTO</param>
        /// <returns>Lista de objetos de clase PrioridadMailChimpCampaniaListaDTO</returns>
        private List<PrioridadMailChimpCampaniaListaDTO> ActualizarMetricaDatosGeneralesMailChimp(ListaReporteCampaniaMailchimpResultadoCrudoDTO listaEncontradaMailChimpApi)
        {
            try
            {
                var formatoDecimal = new CultureInfo("en-US");

                List<PrioridadMailChimpListaBO> listaAActualizar = new List<PrioridadMailChimpListaBO>();

                var subListaEncontradaMailChimpApi = listaEncontradaMailChimpApi.reports
                                                    .Select((x, i) => new { Index = i, Value = x })
                                                    .GroupBy(x => x.Index / 250)
                                                    .Select(x => x.Select(v => v.Value).ToList())
                                                    .ToList();

                foreach (var subLista in subListaEncontradaMailChimpApi)
                {
                    var listaCampaniaMailChimpIntegra = _repPrioridadMailChimpLista.GetBy(x => subLista.Select(s => s.id).Contains(x.IdCampaniaMailchimp)).ToList();

                    foreach (var campaniaMailChimpIntegra in listaCampaniaMailChimpIntegra)
                    {
                        var resultadoEncontrado = subLista.FirstOrDefault(x => x.id == campaniaMailChimpIntegra.IdCampaniaMailchimp && x.list_id == campaniaMailChimpIntegra.IdListaMailchimp);

                        if (resultadoEncontrado != null)
                        {
                            try
                            {
                                var campaniaActualizable = campaniaMailChimpIntegra;

                                campaniaActualizable.CantidadEnviadosMailChimp = resultadoEncontrado.emails_sent;
                                campaniaActualizable.CantidadAperturaUnica = resultadoEncontrado.opens.unique_opens;
                                campaniaActualizable.CantidadReboteSuave = resultadoEncontrado.bounces.soft_bounces;
                                campaniaActualizable.CantidadReboteSintaxis = resultadoEncontrado.bounces.syntax_errors;
                                campaniaActualizable.CantidadReboteDuro = resultadoEncontrado.bounces.hard_bounces;
                                campaniaActualizable.TasaApertura = !string.IsNullOrEmpty(resultadoEncontrado.opens.open_rate) ? decimal.Parse(resultadoEncontrado.opens.open_rate.Length > 5 ? resultadoEncontrado.opens.open_rate.Substring(0, 5) : resultadoEncontrado.opens.open_rate, formatoDecimal) : 0;
                                campaniaActualizable.TasaClic = !string.IsNullOrEmpty(resultadoEncontrado.clicks.click_rate) ? decimal.Parse(resultadoEncontrado.clicks.click_rate.Length > 5 ? resultadoEncontrado.clicks.click_rate.Substring(0, 5) : resultadoEncontrado.clicks.click_rate, formatoDecimal) : 0;
                                campaniaActualizable.CantidadClicUnico = resultadoEncontrado.clicks.unique_subscriber_clicks;
                                campaniaActualizable.CantidadTotalClic = resultadoEncontrado.clicks.clicks_total;
                                campaniaActualizable.CantidadReporteAbuso = resultadoEncontrado.abuse_reports;
                                campaniaActualizable.CantidadDesuscritos = resultadoEncontrado.unsubscribed;

                                campaniaActualizable.UsuarioModificacion = "Regularizacion Metrica";
                                campaniaActualizable.FechaModificacion = DateTime.Now;

                                listaAActualizar.Add(campaniaActualizable);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }

                bool resultadoActualizacion = _repPrioridadMailChimpLista.Update(listaAActualizar);

                if (!resultadoActualizacion)
                    throw new Exception("Ocurrio un fallo en la actualizacion de la metrica ya obtenida principal");

                return listaAActualizar.Select(s => new PrioridadMailChimpCampaniaListaDTO { Id = s.Id, CampaniaMailChimpId = s.IdCampaniaMailchimp, ListaMailChimpId = s.IdListaMailchimp, CantidadAperturaUnica = s.CantidadAperturaUnica }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza la metrica diaria de las campanias de MailChimp
        /// </summary>
        /// <param name="actividadEncontradaDiariaMailChimpApi">Objeto de clase ListaActividadDiariaMailChimpCrudoDTO</param>
        /// <param name="resultadoDescargaReporteDiario">OBjeto de clase ListaActividadDiariaMailChimpCrudoDTO</param>
        /// <returns>Bool</returns>
        private bool ActualizarMetricaDatosDiariosMailChimp(PrioridadMailChimpCampaniaListaDTO actividadEncontradaDiariaMailChimpApi, ListaActividadDiariaMailChimpCrudoDTO resultadoDescargaReporteDiario)
        {
            try
            {
                var listaAActualizar = new List<MailchimpEstadisticaPorPrioridadBO>();
                var listaExistente = _repMailchimpEstadisticaPorPrioridad.GetBy(x => x.IdPrioridadMailChimpLista == actividadEncontradaDiariaMailChimpApi.Id).ToList();

                var resultado = resultadoDescargaReporteDiario.members.GroupBy(g => new { FechaConsulta = DateTime.Parse(g.opens[0].timestamp).Date }).ToList();

                foreach (var elemento in resultado)
                {
                    var elementoAEvaluar = listaExistente.FirstOrDefault(x => x.FechaConsulta == elemento.Key.FechaConsulta);

                    if (elementoAEvaluar != null)
                    {
                        if (!(elementoAEvaluar.CantidadCorreoAbierto == elemento.Count()))
                        {
                            elementoAEvaluar.CantidadCorreoAbierto = elemento.Count();
                            elementoAEvaluar.FechaModificacion = DateTime.Now;

                            listaAActualizar.Add(elementoAEvaluar);
                        }
                    }
                    else
                    {
                        listaAActualizar.Add(MapeoEstadisticaPorPrioridadDTOBO(new PrioridadMailChimpCampaniaListaDTO
                        {
                            Id = actividadEncontradaDiariaMailChimpApi.Id,
                            CampaniaMailChimpId = actividadEncontradaDiariaMailChimpApi.CampaniaMailChimpId,
                            ListaMailChimpId = actividadEncontradaDiariaMailChimpApi.ListaMailChimpId,
                            CantidadCorreoAbierto = elemento.Count(),
                            FechaConsulta = elemento.Key.FechaConsulta
                        }));
                    }
                }

                return _repMailchimpEstadisticaPorPrioridad.Update(listaAActualizar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mapear estadistica por prioridad de DTO a BO
        /// </summary>
        /// <param name="datosALlenar">Objeto de clase PrioridadMailChimpCampaniaListaDTO</param>
        /// <returns>Objeto de clase MailchimpEstadisticaPorPrioridadBO</returns>
        private MailchimpEstadisticaPorPrioridadBO MapeoEstadisticaPorPrioridadDTOBO(PrioridadMailChimpCampaniaListaDTO datosALlenar)
        {
            try
            {
                MailchimpEstadisticaPorPrioridadBO estadisticaPrioridad = new MailchimpEstadisticaPorPrioridadBO()
                {
                    IdPrioridadMailChimpLista = datosALlenar.Id,
                    CantidadCorreoAbierto = datosALlenar.CantidadCorreoAbierto,
                    FechaConsulta = datosALlenar.FechaConsulta,
                    Estado = true,
                    UsuarioCreacion = "Regularizacion Metrica",
                    UsuarioModificacion = "Regularizacion Metrica",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                return estadisticaPrioridad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
