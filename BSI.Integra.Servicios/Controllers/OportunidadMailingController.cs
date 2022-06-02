using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Servicios.BO;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using MailBee.Mime;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Transversal.BO;
using MailBee.ImapMail;
using BSI.Integra.Aplicacion.Marketing.Repositorio;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/OportunidadMailing")]
    public class OportunidadMailingController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public OportunidadMailingController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerDataFiltros()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaEstadoCreacionOportunidad = new[] {
                    new { Id = 1, Nombre = "No cumplen el criterio de contener el Id de mailchimp" },
                    new { Id = 2, Nombre = "Cumplen criterios pero no esta seleccionado para crear oportunidad" },
                    new { Id = 3, Nombre = "Cumplen criterios y esta seleccionado para crear oportunidad" }//,
                    //new { Id = 4, Nombre = "Oportunidad creada" }
                }.ToList();

                var listasFiltro = new
                {
                    listaEstadoCreacionOportunidad
                };

                return Ok(listasFiltro);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdGmailFolder}")]
        [HttpGet]
        public ActionResult ObtenerUltimaFechaDescarga(int IdGmailFolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GmailFolderRepositorio _repGmailFolder = new GmailFolderRepositorio(_integraDBContext);
                if (!_repGmailFolder.Exist(IdGmailFolder))
                {
                    return BadRequest("Registro no existente");
                }
                var correoGmail = new CorreoGmailBO(_integraDBContext)
                {
                    IdGmailFolder = IdGmailFolder
                };
                return Ok(new { UltimaFechaDescarga = correoGmail.ObtenerUltimaFechaDescarga() });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdCorreoGmail}/{NombreUsuario}")]
        [HttpGet]
        public ActionResult ObtenerInformacionGmail(int IdCorreoGmail, string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CorreoGmailRepositorio _repCorreoGmailRepositorio = new CorreoGmailRepositorio();
                if (!_repCorreoGmailRepositorio.Exist(IdCorreoGmail))
                {
                    return BadRequest("Registro no existente");
                }
                var correoGmail = _repCorreoGmailRepositorio.FirstById(IdCorreoGmail);
                correoGmail.EsLeido = true;
                correoGmail.FechaModificacion = DateTime.Now;
                correoGmail.UsuarioModificacion = NombreUsuario;
                if (!correoGmail.HasAlerts)
                {
                    _repCorreoGmailRepositorio.Update(correoGmail);
                }
                else
                {
                    return BadRequest(correoGmail.GetErrors());
                }
                return Ok(_repCorreoGmailRepositorio.ObtenerInformacionCorreo(IdCorreoGmail));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCorreos([FromBody] FiltroBandejaCorreoGmailDTO FiltroBandejaCorreoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repCorreoGmail = new CorreoGmailRepositorio();
                return Ok(_repCorreoGmail.ObtenerCorreos(FiltroBandejaCorreoDTO));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCorreosConOportunidadCreada([FromBody] FiltroBandejaCorreoGmailDTO FiltroBandejaCorreoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CorreoGmailRepositorio _repCorreoGmail = new CorreoGmailRepositorio();
                return Ok(_repCorreoGmail.ObtenerCorreosOportunidadCreada(FiltroBandejaCorreoDTO));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{NombreUsuario}/{IdGmailFolder}")]
        [HttpGet]
        public ActionResult DescargarCorreo(string NombreUsuario, int IdGmailFolder)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TMK_ImapServiceImpl _imapService = new TMK_ImapServiceImpl();

                GmailFolderRepositorio _repGmailFolder = new GmailFolderRepositorio(_integraDBContext);
                CorreoGmailRepositorio _repCorreoGmail = new CorreoGmailRepositorio(_integraDBContext);

                List<CorreoGmailBO> ListaCorreoGmail = new List<CorreoGmailBO>();

                if (!_repGmailFolder.Exist(IdGmailFolder))
                {
                    return BadRequest("Registro no existente");
                }
                var gmailFolder = _repGmailFolder.FirstById(IdGmailFolder);

                var filtroBandejaCorreoDTO = new FiltroBandejaCorreoDTO()
                {
                    Page = 100000,
                    PageSize = 100000,
                    Skip = 0,
                    Take = 100000,
                    Folder = gmailFolder.Nombre,
                    IdAsesor = 643,
                    FiltroKendo = new GridFiltersDTO()
                    {
                        Filters = new List<GridFilterDTO>() {
                            new GridFilterDTO(){
                                Field = "Fecha",
                                Operator= "=",
                                Value = ""
                            }
                        }
                    }
                };

                FiltroBandejaCorreoGmailBO filtroBandejaCorreoGmail = new FiltroBandejaCorreoGmailBO
                {
                    Page = filtroBandejaCorreoDTO.Page,
                    PageSize = filtroBandejaCorreoDTO.PageSize,
                    Skip = filtroBandejaCorreoDTO.Skip,
                    Take = filtroBandejaCorreoDTO.Take,
                    Folder = filtroBandejaCorreoDTO.Folder,
                    FiltroKendo = filtroBandejaCorreoDTO.FiltroKendo
                };

                if (filtroBandejaCorreoGmail.HasErrors)
                {
                    return BadRequest(filtroBandejaCorreoGmail.GetErrors(null));
                }

                var correoClienteCredencialDTO = new CorreoClienteCredencialDTO()
                {
                    IdAsesor = 643,
                    EmailAsesor = "webinars@bsgrupo.com",
                    PasswordCorreo = "tzixlhaicfuctzeu"
                };

                EnvelopeCollection listaGmail = new EnvelopeCollection();
                var fechaUltimaDescarga = _repCorreoGmail.ObtenerUltimaFechaDescarga(IdGmailFolder);

                //obtenemos los ids regitrados durante el ultimo dia buscado
                var listaUidsDescargados = _repCorreoGmail.ObtenerUidsPorFecha(fechaUltimaDescarga);

                if (correoClienteCredencialDTO != null)
                {
                    listaGmail = filtroBandejaCorreoGmail.ObtenerBandejaEntradaMailInbox(correoClienteCredencialDTO.EmailAsesor, correoClienteCredencialDTO.PasswordCorreo, fechaUltimaDescarga, listaUidsDescargados);
                }

                if (listaGmail == null)
                {
                    return BadRequest("Sucedio un error obteniendo los correos");
                }
                string adjunto = string.Empty;
                bool hasAttachments = false;
                bool correoLeido = false;

                foreach (Envelope msg in listaGmail)
                {
                    //Si existe en la base de datos no lo agregamos
                    if (_repCorreoGmail.Exist(
                        x => x.GmailCorreoId == msg.Uid && x.IdGmailFolder == gmailFolder.Id
                        && x.IdPersonal == null)
                    )
                    {
                        continue;
                    }

                    if (
                        true
                        //msg.From.Email == "wchoque@bsginstitute.com"  ||
                        )
                    {
                        var correoGmail = new CorreoGmailBO()
                        {
                            IdGmailFolder = IdGmailFolder,
                            GmailCorreoId = msg.MessageNumber,
                            Asunto = msg.Subject.ToString(),
                            Fecha = msg.DateReceived,
                            CuerpoHtml = "",
                            EsLeido = false,
                            NombreRemitente = msg.From.DisplayName,
                            EmailRemitente = msg.From.Email,
                            Destinatarios = msg.To,
                            EmailConCopiaOculta = msg.Bcc,
                            EmailConCopia = msg.Cc,
                            AplicaCrearOportunidad = false,
                            SeCreoOportunidad = false,
                            CumpleCriterioCrearOportunidad = false,
                            IdPrioridadMailChimpListaCorreo = null,
                            Estado = true,
                            UsuarioCreacion = NombreUsuario,
                            UsuarioModificacion = NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        if (msg.Flags != null)
                        {
                            if (msg.Flags.SystemFlags.ToString().Contains("Seen"))
                            {
                                correoLeido = true;
                            }
                            else
                            {
                                correoLeido = false;
                            }
                        }
                        if (msg.Cc.Count > 0)
                        {
                            correoGmail.Destinatarios += "," + msg.Cc.ToString();
                        }
                        ImapBodyStructureCollection parts = msg.BodyStructure.GetAllParts();
                        foreach (ImapBodyStructure part in parts)
                        {
                            if ((part.Disposition != null && part.Disposition.ToLower() == "attachment") ||
                                (part.Filename != null && part.Filename != string.Empty) ||
                                (part.ContentType != null && part.ContentType.ToLower() == "message/rfc822"))
                            {
                                hasAttachments = true;
                                break;
                            }
                        }
                        if (hasAttachments)
                        {
                            adjunto = "(A) ";
                            hasAttachments = false;
                        }
                        else
                        {
                            adjunto = string.Empty;
                        }
                        correoGmail.Destinatarios = correoGmail.Destinatarios.Replace(", ", ",");
                        correoGmail.Destinatarios = correoGmail.Destinatarios.Replace(",,", ",");
                        correoGmail.Asunto = adjunto + msg.Subject.ToString();
                        correoGmail.EsLeido = correoLeido;
                        correoGmail.EsDesuscritoCorrectamente = false;
                        correoGmail.EsMarcadoDesuscrito = false;
                        correoGmail.EsDescartado = false;
                        //obtener cuerpo y adjuntos
                        MailMessage mensaje = _imapService.GetBodyCorreo_byUid(msg.MessageNumber, correoClienteCredencialDTO.EmailAsesor, correoClienteCredencialDTO.PasswordCorreo, gmailFolder.Nombre);
                        mensaje.Parser.PlainToHtmlMode = PlainToHtmlAutoConvert.IfNoHtml;

                        string correo = correoClienteCredencialDTO.EmailAsesor.Substring(0, correoClienteCredencialDTO.EmailAsesor.IndexOf('@'));
                        correoGmail.CuerpoHtml = mensaje.GetHtmlWithBase64EncodedRelatedFiles();

                        if (mensaje.Attachments.Count > 0)
                        {
                            foreach (Attachment attach in mensaje.Attachments)
                            {
                                var correoGmailArchivoAdjunto = new CorreoGmailArchivoAdjuntoBO()
                                {
                                    Nombre = attach.Filename.ToString(),
                                    Estado = true,
                                    UsuarioCreacion = NombreUsuario,
                                    UsuarioModificacion = NombreUsuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                correoGmail.CorreoGmailArchivoAdjunto.Add(correoGmailArchivoAdjunto);
                            }
                        }
                        correoGmail.ValidarCriterios();

                        if (!_repCorreoGmail.Exist(
                            x => //x.CuerpoHtml == correoGmail.CuerpoHtml &&
                            x.Asunto == correoGmail.Asunto &&
                            x.EmailRemitente == correoGmail.EmailRemitente &&
                            x.NombreRemitente == correoGmail.NombreRemitente &&
                            x.IdGmailFolder == correoGmail.IdGmailFolder &&
                            x.Destinatarios == correoGmail.Destinatarios
                        ))
                        {
                            ListaCorreoGmail.Add(correoGmail);
                        }
                    }
                }
                if (ListaCorreoGmail != null)
                {
                    ListaCorreoGmail = ListaCorreoGmail.OrderBy(x => x.GmailCorreoId).ToList();
                }
                _repCorreoGmail.Insert(ListaCorreoGmail);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{NombreUsuario}/{IdCorreoGmail}")]
        [HttpGet]
        public ActionResult CalcularCriterios(string NombreUsuario, int IdCorreoGmail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TMK_ImapServiceImpl _imapService = new TMK_ImapServiceImpl();

                GmailFolderRepositorio _repGmailFolder = new GmailFolderRepositorio(_integraDBContext);
                CorreoGmailRepositorio _repCorreoGmail = new CorreoGmailRepositorio(_integraDBContext);

                List<CorreoGmailBO> ListaCorreoGmail = new List<CorreoGmailBO>();

                if (IdCorreoGmail == 0)
                {
                    ListaCorreoGmail = _repCorreoGmail.ObtenerValidarCumplenCriterio();
                }
                else
                {
                    if (_repCorreoGmail.Exist(IdCorreoGmail))
                    {
                        ListaCorreoGmail.Add(_repCorreoGmail.FirstById(IdCorreoGmail));
                    }
                }

                foreach (var item in ListaCorreoGmail)
                {
                    item.ValidarCriterios();
                    item.FechaModificacion = DateTime.Now;
                    item.UsuarioModificacion = NombreUsuario;
                }
                _repCorreoGmail.Update(ListaCorreoGmail);
                return Ok(new { CorreosCumplenCriterio = ListaCorreoGmail.Where(x => x.CumpleCriterioCrearOportunidad) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult MarcarAplicaCrearOportunidad([FromBody] CorreoGmailAplicaCrearOportunidadDTO GmailCorreo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CorreoGmailRepositorio _repCorreoGmail = new CorreoGmailRepositorio();
                if (!_repCorreoGmail.Exist(GmailCorreo.IdCorreoGmail))
                {
                    return BadRequest("Entidad no existe");
                }
                var correoGmail = _repCorreoGmail.FirstById(GmailCorreo.IdCorreoGmail);
                correoGmail.AplicaCrearOportunidad = GmailCorreo.AplicaCrearOportunidad;
                correoGmail.FechaModificacion = DateTime.Now;
                correoGmail.UsuarioModificacion = GmailCorreo.NombreUsuario;
                _repCorreoGmail.Update(correoGmail);

                return Ok(correoGmail);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdCorreoGmail}/{NombreArchivo}/{IdGmailFolder}")]
        [HttpGet]
        public ActionResult DownloadFiles(int IdCorreoGmail, string NombreArchivo, int IdGmailFolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repGmailFolder = new GmailFolderRepositorio();
                var _repCorreoGmail = new CorreoGmailRepositorio();
                if (!_repCorreoGmail.Exist(IdCorreoGmail))
                {
                    return BadRequest("No existe el registro");
                }
                if (!_repGmailFolder.Exist(IdGmailFolder))
                {
                    return BadRequest("No existe el folder");
                }
                var gmailFolder = _repGmailFolder.FirstById(IdGmailFolder);
                var correoGmail = _repCorreoGmail.FirstById(IdCorreoGmail);

                var correoClienteCredencial = new CorreoClienteCredencialDTO()
                {
                    IdAsesor = 0,
                    EmailAsesor = "webinars@bsgrupo.com",
                    PasswordCorreo = "tzixlhaicfuctzeu"
                };

                TMK_ImapServiceImpl _ImapServiceImpl = new TMK_ImapServiceImpl();

                var correo = _ImapServiceImpl.ObtenerCorreos_ByUid(correoGmail.GmailCorreoId.ToString());

                foreach (Envelope msg in correo)
                {
                    byte[] fileEmail = _ImapServiceImpl.DownloadFileEmailInbox_ByUid(msg.MessageNumber, correoClienteCredencial.EmailAsesor, correoClienteCredencial.PasswordCorreo, NombreArchivo, gmailFolder.Nombre);
                    return File(fileEmail, "application/pdf", NombreArchivo);
                }
                return BadRequest("Archivo no encontrado");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult MarcarParaDesuscribir([FromBody] CorreoGmailDesuscribirDTO GmailCorreo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CorreoGmailRepositorio _repCorreoGmail = new CorreoGmailRepositorio();
                if (!_repCorreoGmail.Exist(GmailCorreo.IdCorreoGmail))
                {
                    return BadRequest("Entidad no existe");
                }
                var correoGmail = _repCorreoGmail.FirstById(GmailCorreo.IdCorreoGmail);
                correoGmail.EsMarcadoDesuscrito = GmailCorreo.EsMarcadoDesuscrito;
                correoGmail.FechaModificacion = DateTime.Now;
                correoGmail.UsuarioModificacion = GmailCorreo.NombreUsuario;
                _repCorreoGmail.Update(correoGmail);

                return Ok(correoGmail);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult MarcarDescartar([FromBody] CorreoGmailDescartarDTO GmailCorreo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CorreoGmailRepositorio _repCorreoGmail = new CorreoGmailRepositorio();
                if (!_repCorreoGmail.Exist(GmailCorreo.IdCorreoGmail))
                {
                    return BadRequest("Entidad no existente!");
                }
                var correoGmail = _repCorreoGmail.FirstById(GmailCorreo.IdCorreoGmail);
                if (correoGmail.CumpleCriterioCrearOportunidad == false)
                {
                    return BadRequest("Solo puede descartarse los correos que cumplen el criterio de contener el Id de mailchimp!");
                }
                if (correoGmail.EsDescartado == true)
                {
                    return BadRequest("Este registro ya ha sido descartado!");
                }
                if (correoGmail.AplicaCrearOportunidad == true && correoGmail.SeCreoOportunidad == false)
                {
                    return BadRequest("Este registro ha sido marcado para crear oportunidad, no puede descartarse!");
                }
                if (correoGmail.AplicaCrearOportunidad == true && correoGmail.SeCreoOportunidad == true)
                {
                    return BadRequest("Este registro no puede descartarse porque ya se creo una oportunidad, para visualizarlo revise el tab de oportunidad creada");
                }
                correoGmail.EsDescartado = GmailCorreo.EsDescartado;
                correoGmail.FechaModificacion = DateTime.Now;
                correoGmail.UsuarioModificacion = GmailCorreo.NombreUsuario;
                _repCorreoGmail.Update(correoGmail);

                return Ok(correoGmail);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]/{NombreUsuario}")]
        [HttpGet]
        public ActionResult DesuscribirContactos(string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ListError listError = new ListError();

                GmailFolderRepositorio _repGmailFolder = new GmailFolderRepositorio(_integraDBContext);
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                PrioridadMailChimpListaCorreoRepositorio _repPrioridadMailChimpListaCorreo = new PrioridadMailChimpListaCorreoRepositorio(_integraDBContext);
                CorreoGmailRepositorio _repCorreoGmail = new CorreoGmailRepositorio(_integraDBContext);
                var prioridadMailChimpListaCorreoActual = new PrioridadMailChimpListaCorreoBO();

                var correosDesuscribir = _repCorreoGmail.ObtenerMarcadosDesuscribir();
                foreach (var item in correosDesuscribir)
                {

                    //Obtenemos el id de alumno
                    if (!_repPrioridadMailChimpListaCorreo.Exist(item.IdPrioridadMailChimpListaCorreo.Value))
                    {
                        listError.AgregarError(new Error(item.IdPrioridadMailChimpListaCorreo.Value, "IdPrioridadMailChimpListaCorreo", "IdPrioridadMailChimpListaCorreo no encontrado"));
                        continue;
                    }

                    prioridadMailChimpListaCorreoActual = _repPrioridadMailChimpListaCorreo.FirstById(item.IdPrioridadMailChimpListaCorreo.Value);

                    if (!_repAlumno.Exist(prioridadMailChimpListaCorreoActual.IdAlumno))
                    {
                        listError.AgregarError(new Error(prioridadMailChimpListaCorreoActual.IdAlumno, "IdAlumno", "IdAlumno no encontrado"));
                        continue;
                    }

                    var resultado = _repAlumno.Desuscribir(prioridadMailChimpListaCorreoActual.IdAlumno, NombreUsuario);
                    if (resultado == true)
                    {
                        item.EsDesuscritoCorrectamente = true;
                        item.FechaModificacion = DateTime.Now;
                        item.UsuarioModificacion = NombreUsuario;
                    }
                }
                _repCorreoGmail.Update(correosDesuscribir);
                return Ok(new { ContactosDesuscritos = correosDesuscribir.Where(x => x.EsDesuscritoCorrectamente == true), Errores = listError.ObtenerErrores() });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdCorreoGmail}/{NombreUsuario}")]
        [HttpGet]
        public ActionResult ObtenerCuerpoHMTLArchivoAdjunto(int IdCorreoGmail, string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repCorreoGmail = new CorreoGmailRepositorio(_integraDBContext);
                var _repCorreoGmailArchivoAdjunto = new CorreoGmailArchivoAdjuntoRepositorio(_integraDBContext);

                if (!_repCorreoGmail.Exist(IdCorreoGmail))
                {
                    return BadRequest("No se encontro el registro");
                }

                var correoGmail = _repCorreoGmail.FirstById(IdCorreoGmail);

                var _repGmailFolder = new GmailFolderRepositorio(_integraDBContext);
                var gmailFolder = _repGmailFolder.FirstById(correoGmail.IdGmailFolder);

                if (correoGmail.IdPersonal is null)
                {
                    return BadRequest("No tiene personal");
                }
                var _gmailClienteRepositorio = new GmailClienteRepositorio();
                var correoClienteCredencialDTO = _gmailClienteRepositorio.GetClienteCredencial(correoGmail.IdPersonal.Value);

                var _imapService = new TMK_ImapServiceImpl();
                var mensaje = _imapService.GetBodyCorreo(Convert.ToInt32(correoGmail.GmailCorreoId), correoClienteCredencialDTO.EmailAsesor, correoClienteCredencialDTO.PasswordCorreo, gmailFolder.Nombre);
                mensaje.Parser.PlainToHtmlMode = PlainToHtmlAutoConvert.IfNoHtml;

                CorreoBodyDTO correoBodyDTO = new CorreoBodyDTO();
                string correo = correoClienteCredencialDTO.EmailAsesor.Substring(0, correoClienteCredencialDTO.EmailAsesor.IndexOf('@'));
                correoBodyDTO.EmailBody = "<br>" + mensaje.GetHtmlWithBase64EncodedRelatedFiles() + "<br/>";
                //correoBodyDTO.EmailBody = "<hr>" + mensaje.GetHtmlWithBase64EncodedRelatedFiles() + "<br/>--<br/>" + "<img src='" + _linkRepositorioFirmas + correo + ".png' />";
                correoGmail.CuerpoHtml = correoBodyDTO.EmailBody;

                CorreoGmailArchivoAdjuntoBO correoGmailArchivoAdjunto;
                if (mensaje.Attachments.Count > 0)
                {
                    correoBodyDTO.ArchivosAdjuntos = new List<CorreoArchivoAdjuntoDTO>();
                    foreach (Attachment attach in mensaje.Attachments)
                    {


                        //CorreoArchivoAdjuntoDTO archivoAdjunto = new CorreoArchivoAdjuntoDTO
                        //{
                        //    IdCorreo = correoGmail.Id,
                        //    NombreArchivo = attach.Filename.ToString()
                        //};
                        //correoBodyDTO.ArchivosAdjuntos.Add(archivoAdjunto);
                        var urlArchivoRepositorio = "";
                        if (attach != null)
                        {
                            var nombreArchivo = string.Concat(correoGmail.Id, "-", attach.Filename);

                            if (!_repCorreoGmailArchivoAdjunto.Exist(x => x.IdCorreoGmail == correoGmail.Id && x.Nombre.Equals(nombreArchivo)))
                            {
                                urlArchivoRepositorio = _repCorreoGmail.SubirArchivoRepositorio(attach.GetData(), attach.ContentType, nombreArchivo);
                                correoGmailArchivoAdjunto = new CorreoGmailArchivoAdjuntoBO()
                                {
                                    Nombre = nombreArchivo,
                                    UrlArchivoRepositorio = urlArchivoRepositorio,
                                    Estado = true,
                                    UsuarioCreacion = NombreUsuario,
                                    UsuarioModificacion = NombreUsuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                correoGmail.CorreoGmailArchivoAdjunto.Add(correoGmailArchivoAdjunto);
                            }
                        }
                    }
                }
                _repCorreoGmail.Update(correoGmail);
                return Ok(correoGmail);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdInicial}/{Cantidad}/{NombreUsuario}/{EjecutarUnaVez}")]
        [HttpGet]
        public ActionResult ObtenerCorreoMasivo(int IdInicial, int Cantidad, string NombreUsuario, bool EjecutarUnaVez)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repCorreoGmail = new CorreoGmailRepositorio(_integraDBContext);
                var maxValue = 5000000;
                var listaCorreoGmail = new List<int>();
                //if (EjecutarUnaVez)
                //{
                //    listaCorreoGmail = _repCorreoGmail.GetBy(x => x.IdPersonal != null, 0, Cantidad).Select(x => x.Id).ToList();
                //}
                //else {
                //    listaCorreoGmail = _repCorreoGmail.GetBy(x => x.IdPersonal != null, 0, Cantidad).Select(x => x.Id).ToList();
                //}
                while (maxValue >= IdInicial)
                {
                    listaCorreoGmail = _repCorreoGmail.GetBy(x => x.IdPersonal != null, 0, Cantidad).Select(x => x.Id).ToList();
                    foreach (var item in listaCorreoGmail)
                    {
                        this.ObtenerCuerpoHMTLArchivoAdjunto(item, NombreUsuario);
                    }
                    IdInicial += Cantidad;
                }
                return Ok(listaCorreoGmail);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdCorreoGmail}/{NombreUsuario}")]
        [HttpGet]
        public ActionResult ObtenerCuerpoHMTL(int IdCorreoGmail, string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repCorreoGmail = new CorreoGmailRepositorio(_integraDBContext);
                var _repCorreoGmailArchivoAdjunto = new CorreoGmailArchivoAdjuntoRepositorio(_integraDBContext);

                if (!_repCorreoGmail.Exist(IdCorreoGmail))
                {
                    return BadRequest("No se encontro el registro");
                }

                var correoGmail = _repCorreoGmail.FirstById(IdCorreoGmail);

                var _repGmailFolder = new GmailFolderRepositorio(_integraDBContext);
                var gmailFolder = _repGmailFolder.FirstById(correoGmail.IdGmailFolder);

                if (correoGmail.IdPersonal is null)
                {
                    return BadRequest("No tiene personal");
                }
                var _gmailClienteRepositorio = new GmailClienteRepositorio();
                var correoClienteCredencialDTO = _gmailClienteRepositorio.GetClienteCredencial(correoGmail.IdPersonal.Value);

                var _imapService = new TMK_ImapServiceImpl();
                var mensaje = _imapService.GetBodyCorreo(Convert.ToInt32(correoGmail.GmailCorreoId), correoClienteCredencialDTO.EmailAsesor, correoClienteCredencialDTO.PasswordCorreo, gmailFolder.Nombre);
                mensaje.Parser.PlainToHtmlMode = PlainToHtmlAutoConvert.IfNoHtml;

                CorreoBodyDTO correoBodyDTO = new CorreoBodyDTO();
                string correo = correoClienteCredencialDTO.EmailAsesor.Substring(0, correoClienteCredencialDTO.EmailAsesor.IndexOf('@'));
                correoBodyDTO.EmailBody = "<br>" + mensaje.GetHtmlWithBase64EncodedRelatedFiles() + "<br/>";
                //correoBodyDTO.EmailBody = "<hr>" + mensaje.GetHtmlWithBase64EncodedRelatedFiles() + "<br/>--<br/>" + "<img src='" + _linkRepositorioFirmas + correo + ".png' />";
                correoGmail.CuerpoHtml = correoBodyDTO.EmailBody;

                //CorreoGmailArchivoAdjuntoBO correoGmailArchivoAdjunto;
                //if (mensaje.Attachments.Count > 0)
                //{
                //    correoBodyDTO.ArchivosAdjuntos = new List<CorreoArchivoAdjuntoDTO>();
                //    foreach (Attachment attach in mensaje.Attachments)
                //    {


                //        //CorreoArchivoAdjuntoDTO archivoAdjunto = new CorreoArchivoAdjuntoDTO
                //        //{
                //        //    IdCorreo = correoGmail.Id,
                //        //    NombreArchivo = attach.Filename.ToString()
                //        //};
                //        //correoBodyDTO.ArchivosAdjuntos.Add(archivoAdjunto);
                //        var urlArchivoRepositorio = "";
                //        if (attach != null)
                //        {
                //            var nombreArchivo = string.Concat(correoGmail.Id, "-", attach.Filename);

                //            if (!_repCorreoGmailArchivoAdjunto.Exist(x => x.IdCorreoGmail == correoGmail.Id && x.Nombre.Equals(nombreArchivo)))
                //            {
                //                urlArchivoRepositorio = _repCorreoGmail.SubirArchivoRepositorio(attach.GetData(), attach.ContentType, nombreArchivo);
                //                correoGmailArchivoAdjunto = new CorreoGmailArchivoAdjuntoBO()
                //                {
                //                    Nombre = nombreArchivo,
                //                    UrlArchivoRepositorio = urlArchivoRepositorio,
                //                    Estado = true,
                //                    UsuarioCreacion = NombreUsuario,
                //                    UsuarioModificacion = NombreUsuario,
                //                    FechaCreacion = DateTime.Now,
                //                    FechaModificacion = DateTime.Now
                //                };
                //                correoGmail.CorreoGmailArchivoAdjunto.Add(correoGmailArchivoAdjunto);
                //            }
                //        }
                //    }
                //}
                _repCorreoGmail.Update(correoGmail);
                return Ok(correoGmail);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdInicial}/{IdMaximo}/{Cantidad}")]
        [HttpGet]
        public ActionResult ObtenerCuerpoHTMLCorreoMasivo(int IdInicial, int IdMaximo, int Cantidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repCorreoGmail = new CorreoGmailRepositorio(_integraDBContext);
                var maxValue = IdMaximo;
                var listaCorreoGmail = new List<int>();
                var listaCorrecto = new List<int>();
                var listaIncorrecto = new List<int>();
                while (maxValue >= IdInicial)
                {
                    if (!_repCorreoGmail.ContinuarDescargando())
                    {
                        break;
                    }
                    //listaCorreoGmail = _repCorreoGmail.GetBy(x => x.IdPersonal != null, 0, Cantidad).Select(x => x.Id).ToList();
                    listaCorreoGmail = _repCorreoGmail.GetBy(x => x.IdPersonal != null && string.IsNullOrEmpty(x.CuerpoHtml), 0, Cantidad).Select(x => x.Id).ToList();
                    foreach (var item in listaCorreoGmail)
                    {
                        try
                        {
                            this.ObtenerCuerpoHMTL(item, "system");
                            listaCorrecto.Add(item);
                        }
                        catch (Exception e)
                        {
                            listaIncorrecto.Add(item);
                        }
                    }
                    IdInicial = listaCorreoGmail.FirstOrDefault();

                    IdInicial += Cantidad;
                }
                return Ok(new { listaCorrecto, listaIncorrecto });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdInicial}/{IdMaximo}/{Cantidad}/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCuerpoHTMLCorreoMasivoPorPersonal(int IdInicial, int IdMaximo, int Cantidad, int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repCorreoGmail = new CorreoGmailRepositorio(_integraDBContext);
                var maxValue = IdMaximo;
                var listaCorreoGmail = new List<int>();
                var listaCorrecto = new List<int>();
                var listaIncorrecto = new List<int>();
                while (maxValue >= IdInicial)
                {
                    if (!_repCorreoGmail.ContinuarDescargando())
                    {
                        break;
                    }
                    //listaCorreoGmail = _repCorreoGmail.GetBy(x => x.IdPersonal != null, 0, Cantidad).Select(x => x.Id).ToList();
                    listaCorreoGmail = _repCorreoGmail.GetBy(x => x.IdPersonal != null && x.IdPersonal == IdPersonal && string.IsNullOrEmpty(x.CuerpoHtml), 0, Cantidad).Select(x => x.Id).ToList();
                    foreach (var item in listaCorreoGmail)
                    {
                        try
                        {
                            this.ObtenerCuerpoHMTL(item, "system");
                            listaCorrecto.Add(item);
                        }
                        catch (Exception e)
                        {
                            listaIncorrecto.Add(item);
                        }
                    }
                    IdInicial = listaCorreoGmail.FirstOrDefault();

                    IdInicial += Cantidad;
                }
                return Ok(new { listaCorrecto, listaIncorrecto });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
