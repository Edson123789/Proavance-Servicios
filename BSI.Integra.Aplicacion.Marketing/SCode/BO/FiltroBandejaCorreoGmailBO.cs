using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Servicios.BO;
using MailBee.ImapMail;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FiltroBandejaCorreoGmailBO : BaseBO
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string Folder { get; set; }
        public GridFiltersDTO FiltroKendo { get; set; }
        public List<int> IdGmail { get;set;}

        private int _idAsesor;
        private TMK_ImapServiceImpl _imap;

        public FiltroBandejaCorreoGmailBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }

        /// <summary>
        /// Obtiene la bandeja de Entrada del servicio Imap, de acuerdo al filtrado solicitado por el Asesor.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="passwordCorreo"></param>
        /// <returns></returns>
        public BandejaCorreoDTO GetBandejaEntradaMailInbox(string email, string passwordCorreo)
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
                            objHeader.Destinatarios += "," + msg.Cc.ToString();
                            objHeader.Destinatarios = objHeader.Destinatarios.Replace(email, "");
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

                        objHeader.Destinatarios = objHeader.Destinatarios.Replace(", ", ",");
                        objHeader.Destinatarios = objHeader.Destinatarios.Replace(",,", ",");
                        objHeader.Fecha = msg.DateReceived;
                        objHeader.Asunto = adjunto + msg.Subject.ToString();
                        objHeader.Seen = correoLeido;

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


        public EnvelopeCollection ObtenerBandejaEntradaMailInbox(string email, string passwordCorreo, DateTime fechaInicial, List<long> listaUidsCorreoDescargado)
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
                //MessageNumberCollection numeroMensajes = null;
                UidCollection numeroMensajes = null;
                
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
                            //numeroMensajes = _imap.CantidadCorreosConFiltroAsunto(FiltroKendo.Filters[0].Value);
                            totalCorreos = numeroMensajes.Count;
                            break;
                        case "Remitente":
                            //numeroMensajes = _imap.CantidadCorreosConFiltroRemitente(FiltroKendo.Filters[0].Value);
                            totalCorreos = numeroMensajes.Count;
                            break;
                        case "Fecha":
                            numeroMensajes = _imap.CantidadCorreosConFiltroFecha_ByUid(fechaInicial);//ayer
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
                    //numeroMensajes = _imap.CantidadCorreosConFiltroAsuntoRemitente(asunto, remitente);
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
                            if (long.TryParse(numeroMensajes[i].ToString(), out long outputInt))
                            {
                                if (!listaUidsCorreoDescargado.Contains(outputInt))
                                {
                                    datos_buscados[contador] = numeroMensajes[i].ToString();
                                }
                            }
                            contador++;
                        }
                        rango_datos = string.Join(",", datos_buscados);
                        msgsc = _imap.ObtenerCorreos_ByUid(rango_datos);
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

        public EnvelopeCollection ObtenerBandejaEntradaMailInbox_CuerpoHTML(string email, string passwordCorreo, DateTime fechaInicial, List<long> listaUidsCorreoDescargado)
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
                //MessageNumberCollection numeroMensajes = null;
                UidCollection numeroMensajes = null;

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
                            //numeroMensajes = _imap.CantidadCorreosConFiltroAsunto(FiltroKendo.Filters[0].Value);
                            totalCorreos = numeroMensajes.Count;
                            break;
                        case "Remitente":
                            //numeroMensajes = _imap.CantidadCorreosConFiltroRemitente(FiltroKendo.Filters[0].Value);
                            totalCorreos = numeroMensajes.Count;
                            break;
                        case "Fecha":
                            numeroMensajes = _imap.CantidadCorreosConFiltroFecha_ByUid(fechaInicial);//ayer
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
                    //numeroMensajes = _imap.CantidadCorreosConFiltroAsuntoRemitente(asunto, remitente);
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
                            if (long.TryParse(numeroMensajes[i].ToString(), out long outputInt))
                            {
                                if (!listaUidsCorreoDescargado.Contains(outputInt))
                                {
                                    datos_buscados[contador] = numeroMensajes[i].ToString();
                                }
                            }
                            contador++;
                        }
                        rango_datos = string.Join(",", datos_buscados);
                        msgsc = _imap.ObtenerCorreos_ByUid(rango_datos);
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

    }

}
