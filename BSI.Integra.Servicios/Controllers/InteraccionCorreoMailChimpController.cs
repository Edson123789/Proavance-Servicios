using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Marketing.BO;
using Service.DTOs;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Servicios.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using MailChimp.Net.Core;
using BSI.Integra.Aplicacion.Transversal.Helper;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Planificacion/CampaniaMailing
    /// Autor: Wilber Choque - Carlos Crispin - Gian Miranda
    /// Fecha: 27/05/2021
    /// <summary>
    /// Contiene las acciones para la interaccion de correos de mailchimp
    /// </summary>
    [Route("api/InteraccionCorreoMailChimp")]
    public class InteraccionCorreoMailChimpController : Controller
    {
        private readonly PrioridadMailChimpListaCorreoRepositorio _repPrioridadMailChimpListaCorreo;
        private readonly PrioridadMailChimpListaRepositorio _repPrioridadMailChimpLista;
        private readonly CampaniaGeneralDetalleRepositorio _repCampaniaGeneralDetalle;
        private List<PrioridadMailChimpListaCorreoBO> listaPrioridadMailChimpListaCorreoActualizar;
        private readonly Aplicacion.Servicios.Services.InteraccionMailChimpService _interaccionMailChimpService;
        private MailChimpListaDetalleDTO mailChimpListaDetalle;
        private readonly integraDBContext _integraDBContext;


        //InsertarActualizarInteraccionInteraccionCorreoAbierto(listaInteraccionCorreo);
        private int _tipoInteraccionAperturaCorreoElectronico = 1;

        private readonly InteraccionCorreoMailChimpRepositorio _repInteraccionCorreoMailChimp;
        private readonly InteraccionCorreoDetalleMailChimpRepositorio _repInteraccionCorreoDetalleMailChimp;
        //var _repPrioridadMailChimpListaCorreo = new PrioridadMailChimpListaCorreoRepositorio();
        private List<InteraccionCorreoMailChimpBO> listadoInteraccionCorreoMailChimp;

        public InteraccionCorreoMailChimpController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _interaccionMailChimpService = new Aplicacion.Servicios.Services.InteraccionMailChimpService();
            _repPrioridadMailChimpListaCorreo = new PrioridadMailChimpListaCorreoRepositorio(_integraDBContext);
            _repPrioridadMailChimpLista = new PrioridadMailChimpListaRepositorio(_integraDBContext);
            _repCampaniaGeneralDetalle = new CampaniaGeneralDetalleRepositorio(_integraDBContext);
            _repInteraccionCorreoMailChimp = new InteraccionCorreoMailChimpRepositorio(_integraDBContext);
            _repInteraccionCorreoDetalleMailChimp = new InteraccionCorreoDetalleMailChimpRepositorio(_integraDBContext);
        }

        /// TipoFuncion: GET
		/// Autor: Gian Miranda
		/// Fecha: 08/06/2021
		/// Versión: 1.0
		/// <summary>
		/// Descarga la interaccion de correos abiertos por campania mailing
		/// </summary>
        /// <param name="FechaInicio">Fecha de inicio de descarga</param>
        /// <param name="FechaFin">Fecha de fin de descarga</param>
        /// <param name="IdCampaniaMailing">Id de la campania mailing (PK de la tabla mkt.T_CampaniaMailing)</param>
		/// <returns>ActionResult</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult DescargarInteraccionCorreoAbiertoPorCampaniaMailing(DateTime FechaInicio, DateTime FechaFin, int? IdCampaniaMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                List<PrioridadMailChimpListaBO> listadoPrioridadMailChimpLista = new List<PrioridadMailChimpListaBO>();
                if (IdCampaniaMailing.HasValue)
                {
                    listadoPrioridadMailChimpLista = _repPrioridadMailChimpLista.ObtenerPorCampaniaMailing(IdCampaniaMailing.Value);
                }
                else
                {
                    FechaInicio = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
                    FechaFin = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 59);
                    var listadoCampanias = _repPrioridadMailChimpLista.ObtenerCampaniasPorFechaEnvio(FechaInicio, FechaFin);
                    listadoPrioridadMailChimpLista = listadoCampanias.DistinctBy(x => x.IdCampaniaMailchimp).ToList();
                }
                var listadoError = new List<string>();
                var listadoCorrecto = new List<string>();
                foreach (var item in listadoPrioridadMailChimpLista)
                {
                    try
                    {
                        var resultado = this.DescargarInteraccionCorreoAbiertoPorCampaniaMailChimp(item.IdCampaniaMailchimp);
                        listadoCorrecto.Add(item.IdListaMailchimp);
                    }
                    catch (Exception)
                    {
                        listadoError.Add(item.IdListaMailchimp);
                    }
                }
                //Notificacion de termino de descarga
                List<string> correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "jdiazp@bsginstitute.com"
                };
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                var subject = "";
                if (IdCampaniaMailing.HasValue)
                {
                    subject = string.Concat("Se termino de descargar interaccion correo abierto de la campaña con Id ", IdCampaniaMailing.ToString());
                }
                else
                {
                    subject = string.Concat("Se termino de descargar interaccion correo abierto desde el ", FechaInicio.ToString("yyyy/MM/dd"), " hasta el ", FechaFin.ToString("yyyy/MM/dd"));
                }
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = subject,
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(new { listadoError, listadoCorrecto })),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
                return Ok(new { listadoError, listadoCorrecto });
            }
            catch (Exception e)
            {
                List<string> correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "wchoque@bsginstitute.com"
                };
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                var subject = "";
                if (IdCampaniaMailing.HasValue)
                {
                    subject = string.Concat("ERROR - Descargar interaccion click enlace de la campania con Id ", IdCampaniaMailing.ToString());
                }
                else
                {
                    subject = string.Concat("ERROR - Descargar interaccion click enlace desde el ", FechaInicio.ToString("yyyy/MM/dd"), " hasta el ", FechaFin.ToString("yyyy/MM/dd"));
                }
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = subject,
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(e)),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();

                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult DescargarDetalle()
        {
            try
            {
                var listaCorrecta = new List<string>();
                var listaError = new List<string>();
                var listaMailChimp = _repPrioridadMailChimpLista.ObtenerListasDescargar();

                foreach (var item in listaMailChimp)
                {
                    try
                    {
                        this.DescargarDetalleCampaniaMailChimp(item.Valor, 0, 100);
                        listaCorrecta.Add(item.Valor);
                    }
                    catch (Exception e)
                    {
                        listaError.Add(item.Valor);
                    }
                }
                return Ok(new { listaCorrecta, listaError });
            }
            catch (Exception)
            {
                throw;
            }
        }

        private MailChimpDetalleCampaniaDTO DescargarDetalleMailChimpPorCampaniaMailChimp(string IdCampaniaMailChimp)
        {
            try
            {
                MailChimpDetalleCampaniaDTO resultado = _interaccionMailChimpService.DescargaInformacionCampaniaMailChimp(IdCampaniaMailChimp);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private MailChimpReporteDTO DescargarReporteMailChimpPorCampaniaMailChimp(string IdCampaniaMailChimp)
        {
            try
            {
                MailChimpReporteDTO resultado = _interaccionMailChimpService.DescargaCantidadEnviadosMailChimp(IdCampaniaMailChimp);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Carlos Crispin - Gian Miranda
        /// Fecha: 08/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Descarga de interacciones de correo abierto por campania Mailchimp
        /// </summary>
        /// <param name="IdCampaniaMailChimp">Id original de la campania mailchimp</param>
        /// <param name="count">Conteo para descarga</param>
        /// <param name="offset">Margen inicial</param>
        /// <returns>ActionResult</returns>
        [Route("[action]/{IdCampaniaMailChimp}/{offset}/{count}")]
        [HttpGet]
        public ActionResult DescargarInteraccionCorreoAbiertoPorCampaniaMailChimp(string IdCampaniaMailChimp, int offset = 0, int count = 100)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var cantidadRepeticiones = 1;
                bool primeraVez = true;
                CorreoAbiertoPorCampaniaMailchimp listaInteraccionCorreoAbierto = null;
                for (int i = 0; i < cantidadRepeticiones; i++)
                {
                    if (primeraVez)
                    {
                        listaInteraccionCorreoAbierto = _interaccionMailChimpService.ObtenerInteraccionCorreoAbierto(IdCampaniaMailChimp, offset, count);
                        cantidadRepeticiones = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(listaInteraccionCorreoAbierto.total_items) / count));
                        primeraVez = false;
                    }
                    else
                    {
                        listaInteraccionCorreoAbierto.members.AddRange(_interaccionMailChimpService.ObtenerInteraccionCorreoAbierto(IdCampaniaMailChimp, offset, count).members);
                    }
                    offset += count;
                }

                var prioridadMailchimpListaExistente = _repPrioridadMailChimpLista.GetBy(x => x.IdCampaniaMailchimp == IdCampaniaMailChimp);
                var prioridadMailchimpListaCorreoExistente = _repPrioridadMailChimpListaCorreo.PrioridadMailChimpListaCorreoCompletoPorCampaniaMailChimpId(IdCampaniaMailChimp);
                var interaccionCorreoMailChimpExistente = _repInteraccionCorreoMailChimp.InteraccionCorreoCompletoPorCampaniaMailChimpId(IdCampaniaMailChimp);
                _repInteraccionCorreoDetalleMailChimp.EliminarInteraccionCorreoDetalleMailChimpPorCampaniaMailChimpId(IdCampaniaMailChimp);

                listadoInteraccionCorreoMailChimp = new List<InteraccionCorreoMailChimpBO>();
                foreach (var item in listaInteraccionCorreoAbierto.members)
                {
                    if (string.IsNullOrEmpty(item.merge_fields.IDPMLC))
                    {
                        var prioridadMailchimpLista = prioridadMailchimpListaExistente.FirstOrDefault(x => x.IdListaMailchimp == item.list_id);
                        // Si no existe deberia enviarme un aviso que esta interaccion no puede guardarse porque no pertenece a ninguna prioridad mailchimp lista
                        if (prioridadMailchimpLista != null)
                        {
                            continue;
                        }

                        var prioridadMailchimpListaCorreo = prioridadMailchimpListaCorreoExistente.FirstOrDefault(x => x.IdPrioridadMailChimpLista == prioridadMailchimpLista.Id && x.Email1 == item.email_address);

                        //Si no existe prioridad mailchimp lista correo debemos avisar que no existe una prioridad 
                        if (prioridadMailchimpListaCorreo != null)
                        {
                            continue;
                        }

                        InteraccionCorreoMailChimpBO interaccionCorreoMailChimp = null;
                        if (interaccionCorreoMailChimpExistente.Any(x => x.Email == item.email_address && x.IdTipoInteraccion == _tipoInteraccionAperturaCorreoElectronico && x.IdPrioridadMailChimpListaCorreo == prioridadMailchimpListaCorreo.Id && x.EmailMailchimpId == item.email_id))
                        {
                            interaccionCorreoMailChimp = interaccionCorreoMailChimpExistente.FirstOrDefault(x => x.Email == item.email_address && x.IdTipoInteraccion == _tipoInteraccionAperturaCorreoElectronico && x.IdPrioridadMailChimpListaCorreo == prioridadMailchimpListaCorreo.Id && x.EmailMailchimpId == item.email_id);
                            interaccionCorreoMailChimp.CantidadInteracciones = item.opens_count;
                            interaccionCorreoMailChimp.EstadoSuscripcion = item.contact_status;
                            interaccionCorreoMailChimp.FechaModificacion = DateTime.Now;
                            interaccionCorreoMailChimp.UsuarioModificacion = "SystemModificacion";
                        }
                        else
                        {
                            interaccionCorreoMailChimp = new InteraccionCorreoMailChimpBO()
                            {
                                Email = item.email_address,
                                IdTipoInteraccion = _tipoInteraccionAperturaCorreoElectronico,
                                IdPrioridadMailChimpListaCorreo = prioridadMailchimpListaCorreo.Id,
                                EmailMailchimpId = item.email_id,
                                CantidadInteracciones = item.opens_count,
                                EstadoSuscripcion = item.contact_status,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "SYSTEM",
                                UsuarioModificacion = "SYSTEM",
                                Estado = true
                            };
                        }

                        foreach (var interaccionCorreo in item.opens)
                        {
                            var interaccionCorreoDetalleMailChimpBO = new InteraccionCorreoDetalleMailChimpBO()
                            {
                                Fecha = interaccionCorreo.timestamp,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "SYSTEM",
                                UsuarioModificacion = "SYSTEM",
                                Estado = true
                            };
                            interaccionCorreoMailChimp.ListaInteraccionCorreoDetalleMailChimp.Add(interaccionCorreoDetalleMailChimpBO);
                        }
                        listadoInteraccionCorreoMailChimp.Add(interaccionCorreoMailChimp);
                    }
                    //en caso si tenga el id de prioridad mailchimp lista correo
                    if (Int32.TryParse(item.merge_fields.IDPMLC, out int idPrioridadMailChimpListaCorreo))
                    {
                        if (!prioridadMailchimpListaCorreoExistente.Any(x => x.Id == idPrioridadMailChimpListaCorreo))
                        {
                            continue;
                        }

                        var _prioridadMailChimpListaCorreo = prioridadMailchimpListaCorreoExistente.FirstOrDefault(x => x.Id == idPrioridadMailChimpListaCorreo);

                        InteraccionCorreoMailChimpBO interaccionCorreoMailChimp = new InteraccionCorreoMailChimpBO();
                        if (interaccionCorreoMailChimpExistente.Any(x => x.Email == item.email_address && x.IdTipoInteraccion == _tipoInteraccionAperturaCorreoElectronico && x.IdPrioridadMailChimpListaCorreo == _prioridadMailChimpListaCorreo.Id && x.EmailMailchimpId == item.email_id))
                        {
                            interaccionCorreoMailChimp = interaccionCorreoMailChimpExistente.FirstOrDefault(x => x.Email == item.email_address && x.IdTipoInteraccion == _tipoInteraccionAperturaCorreoElectronico && x.IdPrioridadMailChimpListaCorreo == _prioridadMailChimpListaCorreo.Id && x.EmailMailchimpId == item.email_id);
                            interaccionCorreoMailChimp.CantidadInteracciones = item.opens_count;
                            interaccionCorreoMailChimp.EstadoSuscripcion = item.contact_status;
                            interaccionCorreoMailChimp.FechaModificacion = DateTime.Now;
                            interaccionCorreoMailChimp.UsuarioModificacion = "SystemModificacion";
                        }
                        else
                        {
                            interaccionCorreoMailChimp = new InteraccionCorreoMailChimpBO()
                            {
                                Email = item.email_address,
                                IdTipoInteraccion = _tipoInteraccionAperturaCorreoElectronico,
                                IdPrioridadMailChimpListaCorreo = _prioridadMailChimpListaCorreo.Id,
                                EmailMailchimpId = item.email_id,
                                CantidadInteracciones = item.opens_count,
                                EstadoSuscripcion = item.contact_status,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "SYSTEM",
                                UsuarioModificacion = "SYSTEM",
                                Estado = true
                            };
                        }

                        foreach (var interaccionCorreo in item.opens)
                        {
                            var interaccionCorreoDetalleMailChimpBO = new InteraccionCorreoDetalleMailChimpBO()
                            {
                                Fecha = interaccionCorreo.timestamp,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "SYSTEM",
                                UsuarioModificacion = "SYSTEM",
                                Estado = true
                            };
                            interaccionCorreoMailChimp.ListaInteraccionCorreoDetalleMailChimp.Add(interaccionCorreoDetalleMailChimpBO);
                        }
                        listadoInteraccionCorreoMailChimp.Add(interaccionCorreoMailChimp);
                    }
                }

                if (listadoInteraccionCorreoMailChimp.Any())
                    _repInteraccionCorreoMailChimp.Update(listadoInteraccionCorreoMailChimp);

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 30/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Descarga la cantidad efectiva de envio de Mailchimp
        /// </summary>
        /// <param name="IdCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <param name="Usuario">Nombre del usuario que realiza la descarga</param>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]/{IdCampaniaGeneral}/{Usuario}")]
        [HttpGet]
        public ActionResult ObtenerCantidadEnviadoMailchimp(int IdCampaniaGeneral, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                #region Preparacion de repositorios
                CampaniaGeneralRepositorio _repCampaniaGeneral = new CampaniaGeneralRepositorio(_integraDBContext);
                CampaniaGeneralDetalleRepositorio _repCampaniaGeneralDetalle = new CampaniaGeneralDetalleRepositorio(_integraDBContext);
                List<PrioridadMailChimpListaBO> listadoPrioridadMailChimpLista = new List<PrioridadMailChimpListaBO>();
                #endregion

                List<int> IdCampaniaGeneralDetalle = _repCampaniaGeneralDetalle.GetBy(w => w.IdCampaniaGeneral == IdCampaniaGeneral).Select(w => w.Id).ToList();
                listadoPrioridadMailChimpLista = _repPrioridadMailChimpLista.ObtenerPorCampaniaGeneralDetalle(IdCampaniaGeneralDetalle);

                #region Descarga de cantidad enviados correctamente
                foreach (var item in listadoPrioridadMailChimpLista)
                {
                    var resultado = this.DescargarReporteMailChimpPorCampaniaMailChimp(item.IdCampaniaMailchimp);

                    item.CantidadEnviadosMailChimp = resultado.emails_sent;
                    item.FechaModificacion = DateTime.Now;
                    item.UsuarioModificacion = Usuario;
                }

                _repPrioridadMailChimpLista.Update(listadoPrioridadMailChimpLista);
                #endregion

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 30/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Descarga la interaciones,correos abiertos,detalles de las campanias
        /// </summary>
        /// <param name="IdCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <param name="Usuario">Nombre del usuario que realiza la descarga</param>
        /// <returns>ListaError,List(string)</returns>
        [Route("[Action]/{IdCampaniaGeneral}/{Usuario}")]
        [HttpGet]
        public ActionResult DescargarInteracciones(int IdCampaniaGeneral, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int idEstadoEnvioTemporal = ValorEstatico.IdEstadoEnvioEnProceso;
            try
            {
                #region Preparacion de repositorios
                CampaniaGeneralRepositorio _repCampaniaGeneral = new CampaniaGeneralRepositorio(_integraDBContext);
                IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
                #endregion

                if (!_repIntegraAspNetUsers.ExistePorNombreUsuario(Usuario))
                {
                    return BadRequest("El usuario no existe");
                }

                #region Preparacion Envio Correo
                TMK_MailServiceImpl serviceEnvioNotificacion = new TMK_MailServiceImpl();

                string usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(Usuario);

                List<string> correosCopia = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };
                #endregion

                #region Preparacion de listado de prioridades
                CampaniaGeneralBO campaniaGeneral = _repCampaniaGeneral.FirstById(IdCampaniaGeneral);
                CampaniaGeneralDetalleBO campaniaGeneralDetalle = new CampaniaGeneralDetalleBO(_integraDBContext);
                idEstadoEnvioTemporal = campaniaGeneral.IdEstadoEnvioMailing;
                campaniaGeneral.IdEstadoEnvioMailing = ValorEstatico.IdEstadoEnvioEnProceso;
                _repCampaniaGeneral.Update(campaniaGeneral);

                List<int> listaIdCampaniaGeneralDetalle = new List<int>();

                List<PrioridadMailChimpListaBO> listadoPrioridadMailChimpLista = new List<PrioridadMailChimpListaBO>();
                listaIdCampaniaGeneralDetalle = _repCampaniaGeneralDetalle.GetBy(w => w.IdCampaniaGeneral == IdCampaniaGeneral).Select(w => w.Id).ToList();
                listadoPrioridadMailChimpLista = _repPrioridadMailChimpLista.ObtenerPorCampaniaGeneralDetalle(listaIdCampaniaGeneralDetalle);
                #endregion

                #region Descarga de clics de enlaces
                List<string> listadoClickEnlaceError = new List<string>();
                List<string> listadoClickEnlaceCorrecto = new List<string>();

                foreach (var item in listadoPrioridadMailChimpLista)
                {
                    try
                    {
                        var resultado = this.DescargarClickEnlacesCampaniaPorCampaniaMailChimp(item.IdCampaniaMailchimp);
                        listadoClickEnlaceCorrecto.Add(item.IdListaMailchimp);
                    }
                    catch (Exception)
                    {
                        listadoClickEnlaceError.Add(item.IdListaMailchimp ?? "Sin Id de Mailchimp");
                    }
                }

                string asuntoInteraccionClic = string.Concat("Se terminó de descargar interacciones click enlaces de la campaña general con Id ", IdCampaniaGeneral.ToString());

                // Envio de correo
                TMKMailDataDTO mailData1 = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = asuntoInteraccionClic,
                    Message = campaniaGeneralDetalle.GenerarNuevaPlantillaArchivadoCampaniaGeneral("Descarga de clicks", campaniaGeneral.Nombre, listadoClickEnlaceCorrecto, listadoClickEnlaceError),
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };

                serviceEnvioNotificacion.SetData(mailData1);
                serviceEnvioNotificacion.SendMessageTask();
                #endregion

                #region Descarga correos abiertos
                List<string> listadoCorreoAbiertoError = new List<string>();
                List<string> listadoCorreoAbiertoCorrecto = new List<string>();

                foreach (var item in listadoPrioridadMailChimpLista)
                {
                    try
                    {
                        var resultado = this.DescargarInteraccionCorreoAbiertoPorCampaniaMailChimp(item.IdCampaniaMailchimp);
                        listadoCorreoAbiertoCorrecto.Add(item.IdListaMailchimp);
                    }
                    catch (Exception)
                    {
                        listadoCorreoAbiertoError.Add(item.IdListaMailchimp ?? "Sin Id de Mailchimp");
                    }
                }

                string asuntoInteraccionCorreo = string.Concat("Se termino de descargar interaccion correo abierto de la campaña general con Id ", IdCampaniaGeneral.ToString());

                // Envio de correo
                TMKMailDataDTO mailData2 = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = asuntoInteraccionCorreo,
                    Message = campaniaGeneralDetalle.GenerarNuevaPlantillaArchivadoCampaniaGeneral("Descarga de correos abiertos", campaniaGeneral.Nombre, listadoCorreoAbiertoCorrecto, listadoCorreoAbiertoError),
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };

                serviceEnvioNotificacion.SetData(mailData2);
                serviceEnvioNotificacion.SendMessageTask();
                #endregion

                CampaniaGeneralBO campaniaGeneralFinalizacion = _repCampaniaGeneral.FirstById(IdCampaniaGeneral);
                campaniaGeneralFinalizacion.IdEstadoEnvioMailing = idEstadoEnvioTemporal;
                _repCampaniaGeneral.Update(campaniaGeneralFinalizacion);

                return Ok(true);
            }
            catch (Exception e)
            {
                IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
                string usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(Usuario);

                CampaniaGeneralRepositorio _repCampaniaGeneral = new CampaniaGeneralRepositorio(_integraDBContext);
                CampaniaGeneralBO campaniaGeneral = _repCampaniaGeneral.FirstById(IdCampaniaGeneral);
                campaniaGeneral.IdEstadoEnvioMailing = idEstadoEnvioTemporal;
                _repCampaniaGeneral.Update(campaniaGeneral);

                List<string> correosCopia = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl serviceNotificacion = new TMK_MailServiceImpl();
                string asuntoError = string.Concat("ERROR - Descarga interacciones campania general con Id: ", IdCampaniaGeneral.ToString());

                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = asuntoError,
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(e)),
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };
                serviceNotificacion.SetData(mailData);
                serviceNotificacion.SendMessageTask();

                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 30/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Descarga la interaciones,correos abiertos,detalles de las campanias
        /// </summary>
        /// <param name="IdCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <param name="Usuario">Usuario que realiza el archivado</param>
        /// <returns>ListaError,List(string)</returns>
        [Route("[Action]/{IdCampaniaGeneral}/{Usuario}")]
        [HttpGet]
        public ActionResult ArchivarCampaniaGeneral(int? IdCampaniaGeneral, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int idEstadoEnvioTemporal = ValorEstatico.IdEstadoEnvioEnProceso;
            try
            {
                #region Preparacion de repositorios
                CampaniaGeneralRepositorio _repCampaniaGeneral = new CampaniaGeneralRepositorio(_integraDBContext);
                IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
                #endregion

                if (!_repIntegraAspNetUsers.ExistePorNombreUsuario(Usuario))
                {
                    return BadRequest("El usuario no existe");
                }

                #region Preparacion Envio Correo
                TMK_MailServiceImpl serviceEnvioNotificacion = new TMK_MailServiceImpl();

                string usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(Usuario);

                List<string> correosCopia = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };
                #endregion

                #region Preparacion de listado de prioridades
                CampaniaGeneralBO campaniaGeneral = _repCampaniaGeneral.FirstById(IdCampaniaGeneral.Value);
                idEstadoEnvioTemporal = campaniaGeneral.IdEstadoEnvioMailing;
                campaniaGeneral.IdEstadoEnvioMailing = ValorEstatico.IdEstadoEnvioEnProceso;
                _repCampaniaGeneral.Update(campaniaGeneral);

                CampaniaGeneralDetalleBO campaniaGeneralDetalle = new CampaniaGeneralDetalleBO(_integraDBContext);
                List<int> listaIdCampaniaGeneralDetalle = new List<int>();

                List<PrioridadMailChimpListaBO> listadoPrioridadMailChimpLista = new List<PrioridadMailChimpListaBO>();
                if (IdCampaniaGeneral.HasValue)
                {
                    listaIdCampaniaGeneralDetalle = _repCampaniaGeneralDetalle.GetBy(w => w.IdCampaniaGeneral == IdCampaniaGeneral.Value).Select(w => w.Id).ToList();
                    listadoPrioridadMailChimpLista = _repPrioridadMailChimpLista.ObtenerPorCampaniaGeneralDetalle(listaIdCampaniaGeneralDetalle);
                }
                else
                {
                    return BadRequest("Valor invalido para IdCampaniaGeneral");
                }
                #endregion

                PrioridadMailChimpListaBO prioridadMailChimpLista = new PrioridadMailChimpListaBO();

                #region Pausa y descarga de cantidad enviados correctamente
                foreach (var item in listadoPrioridadMailChimpLista)
                {
                    try
                    {
                        // Pausar envio
                        var resultadoInformacion = this.DescargarDetalleMailChimpPorCampaniaMailChimp(item.IdCampaniaMailchimp);

                        if (resultadoInformacion.status == "schedule")
                        {
                            prioridadMailChimpLista.PausarHorario(item.IdCampaniaMailchimp, new CampaignScheduleRequest() { }).Wait();
                        }

                        var resultado = this.DescargarReporteMailChimpPorCampaniaMailChimp(item.IdCampaniaMailchimp);

                        item.CantidadEnviadosMailChimp = resultado.emails_sent;
                        if (!string.IsNullOrEmpty(resultado.send_time)) item.FechaEnvio = DateTime.Parse(resultado.send_time);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                _repPrioridadMailChimpLista.Update(listadoPrioridadMailChimpLista);
                #endregion

                #region Descarga de clics de enlaces
                List<string> listadoClickEnlaceError = new List<string>();
                List<string> listadoClickEnlaceCorrecto = new List<string>();

                foreach (var item in listadoPrioridadMailChimpLista)
                {
                    try
                    {
                        var resultado = this.DescargarClickEnlacesCampaniaPorCampaniaMailChimp(item.IdCampaniaMailchimp);
                        listadoClickEnlaceCorrecto.Add(item.IdListaMailchimp);
                    }
                    catch (Exception)
                    {
                        listadoClickEnlaceError.Add(item.IdListaMailchimp ?? "Sin Id de Mailchimp");
                    }
                }

                string asuntoInteraccionClic = string.Concat("Se terminó de descargar interacciones click enlaces de la campaña general con Id ", IdCampaniaGeneral.ToString());

                // Envio de correo
                TMKMailDataDTO mailData1 = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = asuntoInteraccionClic,
                    Message = campaniaGeneralDetalle.GenerarNuevaPlantillaArchivadoCampaniaGeneral("Descarga de clicks", campaniaGeneral.Nombre, listadoClickEnlaceCorrecto, listadoClickEnlaceError),
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };

                serviceEnvioNotificacion.SetData(mailData1);
                serviceEnvioNotificacion.SendMessageTask();
                #endregion

                #region Descarga correos abiertos
                List<string> listadoCorreoAbiertoError = new List<string>();
                List<string> listadoCorreoAbiertoCorrecto = new List<string>();

                foreach (var item in listadoPrioridadMailChimpLista)
                {
                    try
                    {
                        var resultado = this.DescargarInteraccionCorreoAbiertoPorCampaniaMailChimp(item.IdCampaniaMailchimp);
                        listadoCorreoAbiertoCorrecto.Add(item.IdListaMailchimp);
                    }
                    catch (Exception)
                    {
                        listadoCorreoAbiertoError.Add(item.IdListaMailchimp ?? "Sin Id de Mailchimp");
                    }
                }

                string asuntoInteraccionCorreo = string.Concat("Se termino de descargar interaccion correo abierto de la campaña general con Id ", IdCampaniaGeneral.ToString());

                // Envio de correo
                TMKMailDataDTO mailData2 = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = asuntoInteraccionCorreo,
                    Message = campaniaGeneralDetalle.GenerarNuevaPlantillaArchivadoCampaniaGeneral("Descarga de correos abiertos", campaniaGeneral.Nombre, listadoCorreoAbiertoCorrecto, listadoCorreoAbiertoError),
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };

                serviceEnvioNotificacion.SetData(mailData2);
                serviceEnvioNotificacion.SendMessageTask();
                #endregion

                #region Descarga de detalles de la campania
                List<string> listadoDetalleCampaniaError = new List<string>();
                List<string> listadoDetalleCampaniaCorrecto = new List<string>();

                foreach (var item in listadoPrioridadMailChimpLista)
                {
                    try
                    {
                        var resultado = this.DescargarDetalleCampaniaMailChimp(item.IdListaMailchimp);
                        listadoDetalleCampaniaCorrecto.Add(item.IdListaMailchimp);
                    }
                    catch (Exception e)
                    {
                        listadoDetalleCampaniaError.Add(item.IdListaMailchimp ?? "Sin Id de Mailchimp");
                    }
                }

                string asuntoDescargaDetalle = string.Concat("Se termino de descargar detalle de campaña de la campaña general con Id ", IdCampaniaGeneral.ToString());

                TMKMailDataDTO mailData3 = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = asuntoDescargaDetalle,
                    Message = campaniaGeneralDetalle.GenerarNuevaPlantillaArchivadoCampaniaGeneral("Descarga de clicks", campaniaGeneral.Nombre, listadoDetalleCampaniaCorrecto, listadoDetalleCampaniaError),
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };
                serviceEnvioNotificacion.SetData(mailData3);
                serviceEnvioNotificacion.SendMessageTask();
                #endregion

                #region Archivado de contactos
                List<string> listadoListaMailchimpError = new List<string>();
                List<string> listadoListaMailchimpCorrecto = new List<string>();

                foreach (var item in listadoPrioridadMailChimpLista)
                {
                    try
                    {
                        var mailchimpList = item.GetListAsync(item.IdListaMailchimp).Result;
                        var serviceMailchimp = new TMK_MailchimpServiceImpl();
                        var listaMiembros = serviceMailchimp.GetAllAsync(item.IdListaMailchimp).Result;

                        while (listaMiembros.Any())
                        {
                            var archivadoCorrectamente = item.ArchivarListaConMiembros(listaMiembros.ToList(), mailchimpList).Result;
                            listaMiembros = serviceMailchimp.GetAllAsync(item.IdListaMailchimp).Result;
                        }

                        listadoListaMailchimpCorrecto.Add(item.IdListaMailchimp);
                    }
                    catch (Exception)
                    {
                        listadoListaMailchimpError.Add(item.IdListaMailchimp ?? "No se subio a mailchimp la lista");
                    }
                }

                string asuntoArchivado = string.Concat("Se termino de archivar los contactos de la campaña general con Id ", IdCampaniaGeneral.ToString());

                TMKMailDataDTO mailData4 = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = asuntoArchivado,
                    Message = campaniaGeneralDetalle.GenerarNuevaPlantillaArchivadoCampaniaGeneral("Archivado de contactos", campaniaGeneral.Nombre, listadoListaMailchimpCorrecto, listadoListaMailchimpError),
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };
                serviceEnvioNotificacion.SetData(mailData4);
                serviceEnvioNotificacion.SendMessageTask();

                // Verificacion de enviado para cambiar estado archivado
                CampaniaGeneralBO campaniaGeneralFinalizacion = _repCampaniaGeneral.FirstById(IdCampaniaGeneral.Value);
                bool resultadoActualizacionEstado = campaniaGeneral.ActualizarEstadoArchivado(campaniaGeneralFinalizacion);
                #endregion

                return Ok(new { listadoClickEnlaceError, listadoClickEnlaceCorrecto, listadoCorreoAbiertoError, listadoCorreoAbiertoCorrecto, listadoDetalleCampaniaError, listadoDetalleCampaniaCorrecto, listadoListaMailchimpError, listadoListaMailchimpCorrecto });
            }
            catch (Exception e)
            {
                IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
                string usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(Usuario);

                CampaniaGeneralRepositorio _repCampaniaGeneral = new CampaniaGeneralRepositorio(_integraDBContext);
                CampaniaGeneralBO campaniaGeneral = _repCampaniaGeneral.FirstById(IdCampaniaGeneral.Value);
                campaniaGeneral.IdEstadoEnvioMailing = idEstadoEnvioTemporal;
                _repCampaniaGeneral.Update(campaniaGeneral);

                List<string> correosCopia = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl serviceNotificacion = new TMK_MailServiceImpl();
                string asuntoError = string.Concat("ERROR - Archivar campania general con Id: ", IdCampaniaGeneral.ToString());

                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = asuntoError,
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(e)),
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };
                serviceNotificacion.SetData(mailData);
                serviceNotificacion.SendMessageTask();

                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Gian Miranda
        /// Fecha: 08/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Descarga la interaccion de clicks en enlaces por campania mailing
        /// </summary>
        /// <param name="FechaInicio">Fecha de inicio de descarga</param>
        /// <param name="FechaFin">Fecha de fin de descarga</param>
        /// <param name="IdCampaniaMailing">Id de la campania mailing (PK de la tabla mkt.T_CampaniaMailing)</param>
        /// <returns>ActionResult</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult DescargarInteraccionClickEnlaceCampanias(DateTime FechaInicio, DateTime FechaFin, int? IdCampaniaMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                List<PrioridadMailChimpListaBO> listadoPrioridadMailChimpLista = new List<PrioridadMailChimpListaBO>();
                if (IdCampaniaMailing.HasValue)
                {
                    listadoPrioridadMailChimpLista = _repPrioridadMailChimpLista.ObtenerPorCampaniaMailing(IdCampaniaMailing.Value);
                }
                else
                {
                    FechaInicio = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
                    FechaFin = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 59);
                    var listadoCampanias = _repPrioridadMailChimpLista.ObtenerCampaniasPorFechaEnvio(FechaInicio, FechaFin);
                    listadoPrioridadMailChimpLista = listadoCampanias.DistinctBy(x => x.IdCampaniaMailchimp).ToList();
                }
                var listadoError = new List<string>();
                var listadoCorrecto = new List<string>();
                foreach (var item in listadoPrioridadMailChimpLista)
                {
                    try
                    {
                        var resultado = this.DescargarClickEnlacesCampaniaPorCampaniaMailChimp(item.IdCampaniaMailchimp);
                        listadoCorrecto.Add(item.IdListaMailchimp);
                    }
                    catch (Exception)
                    {
                        listadoError.Add(item.IdListaMailchimp);
                    }
                }
                //Notificacion de termino de descarga
                List<string> correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "jdiazp@bsginstitute.com"
                };
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                var subject = "";
                if (IdCampaniaMailing.HasValue)
                {
                    subject = string.Concat("Se termino de descargar interaccion click enlace de la campaña con Id ", IdCampaniaMailing.ToString());
                }
                else
                {
                    subject = string.Concat("Se termino de descargar interaccion click enlace desde el ", FechaInicio.ToString("yyyy/MM/dd"), " hasta el ", FechaFin.ToString("yyyy/MM/dd"));
                }
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = subject,
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(new { listadoError, listadoCorrecto })),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
                return Ok(new { listadoError, listadoCorrecto });
            }
            catch (Exception e)
            {
                List<string> correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "jdiazp@bsginstitute.com"
                };
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                var subject = "";
                if (IdCampaniaMailing.HasValue)
                {
                    subject = string.Concat("ERROR - Descargar interaccion click enlace con Id ", IdCampaniaMailing.ToString());
                }
                else
                {
                    subject = string.Concat("ERROR - Descargar interaccion click enlace desde el ", FechaInicio.ToString("yyyy/MM/dd"), " hasta el ", FechaFin.ToString("yyyy/MM/dd"));
                }
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = subject,
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(e)),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();

                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin - Gian Miranda
        /// Fecha: 27/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Descarga clicks en los enlaces de la campania por campania mailchimp
        /// </summary>
        /// <param name="IdCampaniaMailChimp">Id de la campania Mailchimp</param>
        /// <param name="offset">Margen para la descarga</param>
        /// <param name="count">Contador de descarga</param>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje del error</returns>
        [Route("[action]/{IdCampaniaMailChimp}/{offset}/{count}")]
        [HttpGet]
        public ActionResult DescargarClickEnlacesCampaniaPorCampaniaMailChimp(string IdCampaniaMailChimp, int offset = 0, int count = 100)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var cantidadRepeticiones = 1;
                bool primeraVez = true;
                ClickEnlacePorCampaniaMailchimp listaEnlacesMailchimp = new ClickEnlacePorCampaniaMailchimp();
                for (int i = 0; i < cantidadRepeticiones; i++)
                {
                    if (primeraVez)
                    {
                        listaEnlacesMailchimp = _interaccionMailChimpService.ObtenerEnlaces(IdCampaniaMailChimp, offset, count);

                        cantidadRepeticiones = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(listaEnlacesMailchimp.total_items) / (count == 0 ? 1 : count)));
                        primeraVez = false;
                    }
                    else
                    {
                        listaEnlacesMailchimp.urls_clicked.AddRange(_interaccionMailChimpService.ObtenerEnlaces(IdCampaniaMailChimp, offset, count).urls_clicked);
                    }

                    offset += count;
                }

                InsertarEnlaceMailChimp(listaEnlacesMailchimp, IdCampaniaMailChimp);

                return Ok(true);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Carlos Crispin - Gian Miranda
        /// Fecha: 08/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Descarga la interaccion de correos abiertos por campania mailing
        /// </summary>
        /// <param name="ListaEnlacesMailchimp">Objeto de clase ClickEnlacePorCampaniaMailchimp</param>
        /// <param name="IdCampaniaMailChimp">Id de la campania MailChimp (Id segun estandar de Mailchimp)</param>
        /// <returns>ActionResult</returns>
        private void InsertarEnlaceMailChimp(ClickEnlacePorCampaniaMailchimp ListaEnlacesMailchimp, string IdCampaniaMailChimp)
        {
            var tipoInteraccionClickEnlace = ValorEstatico.IdTipoInteraccionCorreoElectronicoRecibido;

            var _repEnlaceMailChimp = new EnlaceMailChimpRepositorio(_integraDBContext);
            var _repPrioridadMailChimpLista = new PrioridadMailChimpListaRepositorio(_integraDBContext);
            var _repInteraccionEnlaceMailchimp = new InteraccionEnlaceMailchimpRepositorio(_integraDBContext);
            var _repInteraccionEnlaceDetalleMailChimp = new InteraccionEnlaceDetalleMailChimpRepositorio(_integraDBContext);

            // Nueva logica
            var listaEnlaceMailChimpExistente = _repEnlaceMailChimp.GetBy(x => x.CampaniaMailChimpId == IdCampaniaMailChimp).ToList();
            var listaInteraccionEnlaceMailchimpExistente = _repInteraccionEnlaceMailchimp.InteraccionEnlaceMailchimpCompletoPorCampaniaMailChimpId(IdCampaniaMailChimp);
            var listaInteraccionEnlaceDetalleMailChimpExistente = _repInteraccionEnlaceDetalleMailChimp.InteraccionEnlaceDetalleMailchimpCompletoPorCampaniaMailChimpId(IdCampaniaMailChimp);

            var listadoEnlaceMailChimp = new List<EnlaceMailChimpBO>();
            foreach (var item in ListaEnlacesMailchimp.urls_clicked)
            {
                EnlaceMailChimpBO enlaceMailChimp = null;

                if (listaEnlaceMailChimpExistente.Any(x => x.UrlMailChimpId == item.id))
                {
                    enlaceMailChimp = listaEnlaceMailChimpExistente.FirstOrDefault(w => w.UrlMailChimpId == item.id);

                    enlaceMailChimp.TotalClicks = item.total_clicks;
                    enlaceMailChimp.PorcentajeClicks = item.click_percentage;
                    enlaceMailChimp.ClicksUnicos = item.unique_clicks;
                    enlaceMailChimp.PorcentajeClicksUnicos = item.unique_click_percentage;
                    enlaceMailChimp.FechaUltimoClick = item.last_click;
                    enlaceMailChimp.FechaModificacion = DateTime.Now;
                    enlaceMailChimp.UsuarioModificacion = "SYSTEM";
                }
                else
                {
                    enlaceMailChimp = new EnlaceMailChimpBO()
                    {
                        CampaniaMailChimpId = item.campaign_id,
                        UrlMailChimpId = item.id,
                        Url = item.url,
                        TotalClicks = item.total_clicks,
                        PorcentajeClicks = item.click_percentage,
                        ClicksUnicos = item.unique_clicks,
                        PorcentajeClicksUnicos = item.unique_click_percentage,
                        FechaUltimoClick = item.last_click,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = "SYSTEM",
                        UsuarioModificacion = "SYSTEM",
                        Estado = true
                    };
                }

                var cantidadRepeticionesEnlace = 1;
                bool primeraVez = true;
                var offsetEnlace = 0;
                var countEnlace = 1000;

                ClickEnlaceContactoPorCampaniaMailchimp listaEnlacesCampaniaContactoMailchimp = new ClickEnlaceContactoPorCampaniaMailchimp();
                for (int i = 0; i < cantidadRepeticionesEnlace; i++)
                {
                    if (primeraVez)
                    {
                        try
                        {
                            listaEnlacesCampaniaContactoMailchimp = _interaccionMailChimpService.ObtenerInteraccionClickEnlace(IdCampaniaMailChimp, item.id, offsetEnlace, countEnlace);
                        }
                        catch (Exception e)
                        {
                            listaEnlacesCampaniaContactoMailchimp = _interaccionMailChimpService.ObtenerInteraccionEnlace(IdCampaniaMailChimp, item.id);
                        }

                        cantidadRepeticionesEnlace = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(listaEnlacesCampaniaContactoMailchimp.total_items) / countEnlace));

                        primeraVez = false;
                    }
                    else
                    {
                        try
                        {
                            listaEnlacesCampaniaContactoMailchimp.members.AddRange(_interaccionMailChimpService.ObtenerInteraccionClickEnlace(IdCampaniaMailChimp, item.id, offsetEnlace, countEnlace).members);
                        }
                        catch (Exception e)
                        {
                            listaEnlacesCampaniaContactoMailchimp.members.AddRange(_interaccionMailChimpService.ObtenerInteraccionEnlace(IdCampaniaMailChimp, item.id).members);
                        }
                    }

                    offsetEnlace += countEnlace;
                }

                if (listaEnlacesCampaniaContactoMailchimp != null)
                {
                    var listaPrioridadMailChimpExistente = _repPrioridadMailChimpLista.GetBy(x => x.IdCampaniaMailchimp == IdCampaniaMailChimp);

                    foreach (var interaccionContacto in listaEnlacesCampaniaContactoMailchimp.members)
                    {
                        // Agregamos interacciones por enlace
                        var prioridadMailChimpLista = listaPrioridadMailChimpExistente.FirstOrDefault(w => w.IdCampaniaMailchimp == interaccionContacto.campaign_id && w.IdListaMailchimp == interaccionContacto.list_id);

                        if (prioridadMailChimpLista != null)
                        {
                            InteraccionEnlaceMailchimpBO interaccionEnlaceMailchimp = new InteraccionEnlaceMailchimpBO();
                            InteraccionEnlaceDetalleMailChimpBO interaccionEnlaceDetalleMailChimp = new InteraccionEnlaceDetalleMailChimpBO();

                            if (enlaceMailChimp.Id > 0 && listaInteraccionEnlaceMailchimpExistente.Any(x => x.IdEnlaceMailChimp == enlaceMailChimp.Id && x.IdPrioridadMailChimpLista == prioridadMailChimpLista.Id && x.EmailMailChimpId == interaccionContacto.email_id))
                            {
                                interaccionEnlaceMailchimp = listaInteraccionEnlaceMailchimpExistente.FirstOrDefault(x => x.IdEnlaceMailChimp == enlaceMailChimp.Id && x.IdPrioridadMailChimpLista == prioridadMailChimpLista.Id && x.EmailMailChimpId == interaccionContacto.email_id);

                                interaccionEnlaceMailchimp.EsVip = interaccionContacto.vip;
                                interaccionEnlaceMailchimp.EstadoSuscripcion = interaccionContacto.contact_status;
                                interaccionEnlaceMailchimp.NroClicks = interaccionContacto.clicks;
                                interaccionEnlaceMailchimp.FechaModificacion = DateTime.Now;
                                interaccionEnlaceMailchimp.UsuarioModificacion = "SYSTEM";

                                interaccionEnlaceDetalleMailChimp = listaInteraccionEnlaceDetalleMailChimpExistente.FirstOrDefault(x => x.IdInteraccionEnlaceMailChimp == interaccionEnlaceMailchimp.Id);

                                if (interaccionEnlaceDetalleMailChimp != null)
                                {
                                    interaccionEnlaceDetalleMailChimp.CantidadClicks = interaccionContacto.clicks;
                                    interaccionEnlaceDetalleMailChimp.Fecha = DateTime.Now;
                                    interaccionEnlaceDetalleMailChimp.FechaModificacion = DateTime.Now;
                                    interaccionEnlaceDetalleMailChimp.UsuarioModificacion = "SYSTEM";
                                }
                                else
                                {
                                    interaccionEnlaceDetalleMailChimp = new InteraccionEnlaceDetalleMailChimpBO()
                                    {
                                        CantidadClicks = interaccionContacto.clicks,
                                        Fecha = DateTime.Now,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = "SYSTEM",
                                        UsuarioModificacion = "SYSTEM",
                                        Estado = true
                                    };
                                }
                            }
                            else
                            {
                                interaccionEnlaceMailchimp = new InteraccionEnlaceMailchimpBO()
                                {
                                    IdPrioridadMailChimpLista = prioridadMailChimpLista.Id,
                                    Email = interaccionContacto.email_address,
                                    EmailMailChimpId = interaccionContacto.email_id,
                                    IdTipoInteraccion = tipoInteraccionClickEnlace,
                                    EsVip = interaccionContacto.vip,
                                    EstadoSuscripcion = interaccionContacto.contact_status,
                                    NroClicks = interaccionContacto.clicks,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = "SYSTEM",
                                    UsuarioModificacion = "SYSTEM",
                                    Estado = true
                                };

                                interaccionEnlaceDetalleMailChimp = new InteraccionEnlaceDetalleMailChimpBO()
                                {
                                    CantidadClicks = interaccionContacto.clicks,
                                    Fecha = DateTime.Now,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = "SYSTEM",
                                    UsuarioModificacion = "SYSTEM",
                                    Estado = true
                                };
                            }

                            if (interaccionEnlaceDetalleMailChimp != null)
                                interaccionEnlaceMailchimp.ListaInteraccionEnlaceDetalleMailChimp.Add(interaccionEnlaceDetalleMailChimp);
                            if (interaccionEnlaceMailchimp != null)
                                enlaceMailChimp.ListaInteraccionEnlaceMailChimp.Add(interaccionEnlaceMailchimp);
                        }
                    }
                }

                if (enlaceMailChimp != null)
                    listadoEnlaceMailChimp.Add(enlaceMailChimp);
            }

            if (listadoEnlaceMailChimp.ToList().Any())
                _repEnlaceMailChimp.Update(listadoEnlaceMailChimp.ToList());
        }

        /// TipoFuncion: GET
        /// Autor: Gian Miranda
        /// Fecha: 08/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Descarga el detalle por campania mailing
        /// </summary>
        /// <param name="FechaInicio">Fecha de inicio de descarga</param>
        /// <param name="FechaFin">Fecha de fin de descarga</param>
        /// <param name="IdCampaniaMailing">Id de la campania mailing (PK de la tabla mkt.T_CampaniaMailing)</param>
        /// <returns>ActionResult</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult DescargarDetalleCampania(DateTime FechaInicio, DateTime FechaFin, int? IdCampaniaMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<PrioridadMailChimpListaBO> listadoPrioridadMailChimpLista = new List<PrioridadMailChimpListaBO>();
                if (IdCampaniaMailing.HasValue)
                {
                    listadoPrioridadMailChimpLista = _repPrioridadMailChimpLista.ObtenerPorCampaniaMailing(IdCampaniaMailing.Value);
                }
                else
                {
                    FechaInicio = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
                    FechaFin = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 59);
                    var listadoCampanias = _repPrioridadMailChimpLista.ObtenerCampaniasPorFechaEnvio(FechaInicio, FechaFin);
                    listadoPrioridadMailChimpLista = listadoCampanias.DistinctBy(x => x.IdCampaniaMailchimp).ToList();
                }

                var listadoError = new List<string>();
                var listadoCorrecto = new List<string>();
                foreach (var item in listadoPrioridadMailChimpLista)
                {
                    try
                    {
                        var resultado = this.DescargarDetalleCampaniaMailChimp(item.IdListaMailchimp);
                        listadoCorrecto.Add(item.IdListaMailchimp);
                    }
                    catch (Exception e)
                    {
                        listadoError.Add(item.IdListaMailchimp);
                    }
                }

                //Notificacion de termino de descarga
                List<string> correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "jdiazp@bsginstitute.com"
                };
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                var subject = "";
                if (IdCampaniaMailing.HasValue)
                {
                    subject = string.Concat("Se termino de descargar detalle de campaña de la campaña con Id ", IdCampaniaMailing.ToString());
                }
                else
                {
                    subject = string.Concat("Se termino de descargar detalle de campaña desde el ", FechaInicio.ToString("yyyy/MM/dd"), " hasta el ", FechaFin.ToString("yyyy/MM/dd"));
                }
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = subject,
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(new { listadoError, listadoCorrecto })),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
                return Ok(new { listadoError, listadoCorrecto });
            }
            catch (Exception e)
            {
                List<string> correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "jdiazp@bsginstitute.com"
                };
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                var subject = "";
                if (IdCampaniaMailing.HasValue)
                {
                    subject = string.Concat("ERROR - Descargar detalle de campaña con Id ", IdCampaniaMailing.ToString());
                }
                else
                {
                    subject = string.Concat("ERROR - Descargar detalle de campaña desde el ", FechaInicio.ToString("yyyy/MM/dd"), " hasta el ", FechaFin.ToString("yyyy/MM/dd"));
                }
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = subject,
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(e)),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();

                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 26/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Descarga el detalle de la campania mailchimp
        /// </summary>
        /// <param name="IdListaMailchimp">Id de mailchimp unico de la lista</param>
        /// <param name="offset">Offset inicial del batch</param>
        /// <param name="count">Contador de iteracion para procesar</param>
        /// <returns>Response 200 con booleano, caso contrario response 400 con el mensaje de error</returns>
        [Route("[action]/{IdListaMailchimp}/{offset}/{count}")]
        [HttpGet]
        public ActionResult DescargarDetalleCampaniaMailChimp(string IdListaMailchimp, int offset = 0, int count = 500)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (string.IsNullOrEmpty(IdListaMailchimp))
                {
                    throw new Exception("Valor invalido para IdCampaniaMailChimp");
                }

                var cantidadRepeticiones = 1;
                bool esPrimeraVez = true;

                for (int i = 0; i < cantidadRepeticiones; i++)
                {
                    if (esPrimeraVez)
                    {
                        mailChimpListaDetalle = _interaccionMailChimpService.DescargaListaDetalllePorListaMailChimp(IdListaMailchimp, offset, count);

                        cantidadRepeticiones = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(mailChimpListaDetalle.total_items) / count));
                        esPrimeraVez = false;
                    }
                    else
                    {
                        mailChimpListaDetalle.members.AddRange(_interaccionMailChimpService.DescargaListaDetalllePorListaMailChimp(IdListaMailchimp, offset, count).members);
                    }
                    offset += count;
                }

                var prioridadMailchimpListaExistente = _repPrioridadMailChimpLista.GetBy(x => x.IdListaMailchimp == IdListaMailchimp);
                var prioridadMailchimpListaCorreoExistente = _repPrioridadMailChimpListaCorreo.PrioridadMailChimpListaCorreoCompletoPorListaMailChimpId(IdListaMailchimp);

                listaPrioridadMailChimpListaCorreoActualizar = new List<PrioridadMailChimpListaCorreoBO>();

                foreach (var item in mailChimpListaDetalle.members)
                {
                    if (string.IsNullOrEmpty(item.merge_fields.IDPMLC))
                    {
                        var prioridadMailchimpLista = prioridadMailchimpListaExistente.FirstOrDefault(x => x.IdListaMailchimp == item.list_id);
                        //Si no existe pasamos al siguiente, porque no sabes a quien relacionar el detalle
                        if (prioridadMailchimpLista == null)
                        {
                            continue;
                        }
                        var _prioridadMailChimpListaCorreo = prioridadMailchimpListaCorreoExistente.FirstOrDefault(x => x.Email1 == item.email_address);

                        //Si no existe prioridad mailchimp lista correo pasamos a siguiente porque no podemos relacionarlo
                        if (_prioridadMailChimpListaCorreo == null)
                        {
                            continue;
                        }
                        _prioridadMailChimpListaCorreo.EstadoSuscripcionMailChimp = item.status;
                        _prioridadMailChimpListaCorreo.ObjetoSerializado = JsonConvert.SerializeObject(item);
                        _prioridadMailChimpListaCorreo.UsuarioModificacion = "ServicioDescargaDetalle";
                        _prioridadMailChimpListaCorreo.FechaModificacion = DateTime.Now;
                        // Llenamos la prioridadMailChimpListaCorreo a actualizar
                        listaPrioridadMailChimpListaCorreoActualizar.Add(_prioridadMailChimpListaCorreo);
                    }
                    if (Int32.TryParse(item.merge_fields.IDPMLC, out int idPrioridadMailChimpListaCorreo))
                    {
                        if (!prioridadMailchimpListaCorreoExistente.Any(x => x.Id == idPrioridadMailChimpListaCorreo))
                        {
                            continue;
                        }
                        var _prioridadMailChimpListaCorreo = prioridadMailchimpListaCorreoExistente.FirstOrDefault(x => x.Id == idPrioridadMailChimpListaCorreo);

                        _prioridadMailChimpListaCorreo.EstadoSuscripcionMailChimp = item.status;
                        _prioridadMailChimpListaCorreo.ObjetoSerializado = JsonConvert.SerializeObject(item);
                        _prioridadMailChimpListaCorreo.UsuarioModificacion = "ServicioDescargaDetalle";
                        _prioridadMailChimpListaCorreo.FechaModificacion = DateTime.Now;

                        // Llenamos la prioridadMailChimpListaCorreo a actualizar
                        listaPrioridadMailChimpListaCorreoActualizar.Add(_prioridadMailChimpListaCorreo);
                    }
                }
                // Mandamos al repositorio a actualizar
                _repPrioridadMailChimpListaCorreo.Update(listaPrioridadMailChimpListaCorreoActualizar);

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}