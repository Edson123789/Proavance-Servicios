using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Planificacion/CampaniaGeneral
    /// Autor: Gian Miranda
    /// Fecha: 04/10/2021
    /// <summary>
    /// Configura y procesa los elementos de una campania mailing y WhatsApp(listas, filtro asociado, areas y prioridades)
    /// </summary>
    [Route("api/CampaniaGeneral")]
    public class CampaniaGeneralController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly CampaniaGeneralRepositorio _repCampaniaGeneral;
        private readonly CampaniaGeneralDetalleRepositorio _repCampaniaGeneralDetalle;
        private readonly FiltroSegmentoRepositorio _repFiltroSegmento;
        private readonly PrioridadMailChimpListaRepositorio _repPrioridadMailChimpLista;
        private readonly CampaniaMailingValorTipoRepositorio _repCampaniaMailingValorTipo;
        private readonly FiltroSegmentoValorTipoRepositorio _repFiltroSegmentoValorTipo;
        private readonly IntegraAspNetUsersRepositorio _repIntegraAspNetUsers;
        private readonly CategoriaOrigenRepositorio _repCategoriaOrigen;
        private readonly PespecificoRepositorio _repPEspecifico;
        private readonly WhatsAppMensajePublicidadRepositorio _repWhatsAppMensajePublicidad;
        private readonly HoraRepositorio _repHora;
        private readonly LogRepositorio _repLog;

        private CampaniaGeneralBO CampaniaGeneral;
        private CampaniaGeneralDetalleBO CampaniaGeneralDetalle;
        private FiltroSegmentoBO FiltroSegmento;

        public CampaniaGeneralController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repCampaniaGeneral = new CampaniaGeneralRepositorio(_integraDBContext);
            _repCampaniaGeneralDetalle = new CampaniaGeneralDetalleRepositorio(_integraDBContext);
            _repFiltroSegmento = new FiltroSegmentoRepositorio(_integraDBContext);
            _repPrioridadMailChimpLista = new PrioridadMailChimpListaRepositorio(_integraDBContext);
            _repCampaniaMailingValorTipo = new CampaniaMailingValorTipoRepositorio(_integraDBContext);
            _repFiltroSegmentoValorTipo = new FiltroSegmentoValorTipoRepositorio(_integraDBContext);
            _repWhatsAppMensajePublicidad = new WhatsAppMensajePublicidadRepositorio(_integraDBContext);
            _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
            _repCategoriaOrigen = new CategoriaOrigenRepositorio(_integraDBContext);
            _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
            _repHora = new HoraRepositorio(_integraDBContext);
            _repLog = new LogRepositorio(_integraDBContext);

            CampaniaGeneral = new CampaniaGeneralBO(_integraDBContext);
            FiltroSegmento = new FiltroSegmentoBO(_integraDBContext);
            CampaniaGeneralDetalle = new CampaniaGeneralDetalleBO(_integraDBContext);
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 09/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la grilla de las listas campanias generales del modulo
        /// </summary>
        /// <returns>Lista de DTO con todas las campanias generales(CampaniaGeneralDTO)</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCampaniaGeneralGrid()
        {
            try
            {
                return Ok(_repCampaniaGeneral.ObtenerListaCampaniaGeneral());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 16/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la grilla de las listas campanias generales para gestion
        /// </summary>
        /// <returns>Lista de DTO con todas las campanias generales(CampaniaGeneralDTO)</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerGestionCampaniasGrid()
        {
            try
            {
                return Ok(_repCampaniaGeneral.ObtenerListaCampaniaGeneralGestion());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 11/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las campanias general detalle asociadas a una campania general
        /// </summary>
        /// <param name="IdCampaniaGeneral">Id de la lista campania general detalle</param>
        /// <returns>Lista de DTO para todos los filtros</returns>
        [Route("[Action]/{IdCampaniaGeneral}")]
        [HttpGet]
        public ActionResult ObtenerListaCampaniaGeneralDetalle(int IdCampaniaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(CampaniaGeneral.ObtenerDetalle(IdCampaniaGeneral));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 20/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la cantidad de resultados de Mailing y WhatsApp por el id de la campania general detalle
        /// </summary>
        /// <returns>ActionResult con estado 200, 400 y cantidad de contactos resultantes</returns>
        [Route("[Action]/{IdCampaniaGeneralDetalle}")]
        [HttpGet]
        public ActionResult ObtenerResultadoPorIdCampaniaGeneralDetalle(int IdCampaniaGeneralDetalle)
        {
            try
            {
                if (IdCampaniaGeneralDetalle == 0)
                    return Ok(new { CantidadContactosMailing = 0, CantidadContactosWhatsApp = 0, EnEjecucion = false });

                var resultado = _repCampaniaGeneralDetalle.ObtenerResultadoPorIdCampaniaGeneralDetalle(IdCampaniaGeneralDetalle);

                return Ok(new { CantidadContactosMailing = resultado.CantidadContactosMailing, CantidadContactosWhatsApp = resultado.CantidadContactosWhatsApp, EnEjecucion = resultado.EnEjecucion });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 03/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Procesa los responsables Whatsapp de la prioridad Mailing General - Individual
        /// </summary>
        /// <param name="PreprocesamientoWhatsAppCampaniaGeneral">Objeto de tipo PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO</param>
        /// <returns>ActionResult con estado 200, 400 y cantidad de contactos resultantes</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult FinalizarPreProcesamientoWhatsApp([FromBody] PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO PreprocesamientoWhatsAppCampaniaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Hora de inicio
                var horaInicio = DateTime.Now;

                // Preparacion de ejecucion
                var campaniaGeneral = new CampaniaGeneralBO(_integraDBContext);
                var campaniaGeneralDetalle = new CampaniaGeneralDetalleBO(_integraDBContext);

                // Obtener CampaniaGeneralDetalle
                campaniaGeneralDetalle = _repCampaniaGeneralDetalle.BuscarCampaniaGeneralDetallePorId(PreprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle, _integraDBContext);

                if (!(campaniaGeneralDetalle != null && campaniaGeneralDetalle.Id > 0))
                {
                    return BadRequest("El detalle de la campania no existe");
                }

                if (campaniaGeneralDetalle.EnEjecucion)
                {
                    return BadRequest("La prioridad se encuentra en ejecucion");
                }

                // Actualizacion de estado de ejecucion para integra
                try
                {
                    _repCampaniaGeneralDetalle.ActualizarEstadoEjecucionCampaniaGeneralDetalle(PreprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle, true, PreprocesamientoWhatsAppCampaniaGeneral.Usuario);
                }
                catch (Exception ex)
                {
                    try
                    {
                        var mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                        _repLog.Insert(new TLog
                        {
                            Ip = "-",
                            Usuario = "CampaniaGeneral",
                            Maquina = "-",
                            Ruta = "CampaniaGeneral/FinalizarPreProcesamientoWhatsApp",
                            Parametros = $"CampaniaGeneralDetalle={campaniaGeneralDetalle.Id}",
                            Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                            Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                            Tipo = "UPDATE",
                            IdPadre = 0,
                            UsuarioCreacion = "CampaniaGeneral",
                            UsuarioModificacion = "CampaniaGeneral",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        });
                    }
                    catch (Exception)
                    {
                    }
                }

                campaniaGeneral = _repCampaniaGeneral.FirstBy(x => x.Id == campaniaGeneralDetalle.IdCampaniaGeneral);

                if (campaniaGeneral == null)
                {
                    return BadRequest("La campania no existe");
                }

                if (!campaniaGeneral.IdPlantillaWhatsapp.HasValue)
                {
                    return BadRequest("No tiene una plantilla asignada");
                }

                // Reemplazar por el personal real
                var resultadoActualizacionContactos = _repWhatsAppMensajePublicidad.ActualizarContactosConPrimerPreprocesamientoCampaniaGeneral(PreprocesamientoWhatsAppCampaniaGeneral);

                var pEspecificoObtenido = _repPEspecifico.FirstBy(x => x.IdCentroCosto == campaniaGeneralDetalle.IdCentroCosto);
                var listaPrimerProcesado = _repWhatsAppMensajePublicidad.ObtenerListaWhatsAppPrimerProcesadoCampaniaGeneral(PreprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle);

                bool resultadoInsercion = true;

                if (listaPrimerProcesado.Any())
                {
                    if (pEspecificoObtenido == null)
                        return BadRequest("Ocurrio un error al intentar enlazar el programa general");

                    WhatsAppEnvioAutomaticoController whatsAppConfiguracionEnvio = new WhatsAppEnvioAutomaticoController(_integraDBContext);
                    listaPrimerProcesado = whatsAppConfiguracionEnvio.ReemplazarEtiquetaCampaniaGeneral(listaPrimerProcesado, campaniaGeneral.IdPlantillaWhatsapp.Value, pEspecificoObtenido.IdProgramaGeneral.GetValueOrDefault(), campaniaGeneralDetalle.Id);

                    resultadoInsercion = whatsAppConfiguracionEnvio.RegistraPreValidacionCampaniaGeneral(listaPrimerProcesado, pEspecificoObtenido.IdProgramaGeneral.GetValueOrDefault(), campaniaGeneral.IdPlantillaWhatsapp.Value);

                    // Hora de fin del procesamiento
                    DateTime horaFin = DateTime.Now;

                    string usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(PreprocesamientoWhatsAppCampaniaGeneral.Usuario);

                    if (_repIntegraAspNetUsers.ExistePorNombreUsuario(PreprocesamientoWhatsAppCampaniaGeneral.Usuario))
                    {
                        try
                        {
                            _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(PreprocesamientoWhatsAppCampaniaGeneral.Usuario);
                        }
                        catch (Exception)
                        {
                        }
                    }

                    List<string> copiaCorreos = new List<string>
                    {
                        "gmiranda@bsginstitute.com"
                    };
                    TMK_MailServiceImpl mailservicePersonalizado = new TMK_MailServiceImpl();
                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = "gmiranda@bsginstitute.com",
                        Recipient = usuarioResponsable,
                        Subject = string.Concat("Preparar WhatsApp prioridades Mailing - Correcto ", campaniaGeneral.Nombre),
                        Message = campaniaGeneralDetalle.GenerarPlantillaNotificacionFinalizacionWhatsapp(campaniaGeneral.Nombre, campaniaGeneralDetalle.Nombre, listaPrimerProcesado.Count(), horaInicio, horaFin),
                        Cc = string.Empty,
                        Bcc = string.Join(",", copiaCorreos),
                        AttachedFiles = null
                    };
                    mailservicePersonalizado.SetData(mailDataPersonalizado);
                    mailservicePersonalizado.SendMessageTask();
                }

                try
                {
                    _repCampaniaGeneralDetalle.ActualizarEstadoEjecucionCampaniaGeneralDetalle(PreprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle, false, PreprocesamientoWhatsAppCampaniaGeneral.Usuario);
                }
                catch (Exception)
                {
                }

                return Ok(resultadoInsercion);
            }
            catch (Exception ex)
            {
                var campaniaGeneralDetalle = _repCampaniaGeneralDetalle.BuscarCampaniaGeneralDetallePorId(PreprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle, _integraDBContext);

                _repCampaniaGeneralDetalle.ActualizarEstadoEjecucionCampaniaGeneralDetalle(PreprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle, false, PreprocesamientoWhatsAppCampaniaGeneral.Usuario);

                List<string> copiaCorreos = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };

                string usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(PreprocesamientoWhatsAppCampaniaGeneral.Usuario);

                if (_repIntegraAspNetUsers.ExistePorNombreUsuario(PreprocesamientoWhatsAppCampaniaGeneral.Usuario))
                {
                    try
                    {
                        _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(PreprocesamientoWhatsAppCampaniaGeneral.Usuario);
                    }
                    catch (Exception)
                    {
                    }
                }

                TMK_MailServiceImpl mailservice = new TMK_MailServiceImpl();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = string.Concat("Finalizacion de Procesamiento WhatsApp Campania General - Error ", campaniaGeneralDetalle.Nombre),
                    Message = string.Concat("Mensaje: ", JsonConvert.SerializeObject(ex)),
                    Cc = string.Empty,
                    Bcc = string.Join(",", copiaCorreos),
                    AttachedFiles = null
                };
                mailservice.SetData(mailData);
                mailservice.SendMessageTask();

                return BadRequest(new
                {
                    Resultado = "ERROR",
                    ex.Message
                });
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el estado de ejecucion de los detalles de la campania general
        /// </summary>
        /// <param name="IdCampaniaGeneral">Id de la lista campania general detalle</param>
        /// <returns>Lista de DTO para todos los filtros</returns>
        [Route("[Action]/{IdCampaniaGeneral}")]
        [HttpGet]
        public ActionResult ObtenerEstadoEjecucionCampaniaGeneralDetalle(int IdCampaniaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(CampaniaGeneral.ObtenerEstadoEjecucion(IdCampaniaGeneral));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 24/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de responsables de la campania general detalle
        /// </summary>
        /// <param name="IdCampaniaGeneralDetalle">Id de la lista campania general detalle</param>
        /// <returns>Lista de DTO para todos los filtros</returns>
        [Route("[Action]/{IdCampaniaGeneralDetalle}")]
        [HttpGet]
        public ActionResult ObtenerListaResponsablesCampaniaGeneralDetalle(int IdCampaniaGeneralDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(CampaniaGeneral.ObtenerDetalleResponsables(IdCampaniaGeneralDetalle));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin
        /// Fecha: 10/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta una campania general
        /// </summary>
        /// <param name="CampaniaGeneralDTO">DTO con la informacion necesaria de la campania mailing</param>
        /// <returns>Mensaje de confirmacion y el id de la campania general que se inserto</returns>
        [Route("[Action]/")]
        [HttpPost]
        public ActionResult InsertarCampaniaGeneral([FromBody] CampaniaGeneralDTO CampaniaGeneralDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CampaniaGeneral = new CampaniaGeneralBO(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    CampaniaGeneral.InsertarOActualizarCampaniaGeneral(CampaniaGeneralDTO);
                    if (CampaniaGeneral.HasErrors)
                    {
                        return BadRequest(CampaniaGeneral.GetErrors(null));
                    }
                    _repCampaniaGeneral.Insert(CampaniaGeneral);
                    scope.Complete();
                    return Ok(new { Resultado = "Se Inserto Correctamente", CampaniaGeneral.Id });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Resultado = "ERROR", e.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin
        /// Fecha: 11/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza una campania general especifica
        /// </summary>
        /// <param name="CampaniaGeneralDTO">DTO de la campania mailing a actualizar</param>
        /// <returns>Mensaje de confirmacion y el id de la campania mailing que se inserto</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarCampaniaGeneral([FromBody] CampaniaGeneralDTO CampaniaGeneralDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CampaniaGeneral = new CampaniaGeneralBO(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    CampaniaGeneral.InsertarOActualizarCampaniaGeneral(CampaniaGeneralDTO);
                    if (CampaniaGeneral.HasErrors)
                    {
                        return BadRequest(CampaniaGeneral.GetErrors(null));
                    }
                    _repCampaniaGeneral.Update(CampaniaGeneral);
                    scope.Complete();
                    return Ok(new { Resultado = "Se Actualizo Correctamente", CampaniaGeneralDTO.Id });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Resultado = "ERROR", e.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 14/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina una campania general especificada, guardando el usuario que efectuo tal accion
        /// </summary>
        /// <param name="IdCampaniaGeneral">Id de la Campania Mailing a eliminar</param>
        /// <param name="Usuario">Usuario que ejecuta el eliminado</param>
        /// <returns>DTO con informacion basica de filtros segmento</returns>
        [Route("[Action]/{IdCampaniaGeneral}/{Usuario}")]
        [HttpGet]
        public ActionResult EliminarCampaniaGeneral(int IdCampaniaGeneral, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _repCampaniaGeneral.Delete(IdCampaniaGeneral, Usuario);
                return Ok(new { Resultado = "Se elimino correctamente", Id = IdCampaniaGeneral });
            }
            catch (Exception e)
            {
                return BadRequest(new { Resultado = e.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin
        /// Fecha: 14/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la gestion de campanias
        /// </summary>
        /// <param name="GestionCampaniaGeneralValorDTO">Objeto de clase GestionCampaniaGeneralValorDTO</param>
        /// <returns>Response 200 con el Id de la campania general, caso contrario response 400 con el error</returns>
        [Route("[Action]/")]
        [HttpPost]
        public ActionResult ActualizarGestionCampanias([FromBody] GestionCampaniaGeneralValorDTO GestionCampaniaGeneralValorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CampaniaGeneral = new CampaniaGeneralBO(_integraDBContext);
                if (GestionCampaniaGeneralValorDTO != null && GestionCampaniaGeneralValorDTO.Id != 0)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        CampaniaGeneral = _repCampaniaGeneral.FirstById(GestionCampaniaGeneralValorDTO.Id);
                        CampaniaGeneral.IncluirRebotes = GestionCampaniaGeneralValorDTO.IncluirRebotes;
                        CampaniaGeneral.UsuarioModificacion = GestionCampaniaGeneralValorDTO.UsuarioModificacion;
                        CampaniaGeneral.FechaModificacion = DateTime.Now;

                        _repCampaniaGeneral.Update(CampaniaGeneral);
                        scope.Complete();
                        return Ok(new { Resultado = "Se Actualizo Correctamente", GestionCampaniaGeneralValorDTO.Id });
                    }
                }
                else
                {
                    throw new Exception("No existe registro!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Resultado = "ERROR", e.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 10/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Procesa prioridad Mailing General - Individual
        /// </summary>
        /// <param name="PrioridadMailing">Objeto de tipo PrioridadMailingDTO</param>
        /// <returns>ActionResult con estado 200, 400 y cantidad de contactos resultantes</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarPrioridadCampaniaGeneral([FromBody] PrioridadMailingEjecucionDTO PrioridadMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DateTime horaInicio = DateTime.Now;

                // Preparacion de ejecucion
                var campaniaGeneralDetalle = new CampaniaGeneralDetalleBO(_integraDBContext);
                var listaCampaniaCorrecta = new List<PrioridadMailChimpListaBO>();
                var listaCampaniaError = new List<ErrorPrioridadMailChimpListaBO>();

                campaniaGeneralDetalle = _repCampaniaGeneralDetalle.BuscarCampaniaGeneralDetallePorId(PrioridadMailing.IdCampaniaGeneralDetalle, _integraDBContext);

                if (campaniaGeneralDetalle == null)
                    return BadRequest("La campaña no existe");

                if (campaniaGeneralDetalle.EnEjecucion)
                    return BadRequest("La prioridad se encuentra en ejecucion");

                #region Actualizacion Estado ejecucion CampaniaGeneralDetalle
                try
                {
                    _repCampaniaGeneralDetalle.ActualizarEstadoEjecucionCampaniaGeneralDetalle(PrioridadMailing.IdCampaniaGeneralDetalle, true, PrioridadMailing.Usuario);
                }
                catch (Exception e)
                {
                    return BadRequest($"Ha ocurrido un error al intentar iniciar el calculo de la prioridad - {e.Message}");
                }
                #endregion

                // Flag de ejecucion correcta
                bool filtroEjecutadoCorrectamente = false;

                var cantidadMailing = new List<CampaniaGeneralDetalleConCantidadDTO>();

                // Filtros de Mailing General
                CampaniaGeneral = _repCampaniaGeneral.ObtenerCampaniaGeneral(campaniaGeneralDetalle.IdCampaniaGeneral);

                #region Eliminado PrevalidacionWhatsApp
                try
                {
                    bool resultadoEliminacionPrevalidacion = _repCampaniaGeneralDetalle.EliminarValidacionWhatsAppIdCampaniaGeneralDetalle(campaniaGeneralDetalle.Id, PrioridadMailing.Usuario);
                }
                catch (Exception e)
                {
                    return BadRequest($"No se ha podido eliminar los numeros validados de WhatsApp anteriores - {e.Message}");
                }
                #endregion
                #region Eliminado Preproceso Mailing
                try
                {
                    bool resultadoEliminacionMailing = _repPrioridadMailChimpLista.EliminarContactosPorIdCampaniaGeneralDetalle(campaniaGeneralDetalle.Id, PrioridadMailing.Usuario);
                }
                catch (Exception e)
                {
                    return BadRequest($"No se ha podido eliminar los contactos de la campania general detalle - {e.Message}");
                }
                #endregion

                var plantillas = campaniaGeneralDetalle.ProcesarPrioridadMailchimp(campaniaGeneralDetalle.Id);
                var listaEtiquetas = campaniaGeneralDetalle.ObtenerEtiquetas(plantillas.Contenido);

                try
                {
                    _repPrioridadMailChimpLista.EliminarListaMailchimpSinEnviarPorIdCampaniaGeneralDetalle(PrioridadMailing.IdCampaniaGeneralDetalle, PrioridadMailing.Usuario);
                }
                catch (Exception e)
                {
                    return BadRequest($"No se ha podido eliminar las listas calculadas anteriormente - {e.Message}");
                }

                // Insercion por SP para replica
                PrioridadMailChimpListaInsercionDTO prioridadMailChimpListaInsercion = new PrioridadMailChimpListaInsercionDTO()
                {
                    IdCampaniaMailing = ValorEstatico.IdMailingGeneralDefecto,
                    IdCampaniaGeneralDetalle = plantillas.IdCampaniaGeneralDetalle,
                    Asunto = plantillas.Asunto + plantillas.Subject,
                    Contenido = plantillas.Contenido,
                    AsuntoLista = plantillas.NombreCampania,
                    IdPersonal = plantillas.IdPersonal,
                    NombreAsesor = plantillas.NombreCompletoPersonal,
                    Alias = plantillas.CorreoElectronico,
                    Etiquetas = string.Join<string>(",", listaEtiquetas),
                    Enviado = false,
                    UsuarioCreacion = PrioridadMailing.Usuario,
                    UsuarioModificacion = PrioridadMailing.Usuario
                };

                prioridadMailChimpListaInsercion.Id = _repPrioridadMailChimpLista.InsertarPrioridadMailChimpListaFiltroCampaniaGeneral(prioridadMailChimpListaInsercion);

                var nuevoPrioridadMailChimpLista = new PrioridadMailChimpListaBO
                {
                    Id = prioridadMailChimpListaInsercion.Id,
                    IdCampaniaMailing = ValorEstatico.IdMailingGeneralDefecto,
                    IdCampaniaGeneralDetalle = plantillas.IdCampaniaGeneralDetalle,
                    Asunto = plantillas.Asunto + plantillas.Subject,
                    Contenido = plantillas.Contenido,
                    AsuntoLista = plantillas.NombreCampania,
                    IdPersonal = plantillas.IdPersonal,
                    NombreAsesor = plantillas.NombreCompletoPersonal,
                    Alias = plantillas.CorreoElectronico,
                    Etiquetas = string.Join<string>(",", listaEtiquetas),
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = PrioridadMailing.Usuario,
                    UsuarioModificacion = PrioridadMailing.Usuario
                };

                int contactosInsertados = 0;

                ValorTipoDTO valorTipo = _repFiltroSegmentoValorTipo.ObtenerFiltrosCampaniaMailchimp(CampaniaGeneral.IdFiltroSegmento.GetValueOrDefault());
                valorTipo.IdFiltroSegmento = CampaniaGeneral.IdFiltroSegmento.GetValueOrDefault();

                var filtroSegmento = _repFiltroSegmento.FirstById(CampaniaGeneral.IdFiltroSegmento.GetValueOrDefault());
                valorTipo.FechaInicioOportunidad = null;

                var listasConfiguradas = CampaniaGeneralDetalle.ObtenerListasConfiguracionFiltro(plantillas.IdCampaniaGeneralDetalle, CampaniaGeneral.IdCategoriaObjetoFiltro);

                filtroSegmento = new FiltroSegmentoBO(_integraDBContext)
                {
                    Id = CampaniaGeneral.IdFiltroSegmento.GetValueOrDefault()
                };

                while (!filtroEjecutadoCorrectamente)
                {
                    try
                    {
                        contactosInsertados = filtroSegmento.EjecutarFiltroMailingGeneral(listasConfiguradas.ListaAreas, listasConfiguradas.ListaSubAreas, listasConfiguradas.ListaProgramas, nuevoPrioridadMailChimpLista.Id, plantillas.IdCampaniaGeneral, CampaniaGeneral.IdProbabilidadRegistroPw, CampaniaGeneral.CantidadPeriodoSinCorreo, CampaniaGeneral.IdTiempoFrecuencia, CampaniaGeneral.IdTipoAsociacion.GetValueOrDefault(), CampaniaGeneral.NroMaximoSegmentos.GetValueOrDefault(), CampaniaGeneral.IdCategoriaObjetoFiltro.GetValueOrDefault());
                        listaCampaniaCorrecta.Add(nuevoPrioridadMailChimpLista);
                        filtroEjecutadoCorrectamente = true;
                    }
                    catch (Exception e)
                    {
                        listaCampaniaError.Add(new ErrorPrioridadMailChimpListaBO() { PrioridadMailChimpLista = nuevoPrioridadMailChimpLista, Exception = e });
                        filtroEjecutadoCorrectamente = false;
                    }
                }

                // Datos para actualizacion por SP, direccion a replica
                CampaniaGeneralDetalleActualizacionDTO campaniaGeneralDetalleActualizacion = new CampaniaGeneralDetalleActualizacionDTO()
                {
                    CantidadContactosMailing = contactosInsertados,
                    CantidadContactosWhatsapp = 0,
                    UsuarioModificacion = PrioridadMailing.Usuario,
                    Id = campaniaGeneralDetalle.Id
                };

                bool resultadoActualizacionFiltro = _repCampaniaGeneralDetalle.ActualizarDatosFiltroMailchimp(campaniaGeneralDetalleActualizacion);

                cantidadMailing.Add(new CampaniaGeneralDetalleConCantidadDTO
                {
                    Id = campaniaGeneralDetalle.Id,
                    Cantidad = contactosInsertados
                });

                #region Procesar lista CampaniaGeneral
                int cantidadContactosWhatsApp = 0;

                if (CampaniaGeneral.IncluyeWhatsapp.Value && (campaniaGeneralDetalle.NoIncluyeWhatsaap == null || !campaniaGeneralDetalle.NoIncluyeWhatsaap.Value))
                {
                    WhatsAppEnvioAutomaticoController whatsAppEnvioAutomaticoController = new WhatsAppEnvioAutomaticoController(_integraDBContext);

                    whatsAppEnvioAutomaticoController.ProcesarListaWhatsAppCampaniaGeneral(campaniaGeneralDetalle.Id);

                    cantidadContactosWhatsApp = _repCampaniaGeneralDetalle.ObtenerCantidadContactosWhatsAppCampaniaGeneral(campaniaGeneralDetalle.Id);
                }
                #endregion

                try
                {
                    _repCampaniaGeneralDetalle.ActualizarEstadoEjecucionCampaniaGeneralDetalle(PrioridadMailing.IdCampaniaGeneralDetalle, false, PrioridadMailing.Usuario);
                }
                catch (Exception)
                {
                }

                // Creacion de Url Formulario
                var resultadoCreacionFormulario = _repCampaniaGeneral.CrearUrlFormularioPrioridad(campaniaGeneralDetalle.Id, PrioridadMailing.Usuario);

                // Obtencion Url Formulario
                var urlFormularioCreado = _repCampaniaGeneral.ObtenerUrlFormularioPrioridad(campaniaGeneralDetalle.Id);

                DateTime horaFin = DateTime.Now;

                // Enviar correo
                var resultado = new
                {
                    listaCampaniaError,
                    listaCampaniaCorrecta
                };

                // Enviar mensaje sistemas
                List<string> correos = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };

                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Procesar oportunidades - Correcto ", CampaniaGeneral.Nombre),
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(resultado)),
                    Cc = string.Empty,
                    Bcc = string.Empty,
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();

                string usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(PrioridadMailing.Usuario);

                if (_repIntegraAspNetUsers.ExistePorNombreUsuario(PrioridadMailing.Usuario))
                {
                    try
                    {
                        _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(PrioridadMailing.Usuario);
                    }
                    catch (Exception)
                    {
                    }
                }

                // Mensaje a usuario final
                var mensajeFinal = new List<MensajeProcesarDTO>();
                var correcto = new MensajeProcesarDTO()
                {
                    Nombre = "CORRECTO",
                    ListaDetalle =
                       listaCampaniaCorrecta.GroupBy(x => x.AsuntoLista).Select(X => new MensajeProcesarDetalleDTO
                       {
                           NombreCampania = CampaniaGeneral.Nombre,
                           NombreLista = X.Key,
                           NroIntentos = X.ToList().Count
                       }).ToList()
                };

                var error = new MensajeProcesarDTO()
                {
                    Nombre = "ERROR",
                    ListaDetalle =
                            listaCampaniaError.Select(x => x.PrioridadMailChimpLista).GroupBy(x => x.AsuntoLista).Select(X => new MensajeProcesarDetalleDTO
                            {
                                NombreCampania = CampaniaGeneral.Nombre,
                                NombreLista = X.Key,
                                NroIntentos = X.ToList().Count
                            }).ToList()
                };

                mensajeFinal.Add(error);
                mensajeFinal.Add(correcto);

                List<string> copiaCorreos = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };
                TMK_MailServiceImpl MailservicePersonalizado = new TMK_MailServiceImpl();
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = string.Concat("Procesar prioridades Mailing - Correcto ", CampaniaGeneral.Nombre),
                    Message = campaniaGeneralDetalle.GenerarNuevaPlantillaNotificacionProcesamientoMailingGeneral(mensajeFinal, cantidadMailing[0].Cantidad, cantidadContactosWhatsApp, urlFormularioCreado, horaInicio, horaFin),
                    Cc = string.Empty,
                    Bcc = string.Join(",", copiaCorreos),
                    AttachedFiles = null
                };
                MailservicePersonalizado.SetData(mailDataPersonalizado);
                MailservicePersonalizado.SendMessageTask();

                return Ok(new { Result = "OK", CantidadContactosMailing = cantidadMailing[0].Cantidad, CantidadContactosWhatsApp = cantidadContactosWhatsApp });
            }
            catch (Exception e)
            {
                CampaniaGeneralDetalle = _repCampaniaGeneralDetalle.BuscarCampaniaGeneralDetallePorId(PrioridadMailing.IdCampaniaGeneralDetalle, _integraDBContext);

                _repCampaniaGeneralDetalle.ActualizarEstadoEjecucionCampaniaGeneralDetalle(PrioridadMailing.IdCampaniaGeneralDetalle, false, PrioridadMailing.Usuario);

                List<string> copiaCorreos = new List<string>
                {
                    "gmiranda@bsginstitute.com"
                };

                string usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(PrioridadMailing.Usuario);

                if (_repIntegraAspNetUsers.ExistePorNombreUsuario(PrioridadMailing.Usuario))
                {
                    try
                    {
                        _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(PrioridadMailing.Usuario);
                    }
                    catch (Exception)
                    {
                    }
                }

                TMK_MailServiceImpl mailservice = new TMK_MailServiceImpl();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = string.Concat("Procesamiento Campania General - Error ", CampaniaGeneralDetalle.Nombre),
                    Message = string.Concat("Mensaje: ", JsonConvert.SerializeObject(e)),
                    Cc = string.Empty,
                    Bcc = string.Join(",", copiaCorreos),
                    AttachedFiles = null
                };
                mailservice.SetData(mailData);
                mailservice.SendMessageTask();
                return BadRequest(new
                {
                    Resultado = "ERROR",
                    e.Message
                });
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de categoria origen
        /// </summary>
        /// <returns>Lista de objetos de clase CategoriaOrigenFiltroDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCategoriaOrigen()
        {
            try
            {
                return Ok(_repCategoriaOrigen.ObtenerListaCategoriaOrigen());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 10/01/2022
        /// Versión: 1.0
        /// <summary>
        /// Verifica si el servidor se encuentra activo
        /// </summary>
        /// <returns>Boolean</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult VerificarEstadoServidor()
        {
            try
            {
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de plantilla de Whatsapp
        /// </summary>
        /// <returns>Lista de objetos de clase PlantillaDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPlantillaWhatsapp()
        {
            try
            {
                var _repPlantilla = new PlantillaRepositorio();

                return Ok(_repPlantilla.ObtenerListaPlantillasWhatsAppGeneral());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de probabilidades
        /// </summary>
        /// <returns>Lista de objetos de clase ProbabilidadRegistroPwFiltroDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTiempoFrecuencia()
        {
            try
            {
                var _repTiempoFrecuencia = new TiempoFrecuenciaRepositorio(_integraDBContext);
                var listaTiempoFrecuencia = _repTiempoFrecuencia.ObtenerListaTiempoFrecuencia();
                var listaTiempoFrecuenciaValida = new List<int>() { 2, 3, 4 };
                listaTiempoFrecuencia = listaTiempoFrecuencia.Where(x => listaTiempoFrecuenciaValida.Contains(x.Id)).ToList();
                return Ok(listaTiempoFrecuencia);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de todas las horas registradas en mkt.T_Hora
        /// </summary>
        /// <returns>Lista de DTO de objetos HoraDTO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaHora()
        {
            try
            {
                return Ok(_repHora.ObtenerListaHora());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 10/01/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el estado de recuperacion de la campania general
        /// </summary>
        /// <returns>Obtiene el estado de recuperacion de la campania general</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerEstadoRecuperacionCampaniaGeneral()
        {
            try
            {
                return Ok(_repCampaniaGeneral.ObtenerEstadoRecuperacionWhatsApp(ValorEstatico.IdModuloSistemaWhatsAppMailing));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 27/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de todas las horas registradas en mkt.T_Hora
        /// </summary>
        /// <returns>Lista de DTO con todas las horas</returns>
        [Route("[Action]/{Intervalo}")]
        [HttpGet]
        public ActionResult ObtenerListaHoraIntervalo(int Intervalo)
        {
            try
            {
                return Ok(_repHora.ObtenerListaHoraIntervalo(Intervalo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 06/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la cantidad de prioridades aptas para el procesamiento
        /// </summary>
        /// <param name="IdCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>Response 200 con la cantidad de prioridades a ejecutar, caso contrario response 400</returns>
        [Route("[Action]/{IdCampaniaGeneral}")]
        [HttpGet]
        public ActionResult ObtenerCantidadConjuntoPrioridadAptoEjecucion(int IdCampaniaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaCampaniaGeneralDetalle = _repCampaniaGeneralDetalle.GetBy(x => x.IdCampaniaGeneral == IdCampaniaGeneral && x.EnEjecucion == false).OrderBy(o => o.Prioridad).Select(s => new { IdCampaniaGeneralDetalle = s.Id, NombreCampaniaGeneralDetalle = s.Nombre, Prioridad = s.Prioridad }).ToList();

                return Ok(listaCampaniaGeneralDetalle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 06/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta todas las prioridades disponibles en una campania general
        /// </summary>
        /// <param name="PrioridadMailing">Objeto de clase PrioridadCampaniaGeneralConjuntoEjecucionDTO</param>
        /// <returns>Response 200 con la cantidad de prioridades a ejecutar, caso contrario response 400</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarPrioridadConjunto([FromBody] PrioridadCampaniaGeneralConjuntoEjecucionDTO PrioridadMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaCampaniaGeneralDetalle = _repCampaniaGeneralDetalle.ObtenerDetallePorCampaniaGeneralNoEjecucion(PrioridadMailing.IdCampaniaGeneral).OrderBy(o => o.Prioridad).Select(s => s.Id).ToList();

                foreach (var campaniaGeneralDetalle in listaCampaniaGeneralDetalle)
                {
                    var prioridadActualAEjecutar = new PrioridadMailingEjecucionDTO()
                    {
                        IdCampaniaGeneralDetalle = campaniaGeneralDetalle,
                        Usuario = PrioridadMailing.Usuario
                    };

                    ProcesarPrioridadCampaniaGeneral(prioridadActualAEjecutar);
                }

                return Ok(listaCampaniaGeneralDetalle.Count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private class ErrorPrioridadMailChimpListaBO
        {
            public PrioridadMailChimpListaBO PrioridadMailChimpLista { get; set; }
            public Exception Exception { get; set; }
        }
    }
}
