using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BSI.Integra.Aplicacion.Servicios.BO;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using MailBee.Mime;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using System.Transactions;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Transversal.Helper;
using System.Text;
using BSI.Integra.Persistencia.Models.AulaVirtual;
using BSI.Integra.Aplicacion.Servicios.SCode.DTOs;
using System.Net;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CorreoController
    /// Autor: , Jashin Salazar
    /// Fecha: 15/04/2021
    /// <summary>
    /// (O)Agenda
    /// </summary>
    [Route("api/Correo")]
    public class CorreoController : Controller
    {

        private readonly integraDBContext _integraDBContext;

        public CorreoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene correos enviados por asesor
        /// </summary>
        /// <returns> objetoDTO: BandejaCorreoDTO</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCorreosEnviadosPorAsesor([FromBody] FiltroBandejaCorreoDTO FiltroBandejaCorreoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                FiltroBandejaCorreoBO filtroBandejaCorreo = new FiltroBandejaCorreoBO();
                filtroBandejaCorreo.Page = FiltroBandejaCorreoDTO.Page;
                filtroBandejaCorreo.PageSize = FiltroBandejaCorreoDTO.PageSize;
                filtroBandejaCorreo.Skip = FiltroBandejaCorreoDTO.Skip;
                filtroBandejaCorreo.Take = FiltroBandejaCorreoDTO.Take;
                filtroBandejaCorreo.IdAsesor = FiltroBandejaCorreoDTO.IdAsesor;
                filtroBandejaCorreo.FiltroKendo = FiltroBandejaCorreoDTO.FiltroKendo;
                filtroBandejaCorreo.TipoCorreos = FiltroBandejaCorreoDTO.TipoCorreos;

                if (filtroBandejaCorreo.HasErrors)
                {
                    return BadRequest(filtroBandejaCorreo.GetErrors(null));
                }
                GmailCorreoRepositorio _gmailCorreoRepositorio = new GmailCorreoRepositorio();
                return Ok(_gmailCorreoRepositorio.ObtenerCorreosEnviados(filtroBandejaCorreo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene correos enviados por ID.
        /// </summary>
        /// <returns> objetoDTO: CorreoBodyDTO </returns>
        [Route("[Action]/{IdGmailCorreo}/{Usuario}")]
        [HttpGet]
        public ActionResult ObtenerCorreoEnviadoPorId(int IdGmailCorreo, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdGmailCorreo <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(200));
            }
            try
            {
                var _repGmailCorreo = new GmailCorreoRepositorio();
                var _repGmailCorreoArchivoAdjunto = new GmailCorreoArchivoAdjuntoRepositorio();
                var gmailCorreoBO = new GmailCorreoBO(IdGmailCorreo)
                {
                    Seen = true,
                    FechaModificacion = DateTime.Now,
                    UsuarioModificacion = Usuario
                };

                if (!gmailCorreoBO.HasErrors)
                {
                    _repGmailCorreo.Update(gmailCorreoBO);
                }
                else
                {
                    return BadRequest(gmailCorreoBO.GetErrors(null));
                }
                var correo = new CorreoBodyDTO()
                {
                    EmailBody = gmailCorreoBO.EmailBody
                };
                correo.ArchivosAdjuntos = _repGmailCorreoArchivoAdjunto.GetBy(x => x.IdGmailCorreo == gmailCorreoBO.Id).
                    Select(x => new CorreoArchivoAdjuntoDTO()
                    {
                        IdCorreo = x.Id,
                        NombreArchivo = x.Nombre,
                        UrlArchivoRepositorio = x.UrlArchivoRepositorio
                    }).ToList();
                //var gmailCorreo = gmailCorreoRepositorio.ObtenerCuerpoCorreoEnviado(IdGmailCorreo)
                return Ok(correo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene correo enviado por asesor.
        /// </summary>
        /// <returns> objetoDTO: CorreoBodyDTO </returns>
        [Route("[Action]/{IdPersonal}/{Destinatario}/{Asunto}")]
        [HttpGet]
        public ActionResult ObtenerCorreoEnviadoPorAsesor(int IdPersonal, string Destinatario, string Asunto)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            if (IdPersonal == 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(200));
            }
            try
            {
                GmailCorreoRepositorio gmailCorreoRepositorio = new GmailCorreoRepositorio();
                //GmailCorreoBO gmailCorreoBO = new GmailCorreoBO(IdGmailCorreo);

                var gmailCorreoBO = gmailCorreoRepositorio.GetBy(w => w.IdPersonal == IdPersonal && w.Destinatarios.Contains(Destinatario) && w.Asunto == Asunto).OrderByDescending(w => w.FechaCreacion).FirstOrDefault();


                return Ok(gmailCorreoRepositorio.ObtenerCuerpoCorreoEnviado(gmailCorreoBO.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene correos recibidos.
        /// </summary>
        /// <returns> objetoDTO: BandejaCorreoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GetCorreoRecibido([FromBody] FiltroBandejaCorreoDTO FiltroBandejaCorreoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FiltroBandejaCorreoBO filtroBandejaCorreo = new FiltroBandejaCorreoBO();
                filtroBandejaCorreo.Page = FiltroBandejaCorreoDTO.Page;
                filtroBandejaCorreo.PageSize = FiltroBandejaCorreoDTO.PageSize;
                filtroBandejaCorreo.Skip = FiltroBandejaCorreoDTO.Skip;
                filtroBandejaCorreo.Take = FiltroBandejaCorreoDTO.Take;
                filtroBandejaCorreo.IdAsesor = FiltroBandejaCorreoDTO.IdAsesor;
                filtroBandejaCorreo.IdAsesor = FiltroBandejaCorreoDTO.IdAsesor;
                filtroBandejaCorreo.Folder = FiltroBandejaCorreoDTO.Folder;
                filtroBandejaCorreo.FiltroKendo = FiltroBandejaCorreoDTO.FiltroKendo;

                if (filtroBandejaCorreo.HasErrors)
                {
                    return BadRequest(filtroBandejaCorreo.GetErrors(null));
                }

                GmailClienteRepositorio _gmailClienteRepositorio = new GmailClienteRepositorio();
                CorreoClienteCredencialDTO correoClienteCredencialDTO = _gmailClienteRepositorio.GetClienteCredencial(filtroBandejaCorreo.IdAsesor);
                BandejaCorreoDTO lista = new BandejaCorreoDTO();
                if (correoClienteCredencialDTO != null)
                {

                    lista = filtroBandejaCorreo.GetBandejaEntradaMailInbox(correoClienteCredencialDTO.EmailAsesor, correoClienteCredencialDTO.PasswordCorreo, _integraDBContext);
                    return Ok(lista);
                }
                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene informacion de Gmail.
        /// </summary>
        /// <returns> objetoDTO: CorreoBodyDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult GetInformacionGmail(int IdCorreo, int IdAsesor, string Folder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdAsesor <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(201));
            }
            try
            {
                GmailClienteRepositorio _gmailClienteRepositorio = new GmailClienteRepositorio();
                CorreoClienteCredencialDTO correoClienteCredencialDTO = _gmailClienteRepositorio.GetClienteCredencial(IdAsesor);

                TMK_ImapServiceImpl _imapService = new TMK_ImapServiceImpl();
                MailMessage mensaje = _imapService.GetBodyCorreo(IdCorreo, correoClienteCredencialDTO.EmailAsesor, correoClienteCredencialDTO.PasswordCorreo, Folder);
                mensaje.Parser.PlainToHtmlMode = PlainToHtmlAutoConvert.IfNoHtml;

                CorreoBodyDTO correoBodyDTO = new CorreoBodyDTO();
                string correo = correoClienteCredencialDTO.EmailAsesor.Substring(0, correoClienteCredencialDTO.EmailAsesor.IndexOf('@'));
                correoBodyDTO.EmailBody = "<br>" + mensaje.GetHtmlWithBase64EncodedRelatedFiles() + "<br/>";
                //correoBodyDTO.EmailBody = "<hr>" + mensaje.GetHtmlWithBase64EncodedRelatedFiles() + "<br/>--<br/>" + "<img src='" + _linkRepositorioFirmas + correo + ".png' />";

                if (mensaje.Attachments.Count > 0)
                {
                    correoBodyDTO.ArchivosAdjuntos = new List<CorreoArchivoAdjuntoDTO>();
                    foreach (Attachment attach in mensaje.Attachments)
                    {
                        CorreoArchivoAdjuntoDTO archivoAdjunto = new CorreoArchivoAdjuntoDTO();
                        archivoAdjunto.IdCorreo = IdCorreo;
                        archivoAdjunto.NombreArchivo = attach.Filename.ToString();

                        correoBodyDTO.ArchivosAdjuntos.Add(archivoAdjunto);
                    }
                }
                return Ok(correoBodyDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene informacion de envio masivo.
        /// </summary>
        /// <returns> objetoDTO: CorreoBodyDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult GetInformacionEnvioMasivo(int IdCorreo, int IdAsesor, string Folder, string destinatario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdAsesor <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(201));
            }
            try
            {
                var _repConfiguracionEnvioMailing = new ConfiguracionEnvioMailingRepositorio(_integraDBContext);

                var correoEnvioMandril = _repConfiguracionEnvioMailing.ObtenerEnvioMasivo(IdCorreo);

                CorreoDTO correo = new CorreoDTO();

                CorreoGmailRepositorio objGmail = new CorreoGmailRepositorio(_integraDBContext);
                var queryFiltroGmailCorreo = " AND Id = " + IdCorreo;
                var correoEnvioGmail = objGmail.FiltroCorreosPorPersonaGmailCorreo(queryFiltroGmailCorreo).FirstOrDefault();

                if (correoEnvioMandril != null && correoEnvioMandril.Id > 0)
                {
                    if (correoEnvioGmail != null && correoEnvioGmail.Id > 0)
                    {
                        if (correoEnvioMandril.Destinatarios.Contains(destinatario) && correoEnvioMandril.IdPersonal == IdAsesor)
                        {
                            correo = correoEnvioMandril;
                        }
                        else if (correoEnvioGmail.Destinatarios.Contains(destinatario) && correoEnvioGmail.IdPersonal == IdAsesor)
                        {
                            correo = correoEnvioGmail;
                        }
                    }
                    else
                    {
                        correo = correoEnvioMandril;
                    }
                }
                else if (correoEnvioGmail != null)
                {
                    correo = correoEnvioGmail;
                }
                else
                {
                    return BadRequest("El mensaje no ha sido encontrado");
                }

                CorreoBodyDTO correoBodyDTO = new CorreoBodyDTO()
                {
                    EmailBody = correo.EmailBody
                };
                //string correo = correoClienteCredencialDTO.EmailAsesor.Substring(0, correoClienteCredencialDTO.EmailAsesor.IndexOf('@'));
                //correoBodyDTO.EmailBody = "<br>" + mensaje.GetHtmlWithBase64EncodedRelatedFiles() + "<br/>";
                //correoBodyDTO.EmailBody = "<hr>" + mensaje.GetHtmlWithBase64EncodedRelatedFiles() + "<br/>--<br/>" + "<img src='" + _linkRepositorioFirmas + correo + ".png' />";
                return Ok(correoBodyDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Enviar mensaje.
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EnviarMensaje(CorreoInformacionOportunidadDTO DatosOportunidad, CorreoCabeceraDTO MensajeCabecera, [FromForm] IList<IFormFile> Files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repGmailCorreo = new GmailCorreoRepositorio(_integraDBContext);
                var _repInteraccion = new InteraccionRepositorio(_integraDBContext);
                var _repMandrilEnvioCorreo = new MandrilEnvioCorreoRepositorio(_integraDBContext);
                var _repPersonal = new PersonalRepositorio(_integraDBContext);
                var _repAlumno = new AlumnoRepositorio(_integraDBContext);

                var listaGmailCorreo = new List<GmailCorreoBO>();

                var Mailservice = new TMK_MailServiceImpl();

                var listaMandrilEnvioCorreoBO = new List<MandrilEnvioCorreoBO>();

                MensajeCabecera.DestinatarioCc = MensajeCabecera.DestinatarioCc ?? "";
                MensajeCabecera.DestinatarioBcc = MensajeCabecera.DestinatarioBcc ?? "";

                var asesor = _repPersonal.ObtenerNombreApellido(MensajeCabecera.Remitente);

                byte[] dataMensaje = Convert.FromBase64String(MensajeCabecera.Mensaje);
                var mensajeCorreo = Encoding.UTF8.GetString(dataMensaje);

                if (!mensajeCorreo.Contains("https://repositorioweb.blob.core.windows.net/firmas/"))
                {
                    string firma = string.Empty;
                    string[] separacionEmail = asesor.Email.Split('@');
                    firma = "<img src='https://repositorioweb.blob.core.windows.net/firmas/" + separacionEmail[0] + ".png' />";
                    mensajeCorreo += "<br/>--<br/>" + firma;
                }

                MensajeCabecera.Destinatario = MensajeCabecera.Destinatario.Replace("<", "").Replace(">", "");

                GmailCorreoBO gmailCorreo;
                GmailCorreoArchivoAdjuntoBO gmailCorreoArchivoAdjunto;

                var interacion = new InteraccionBO();
                if (DatosOportunidad.IdActividadDetalle != null)
                {
                    interacion.IdActividadDetalle = DatosOportunidad.IdActividadDetalle;
                    interacion.IdTipoInteraccionGeneral = 1;
                    interacion.Fecha = DateTime.Now;
                    interacion.Estado = true;
                    interacion.FechaCreacion = DateTime.Now;
                    interacion.FechaModificacion = DateTime.Now;
                    interacion.UsuarioCreacion = MensajeCabecera.Usuario;
                    interacion.UsuarioModificacion = MensajeCabecera.Usuario;
                }

                var listaEmails = new List<string>();

                if (MensajeCabecera.envioGrupo)
                {
                    listaEmails = MensajeCabecera.GrupoEmail.Split(',').ToList();
                }
                else
                {
                    listaEmails.Add(MensajeCabecera.Destinatario);
                }

                foreach (var item in listaEmails)
                {
                    var mailData = new TMKMailDataDTO
                    {
                        Sender = MensajeCabecera.Remitente,
                        Recipient = item,
                        Subject = MensajeCabecera.Asunto,
                        Message = mensajeCorreo,
                        Cc = MensajeCabecera.DestinatarioCc,
                        Bcc = MensajeCabecera.DestinatarioBcc,
                        AttachedFiles = null,
                        RemitenteC = string.Concat(asesor.Nombres, ' ', asesor.Apellidos)
                    };

                    Mailservice.SetData(mailData);
                    if (Files != null)
                    {
                        foreach (var file in Files)
                        {
                            Mailservice.SetFiles(file);
                        }
                    }
                    List<TMKMensajeIdDTO> MensajeIdDTO = Mailservice.SendMessageTask();

                    foreach (var mensaje in MensajeIdDTO)
                    {
                        bool existeAlumno = _repAlumno.Exist(x => x.Email1 == mensaje.Email);
                        int IdAlumno = existeAlumno ? _repAlumno.GetBy(x => x.Email1 == mensaje.Email).FirstOrDefault().Id : 0;

                        var mandrilEnvioCorreoBO = new MandrilEnvioCorreoBO
                        {
                            IdOportunidad = DatosOportunidad.IdOportunidad,
                            IdPersonal = asesor.Id,
                            IdAlumno = IdAlumno,
                            IdCentroCosto = DatosOportunidad.IdCentroCosto,
                            IdMandrilTipoAsignacion = DatosOportunidad.IdOportunidad == 0 ? 4 : 0, //Si la oportunidad es null significa que viene desde la bandeja de entrada de la agenda
                            EstadoEnvio = 1,
                            IdMandrilTipoEnvio = 2, //Manual = 2
                            FechaEnvio = DateTime.Now,
                            Asunto = MensajeCabecera.Asunto,
                            FkMandril = mensaje.MensajeId,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = MensajeCabecera.Usuario,
                            UsuarioModificacion = MensajeCabecera.Usuario,
                            EsEnvioMasivo = false
                        };

                        if (!mandrilEnvioCorreoBO.HasErrors)
                        {
                            listaMandrilEnvioCorreoBO.Add(mandrilEnvioCorreoBO);
                        }
                        else
                        {
                            return BadRequest(mandrilEnvioCorreoBO.GetErrors(null));
                        }
                    }

                    //logica guardar correo
                    gmailCorreo = new GmailCorreoBO
                    {
                        IdEtiqueta = 1,//sent:1 , inbox:2
                        Asunto = MensajeCabecera.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = mensajeCorreo,
                        Seen = false,
                        Remitente = MensajeCabecera.Remitente,
                        Cc = MensajeCabecera.DestinatarioCc,
                        Bcc = MensajeCabecera.DestinatarioBcc,
                        Destinatarios = item,
                        IdPersonal = asesor.Id,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = MensajeCabecera.Usuario,
                        UsuarioModificacion = MensajeCabecera.Usuario,
                        IdClasificacionPersona = DatosOportunidad.IdClasificacionPersona
                    };
                    listaGmailCorreo.Add(gmailCorreo);
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    _repGmailCorreo.Insert(listaGmailCorreo);
                    foreach (var item in listaGmailCorreo)
                    {
                        var _gmailCorreo = _repGmailCorreo.FirstById(item.Id);
                        var urlArchivoRepositorio = "";
                        if (Files != null)
                        {
                            foreach (var file in Files)
                            {
                                var nombreArchivo = string.Concat(_gmailCorreo.Id, '-', DateTime.Now.ToString("yyyyMMddHHmmss"), '-', file.FileName);
                                urlArchivoRepositorio = _repGmailCorreo.SubirArchivoRepositorio(file.ConvertToByte(), file.ContentType, nombreArchivo);
                                gmailCorreoArchivoAdjunto = new GmailCorreoArchivoAdjuntoBO()
                                {
                                    Nombre = file.FileName,
                                    UrlArchivoRepositorio = urlArchivoRepositorio,
                                    Estado = true,
                                    UsuarioCreacion = MensajeCabecera.Usuario,
                                    UsuarioModificacion = MensajeCabecera.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                _gmailCorreo.ListaGmailCorreoArchivoAdjunto.Add(gmailCorreoArchivoAdjunto);
                            }
                        }
                        _repGmailCorreo.Update(_gmailCorreo);
                    }

                    if (DatosOportunidad.IdActividadDetalle != null)
                    {
                        _repInteraccion.Insert(interacion);
                    }
                    _repMandrilEnvioCorreo.Insert(listaMandrilEnvioCorreoBO);
                    scope.Complete();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Enviar mensaje Gmail.
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EnviarMensajeGmail(CorreoInformacionOportunidadDTO DatosOportunidad, CorreoCabeceraDTO MensajeCabecera, int IdAsesor, [FromForm] IList<IFormFile> Files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _personalRepositorio = new PersonalRepositorio(_integraDBContext);
                GmailCorreoRepositorio _gmailCorreoRepositorio = new GmailCorreoRepositorio(_integraDBContext);
                InteraccionRepositorio _interaccionRepositorio = new InteraccionRepositorio(_integraDBContext);
                MandrilEnvioCorreoRepositorio _mandrilEnvioCorreoRepositorio = new MandrilEnvioCorreoRepositorio(_integraDBContext);
                PersonalInformacionCorreoDTO asesor = _personalRepositorio.ObtenerNombreApellido(MensajeCabecera.Remitente);
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                MensajeCabecera.DestinatarioCc = MensajeCabecera.DestinatarioCc ?? "";
                MensajeCabecera.DestinatarioBcc = MensajeCabecera.DestinatarioBcc ?? "";
                //string mensajeCorreo = string.Empty;
                //if (DatosOportunidad.IdActividadDetalle == null)
                //{
                //    string firma = string.Empty;
                //    string[] separacionEmail = asesor.Email.Split('@');
                //    firma = "<img src='https://repositorioweb.blob.core.windows.net/firmas/" + separacionEmail[0] + ".png' />";

                //    byte[] dataMensaje = Convert.FromBase64String(MensajeCabecera.Mensaje);
                //    var decodedMensaje = Encoding.UTF8.GetString(dataMensaje);

                //    mensajeCorreo = decodedMensaje + "<br/>--<br/>" + firma;
                //}
                //else
                //{
                //    byte[] dataMensaje = Convert.FromBase64String(MensajeCabecera.Mensaje);
                //    var decodedMensaje = Encoding.UTF8.GetString(dataMensaje);

                //    mensajeCorreo = decodedMensaje;
                //}

                byte[] dataMensaje = Convert.FromBase64String(MensajeCabecera.Mensaje);
                var mensajeCorreo = Encoding.UTF8.GetString(dataMensaje);

                if (!mensajeCorreo.Contains("https://repositorioweb.blob.core.windows.net/firmas/"))
                {
                    string firma = string.Empty;
                    string[] separacionEmail = asesor.Email.Split('@');
                    firma = "<img src='https://repositorioweb.blob.core.windows.net/firmas/" + separacionEmail[0] + ".png' />";
                    mensajeCorreo += "<br/>--<br/>" + firma;
                }

                MensajeCabecera.Destinatario = MensajeCabecera.Destinatario.Replace("<", "").Replace(">", "");

                GmailCorreoBO gmailCorreoBO = new GmailCorreoBO
                {
                    IdEtiqueta = 1,//sent:1 , inbox:2
                    Asunto = MensajeCabecera.Asunto,
                    Fecha = DateTime.Now,
                    EmailBody = mensajeCorreo,
                    Seen = false,
                    Remitente = MensajeCabecera.Remitente,
                    Cc = MensajeCabecera.DestinatarioCc,
                    Bcc = MensajeCabecera.DestinatarioBcc,
                    Destinatarios = MensajeCabecera.Destinatario,
                    IdPersonal = asesor.Id,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = MensajeCabecera.Usuario,
                    UsuarioModificacion = MensajeCabecera.Usuario
                };

                if (gmailCorreoBO.HasErrors)
                {
                    return BadRequest(gmailCorreoBO.GetErrors(null));
                }

                InteraccionBO interacion = new InteraccionBO();
                if (DatosOportunidad.IdActividadDetalle != null)
                {
                    interacion.IdActividadDetalle = DatosOportunidad.IdActividadDetalle;
                    interacion.IdTipoInteraccionGeneral = 1;
                    interacion.Fecha = DateTime.Now;
                    interacion.Estado = true;
                    interacion.FechaCreacion = DateTime.Now;
                    interacion.FechaModificacion = DateTime.Now;
                    interacion.UsuarioCreacion = MensajeCabecera.Usuario;
                    interacion.UsuarioModificacion = MensajeCabecera.Usuario;

                    if (interacion.HasErrors)
                    {
                        return BadRequest(interacion.GetErrors(null));
                    }
                }

                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = MensajeCabecera.Remitente,
                    Recipient = MensajeCabecera.Destinatario,
                    Subject = MensajeCabecera.Asunto,
                    Message = mensajeCorreo,
                    Cc = MensajeCabecera.DestinatarioCc,
                    Bcc = MensajeCabecera.DestinatarioBcc,
                    AttachedFiles = null,
                    RemitenteC = asesor.Nombres + ' ' + asesor.Apellidos
                };

                Mailservice.SetData(mailData);

                if (Files != null)
                {
                    foreach (var file in Files)
                    {
                        Mailservice.SetFiles(file);
                    }
                }

                FiltroBandejaCorreoBO filtroBandejaCorreo = new FiltroBandejaCorreoBO();
                GmailClienteRepositorio _gmailClienteRepositorio = new GmailClienteRepositorio();
                CorreoClienteCredencialDTO correoClienteCredencialDTO = _gmailClienteRepositorio.GetClienteCredencial(IdAsesor);

                if (correoClienteCredencialDTO != null)
                {
                    if (MensajeCabecera.envioGrupo)
                    {
                        var _listaEmails = MensajeCabecera.GrupoEmail.Split(',');
                        foreach (var item in _listaEmails)
                        {
                            mailData.Recipient = item;
                            var rptEnvio = filtroBandejaCorreo.envioEmailAdjunto(correoClienteCredencialDTO.EmailAsesor, correoClienteCredencialDTO.PasswordCorreo, mailData, Files);
                        }
                    }
                    else
                    {
                        var rptEnvio = filtroBandejaCorreo.envioEmailAdjunto(correoClienteCredencialDTO.EmailAsesor, correoClienteCredencialDTO.PasswordCorreo, mailData, Files);
                    }
                }

                //List<TMKMensajeIdDTO> MensajeIdDTO = Mailservice.SendMessageTask();

                //AlumnoRepositorio _alumnoRepositorio = new AlumnoRepositorio();
                //List<MandrilEnvioCorreoBO> listaMandrilEnvioCorreoBO = new List<MandrilEnvioCorreoBO>();

                //foreach (var mensaje in MensajeIdDTO)
                //{
                //    bool existeAlumno = _alumnoRepositorio.Exist(x => x.Email1 == mensaje.Email);
                //    int IdAlumno = existeAlumno ? _alumnoRepositorio.GetBy(x => x.Email1 == mensaje.Email).FirstOrDefault().Id : 0;

                //    MandrilEnvioCorreoBO mandrilEnvioCorreoBO = new MandrilEnvioCorreoBO();
                //    mandrilEnvioCorreoBO.IdOportunidad = DatosOportunidad.IdOportunidad;
                //    mandrilEnvioCorreoBO.IdPersonal = asesor.Id;
                //    mandrilEnvioCorreoBO.IdAlumno = IdAlumno;
                //    mandrilEnvioCorreoBO.IdCentroCosto = DatosOportunidad.IdCentroCosto;
                //    mandrilEnvioCorreoBO.IdMandrilTipoAsignacion = DatosOportunidad.IdOportunidad == 0 ? 4 : 0; //Si la oportunidad es null significa que viene desde la bandeja de entrada de la agenda
                //    mandrilEnvioCorreoBO.EstadoEnvio = 1;
                //    mandrilEnvioCorreoBO.IdMandrilTipoEnvio = 2; //Manual = 2
                //    mandrilEnvioCorreoBO.FechaEnvio = DateTime.Now;
                //    mandrilEnvioCorreoBO.Asunto = MensajeCabecera.Asunto;
                //    mandrilEnvioCorreoBO.FkMandril = mensaje.MensajeId;
                //    mandrilEnvioCorreoBO.Estado = true;
                //    mandrilEnvioCorreoBO.FechaCreacion = DateTime.Now;
                //    mandrilEnvioCorreoBO.FechaModificacion = DateTime.Now;
                //    mandrilEnvioCorreoBO.UsuarioCreacion = MensajeCabecera.Usuario;
                //    mandrilEnvioCorreoBO.UsuarioModificacion = MensajeCabecera.Usuario;

                //    if (!mandrilEnvioCorreoBO.HasErrors)
                //    {
                //        listaMandrilEnvioCorreoBO.Add(mandrilEnvioCorreoBO);
                //    }
                //    else
                //        return BadRequest(mandrilEnvioCorreoBO.GetErrors(null));
                //}

                using (TransactionScope scope = new TransactionScope())
                {
                    _gmailCorreoRepositorio.Insert(gmailCorreoBO);
                    if (DatosOportunidad.IdActividadDetalle != null)
                    {
                        _interaccionRepositorio.Insert(interacion);
                    }
                    //_mandrilEnvioCorreoRepositorio.Insert(listaMandrilEnvioCorreoBO);
                    scope.Complete();
                }
                return Ok();
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Descargar
        /// </summary>
        /// <returns> File </returns>
        [Route("[action]/{IdCorreo}/{NombreArchivo}/{IdAsesor}/{Folder}")]
        [HttpGet]
        public ActionResult Download(int IdCorreo, string NombreArchivo, int IdAsesor, string Folder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GmailClienteRepositorio _gmailClienteRepositorio = new GmailClienteRepositorio();
                CorreoClienteCredencialDTO correoClienteCredencialDTO = _gmailClienteRepositorio.GetClienteCredencial(IdAsesor);
                TMK_ImapServiceImpl _ImapServiceImpl = new TMK_ImapServiceImpl();
                byte[] fileEmail = _ImapServiceImpl.DownloadFileEmailInbox(IdCorreo, correoClienteCredencialDTO.EmailAsesor, correoClienteCredencialDTO.PasswordCorreo, NombreArchivo, Folder);
                return File(fileEmail, "application/pdf", NombreArchivo);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener correos enviados y recibidos.
        /// </summary>
        /// <returns> objetoDTO: BandejaCorreoDTO </returns>
        [Route("[action]/{folderCorreo}")]
        [HttpPost]
        public ActionResult GetCorreosRecibidosEnviados(string folderCorreo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<gmailCredenciales> ListaCorreos = new List<gmailCredenciales>();
                CorreoGmailRepositorio objetoRepoGmail = new CorreoGmailRepositorio();

                var listaCorreos = objetoRepoGmail.ObtenerDatosCorreo();

                FiltroBandejaCorreoBO filtroBandejaCorreo = new FiltroBandejaCorreoBO();

                var lista = filtroBandejaCorreo.GetBandejaEntradaListaMailInbox(folderCorreo, listaCorreos);

                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener correos enviados y recibidos.
        /// </summary>
        /// <returns> objetoDTO: BandejaCorreoDTO </returns>
        [Route("[action]/{folderCorreo}/{IdPersonal}")]
        [HttpPost]
        public ActionResult GetCorreosRecibidosEnviadosPorIdPersonal(string folderCorreo, int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<gmailCredenciales> ListaCorreos = new List<gmailCredenciales>();
                CorreoGmailRepositorio objetoRepoGmail = new CorreoGmailRepositorio();

                var listaCorreos = objetoRepoGmail.ObtenerDatosCorreoPorIdPersonal(IdPersonal);

                FiltroBandejaCorreoBO filtroBandejaCorreo = new FiltroBandejaCorreoBO();

                var lista = filtroBandejaCorreo.GetBandejaEntradaListaMailInbox(folderCorreo, listaCorreos);

                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener credenciales de correo.
        /// </summary>
        /// <returns> objetoDTO: BandejaCorreoDTO </returns>
        [Route("[action]/{folderCorreo}/{IdPersonal}")]
        [HttpPost]
        public ActionResult GetCredencialesCorreoGmail(string folderCorreo, int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<gmailCredenciales> ListaCorreos = new List<gmailCredenciales>();
                CorreoGmailRepositorio objetoRepoGmail = new CorreoGmailRepositorio();

                var listaCorreos = objetoRepoGmail.ObtenerDatosCorreoPorIdPersonal(IdPersonal);

                FiltroBandejaCorreoBO filtroBandejaCorreo = new FiltroBandejaCorreoBO();

                var lista = filtroBandejaCorreo.GetCredencialesCorreoGmail(folderCorreo, listaCorreos);

                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener credenciales de correo.
        /// </summary>
        /// <returns> objetoDTO: BandejaCorreoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GetDescargaCorreoRecibido([FromBody] FiltroBandejaCorreoDTO FiltroBandejaCorreoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FiltroBandejaCorreoBO filtroBandejaCorreo = new FiltroBandejaCorreoBO();
                filtroBandejaCorreo.Page = FiltroBandejaCorreoDTO.Page;
                filtroBandejaCorreo.PageSize = FiltroBandejaCorreoDTO.PageSize;
                filtroBandejaCorreo.Skip = FiltroBandejaCorreoDTO.Skip;
                filtroBandejaCorreo.Take = FiltroBandejaCorreoDTO.Take;
                filtroBandejaCorreo.IdAsesor = FiltroBandejaCorreoDTO.IdAsesor;
                filtroBandejaCorreo.Folder = FiltroBandejaCorreoDTO.Folder;
                filtroBandejaCorreo.FiltroKendo = FiltroBandejaCorreoDTO.FiltroKendo;

                if (filtroBandejaCorreo.HasErrors)
                {
                    return BadRequest(filtroBandejaCorreo.GetErrors(null));
                }

                BandejaCorreoDTO lista = new BandejaCorreoDTO();
                lista = filtroBandejaCorreo.GetBandejaEntradaMailInbox();
                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Speech de correos.
        /// </summary>
        /// <returns> objetoDTO: BandejaCorreoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GetObtnerCorreoSpeech([FromBody] FiltroBandejaCorreoDTO FiltroBandejaCorreoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FiltroBandejaCorreoBO filtroBandejaCorreo = new FiltroBandejaCorreoBO();
                filtroBandejaCorreo.Page = FiltroBandejaCorreoDTO.Page;
                filtroBandejaCorreo.PageSize = FiltroBandejaCorreoDTO.PageSize;
                filtroBandejaCorreo.Skip = FiltroBandejaCorreoDTO.Skip;
                filtroBandejaCorreo.Take = FiltroBandejaCorreoDTO.Take;
                filtroBandejaCorreo.IdAsesor = FiltroBandejaCorreoDTO.IdAsesor;
                filtroBandejaCorreo.Folder = FiltroBandejaCorreoDTO.Folder;
                filtroBandejaCorreo.TipoCorreos = FiltroBandejaCorreoDTO.TipoCorreos;
                filtroBandejaCorreo.FiltroKendo = FiltroBandejaCorreoDTO.FiltroKendo;

                if (filtroBandejaCorreo.HasErrors)
                {
                    return BadRequest(filtroBandejaCorreo.GetErrors(null));
                }

                BandejaCorreoDTO lista = new BandejaCorreoDTO();
                lista = filtroBandejaCorreo.GetBandejaEntradaMailInboxSpeech();

                if (filtroBandejaCorreo.TipoCorreos == "Masivos")
                {

                    var emailPersona = filtroBandejaCorreo.FiltroKendo.Filters.Where(x => x.Field == "Destinatario").FirstOrDefault().Value;
                    var _repConfiguracionEnvioMailing = new ConfiguracionEnvioMailingRepositorio(_integraDBContext);

                    if (emailPersona != null)
                    {
                        var listaMasivos = _repConfiguracionEnvioMailing.ObtenerEnviosMasivos(emailPersona);
                        lista.ListaCorreos.AddRange(listaMasivos);
                        lista.TotalEnviados += listaMasivos.Count;

                        CorreoGmailRepositorio objGmail = new CorreoGmailRepositorio();
                        var queryFiltroGmailCorreo = " AND Destinatarios like '%" + emailPersona + "%' and (ConCopia='modpru@bsginstitute.com' or ConCopia='modpru@bsgrupo.com') AND UsuarioCreacion = 'SYSTEM'";
                        var MensajesGmailCorreo = objGmail.FiltroCorreosPorPersonaGmailCorreo(queryFiltroGmailCorreo);
                        if (MensajesGmailCorreo != null)
                        {
                            foreach (var item in MensajesGmailCorreo)
                            {
                                item.EnvioMasivoMandrill = true;
                            }
                            lista.ListaCorreos.AddRange(MensajesGmailCorreo);
                            lista.TotalEnviados += MensajesGmailCorreo.Count;
                        }
                    }
                    lista.ListaCorreos = lista.ListaCorreos.OrderByDescending(x => x.Fecha).ToList();
                }
                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener  correo.
        /// </summary>
        /// <returns> objetoDTO: BandejaCorreoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCorreo([FromBody] FiltroBandejaCorreoDTO FiltroBandejaCorreoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repGmailRepositorio = new GmailCorreoRepositorio(_integraDBContext);

                var filtroBandejaCorreo = new FiltroBandejaCorreoBO
                {
                    Page = FiltroBandejaCorreoDTO.Page,
                    PageSize = FiltroBandejaCorreoDTO.PageSize,
                    Skip = FiltroBandejaCorreoDTO.Skip,
                    Take = FiltroBandejaCorreoDTO.Take,
                    IdAsesor = FiltroBandejaCorreoDTO.IdAsesor,
                    Folder = FiltroBandejaCorreoDTO.Folder,
                    TipoCorreos = FiltroBandejaCorreoDTO.TipoCorreos,
                    FiltroKendo = FiltroBandejaCorreoDTO.FiltroKendo
                };

                if (filtroBandejaCorreo.HasErrors)
                {
                    return BadRequest(filtroBandejaCorreo.GetErrors(null));
                }

                BandejaCorreoDTO lista = new BandejaCorreoDTO();

                if (filtroBandejaCorreo.TipoCorreos == "Normal")
                {
                    lista = _repGmailRepositorio.ObtenerCorreosEnviados(filtroBandejaCorreo);
                }
                else if (filtroBandejaCorreo.TipoCorreos == "Masivos")
                {
                    //lista = filtroBandejaCorreo.GetBandejaEntradaMailInboxSpeech();

                    var emailPersona = filtroBandejaCorreo.FiltroKendo.Filters.Where(x => x.Field == "Destinatario").FirstOrDefault().Value;
                    var _repConfiguracionEnvioMailing = new ConfiguracionEnvioMailingRepositorio(_integraDBContext);

                    if (emailPersona != null)
                    {
                        var listaMasivos = _repConfiguracionEnvioMailing.ObtenerEnviosMasivos(emailPersona);
                        lista.ListaCorreos.AddRange(listaMasivos);
                        lista.TotalEnviados += listaMasivos.Count;
                    }
                }
                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener correos masivos.
        /// </summary>
        /// <returns> objetoDTO: BandejaCorreoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCorreoMasivo([FromBody] FiltroBandejaCorreoDTO FiltroBandejaCorreoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repGmailRepositorio = new GmailCorreoRepositorio(_integraDBContext);

                var filtroBandejaCorreo = new FiltroBandejaCorreoBO
                {
                    Page = FiltroBandejaCorreoDTO.Page,
                    PageSize = FiltroBandejaCorreoDTO.PageSize,
                    Skip = FiltroBandejaCorreoDTO.Skip,
                    Take = FiltroBandejaCorreoDTO.Take,
                    IdAsesor = FiltroBandejaCorreoDTO.IdAsesor,
                    Folder = FiltroBandejaCorreoDTO.Folder,
                    TipoCorreos = FiltroBandejaCorreoDTO.TipoCorreos,
                    FiltroKendo = FiltroBandejaCorreoDTO.FiltroKendo
                };

                if (filtroBandejaCorreo.HasErrors)
                {
                    return BadRequest(filtroBandejaCorreo.GetErrors(null));
                }

                BandejaCorreoDTO lista = new BandejaCorreoDTO();
                //lista = filtroBandejaCorreo.GetBandejaEntradaMailInboxSpeech();
                lista = _repGmailRepositorio.ObtenerCorreosEnviados(filtroBandejaCorreo);

                if (filtroBandejaCorreo.TipoCorreos == "Masivos")
                {

                    var emailPersona = filtroBandejaCorreo.FiltroKendo.Filters.Where(x => x.Field == "Destinatario").FirstOrDefault().Value;
                    var _repConfiguracionEnvioMailing = new ConfiguracionEnvioMailingRepositorio(_integraDBContext);

                    if (emailPersona != null)
                    {
                        var listaMasivos = _repConfiguracionEnvioMailing.ObtenerEnviosMasivos(emailPersona);
                        lista.ListaCorreos.AddRange(listaMasivos);
                        lista.TotalEnviados += listaMasivos.Count;
                    }
                }
                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener cantidad de correos recibidos por personal.
        /// </summary>
        /// <returns> objetoDTO: BandejaCorreoDTO </returns>
        [Route("[action]/{folderCorreo}/{IdPersonal}")]
        [HttpGet]
        public ActionResult GetCorreosRecibidosPorPersonal(string folderCorreo, int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int _tipoFolder = 0;

                if (folderCorreo.ToLower().Contains("inbox"))
                {
                    _tipoFolder = 1;
                }
                else if (folderCorreo.ToLower().Contains("enviados"))
                {
                    _tipoFolder = 3;
                }

                CorreoGmailRepositorio objGmail = new CorreoGmailRepositorio();

                var listaCorreos = objGmail.ContadorCorreosPorPersona(IdPersonal, _tipoFolder);

                return Ok(listaCorreos);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Enviar Acceso portal web alumno
        /// </summary>
        /// <returns> objetoBO: PlantillaBO</returns>
		[Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarAccesoPortalWebAlumno([FromBody] DatosOportunidadAccesosPortalDTO DatosOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();
                var plantilla = _repPlantilla.FirstBy(x => x.Nombre.Contains("Datos de Acceso Portal Web") && x.IdPlantillaBase == 2);

                return Ok(plantilla);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Enviar Acceso moodle alumno
        /// </summary>
        /// <returns> objetoBO: PlantillaBO</returns>
		[Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarAccesoMoodleAlumno([FromBody] DatosOportunidadAccesosPortalDTO DatosOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                AulaVirtualContext moodleContext = new AulaVirtualContext();
                MdlUser usuarioMoodle = new MdlUser();

                MoodleWebService moodleWebService = new MoodleWebService();

                PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
                var accesos = _repAlumno.ObtenerAccesosInicialesMoodle(DatosOportunidad.IdAlumno);

                var moodleAlumno = moodleContext.MdlUser.Where(x => x.Username == accesos.UsuarioMoodle).FirstOrDefault();
                if (moodleAlumno == null)
                {
                    return BadRequest("El alumno aún no tiene los accesos de aula virtual creados.");
                }

                MoodleWebServiceActualizarClaveDTO moodleWebServiceActualizarClave = new MoodleWebServiceActualizarClaveDTO();
                moodleWebServiceActualizarClave.IdMoodle = moodleAlumno.Id;
                moodleWebServiceActualizarClave.Clave = accesos.PasswordMoodle;

                var actualizarClave = moodleWebService.ActualizarClaveMoodle(moodleWebServiceActualizarClave);
                if (!actualizarClave.Estado)
                {
                    return BadRequest("No se pudo actualizar la clave del alumno en moodle");
                }

                var plantilla = _repPlantilla.FirstBy(x => x.Descripcion.Contains("Accesos para el aula virtual") && x.IdPlantillaBase == 2);
                if (plantilla == null)
                {
                    return BadRequest(ModelState);
                }
                return Ok(plantilla);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Enviar Acceso de portal web alumno por WhatssApp
        /// </summary>
        /// <returns> objetoBO: PlantillaBO</returns>
		[Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarAccesoPortalWebAlumnoWhatsApp([FromBody] DatosOportunidadAccesosPortalDTO DatosOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
                AlumnoBO alumnoBO = new AlumnoBO();
                ValidarNumerosWhatsAppAsyncDTO contact = new ValidarNumerosWhatsAppAsyncDTO();
                contact.contacts = new List<string>();
                var alumno = _repAlumno.FirstById(DatosOportunidad.IdAlumno);
                var alumnoNumero = _repAlumno.ObtenerNumeroWhatsApp(alumno.IdCodigoPais.Value, alumno.Celular);
                contact.contacts.Add("+" + alumnoNumero);
                var validacion = alumnoBO.ValidarNumeroEnvioWhatsApp(DatosOportunidad.IdPersonalAsignado, alumno.IdCodigoPais.Value, contact);
                if (!validacion)
                {
                    return BadRequest("El numero no es valido para WhatsApp");
                }
                var plantilla = _repPlantilla.FirstBy(x => x.Descripcion.Contains("datos_accesos_portal_web") && x.IdPlantillaBase == 8);
                if (plantilla == null || alumnoNumero == null)
                {
                    return BadRequest(ModelState);
                }
                return Ok(new { IdPlantilla = plantilla.Id, Numero = alumnoNumero.Trim() });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Enviar Acceso moodle alumno por WhatssApp
        /// </summary>
        /// <returns> objetoBO: PlantillaBO</returns>
		[Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarAccesoMoodleAlumnoWhatsApp([FromBody] DatosOportunidadAccesosPortalDTO DatosOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                AulaVirtualContext moodleContext = new AulaVirtualContext();
                MdlUser usuarioMoodle = new MdlUser();

                MoodleWebService moodleWebService = new MoodleWebService();

                PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio();

                AlumnoBO alumnoBO = new AlumnoBO();
                ValidarNumerosWhatsAppAsyncDTO contact = new ValidarNumerosWhatsAppAsyncDTO();

                contact.contacts = new List<string>();
                var alumno = _repAlumno.FirstById(DatosOportunidad.IdAlumno);
                var alumnoNumero = _repAlumno.ObtenerNumeroWhatsApp(alumno.IdCodigoPais.Value, alumno.Celular);
                contact.contacts.Add("+" + alumnoNumero);
                var validacion = alumnoBO.ValidarNumeroEnvioWhatsApp(DatosOportunidad.IdPersonalAsignado, alumno.IdCodigoPais.Value, contact);
                if (!validacion)
                {
                    return BadRequest("El numero no es valido para WhatsApp");
                }

                var accesos = _repAlumno.ObtenerAccesosInicialesMoodle(DatosOportunidad.IdAlumno);
                if (accesos == null)
                {
                    return BadRequest("No se registraron los accesos del alumno en Integra");
                }
                var moodleAlumno = moodleContext.MdlUser.Where(x => x.Username == accesos.UsuarioMoodle).FirstOrDefault();
                if (moodleAlumno == null)
                {
                    return BadRequest("El alumno aún no tiene los accesos de aula virtual creados.");
                }

                MoodleWebServiceActualizarClaveDTO moodleWebServiceActualizarClave = new MoodleWebServiceActualizarClaveDTO();
                moodleWebServiceActualizarClave.IdMoodle = moodleAlumno.Id;
                moodleWebServiceActualizarClave.Clave = accesos.PasswordMoodle;

                var actualizarClave = moodleWebService.ActualizarClaveMoodle(moodleWebServiceActualizarClave);
                if (!actualizarClave.Estado)
                {
                    return BadRequest("No se pudo actualizar la clave del alumno en moodle");
                }

                var plantilla = _repPlantilla.FirstBy(x => x.Descripcion.Contains("acceso_aula_virtual_") && x.IdPlantillaBase == 8);
                if (plantilla == null || alumnoNumero == null)
                {
                    return BadRequest(ModelState);
                }
                return Ok(new { IdPlantilla = plantilla.Id, Numero = alumnoNumero.Trim() });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener correos por grupos
        /// </summary>
        /// <returns> objetoBO: ListaCorreosGrupoBO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult obtenerCorreosGrupos(int IdCentroCosto, List<int> Paquetes, List<int> Estados, List<int> SubEstados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                FiltroBandejaCorreoBO filtroBandejaCorreo = new FiltroBandejaCorreoBO();
                ListaCorreosGrupoBO correosBO = new ListaCorreosGrupoBO();
                correosBO.ListaCorreos = "";
                correosBO.TotalCorreos = 0;
                correosBO.Errores = false;
                int CantidadError = 0;
                if ((Paquetes.Count > 0 && Estados.Count > 0 && SubEstados.Count > 0) //si las 3 listas tienen items
                    || (Paquetes.Count > 0 && Estados.Count > 0 && SubEstados.Count == 0)//o si paquetes y estado tienen items
                    || (Paquetes.Count > 0 && Estados.Count == 0 && SubEstados.Count == 0))//o si solo Paquetes tiene items
                {
                    ListaCorreosGrupoBO correos = new ListaCorreosGrupoBO();
                    if (Estados.Count > 0 && SubEstados.Count > 0)//Si estados y subestado tienen items
                    {
                        foreach (var item in Paquetes)
                        {
                            correos = filtroBandejaCorreo.obtenerCorreosGrupos(IdCentroCosto, item, Estados, SubEstados);
                            if (correos.Errores != true)
                            {
                                if (correosBO.ListaCorreos != "")
                                {
                                    correosBO.ListaCorreos = correosBO.ListaCorreos + ',' + correos.ListaCorreos;
                                }
                                else
                                {
                                    correosBO.ListaCorreos = correosBO.ListaCorreos + correos.ListaCorreos;
                                }

                                correosBO.TotalCorreos = correosBO.TotalCorreos + correos.TotalCorreos;
                                correosBO.Errores = correos.Errores;
                            }
                            else
                            {
                                CantidadError++;
                            }
                            //foreach (var est in Estados)
                            //{
                            //    foreach (var subest in SubEstados)
                            //    {

                            //        correos = filtroBandejaCorreo.obtenerCorreosGrupos(IdCentroCosto, item,est,subest);
                            //        if (correos.Errores != true)
                            //        {
                            //            if (correosBO.ListaCorreos != "")
                            //            {
                            //                correosBO.ListaCorreos = correosBO.ListaCorreos + ',' + correos.ListaCorreos;
                            //            }
                            //            else
                            //            {
                            //                correosBO.ListaCorreos = correosBO.ListaCorreos + correos.ListaCorreos;
                            //            }

                            //            correosBO.TotalCorreos = correosBO.TotalCorreos + correos.TotalCorreos;
                            //            correosBO.Errores = correos.Errores;
                            //        }
                            //    }
                            //}        

                        }

                    }
                    if (Estados.Count > 0 && SubEstados.Count == 0)//si estado tiene items
                    {
                        foreach (var item in Paquetes)
                        {
                            correos = filtroBandejaCorreo.obtenerCorreosGrupos(IdCentroCosto, item, Estados, SubEstados);
                            if (correos.Errores != true)
                            {
                                if (correosBO.ListaCorreos != "")
                                {
                                    correosBO.ListaCorreos = correosBO.ListaCorreos + ',' + correos.ListaCorreos;
                                }
                                else
                                {
                                    correosBO.ListaCorreos = correosBO.ListaCorreos + correos.ListaCorreos;
                                }

                                correosBO.TotalCorreos = correosBO.TotalCorreos + correos.TotalCorreos;
                                correosBO.Errores = correos.Errores;
                            }
                            else
                            {
                                CantidadError++;
                            }
                        }
                    }
                    if (Estados.Count == 0 && SubEstados.Count == 0)//si ninguno tiene items solo Paquetes 
                    {
                        foreach (var item in Paquetes)
                        {
                            correos = filtroBandejaCorreo.obtenerCorreosGrupos(IdCentroCosto, item, Estados, SubEstados);
                            if (correos.Errores != true)
                            {
                                if (correosBO.ListaCorreos != "")
                                {
                                    correosBO.ListaCorreos = correosBO.ListaCorreos + ',' + correos.ListaCorreos;
                                }
                                else
                                {
                                    correosBO.ListaCorreos = correosBO.ListaCorreos + correos.ListaCorreos;
                                }

                                correosBO.TotalCorreos = correosBO.TotalCorreos + correos.TotalCorreos;
                                correosBO.Errores = correos.Errores;
                            }
                            else
                            {
                                CantidadError++;
                            }
                        }
                    }
                }
                else
                {
                    ListaCorreosGrupoBO correos = new ListaCorreosGrupoBO();
                    if (Estados.Count > 0 && SubEstados.Count > 0)//Si estados y subestado tienen items
                    {
                        //foreach (var est in Estados)
                        //{
                        //    foreach (var subest in SubEstados)
                        //    {
                        //        correos = filtroBandejaCorreo.obtenerCorreosGrupos(IdCentroCosto, 0, Estados, subest);
                        //        correosBO.ListaCorreos = correos.ListaCorreos;
                        //        correosBO.TotalCorreos = correos.TotalCorreos;
                        //        correosBO.Errores = correos.Errores;
                        //    }
                        //}                        
                        correos = filtroBandejaCorreo.obtenerCorreosGrupos(IdCentroCosto, 0, Estados, SubEstados);
                        correosBO.ListaCorreos = correos.ListaCorreos;
                        correosBO.TotalCorreos = correos.TotalCorreos;
                        correosBO.Errores = correos.Errores;

                    }
                    if (Estados.Count > 0 && SubEstados.Count == 0)//si estado tiene items
                    {
                        //foreach (var est in Estados)
                        //{
                        //    correos = filtroBandejaCorreo.obtenerCorreosGrupos(IdCentroCosto, 0, est, 0);
                        //    correosBO.ListaCorreos = correos.ListaCorreos;
                        //    correosBO.TotalCorreos = correos.TotalCorreos;
                        //    correosBO.Errores = correos.Errores;
                        //}
                        correos = filtroBandejaCorreo.obtenerCorreosGrupos(IdCentroCosto, 0, Estados, SubEstados);
                        correosBO.ListaCorreos = correos.ListaCorreos;
                        correosBO.TotalCorreos = correos.TotalCorreos;
                        correosBO.Errores = correos.Errores;

                    }
                    correos = filtroBandejaCorreo.obtenerCorreosGrupos(IdCentroCosto, 0, Estados, SubEstados);

                    if (Estados.Count == 0 && SubEstados.Count == 0)
                    {
                        correos = filtroBandejaCorreo.obtenerCorreosGrupos(IdCentroCosto, 0, Estados, SubEstados);
                        correosBO.ListaCorreos = correos.ListaCorreos;
                        correosBO.TotalCorreos = correos.TotalCorreos;
                        correosBO.Errores = correos.Errores;
                    }


                }
                if (CantidadError == Paquetes.Count() && Paquetes.Count != 0)
                {
                    correosBO.Errores = true;
                }
                return Ok(correosBO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: -- , Jashin Salazar.
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Enviar condiciones y caracteristicas
        /// </summary>
        /// <returns> Integer </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarCondicionesCaracteristicas([FromBody] DatosOportunidadAccesosPortalDTO DatosOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repPlantilla = new PlantillaRepositorio(_integraDBContext);
                var _repAlumno = new AlumnoRepositorio(_integraDBContext);
                var alumno = _repAlumno.FirstById(DatosOportunidad.IdAlumno);

                //1227	Condiciones y Características - PERÚ OPERACIONES
                //1245	Condiciones y Características - COLOMBIA OPERACIONES
                var _idPlantilla = 0;
                if (alumno.IdCodigoPais == 57)
                {
                    _idPlantilla = 1245;
                }
                else if (alumno.IdCodigoPais == 51)
                {
                    _idPlantilla = 1227;
                }
                return Ok(new { IdPlantilla = _idPlantilla });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}