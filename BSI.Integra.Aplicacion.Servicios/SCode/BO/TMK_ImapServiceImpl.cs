using System;
using System.Collections.Generic;
using System.Text;
using MailBee.ImapMail;
using MailBee.Mime;

namespace BSI.Integra.Aplicacion.Servicios.BO
{
    public class TMK_ImapServiceImpl
    {
        private static string _licenseMailbee = "MN110-41C9EF25D7AB64A6D1A21D2E5135-4113";
        private Imap _imap = new Imap(_licenseMailbee);

        public TMK_ImapServiceImpl()
        {

        }

        public TMK_ImapServiceImpl(string folder,string email, string passwordCorreo)
        {
            _imap.Connect("imap.gmail.com", 993);
            _imap.Login(email, passwordCorreo);
            _imap.SelectFolder(@folder);
        }

        /// <summary>
        /// Obitene el archivo adjunto de un correo electronico
        /// </summary>
        /// <param name="Id">Id del correo electronico</param>
        /// <param name="correo">Cuenta del usuario</param>
        /// <param name="pass">Contraseña del correo electronico</param>
        /// <param name="nombreArchivo">Nombre del archivo a descargar</param>
        /// <returns>Returna el archivo adjunto en un arreglode bytes</returns>
        public byte[] DownloadFileEmailInbox(int id, string correo, string pass, string nombreArchivo, string folder)
        {           
            byte[] archivo = null;
            try
            {
                if (folder == "spam") folder = "[Gmail]/Spam";
                MailMessage msg = Imap.QuickDownloadMessage("imap.gmail.com", correo, pass, folder, id);
                if (msg.Attachments.Count > 0)
                {
                    foreach (Attachment attach in msg.Attachments)
                    {
                        if (attach.Filename.Contains("%2c"))
                        {
                            nombreArchivo = nombreArchivo.Replace(",", "%2c");
                        }
                        if (attach.Filename == nombreArchivo)
                        {
                            archivo = attach.GetData();
                            break;
                        }
                    }
                }
                if(archivo!= null)
                    return archivo;
                else
                    throw new Exception("No se encontro el Archivo Indicado");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public byte[] DownloadFileEmailInbox_ByUid(int messageNumber, string correo, string pass, string nombreArchivo, string folder)
        {
            byte[] archivo = null;
            try
            {
                if (folder == "spam") folder = "[Gmail]/Spam";
                var msg = Imap.QuickDownloadMessage("imap.gmail.com", correo, pass, folder, messageNumber);
                if (msg.Attachments.Count > 0)
                {
                    foreach (Attachment attach in msg.Attachments)
                    {
                        if (attach.Filename.Contains("%2c"))
                        {
                            nombreArchivo = nombreArchivo.Replace(",", "%2c");
                        }
                        if (attach.Filename == nombreArchivo)
                        {
                            archivo = attach.GetData();
                            break;
                        }
                    }
                }
                if (archivo != null)
                    return archivo;
                else
                    throw new Exception("No se encontro el Archivo Indicado");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public MailMessage GetBodyCorreo(int id, string correo, string pass, string folder)
        {
            try
            {
                MailMessage mensaje = Imap.QuickDownloadMessage("imap.gmail.com", correo, pass, folder, id);
                return mensaje;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public MailMessage GetBodyCorreo_byUid(int messageNumber, string correo, string pass, string folder)
        {
            try
            {
                MailMessage mensaje = Imap.QuickDownloadMessage("imap.gmail.com", correo, pass, folder, messageNumber);
                return mensaje;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //public MailMessage GetBodyCorreo_byUid_v2(long messageIndex, string correo, string pass, string folder)
        //{
        //    try
        //    {
        //        MailMessage mensaje = Imap.DownloadEntireMessage(messageIndex, true);
        //        return mensaje;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}


        public int CantidadCorreosSinFiltro()
        {
            try
            {
                return _imap.MessageCount;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MessageNumberCollection CantidadCorreosConFiltroAsunto(string valor)
        {
            try
            {
                MessageNumberCollection NumeroMensajes = (MessageNumberCollection)_imap.Search(false, "SUBJECT " + ImapUtils.ToQuotedString(valor), null);
                return NumeroMensajes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Retorna los mensajes 
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public MessageNumberCollection CantidadCorreosConFiltroFecha(DateTime date)
        {
            try
            {
                //MessageNumberCollection NumeroMensajes = (MessageNumberCollection)_imap.Search(false, "ON  " + ImapUtils.ToQuotedString(valor), null);
                MessageNumberCollection NumeroMensajes = (MessageNumberCollection)_imap.Search(false, "SINCE \"" + ImapUtils.GetImapDateString(date) + "\"", null);

                return NumeroMensajes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public UidCollection CantidadCorreosConFiltroFecha_ByUid(DateTime date)
        {
            try
            {
                var stringDate = ImapUtils.GetImapDateString(date);
                UidCollection NumeroMensajes = (UidCollection)_imap.Search(true, "SINCE \"" + ImapUtils.GetImapDateString(date) + "\"", null);
                return NumeroMensajes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public MessageNumberCollection CantidadCorreosConFiltroRemitente(string valor)
        {
            try
            {
                MessageNumberCollection NumeroMensajes = (MessageNumberCollection)_imap.Search(false, "FROM " + ImapUtils.ToQuotedString(valor), System.Text.Encoding.UTF8.WebName);
                return NumeroMensajes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MessageNumberCollection CantidadCorreosConFiltroDestinatario(string valor)
        {
            try
            {
                MessageNumberCollection NumeroMensajes = (MessageNumberCollection)_imap.Search(false, "TO " + ImapUtils.ToQuotedString(valor), System.Text.Encoding.UTF8.WebName);
                return NumeroMensajes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MessageNumberCollection CantidadCorreosConFiltroAsuntoRemitente(string valorAsunto, string valorRemitente)
        {
            try
            {
                MessageNumberCollection NumeroMensajes = (MessageNumberCollection)_imap.Search(false, "SUBJECT " + ImapUtils.ToQuotedString(valorAsunto) + " FROM " + ImapUtils.ToQuotedString(valorRemitente), System.Text.Encoding.UTF8.WebName);
                return NumeroMensajes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public EnvelopeCollection ObtenerCorreos(string indices)
        {
            try
            {
                EnvelopeCollection listaCorreos = _imap.DownloadEnvelopes(indices, false, EnvelopeParts.All, 0);
                return listaCorreos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public EnvelopeCollection ObtenerCorreos_ByUid(string indices)
        {
            try
            {
                EnvelopeCollection listaCorreos = _imap.DownloadEnvelopes(indices, true, EnvelopeParts.All, 0);
                return listaCorreos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Desconectar()
        {
            try
            {
                _imap.Disconnect();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Folders()
        {
            try
            {
                var datos = _imap.DownloadFolders(false);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
