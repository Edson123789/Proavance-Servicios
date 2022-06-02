using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;

using BSI.Integra.Aplicacion.Marketing.Repositorio;
using System.Net.Mail;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    ///BO: GmailCorreoBO
    ///Autor: Edgar S.
    ///Fecha: 27/01/2021
    ///<summary>
    ///Columnas y funciones de la tabla T_GmailCorreo
    ///</summary>
    public class GmailCorreoBO : BaseBO
    {
        ///Propiedades		        Significado
        ///-------------	        -----------------------
        ///IdEtiqueta               FK de T_Etiqueta
        ///IdGmailCliente           FK de T_GmailCliente
        ///IdCorreoGmailFormat      FK de T_CorreoGmailFormar
        ///Asunto                   Asunto de Correo
        ///Fecha                    Fecha de Correo
        ///EmailBody                Cuerpo de Mensaje
        ///Seen                     Estado de Visualización de mensaje
        ///Remitente                Remitente de Mensaje
        ///Destinatarios            Destinatarios de Mensaje
        ///IdPersonal               FK de T_Personal
        ///Filas                    Filas
        ///IdInteraccion            Id de Interacción
        ///Cc                       Copia de Correo
        ///ResumenMensaje           Resumen de mensaje
        ///IdMigracion              Id de migración
        ///Bcc                      Copia Oculta de Mensaje
        ///IdClasificacionPersona   FK de T_ClasificacionPersona
        public int? IdEtiqueta { get; set; }
        public int? IdGmailCliente { get; set; }
        public string IdCorreoGmailFormat { get; set; }
        public string Asunto { get; set; }
        public DateTime? Fecha { get; set; }
        public string EmailBody { get; set; }
        public bool? Seen { get; set; }
        public string Remitente { get; set; }
        public string Destinatarios { get; set; }
        public int? IdPersonal { get; set; }
        public int? Filas { get; set; }
        public int? IdInteraccion { get; set; }
        public string Cc { get; set; }
        public string ResumenMensaje { get; set; }
        public Guid? IdMigracion { get; set; }
        public string Bcc { get; set; }
        public int? IdClasificacionPersona { get; set; }

        public List<GmailCorreoArchivoAdjuntoBO> ListaGmailCorreoArchivoAdjunto { get; set; }

        private GmailCorreoRepositorio _gmailCorreoRepositorio = new GmailCorreoRepositorio();


        public GmailCorreoBO ()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            ListaGmailCorreoArchivoAdjunto = new List<GmailCorreoArchivoAdjuntoBO>();
        }

        public GmailCorreoBO( int idGmailCorreo)
        {

            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            ListaGmailCorreoArchivoAdjunto = new List<GmailCorreoArchivoAdjuntoBO>();

            var gmailCorreo = _gmailCorreoRepositorio.FirstById(idGmailCorreo);
            this.Id = gmailCorreo.Id;
            this.IdEtiqueta = gmailCorreo.IdEtiqueta;
            this.IdGmailCliente = gmailCorreo.IdGmailCliente;
            this.IdCorreoGmailFormat = gmailCorreo.IdCorreoGmailFormat;
            this.Asunto = gmailCorreo.Asunto;
            this.Fecha = gmailCorreo.Fecha;
            this.EmailBody = gmailCorreo.EmailBody;
            this.Seen = gmailCorreo.Seen;
            this.Remitente = gmailCorreo.Remitente;
            this.Destinatarios = gmailCorreo.Destinatarios;
            this.IdPersonal = gmailCorreo.IdPersonal;
            this.Filas = gmailCorreo.Filas;
            this.Cc = gmailCorreo.Cc;
            this.ResumenMensaje = gmailCorreo.ResumenMensaje;
            this.Bcc = gmailCorreo.Bcc;
            this.Estado = gmailCorreo.Estado;
            this.FechaCreacion = gmailCorreo.FechaCreacion;
            this.FechaModificacion = gmailCorreo.FechaModificacion;
            this.UsuarioCreacion = gmailCorreo.UsuarioCreacion;
            this.UsuarioModificacion = gmailCorreo.UsuarioModificacion;
            this.RowVersion = gmailCorreo.RowVersion;
            this.IdMigracion = gmailCorreo.IdMigracion;
        }


        /// <summary>
        /// Envia Correos de Gmail
        /// </summary>
        /// <param name="emailDestinatario">Email del destinatario</param>
        /// <param name="emailRemitente">Email remitente</param>
        /// <param name="personal">Nombre del Personal</param>
        /// <param name="clave">Clave de correo</param>
        /// <param name="mensaje">Mensaje email</param>
        /// <param name="asunto">Asunto</param>
        /// <returns> Retorna Confirmación de envío de Correo </returns>
        /// <returns> Bool </returns>
        public bool EnviarCorreoGmail(string emailDestinatario, string emailRemitente, string personal, string clave, string mensaje, string asunto)
		{
			string host = "smtp.gmail.com";
			int port = 587;

			using (SmtpClient smtp = new SmtpClient())
			{
				try
				{
					//CONFIGURACION DEL MENSAJE
					MailMessage mail = new MailMessage();
					mail.To.Add(emailDestinatario);
					mail.From = new MailAddress(emailRemitente, personal, System.Text.Encoding.UTF8);
					mail.Subject = asunto;
					mail.Body = mensaje;
					mail.BodyEncoding = System.Text.Encoding.UTF8;
					mail.IsBodyHtml = true;
					//CONFIGURACIÓN DEL STMP

					smtp.Host = host;
					smtp.Port = port;
					smtp.UseDefaultCredentials = false;
					smtp.Credentials = new System.Net.NetworkCredential(emailRemitente, clave);// Enter seders User name and password
					smtp.EnableSsl = true;

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
    }

}
