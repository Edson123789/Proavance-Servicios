using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Servicios.BO;
using MailBee.ImapMail;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System.Net.Mail;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    /// BO: FiltroBandejaCorreoBO
    /// Autor: ---, Jashin Salazar
    /// Fecha: 29/04/2021
    /// <summary>
    /// BO para la logica de los correos
    /// </summary>
    public class FiltroBandejaCorreoBO : BaseBO
    {
        /// Propiedades         Significado 
        /// ---------           ------------ 
        /// Page                Pagina del correo
        /// PageSize            Tamaño de la pagina
        /// Skip                Salto de pagina
        /// Take                Cantidad de correos 
        /// IdAsesor            Id del Asesor
        /// Folder              Folder del correo
        /// TipoCorreos         Tipo de bandeja de correo
        /// FiltroKendo         Filtro enviado desde kendo
        /// _idAsesor           Id de Asesor 
        /// _imap               Protocolo Imap de correo

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int IdAsesor {
            get { return _idAsesor; }
            set
            {
                ValidarValorMayorCeroProperty(this.GetType().Name, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdAsesor").Name,
                                                  "Identificador del Asesor", value);
                _idAsesor = value;
            }
        }
        public string Folder { get; set; }
        public string TipoCorreos { get; set; }
        public GridFiltersDTO FiltroKendo { get; set; }
        private int _idAsesor;
        private TMK_ImapServiceImpl _imap;

        /// <summary>
        /// Crea un objeto FiltroBandejaCorreoBO
        /// </summary>
        /// <returns></returns>
        public FiltroBandejaCorreoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }

        /// <summary>
        /// Obtiene la bandeja de Entrada del servicio Imap, de acuerdo al filtrado solicitado por el Asesor.
        /// </summary>
        /// <param name="email"> Correo </param>
        /// <param name="passwordCorreo"> Contraseña del correo</param>
        /// <returns>BandejaCorreoDTO</returns>
        public BandejaCorreoDTO GetBandejaEntradaMailInbox(string email, string passwordCorreo, integraDBContext _integraDBContext)
        {
            int firstIndex = 0, lastIndex = 0;
            int primero = 0;
            int segundo = 0;
            bool buscarFiltro = false;
            string rango_datos = string.Empty;
            int totalCorreos = 0;

            try
            {
                BandejaCorreoDTO bandejaCorreo = new BandejaCorreoDTO();
                bandejaCorreo.ListaCorreos = new List<CorreoDTO>();
                MessageNumberCollection numeroMensajes = null;
                EnvelopeCollection msgsc = null;
                AlumnoRepositorio objAlumno = new AlumnoRepositorio(_integraDBContext);
                OportunidadRepositorio objOportunidad = new OportunidadRepositorio(_integraDBContext);
                PersonalRepositorio objPersonal = new PersonalRepositorio(_integraDBContext);


                _imap = new TMK_ImapServiceImpl(Folder, email, passwordCorreo);

                _imap.Folders();

                if (FiltroKendo == null || FiltroKendo.Filters.Count == 0)
                {
                    buscarFiltro = false;
                    totalCorreos = _imap.CantidadCorreosSinFiltro();
                }
                else if (FiltroKendo.Filters.Count == 1)
                {
                    switch (FiltroKendo.Filters[0].Field)
                    {
                        case "Asunto":
                            numeroMensajes = _imap.CantidadCorreosConFiltroAsunto(FiltroKendo.Filters[0].Value);
                            totalCorreos = numeroMensajes.Count;
                            break;
                        case "Remitente":
                            numeroMensajes = _imap.CantidadCorreosConFiltroRemitente(FiltroKendo.Filters[0].Value);
                            totalCorreos = numeroMensajes.Count;
                            break;
                        case "Destinatario":
                            numeroMensajes = _imap.CantidadCorreosConFiltroDestinatario(FiltroKendo.Filters[0].Value);
                            totalCorreos = numeroMensajes.Count;
                            break;
                        case "Fecha":
                            numeroMensajes = _imap.CantidadCorreosConFiltroFecha(DateTime.Today.AddDays(-10));//ayer
                            totalCorreos = numeroMensajes.Count;
                            break;
                    }
                    buscarFiltro = true;
                }
                else if (FiltroKendo.Filters.Count == 2)
                {
                    string asunto = string.Empty, remitente = string.Empty;
                    foreach (var item in FiltroKendo.Filters)
                    {
                        switch (item.Field)
                        {
                            case "Asunto":
                                asunto = item.Value;
                                break;
                            case "Remitente":
                                remitente = item.Value;
                                break;
                        }
                    }
                    numeroMensajes = _imap.CantidadCorreosConFiltroAsuntoRemitente(asunto, remitente);
                    totalCorreos = numeroMensajes.Count;
                    buscarFiltro = true;
                }

                if (totalCorreos > 0)
                {
                    if (!buscarFiltro)
                    {
                        if (totalCorreos > PageSize)
                        {
                            firstIndex = totalCorreos - Skip;
                            if (firstIndex <= PageSize)
                            {
                                lastIndex = 1;
                            }
                            else
                            {
                                lastIndex = firstIndex - (PageSize-1);
                            }
                        }
                        else
                        {
                            firstIndex = 1;
                            lastIndex = totalCorreos;
                        }
                        msgsc = _imap.ObtenerCorreos(firstIndex.ToString() + ":" + lastIndex.ToString());
                    }
                    else
                    {
                        if (totalCorreos > PageSize)
                        {
                            primero = totalCorreos - Skip;
                            segundo = primero - PageSize;
                            if (segundo < 0) segundo = 0;
                        }
                        else
                        {
                            primero = totalCorreos;
                            segundo = 0;
                            PageSize = totalCorreos;
                        }

                        string[] datos_buscados = new string[PageSize];
                        int contador = 0;
                        for (int i = segundo; i < primero; i++)
                        {
                            datos_buscados[contador] = numeroMensajes[i].ToString();
                            contador++;
                        }
                        rango_datos = string.Join(",", datos_buscados);
                        msgsc = _imap.ObtenerCorreos(rango_datos);
                    }
                    string adjunto = string.Empty;
                    bool hasAttachments = false;
                    bool correoLeido = false;

                    foreach (Envelope msg in msgsc)
                    {
                        var objHeader = new CorreoDTO();
                        objHeader.Id = msg.MessageNumber;
                        objHeader.Remitente = msg.From;
                        objHeader.From = email;
                        objHeader.Destinatarios = msg.To;
                        
                        if (msg.Cc.Count > 0)
                        {
                            objHeader.ConCopia = msg.Cc.ToString();
                        }
                        else
                        {
                            objHeader.ConCopia = "";
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
                        if (hasAttachments)
                        {
                            adjunto = "(A) ";
                            hasAttachments = false;
                        }
                        else
                        {
                            adjunto = string.Empty;
                        }

                        objHeader.Fecha = msg.DateReceived;
                        objHeader.Asunto = adjunto + msg.Subject.ToString();
                        objHeader.Seen = correoLeido;
                        if(objHeader.Remitente== "openvox@bsginstitute.com")
                        {
                            var datos = msg.Subject.ToString().Split(" ");
                            if(datos[0]!=null)
                            {
                                var celular = datos[0].Replace("+51", "").Replace("+57", "").Replace("+591", "");
                                var alumno = objAlumno.ObtenerPorCelular(celular, null);
                                var asesor = objPersonal.FirstBy(w=>w.Email== email);
                                if(alumno!=null && asesor != null)
                                {
                                    var oportunidad = objOportunidad.ObtenerOportunidadPorAlumno(alumno.Id, celular);
                                    var enviomasivo = objAlumno.ObtenerEnvioMasivoSMS(alumno.Id);
                                    if (oportunidad!=null)
                                    {
                                        if(enviomasivo!=null)
                                        {
                                            objHeader.Asunto = "ENVIO MASIVO - " + enviomasivo.AreaVentas + " - Respuesta SMS de : (" + alumno.Celular + " - " + alumno.Email1 + ") " + alumno.Nombre1 + " " + alumno.Nombre2 + " " + alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno + " - CC: " + oportunidad.NombreCentroCosto + " - FASE: " + oportunidad.Codigo + " - ASESOR: " + oportunidad.Asesor;
                                        }
                                        else
                                        {
                                            objHeader.Asunto = "Respuesta SMS de : (" + alumno.Celular + " - "+alumno.Email1+") " + alumno.Nombre1 + " " + alumno.Nombre2 + " " + alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno + " - CC: " + oportunidad.NombreCentroCosto + " - FASE: " + oportunidad.Codigo + " - ASESOR: " + oportunidad.Asesor;
                                        }
                                    }
                                    else
                                    {
                                        if (enviomasivo != null)
                                        {
                                            objHeader.Asunto = "ENVIO MASIVO - " + enviomasivo.AreaVentas + " - Respuesta SMS de : (" + alumno.Celular + " - " + alumno.Email1 + ") " + alumno.Nombre1 + " " + alumno.Nombre2 + " " + alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno + " - Sin Oportunidad en Seguimiento";
                                        }
                                        else
                                        {
                                            objHeader.Asunto = "Respuesta SMS de : (" + alumno.Celular + " - " + alumno.Email1 + ") " + alumno.Nombre1 + " " + alumno.Nombre2 + " " + alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno + " - Sin Oportunidad en Seguimiento";
                                        }
                                    }
                                }
                            }
                        }

                        bandejaCorreo.ListaCorreos.Add(objHeader);
                    }
                }

                _imap.Desconectar();

                if (bandejaCorreo.ListaCorreos != null)
                {
                    bandejaCorreo.ListaCorreos = bandejaCorreo.ListaCorreos.OrderByDescending(x => x.Id).ToList();
                    bandejaCorreo.TotalEnviados = totalCorreos;
                    
                }
				return bandejaCorreo;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la bandeja de Entrada dependiendo del tipo de folder.
        /// </summary>
        /// <param name="tipoFolder"> Folder de correo. </param>
        /// <param name="ListaCorreos"> Lista de correos. </param>
        /// <returns>BandejaCorreoDTO</returns>
        public BandejaCorreoDTO GetBandejaEntradaListaMailInbox(string tipoFolder, List<DatosGmailDTO> ListaCorreos)
        {
            int firstIndex = 0, lastIndex = 0;
            string rango_datos = string.Empty;
            int totalCorreos = 0;
            int correosGuardados = 0;
            int _tipoFolder = 0;
            bool bandera = false;

            DateTime fechaInicioDescarga = Convert.ToDateTime("01/01/2018 00:00:00");


            string adjunto = string.Empty;
            bool hasAttachments = false;
            bool correoLeido = false;
            bool conexionRealizada = false;

            try
            {
                BandejaCorreoDTO bandejaCorreo = new BandejaCorreoDTO();
                CorreoGmailRepositorio objGmail = new CorreoGmailRepositorio();

                bandejaCorreo.ListaCorreos = new List<CorreoDTO>();
                MessageNumberCollection numeroMensajes = null;
                EnvelopeCollection msgsc = null;

                if (tipoFolder == "Inbox")
                {
                    _tipoFolder = 1;
                }
                else if (tipoFolder.Contains("Enviados"))
                {
                    tipoFolder = "[Gmail]/Enviados";
                    _tipoFolder = 3;
                }

                foreach (var itemCredencial in ListaCorreos)
                {
                    try
                    {
                        _imap = new TMK_ImapServiceImpl(tipoFolder, itemCredencial.AliasCorreo, itemCredencial.Clave);
                        conexionRealizada = true;
                    }
                    catch (Exception)
                    {
                        conexionRealizada = false;
                    }

                    if (conexionRealizada)
                    {
                        totalCorreos = _imap.CantidadCorreosSinFiltro();
                        //var _dataTotal = _imap.CantidadCorreosConFiltroFecha(fechaInicioDescarga);

                        correosGuardados = objGmail.ContadorCorreosPorPersona(itemCredencial.Id, _tipoFolder);

                        if (totalCorreos > 0)
                        {
                            if (correosGuardados > 0)
                            {
                                if (correosGuardados != totalCorreos && correosGuardados < totalCorreos)
                                {
                                    bandera = true;
                                    firstIndex = correosGuardados + 1;
                                    lastIndex = totalCorreos;
                                }
                                else
                                {
                                    bandera = false;
                                    firstIndex = 0;
                                    lastIndex = 0;
                                }

                            }
                            else
                            {
                                bandera = true;
                                firstIndex = 1;
                                lastIndex = totalCorreos;

                            }


                            if (bandera)
                            {
                                msgsc = _imap.ObtenerCorreos(firstIndex.ToString() + ":" + lastIndex.ToString());
                                
                                foreach (Envelope msg in msgsc)
                                {
                                    List<ValorStringDTO> ListaAsignados = new List<ValorStringDTO>();
                                    if (!msg.From.Email.ToLower().Contains("bsginstitute") && tipoFolder == "Inbox")
                                    {
                                        ListaAsignados = objGmail.ObtenerEmailPersonalAsignado(msg.From.Email);
                                    }
                                    
                                    var objHeader = new TCorreoGmail();

                                    objHeader.IdPersonal = itemCredencial.Id;
                                    objHeader.GmailCorreoId = msg.MessageNumber;

                                    if (tipoFolder == "Inbox")
                                    {
                                        objHeader.IdGmailFolder = 1;
                                    }
                                    else if (tipoFolder == "[Gmail]/Enviados")
                                    {
                                        objHeader.IdGmailFolder = 3;
                                    }

                                    objHeader.Asunto = msg.Subject;
                                    objHeader.Fecha = msg.Date;
                                    objHeader.NombreRemitente = msg.From.DisplayName;
                                    objHeader.EmailRemitente = msg.From.Email;
                                    objHeader.Destinatarios = msg.To;

                                    if (!ListaAsignados.Exists(w => objHeader.Destinatarios.Contains(w.Valor)))
                                    {
                                        if (ListaAsignados.Count > 0)
                                        {
                                            objHeader.Destinatarios = ListaAsignados.FirstOrDefault().Valor + "," + objHeader.Destinatarios;
                                        }
                                        
                                    }
                                                              

                                    string _CC = string.Empty;
                                    string _BCC = string.Empty;

                                    if (msg.Cc.Count > 0)
                                    {
                                        _CC = msg.Cc.AsString;
                                    }
                                    objHeader.EmailConCopia = _CC;
                                    if (msg.Bcc.Count > 0)
                                    {
                                        _BCC = msg.Bcc.AsString;
                                    }
                                    objHeader.EmailConCopiaOculta = _BCC;

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
                                    objHeader.EsLeido = correoLeido;


                                    if (hasAttachments)
                                    {
                                        adjunto = "(A)";
                                        hasAttachments = false;
                                    }
                                    else
                                    {
                                        adjunto = string.Empty;
                                    }


                                    objHeader.Fecha = msg.DateReceived;
                                    objHeader.CuerpoHtml = adjunto;

                                    objHeader.AplicaCrearOportunidad = false;
                                    objHeader.CumpleCriterioCrearOportunidad = false;
                                    objHeader.SeCreoOportunidad = false;

                                    objHeader.Estado = true;
                                    objHeader.FechaCreacion = DateTime.Now;
                                    objHeader.FechaModificacion = DateTime.Now;
                                    objHeader.UsuarioCreacion = "svrecupercion";
                                    objHeader.UsuarioModificacion = "svrecupercion";

                                    objGmail.Insert(objHeader);
                                }
                            }
                        }

                        _imap.Desconectar();
                    }
                    

                    
                }

                if (bandejaCorreo.ListaCorreos != null)
                {
                    bandejaCorreo.ListaCorreos = bandejaCorreo.ListaCorreos.OrderByDescending(x => x.Id).ToList();
                    bandejaCorreo.TotalEnviados = totalCorreos;

                }
                return bandejaCorreo;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene credenciales de correo GMail.
        /// </summary>
        /// <param name="tipoFolder"> Folder de correo. </param>
        /// <param name="ListaCorreos"> Lista de correos. </param>
        /// <returns>BandejaCorreoDTO</returns>
        public BandejaCorreoDTO GetCredencialesCorreoGmail(string tipoFolder, List<DatosGmailDTO> ListaCorreos)
        {
            int totalCorreos = 0;
            bool conexionRealizada = false;

            try
            {
                BandejaCorreoDTO bandejaCorreo = new BandejaCorreoDTO();
                CorreoGmailRepositorio objGmail = new CorreoGmailRepositorio();

                bandejaCorreo.ListaCorreos = new List<CorreoDTO>();
                MessageNumberCollection numeroMensajes = null;
                EnvelopeCollection msgsc = null;

                if (tipoFolder == "Inbox")
                {
                    tipoFolder = "Inbox";
                }
                else if (tipoFolder.Contains("Enviados"))
                {
                    tipoFolder = "[Gmail]/Enviados";
                }

                foreach (var itemCredencial in ListaCorreos)
                {
                    try
                    {
                        _imap = new TMK_ImapServiceImpl(tipoFolder, itemCredencial.AliasCorreo, itemCredencial.Clave);
                        conexionRealizada = true;
                    }
                    catch (Exception ex)
                    {
                        conexionRealizada = false;
                    }

                    if (conexionRealizada)
                    {
                        totalCorreos = _imap.CantidadCorreosSinFiltro();
                        _imap.Desconectar();
                    }

                }

                if (bandejaCorreo.ListaCorreos != null)
                {
                    bandejaCorreo.TotalEnviados = totalCorreos;

                }
                return bandejaCorreo;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la bandeja de Entrada del servicio Imap, de acuerdo al filtrado solicitado por el Asesor y la fecha.
        /// </summary>
        /// <param name="email"> Folder de correo. </param>
        /// <param name="passwordCorreo"> Lista de correos. </param>
        /// <param name="fechaInicial"> Fecha de inicio. </param>
        /// <returns>EnvelopeCollection</returns>
        public EnvelopeCollection ObtenerBandejaEntradaMailInbox(string email, string passwordCorreo, DateTime fechaInicial)
        {
            int firstIndex = 0, lastIndex = 0;
            int primero = 0;
            int segundo = 0;
            bool buscarFiltro = false;
            string rango_datos = string.Empty;
            int totalCorreos = 0;

            try
            {
                BandejaCorreoDTO bandejaCorreo = new BandejaCorreoDTO();
                bandejaCorreo.ListaCorreos = new List<CorreoDTO>();
                MessageNumberCollection numeroMensajes = null;
                EnvelopeCollection msgsc = null;
                _imap = new TMK_ImapServiceImpl(Folder, email, passwordCorreo);
                if (FiltroKendo == null || FiltroKendo.Filters.Count == 0)
                {
                    buscarFiltro = false;
                    totalCorreos = _imap.CantidadCorreosSinFiltro();
                }
                else if (FiltroKendo.Filters.Count == 1)
                {
                    switch (FiltroKendo.Filters[0].Field)
                    {
                        case "Asunto":
                            numeroMensajes = _imap.CantidadCorreosConFiltroAsunto(FiltroKendo.Filters[0].Value);
                            totalCorreos = numeroMensajes.Count;
                            break;
                        case "Remitente":
                            numeroMensajes = _imap.CantidadCorreosConFiltroRemitente(FiltroKendo.Filters[0].Value);
                            totalCorreos = numeroMensajes.Count;
                            break;
                        case "Fecha":
                            numeroMensajes = _imap.CantidadCorreosConFiltroFecha(fechaInicial);//ayer
                            totalCorreos = numeroMensajes.Count;
                            break;
                    }
                    buscarFiltro = true;
                }
                else if (FiltroKendo.Filters.Count == 2)
                {
                    string asunto = string.Empty, remitente = string.Empty;
                    foreach (var item in FiltroKendo.Filters)
                    {
                        switch (item.Field)
                        {
                            case "Asunto":
                                asunto = item.Value;
                                break;
                            case "Remitente":
                                remitente = item.Value;
                                break;
                        }
                    }
                    numeroMensajes = _imap.CantidadCorreosConFiltroAsuntoRemitente(asunto, remitente);
                    totalCorreos = numeroMensajes.Count;
                    buscarFiltro = true;
                }

                if (totalCorreos > 0)
                {
                    if (!buscarFiltro)
                    {
                        if (totalCorreos > PageSize)
                        {
                            firstIndex = totalCorreos - Skip;
                            if (firstIndex <= PageSize)
                            {
                                lastIndex = 1;
                            }
                            else
                            {
                                lastIndex = firstIndex - (PageSize - 1);
                            }
                        }
                        else
                        {
                            firstIndex = 1;
                            lastIndex = totalCorreos;
                        }
                        msgsc = _imap.ObtenerCorreos(firstIndex.ToString() + ":" + lastIndex.ToString());
                    }
                    else
                    {
                        if (totalCorreos > PageSize)
                        {
                            primero = totalCorreos - Skip;
                            segundo = primero - PageSize;
                            if (segundo < 0) segundo = 0;
                        }
                        else
                        {
                            primero = totalCorreos;
                            segundo = 0;
                            PageSize = totalCorreos;
                        }

                        string[] datos_buscados = new string[PageSize];
                        int contador = 0;
                        for (int i = segundo; i < primero; i++)
                        {
                            datos_buscados[contador] = numeroMensajes[i].ToString();
                            contador++;
                        }
                        rango_datos = string.Join(",", datos_buscados);
                        msgsc = _imap.ObtenerCorreos(rango_datos);
                    }
                }
                _imap.Desconectar();
                return msgsc;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Enviar correo con archivo adjunto.
        /// </summary>
        /// <param name="Correo"> Correo. </param>
        /// <param name="Clave"> Contraseña del Correo. </param>
        /// <param name="mailData"> Datos del correo. </param>
        /// <param name="Files"> Archivos adjuntos. </param>
        /// <returns>Booleano</returns>
        public bool envioEmailAdjunto(string Correo, string Clave, TMKMailDataDTO mailData, [FromForm] IList<IFormFile> Files)
        {
            string host = "smtp.gmail.com";
            int port = 587;

            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    //CONFIGURACION DEL MENSAJE
                    MailMessage mail = new MailMessage();
                    mail.To.Add(mailData.Recipient);

                    if (!string.IsNullOrEmpty(mailData.Cc))
                    {
                        var copiasCC = mailData.Cc.Split(',');

                        foreach (var copiaCC in copiasCC)
                        {
                            mail.CC.Add(copiaCC);
                        }
                    }
                    
                    if (!string.IsNullOrEmpty(mailData.Bcc))
                    {
                        var copiasBcC = mailData.Bcc.Split(',');

                        foreach (var copiaBcc in copiasBcC)
                        {
                            mail.CC.Add(copiaBcc);
                        }
                    }
                    

                    mail.From = new MailAddress(Correo, mailData.RemitenteC, System.Text.Encoding.UTF8);
                    mail.Subject = mailData.Subject;
                    mail.Body = mailData.Message;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    //CONFIGURACIÓN DEL STMP

                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(Correo, Clave);// Enter seders User name and password
                    smtp.EnableSsl = true;

                    WhatsAppMensajeRecibidoRepositorio _objetoRecibido = new WhatsAppMensajeRecibidoRepositorio();

                    foreach (var file in Files)
                    {

                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            mail.Attachments.Add(new Attachment(new MemoryStream(fileBytes), file.FileName));
                        }

                    }

                    smtp.Send(mail);
                    mail.Dispose();
                    smtp.Dispose();

                    return true;
                }
                catch (Exception ex)
                {
                    smtp.Dispose();
                    return false;
                }
            }
        }

        /// <summary>
        /// Obtener bandejar de entrada de correo.
        /// </summary>
        /// <returns>BandejaCorreoDTO</returns>
        public BandejaCorreoDTO GetBandejaEntradaMailInbox()
        {
            string rango_datos = string.Empty;
            int _tipoFolder = 0;
            string _queryFiltro = string.Empty;

            try
            {
                BandejaCorreoDTO bandejaCorreo = new BandejaCorreoDTO();
                CorreoGmailRepositorio objGmail = new CorreoGmailRepositorio();

                bandejaCorreo.ListaCorreos = new List<CorreoDTO>();

                if (Folder == "inbox")
                {
                    _tipoFolder = 1;
                }
                else if (Folder == "[Gmail]/Enviados")
                {
                    _tipoFolder = 3;
                }

                if (FiltroKendo == null || FiltroKendo.Filters.Count == 0)
                {
                    _queryFiltro = "";
                    bandejaCorreo.ListaCorreos = objGmail.FiltroCorreosPorPersona(this.IdAsesor, _tipoFolder, _queryFiltro);
                }
                else if (FiltroKendo.Filters.Count == 1)
                {
                    switch (FiltroKendo.Filters[0].Field)
                    {
                        case "Asunto":
                            _queryFiltro = " AND Asunto like '%" + FiltroKendo.Filters[0].Value + "%'";
                            bandejaCorreo.ListaCorreos = objGmail.FiltroCorreosPorPersona(this.IdAsesor, _tipoFolder, _queryFiltro);
                            break;
                        case "Remitente":
                        case "Destinatario":
                            if (_tipoFolder == 1)
                            {
                                _queryFiltro = " AND EmailRemitente like '%" + FiltroKendo.Filters[0].Value + "%'";
                            }
                            else if (_tipoFolder == 3)
                            {
                                _queryFiltro = " AND Destinatarios like '%" + FiltroKendo.Filters[0].Value + "%'";
                            }

                            bandejaCorreo.ListaCorreos = objGmail.FiltroCorreosPorPersona(this.IdAsesor, _tipoFolder, _queryFiltro);
                            break;
                    }
                }
                else if (FiltroKendo.Filters.Count == 2)
                {
                    foreach (var item in FiltroKendo.Filters)
                    {
                        switch (item.Field)
                        {
                            case "Asunto":
                                _queryFiltro += " AND Asunto like '%" + FiltroKendo.Filters[0].Value + "%' ";
                                break;
                            case "Remitente":
                            case "Destinatario":
                                if (_tipoFolder == 1)
                                {
                                    _queryFiltro = " AND EmailRemitente like '%" + FiltroKendo.Filters[0].Value + "%'";
                                }
                                else if (_tipoFolder == 3)
                                {
                                    _queryFiltro = " AND Destinatarios like '%" + FiltroKendo.Filters[0].Value + "%'";
                                }

                                break;
                        }
                    }
                    bandejaCorreo.ListaCorreos = objGmail.FiltroCorreosPorPersona(this.IdAsesor, _tipoFolder, _queryFiltro);
                }

                if (bandejaCorreo.ListaCorreos != null)
                {
                    bandejaCorreo.ListaCorreos = bandejaCorreo.ListaCorreos.OrderByDescending(x => x.Id).ToList();
                    bandejaCorreo.TotalEnviados = bandejaCorreo.ListaCorreos.Count;

                }
                return bandejaCorreo;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtener bandejar de entrada de correo en speech de oportunidad.
        /// </summary>
        /// <returns>BandejaCorreoDTO</returns>
        public BandejaCorreoDTO GetBandejaEntradaMailInboxSpeech()
        {
            string rango_datos = string.Empty;
            int _tipoFolder = 0;
            string _queryFiltro = string.Empty;

            try
            {
                BandejaCorreoDTO bandejaCorreo = new BandejaCorreoDTO();
                CorreoGmailRepositorio objGmail = new CorreoGmailRepositorio();

                bandejaCorreo.ListaCorreos = new List<CorreoDTO>();

                if (Folder == "inbox")
                {
                    _tipoFolder = 1;
                }
                else if (Folder == "[Gmail]/Enviados")
                {
                    _tipoFolder = 3;
                }

                if (FiltroKendo == null || FiltroKendo.Filters.Count == 0)
                {
                    _queryFiltro = "";
                    bandejaCorreo.ListaCorreos = objGmail.FiltroCorreosPorPersona(_tipoFolder, _queryFiltro);
                }
                else if (FiltroKendo.Filters.Count == 1)
                {
                    switch (FiltroKendo.Filters[0].Field)
                    {
                        case "Asunto":
                            _queryFiltro = " AND Asunto like '%" + FiltroKendo.Filters[0].Value + "%'";
                            bandejaCorreo.ListaCorreos = objGmail.FiltroCorreosPorPersona(_tipoFolder, _queryFiltro);
                            break;
                        case "Remitente":
                        case "Destinatario":

                            if (_tipoFolder == 1)
                            {
                                if (TipoCorreos == "Normal")
                                {
                                    _queryFiltro = " AND EmailRemitente like '%" + FiltroKendo.Filters[0].Value + "%' and (EmailConCopiaOculta<>'modpru@bsginstitute.com' and EmailConCopiaOculta<>'modpru@bsgrupo.com')";
                                }
                                else
                                {
                                    _queryFiltro = " AND EmailRemitente like '%" + FiltroKendo.Filters[0].Value + "%' and (EmailConCopiaOculta='modpru@bsginstitute.com' or EmailConCopiaOculta='modpru@bsgrupo.com')";
                                }
                                
                            }
                            else if (_tipoFolder == 3)
                            {
                                if (TipoCorreos == "Normal")
                                {
                                    _queryFiltro = " AND Destinatarios like '%" + FiltroKendo.Filters[0].Value + "%' and (EmailConCopiaOculta<>'modpru@bsginstitute.com' and EmailConCopiaOculta<>'modpru@bsgrupo.com')";
                                }
                                else
                                {
                                    _queryFiltro = " AND Destinatarios like '%" + FiltroKendo.Filters[0].Value + "%' and (EmailConCopiaOculta='modpru@bsginstitute.com' or EmailConCopiaOculta='modpru@bsgrupo.com')";
                                }
                                
                            }

                            bandejaCorreo.ListaCorreos = objGmail.FiltroCorreosPorPersona(_tipoFolder, _queryFiltro);
                            break;
                    }
                }
                else if (FiltroKendo.Filters.Count == 2)
                {
                    foreach (var item in FiltroKendo.Filters)
                    {
                        switch (item.Field)
                        {
                            case "Asunto":
                                _queryFiltro += " AND Asunto like '%" + FiltroKendo.Filters[0].Value + "%' ";
                                break;
                            case "Remitente":
                            case "Destinatario":
                                if (_tipoFolder == 1)
                                {
                                    _queryFiltro = " AND EmailRemitente like '%" + FiltroKendo.Filters[0].Value + "%'";
                                }
                                else if (_tipoFolder == 3)
                                {
                                    _queryFiltro = " AND Destinatarios like '%" + FiltroKendo.Filters[0].Value + "%'";
                                }

                                break;
                        }
                    }
                    bandejaCorreo.ListaCorreos = objGmail.FiltroCorreosPorPersona(_tipoFolder, _queryFiltro);
                }

                if (bandejaCorreo.ListaCorreos != null)
                {
                    bandejaCorreo.ListaCorreos = bandejaCorreo.ListaCorreos.OrderByDescending(x => x.Fecha).ToList();
                    bandejaCorreo.TotalEnviados = bandejaCorreo.ListaCorreos.Count;

                }
                return bandejaCorreo;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtener correos por grupos
        /// </summary>
        /// <param name="IdCentroCosto"> Id del centro de costo</param>
        /// <param name="IdPaquete"> Id de paquete </param>
        /// <param name="Estados"> Estados </param>
        /// <param name="SubEstados"> Sub estados </param>
        /// <returns>Booleano</returns>
        public ListaCorreosGrupoBO obtenerCorreosGrupos(int IdCentroCosto,int IdPaquete, List<int> Estados, List<int> SubEstados)
        {
            string rango_datos = string.Empty;
            int _tipoFolder = 0;
            string _queryFiltro = string.Empty;

            try
            {
                CorreoGmailRepositorio objGmail = new CorreoGmailRepositorio();

                ListaCorreosGrupoBO _respuesta = new ListaCorreosGrupoBO();
                if (IdPaquete == 0)
                {
                    _respuesta = objGmail.obtenerCorreosGruposSinVersion(IdCentroCosto,Estados,SubEstados);
                }
                else
                {
                    _respuesta = objGmail.obtenerCorreosGruposConVersion(IdCentroCosto,IdPaquete,Estados,SubEstados);
                }


                if (_respuesta != null)
                {
                    return _respuesta;

                }
                else
                {
                    ListaCorreosGrupoBO _rptSinDatos = new ListaCorreosGrupoBO();
                    _rptSinDatos.ListaCorreos = "";
                    _rptSinDatos.TotalCorreos = 0;
                    _rptSinDatos.Errores = true;

                    return _rptSinDatos;
                }
                

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
