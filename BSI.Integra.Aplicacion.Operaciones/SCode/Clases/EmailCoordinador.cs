using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using MailBee.Mime;
using MailBee.SmtpMail;
using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSI.Integra.Aplicacion.Operaciones.Clases
{
    public class EmailCoordinador
    {
        private Smtp _mailer;
        private LicenciaRepositorio _repLicencia = new LicenciaRepositorio();
        private RaRemitenteRepositorio _repRemitente = new RaRemitenteRepositorio();

        public bool prueba { get; set; }

        public EmailCoordinador()
        {
            MailBee.Global.LicenseKey = _repLicencia.ObtenerPorNombre("MailBee").Data;
            this.prueba = true;
        }

        private string EnviarMails()
        {
            try
            {
                _mailer.Send();
                return "OK";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            //_mailer.SendMailMerge(null, null, WorkTable);
        }

        private RemitenteDTO ObtenerRemitente(string usuario)
        {
            if (_repRemitente.GetBy(x => x.Usuario == usuario).Count() > 0)
            {
                var remitente = _repRemitente.ObtenerPorUsuario(usuario);
                return new RemitenteDTO() { Nombre = remitente.Nombre, Clave = remitente.ClaveAplicacion, Correo = remitente.Correo, Firma = remitente.RutaFirma + remitente.Firma, AliasCorreo = remitente.AliasCorreo };
            }
            return null;
        }

        ////Envíos Constancia
        //public RespuestaWeb Enviar_Confirmacion_GeneracionConstancia(ra_ConstanciaAlumno constancia, Coordinadoras coordinador, byte[] constancia_membrete, byte[] constancia_sin_membrete)
        //{
        //    try
        //    {
        //        _mailer = new Smtp();

        //        Remitente remitente = ObtenerRemitente("modpru");

        //        var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
        //        _mailer.Message.Subject = "Confirmación - Generación de Constancia de " + constancia.Tipo + " - " + constancia.CodigoAlumno;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = coordinador.AliasCorreo;
        //            _mailer.Message.Cc.AsString = remitente.AliasCorreo + ";medinac@bsginstitute.com;" + "jeperez@bsginstitute.com;" + "echirinos@bsginstitute.com;" + "adespinoza@bsginstitute.com;";
        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesCoordinador();
        //        correo.Confirmacion_EmisionConstancia(constancia, coordinador);

        //        //adjuntos
        //        _mailer.Message.Attachments.Add(constancia_membrete, "Constancia-Membretada.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir67fdgdf1781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
        //        _mailer.Message.Attachments.Add(constancia_sin_membrete, "Constancia-SinMembrete.pdf", "<12s4a8a87dsgd78c$5664i1b1$dsgir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

        //        //firma
        //        //var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/" + remitente.Firma;
        //        //_mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = correo.mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Confirmación de Generación de Constancia - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Confirmación de Generación de Constancia. " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Confirmación de Generación de Constancia. " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}

        //internal RespuestaWeb Enviar_SolicitudConfirmacion_PagoConstancia(ra_ConstanciaAlumno constancia, Coordinadoras coordinador)
        //{
        //    try
        //    {
        //        _mailer = new Smtp();

        //        Remitente remitente = ObtenerRemitente("modpru");

        //        var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
        //        _mailer.Message.Subject = "Solicitud de Confirmación - Pago Constancia " + constancia.Tipo + " - " + constancia.CodigoAlumno;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = "pbeltran@bsginstitute.com;";
        //            _mailer.Message.Cc.AsString = coordinador.AliasCorreo + ";" + remitente.AliasCorreo + ";medinac@bsginstitute.com;" + "jeperez@bsginstitute.com;" + "echirinos@bsginstitute.com;" + "adespinoza@bsginstitute.com;";
        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesCoordinador();
        //        correo.SolicitudConfirmacion_PagoConstancia(constancia, coordinador);

        //        //adjuntos
        //        //_mailer.Message.Attachments.Add(constancia_membrete, "Constancia-Membretada.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir67fdgdf1781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

        //        //firma
        //        //var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/operaciones/" + remitente.Firma;
        //        //_mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = correo.mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Confirmación de Pago de Constancia - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Confirmación de Pago de Constancia. " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Confirmación de Pago de Constancia. " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}

        //internal RespuestaWeb Enviar_Constancia_Coordinador(ra_ConstanciaAlumno constancia, Coordinadoras coordinador, byte[] constancia_membrete, byte[] constancia_sin_membrete)
        //{
        //    try
        //    {
        //        _mailer = new Smtp();

        //        Remitente remitente = ObtenerRemitente("modpru");
        //        //Remitente remitente = new Remitente("Mod Pru", "modpru@bsginstitute.com", "BSgrupo123", "echirinos.png");
        //        //Remitente remitente = new Remitente("Esther Chirinos", "echirinos@bsginstitute.com", "nsvwpnbbqpwmnxpf", "echirinos.png");

        //        var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
        //        _mailer.Message.Subject = "Constancia de " + constancia.Tipo + " - " + constancia.CodigoAlumno;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = coordinador.AliasCorreo;
        //            //_mailer.Message.Cc.AsString = remitente.Correo + ";medinac@bsginstitute.com;";
        //            //_mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesCoordinador();
        //        correo.Constancia_Coordinador(constancia, coordinador);

        //        //adjuntos
        //        if (constancia_membrete != null)
        //            _mailer.Message.Attachments.Add(constancia_membrete, "Constancia-Membretada.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir67fdgdf1781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
        //        if (constancia_sin_membrete != null)
        //            _mailer.Message.Attachments.Add(constancia_sin_membrete, "Constancia-SinMembrete.pdf", "<12s4a8a87dsgd78c$5664i1b1$dsgir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

        //        ////firma
        //        //var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/operaciones/" + remitente.Firma;
        //        //_mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = correo.mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Envío de Constancia al Coordinador - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar la Constancia al Coordinador . " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Envío de Constancia al Coordinador . " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}

        ////Envios Certificados
        //internal RespuestaWeb Enviar_Certificado_Alumno(AlumnosMatriculados alumno, ra_Certificado_Detalle detalle_certificado, ra_Certificado_Envio envio, Coordinadoras coordinador, byte[] certificado_frontal, byte[] certificado_reverso, bool solo_digital)
        //{
        //    try
        //    {
        //        _mailer = new Smtp();

        //        //Remitente remitente = new Remitente("Mod Pru", "modpru@bsginstitute.com", "BSgrupo123", "echirinos.png");
        //        //Remitente remitente = new Remitente("Esther Chirinos", "echirinos@bsginstitute.com", "nsvwpnbbqpwmnxpf", "echirinos.png");

        //        var server = new SmtpServer("smtp.gmail.com", coordinador.Correo, coordinador.Password);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = coordinador.Coordinadora + "<" + coordinador.AliasCorreo + ">";
        //        if (solo_digital)
        //            _mailer.Message.Subject =
        //                $"CERTIFICADO DIGITAL - {alumno.centrocosto} - {detalle_certificado.CodigoAlumno}";
        //        else
        //            _mailer.Message.Subject =
        //                $"ENTREGA DE CERTIFICADO - {alumno.centrocosto} - {detalle_certificado.CodigoAlumno}";

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = alumno.email1 + ";" + alumno.email2;
        //            _mailer.Message.Cc.AsString = coordinador.AliasCorreo;
        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesCoordinador();
        //        if (solo_digital)
        //            correo.Certificado_Alumno_SoloDigital(alumno, detalle_certificado, coordinador);
        //        else
        //        {
        //            switch (envio.ra_Certificado_TipoEnvio.Proveedor)
        //            {
        //                case TipoEnvio_Certificado.BSArequipa:
        //                    correo.Certificado_Alumno_Recogo_Arequipa(alumno, detalle_certificado, envio, coordinador);
        //                    break;
        //                case TipoEnvio_Certificado.BSLima:
        //                    correo.Certificado_Alumno_Recogo_Lima(alumno, detalle_certificado, envio, coordinador);
        //                    break;
        //                case TipoEnvio_Certificado.BSColombia:
        //                    correo.Certificado_Alumno_Recogo_Bogota(alumno, detalle_certificado, envio, coordinador);
        //                    break;
        //                case TipoEnvio_Certificado.OLVA:
        //                    correo.Certificado_Alumno_Recogo_OLVA(alumno, detalle_certificado, envio, coordinador);
        //                    break;
        //                case TipoEnvio_Certificado.TNT:
        //                    correo.Certificado_Alumno_Recogo_TNT(alumno, detalle_certificado, envio, coordinador);
        //                    break;
        //            }
        //        }

        //        //adjuntos
        //        if (certificado_frontal != null)
        //            _mailer.Message.Attachments.Add(certificado_frontal, "Certificado-Frontal.pdf", "<12s4asdghd8a87dsgd78c$5664i1b1$ir67fdgdf1781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
        //        if (certificado_reverso != null)
        //            _mailer.Message.Attachments.Add(certificado_reverso, "Certificado-Reverso.pdf", "<1sdgda8a87dsgd78c$5664i1b1$dsgir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

        //        //firma
        //        var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/coordinadores/" + coordinador.Firma;
        //        _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        //firma
        //        var rutaEncabecado = HostingEnvironment.ApplicationPhysicalPath + "Content/img/cabecera.png";
        //        _mailer.Message.Attachments.Add(rutaEncabecado, null, "cabecera");

        //        _mailer.Message.BodyHtmlText = correo.mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Envío de Certificado al Alumno - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Certificado al Alumno. " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Envío de Certificado al Alumno. " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}

        //Envios Historial Cambio Alumno
        public RespuestaWebDTO EnviarHistorialCambioAlumnoRegistro(HistorialCambioDetalleDTO solicitud, AlumnoMatriculadoDatoDTO alumno, CoordinadoraBO coordinadorSolicitante)
        {
            try
            {
                RespuestaWebDTO respuesta = new RespuestaWebDTO();
                _mailer = new Smtp();
                //Remitente remitente = ObtenerRemitente(nombre_usuario);

                var server = new SmtpServer("smtp.gmail.com", coordinadorSolicitante.Correo, coordinadorSolicitante.Password);
                //server.SslMode = SslStartupMode.UseStartTls;
                server.Port = 465; //587
                _mailer.SmtpServers.Add(server);

                // Set static From and Subject.
                _mailer.Message.From.AsString = coordinadorSolicitante.NombreResumido + "<" + coordinadorSolicitante.AliasCorreo + ">";
                _mailer.Message.Subject = "Solicitud de " + solicitud.TipoHistoricoCambioAlumno + " - " + solicitud.CodigoAlumno;

                // Set templates for To, body, and attachment.
                if (this.prueba)
                {
                    _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
                    _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
                }
                else
                {
                    _mailer.Message.To.AsString = "bamontoya@bsginstitute.com;";
                    _mailer.Message.Cc.AsString = coordinadorSolicitante.AliasCorreo;
                    if (solicitud.CentroCostoOrigen.Contains("ONLINE"))
                        _mailer.Message.Cc.AsString += ";" + "atorres@bsginstitute.com;";
                    _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
                }

                var correo = new MailTemplateCoordinador();
                correo.HistorialCambioAlumnoRegistro(solicitud, alumno, coordinadorSolicitante, this.prueba);

                ////adjuntos
                //if (certificado_reverso != null)
                //    _mailer.Message.Attachments.Add(certificado_reverso, "Certificado-Reverso.pdf", "<1sdgda8a87dsgd78c$5664i1b1$dsgir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

                //firma
                var rutaFirma = new HostingEnvironment().WebRootPath + "Content/img/firmas/coordinadores/" + coordinadorSolicitante.Firma;
                _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

                _mailer.Message.BodyHtmlText = correo.Mensaje;

                var resultado_envio = EnviarMails();

                respuesta.Estado = true;
                respuesta.Mensaje = "Correo de Envío de Solicitud de Cambio - enviado correctamente";
                return respuesta;
            }
            catch (Exception e)
            {
                var mensaje = "Hubo un error al generar el Correo de Solicitud de Cambio. " + e.Message;
                throw new Exception(mensaje);
            }
        }
        public RespuestaWebDTO EnviarHistorialCambioAlumnoCancelar(HistorialCambioDetalleDTO solicitud, AlumnoMatriculadoDatoDTO alumno, CoordinadoraBO coordinadorSolicitante)
        {
            try
            {
                RespuestaWebDTO respuesta = new RespuestaWebDTO();

                _mailer = new Smtp();

                RemitenteDTO remitente = ObtenerRemitente("modpru");

                var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
                //server.SslMode = SslStartupMode.UseStartTls;
                server.Port = 465; //587
                _mailer.SmtpServers.Add(server);

                // Set static From and Subject.
                _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
                _mailer.Message.Subject = "Cancelación - Solicitud de " + solicitud.TipoHistoricoCambioAlumno + " - " + solicitud.CodigoAlumno;

                // Set templates for To, body, and attachment.
                if (this.prueba)
                {
                    _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
                    _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
                }
                else
                {
                    _mailer.Message.To.AsString = "bamontoya@bsginstitute.com;";
                    _mailer.Message.Cc.AsString = coordinadorSolicitante.AliasCorreo;
                    if (solicitud.CentroCostoOrigen.Contains("ONLINE"))
                        _mailer.Message.Cc.AsString += ";" + "atorres@bsginstitute.com;";
                    _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
                }

                var correo = new MailTemplateCoordinador();
                correo.HistorialCambioAlumnoCancelar(solicitud, alumno, coordinadorSolicitante, this.prueba);

                ////adjuntos
                //if (certificado_reverso != null)
                //    _mailer.Message.Attachments.Add(certificado_reverso, "Certificado-Reverso.pdf", "<1sdgda8a87dsgd78c$5664i1b1$dsgir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

                ////firma
                //var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/coordinadores/" + coordinador_solicitante.Firma;
                //_mailer.Message.Attachments.Add(rutaFirma, null, "firma");

                _mailer.Message.BodyHtmlText = correo.Mensaje;

                // Generate and send e-mails.
                var resultado_envio = EnviarMails();
                respuesta.Estado = true;
                respuesta.Mensaje = "Correo de Envío de Cancelación de Solicitud de Cambio - enviado correctamente";
                return respuesta;
            }
            catch (Exception e)
            {
                var mensaje = "Hubo un error al generar el Correo de Cancelación de Solicitud de Cambio. " + e.Message;
                throw new Exception(mensaje);
            }
        }
        public RespuestaWebDTO EnviarHistorialCambioAlumnoAprobar(HistorialCambioDetalleDTO solicitud, AlumnoMatriculadoDatoDTO alumno, CoordinadoraBO coordinadorSolicitante)
        {
            try
            {
                _mailer = new Smtp();
                RespuestaWebDTO respuesta = new RespuestaWebDTO();
                RemitenteDTO remitente = ObtenerRemitente("modpru");

                var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave)
                {
                    //server.SslMode = SslStartupMode.UseStartTls;
                    Port = 465 //587
                };
                _mailer.SmtpServers.Add(server);

                // Set static From and Subject.
                _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
                if (solicitud.Aprobado == true)
                    _mailer.Message.Subject = "Aprobación - Solicitud de " + solicitud.TipoHistoricoCambioAlumno + " - " + solicitud.CodigoAlumno;
                else
                    _mailer.Message.Subject = "Desaprobación - Solicitud de " + solicitud.TipoHistoricoCambioAlumno + " - " + solicitud.CodigoAlumno;

                // Set templates for To, body, and attachment.
                if (this.prueba)
                {
                    _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
                    _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
                }
                else
                {
                    _mailer.Message.To.AsString = "bamontoya@bsginstitute.com;";
                    _mailer.Message.Cc.AsString = coordinadorSolicitante.AliasCorreo;
                    if (solicitud.CentroCostoOrigen.Contains("ONLINE"))
                        _mailer.Message.Cc.AsString += ";" + "atorres@bsginstitute.com;";
                    _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
                }

                var correo = new MailTemplateCoordinador();
                correo.HistorialCambioAlumnoAprobar(solicitud, alumno, coordinadorSolicitante, this.prueba);

                ////adjuntos
                //if (certificado_reverso != null)
                //    _mailer.Message.Attachments.Add(certificado_reverso, "Certificado-Reverso.pdf", "<1sdgda8a87dsgd78c$5664i1b1$dsgir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

                ////firma
                //var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/coordinadores/" + coordinador_solicitante.Firma;
                //_mailer.Message.Attachments.Add(rutaFirma, null, "firma");

                _mailer.Message.BodyHtmlText = correo.Mensaje;

                // Generate and send e-mails.
                var resultado_envio = EnviarMails();
                respuesta.Estado = true;
                respuesta.Mensaje = "Correo de Envío de Cancelación de Solicitud de Cambio - enviado correctamente";
                return respuesta;
            }
            catch (Exception e)
            {
                var mensaje = "Hubo un error al generar el Correo de Cancelación de Solicitud de Cambio. " + e.Message;
                throw new Exception(mensaje);
            }
        }

        ////Envios Solicitud Acceso Portal Web
        //public RespuestaWeb Enviar_Coordinador_SolicitudAcceso_PortalWeb_Registrar(Registro_Solicitud_AccesoPortalWebViewModel solicitud, Coordinadoras coordinador)
        //{
        //    try
        //    {
        //        _mailer = new Smtp();

        //        //Remitente remitente = ObtenerRemitente("modpru");
        //        Remitente remitente = new Remitente(coordinador.Coordinadora, coordinador.Correo, coordinador.Password,
        //            coordinador.Firma);

        //        var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
        //        _mailer.Message.Subject = "Solicitud de Creación de Accesos Portal Web - " + solicitud.CodigoAlumno;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = "jrivera@bsginstitute.com;";
        //            _mailer.Message.Cc.AsString = remitente.AliasCorreo;
        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesCoordinador();
        //        correo.SolicitudAcceso_PortalWeb_Registrar(solicitud, coordinador);

        //        ////adjuntos
        //        //if (certificado_reverso != null)
        //        //    _mailer.Message.Attachments.Add(certificado_reverso, "Certificado-Reverso.pdf", "<1sdgda8a87dsgd78c$5664i1b1$dsgir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

        //        //firma
        //        var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/coordinadores/" + coordinador.Firma;
        //        _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = correo.mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Envío de Solicitud Accesos Portal Web - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el correo de Solicitud Accesos Portal Web. " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Solicitud Accesos Portal Web. " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}

        //public RespuestaWeb Enviar_Coordinador_SolicitudAcceso_PortalWeb_RegistrarMultiple(List<Registro_Solicitud_AccesoPortalWebViewModel> listado_solicitud, Coordinadoras coordinador)
        //{
        //    try
        //    {
        //        _mailer = new Smtp();

        //        //Remitente remitente = ObtenerRemitente("modpru");
        //        Remitente remitente = new Remitente(coordinador.Coordinadora, coordinador.Correo, coordinador.Password,
        //            coordinador.Firma);

        //        var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
        //        _mailer.Message.Subject = "Solicitud de Creación de Accesos Portal Web";

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = "jrivera@bsginstitute.com;" + "rpuma@bsginstitute.com;";
        //            _mailer.Message.Cc.AsString = remitente.AliasCorreo;
        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesCoordinador();
        //        correo.SolicitudAcceso_PortalWeb_RegistrarMultiple(listado_solicitud, coordinador);

        //        ////adjuntos
        //        //if (certificado_reverso != null)
        //        //    _mailer.Message.Attachments.Add(certificado_reverso, "Certificado-Reverso.pdf", "<1sdgda8a87dsgd78c$5664i1b1$dsgir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

        //        //firma
        //        var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/coordinadores/" + coordinador.Firma;
        //        _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = correo.mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Envío de Solicitud Accesos Portal Web - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el correo de Solicitud Accesos Portal Web. " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Solicitud Accesos Portal Web. " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}

        ////Envios Solicitud Acceso Aula Virtual
        //public RespuestaWeb Enviar_AccesoAulaVirtual_Alumno(AccesoAulaVirtual_ModeloCorreo modelo, string mensaje, string nombre_usuario)
        //{
        //    RespuestaWeb respuesta = new RespuestaWeb();
        //    try
        //    {
        //        _mailer = new Smtp();

        //        //Remitente remitente = ObtenerRemitente(modelo.CentroCosto.ResponsableCoordinacion);
        //        Coordinadoras coordinador = modelo.Coordinador;

        //        //cambia el coordinador si es de bogota
        //        if (modelo.Alumno.centrocosto.Contains("BOGOTA") && !modelo.Alumno.centrocosto.Contains("ONLINE"))
        //            coordinador = analisis.Coordinadoras.FirstOrDefault(w => w.coordinadoraacademica == "esanchez");

        //        var server = new SmtpServer("smtp.gmail.com", coordinador.Correo, coordinador.Password);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = coordinador.Coordinadora + "<" + coordinador.AliasCorreo + ">";
        //        _mailer.Message.Subject = "Cronograma y accesos - " + modelo.Alumno.centrocosto + " - " + modelo.Alumno.CodigoAlumno;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = modelo.Alumno.email1 + ";" + modelo.Alumno.email2;
        //            _mailer.Message.Cc.AsString = coordinador.AliasCorreo;

        //            //coloca en copia segun sede
        //            if (modelo.Alumno.centrocosto.Contains("AQP"))
        //                _mailer.Message.Cc.AsString += ";" + "ehuamani@bsginstitute.com;";

        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;" + "controldeaccesos@bsginstitute.com;";
        //            if (modelo.Alumno.centrocosto.Contains("ONLINE"))
        //                _mailer.Message.Bcc.AsString += ";" + "krueda@bsginstitute.com;";
        //        }

        //        //var correo = new MailTemplatesPresencial();
        //        //correo.Enviar_SolicitudMaterialDocente(modelo);

        //        //adjuntos
        //        var rutaCabecera = HostingEnvironment.ApplicationPhysicalPath + "Content/img/" + "cabecera.png";
        //        _mailer.Message.Attachments.Add(rutaCabecera, null, "cabecera");

        //        var rutaManual = HostingEnvironment.ApplicationPhysicalPath + "Content/Attachments/Coordinador/AccesoAulaVirtual/MANUAL ALUMNOS AULA VIRTUAL BS GRUPO V3 LIMA - AREQUIPA.pdf";
        //        _mailer.Message.Attachments.Add(rutaManual, null, "manual");

        //        //firma
        //        var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/coordinadores/" + coordinador.Firma;
        //        _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Cronograma y accesos - enviado correctamente.";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //        }
        //        else
        //        {
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = $"Hubo un error al intentar enviar el de Cronograma y accesos - {resultado_envio}.";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = $"Hubo un error al generar el Correo Cronograma y accesos - {exception.Message}.";
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //    }
        //    return respuesta;
        //}

        //public RespuestaWeb Enviar_AccesoAulaVirtual_Alumno_ManualBsPlay(ra_AccesoAulaVirtual_DatosCronograma alumno, Coordinadoras coordinador)
        //{
        //    RespuestaWeb respuesta = new RespuestaWeb();
        //    try
        //    {
        //        _mailer = new Smtp();

        //        //Remitente remitente = ObtenerRemitente(modelo.CentroCosto.ResponsableCoordinacion);

        //        var server = new SmtpServer("smtp.gmail.com", coordinador.Correo, coordinador.Password);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = coordinador.Coordinadora + "<" + coordinador.AliasCorreo + ">";
        //        _mailer.Message.Subject = "Manual BS Play - " + alumno.CodigoAlumno;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = alumno.email1 + ";" + alumno.email2;
        //            _mailer.Message.Cc.AsString = coordinador.AliasCorreo;

        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesCoordinador();
        //        correo.Envio_ManualBsPlay(alumno, coordinador);

        //        ////adjuntos
        //        //var rutaCabecera = HostingEnvironment.ApplicationPhysicalPath + "Content/img/" + "cabecera.png";
        //        //_mailer.Message.Attachments.Add(rutaCabecera, null, "cabecera");

        //        //firma
        //        var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/coordinadores/" + coordinador.Firma;
        //        _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = correo.mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Manual Bs Play - enviado correctamente.";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //        }
        //        else
        //        {
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = $"Hubo un error al intentar enviar el Correo de Manual Bs Play - {resultado_envio}.";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = $"Hubo un error al generar el Correo de Manual Bs Play - {exception.Message}.";
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //    }
        //    return respuesta;
        //}

        //public RespuestaWeb Enviar_AccesoAulaVirtual_Aviso(AccesoAulaVirtual_ModeloCorreo modelo)
        //{
        //    RespuestaWeb respuesta = new RespuestaWeb();
        //    try
        //    {
        //        _mailer = new Smtp();

        //        Remitente remitente = ObtenerRemitente("modpru");

        //        var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
        //        if (modelo.Solicitud.ra_AccesoAulaVirtual_Tipo.Nombre == Tipo_AccesoAulaVirtual.Regular || modelo.Solicitud.ra_AccesoAulaVirtual_Tipo.Nombre == Tipo_AccesoAulaVirtual.Traslado)
        //            _mailer.Message.Subject = "MATRÍCULA - " + modelo.Alumno.Alumno + " - " + modelo.Alumno.CodigoAlumno;
        //        if (modelo.Solicitud.ra_AccesoAulaVirtual_Tipo.Nombre == Tipo_AccesoAulaVirtual.Recuperacion)
        //            _mailer.Message.Subject = "AVISO RECUPERACIÓN - " + modelo.Alumno.CodigoAlumno;
        //        if (modelo.Solicitud.ra_AccesoAulaVirtual_Tipo.Nombre == Tipo_AccesoAulaVirtual.Temporal)
        //            _mailer.Message.Subject = "AVISO DE ACCESO TEMPORAL - " + modelo.Alumno.CodigoAlumno;


        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = "jeperez@bsginstitute.com;";
        //            //_mailer.Message.Cc.AsString = remitente.Correo;

        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesCoordinador();
        //        if (modelo.Solicitud.ra_AccesoAulaVirtual_Tipo.Nombre == Tipo_AccesoAulaVirtual.Regular)
        //            correo.SolicitudAcceso_AulaVirtual_Aviso_Regular(modelo);
        //        if (modelo.Solicitud.ra_AccesoAulaVirtual_Tipo.Nombre == Tipo_AccesoAulaVirtual.Traslado)
        //            correo.SolicitudAcceso_AulaVirtual_Aviso_Traslado(modelo);
        //        if (modelo.Solicitud.ra_AccesoAulaVirtual_Tipo.Nombre == Tipo_AccesoAulaVirtual.Recuperacion)
        //            correo.SolicitudAcceso_AulaVirtual_Aviso_Recuperacion(modelo);
        //        if (modelo.Solicitud.ra_AccesoAulaVirtual_Tipo.Nombre == Tipo_AccesoAulaVirtual.Temporal)
        //            correo.SolicitudAcceso_AulaVirtual_Aviso_Temporal(modelo);

        //        ////adjuntos
        //        //var rutaCabecera = HostingEnvironment.ApplicationPhysicalPath + "Content/img/" + "cabecera.png";
        //        //_mailer.Message.Attachments.Add(rutaCabecera, null, "cabecera");

        //        ////firma
        //        //var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/coordinadores/" + coordinador.Firma;
        //        //_mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = correo.mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Aviso - enviado correctamente.";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //        }
        //        else
        //        {
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = $"Hubo un error al intentar enviar el correo de Aviso - {resultado_envio}.";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = $"Hubo un error al generar el Correo de Aviso - {exception.Message}.";
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //    }
        //    return respuesta;
        //}

        //public RespuestaWeb Enviar_RestablecerClave_AlumnoPortalWeb(AlumnoPresencialBOL alumno_integra, AspNetUsers alumno_portal, Coordinadoras coordinador)
        //{
        //    RespuestaWeb respuesta = new RespuestaWeb();
        //    try
        //    {
        //        _mailer = new Smtp();

        //        //Remitente remitente = ObtenerRemitente(coordinador.coordinadoraacademica);

        //        var server = new SmtpServer("smtp.gmail.com", coordinador.Correo, coordinador.Password);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = coordinador.Coordinadora + "<" + coordinador.AliasCorreo + ">";
        //        _mailer.Message.Subject = "Recordatorio Clave Portal Web - " + alumno_integra.CodigoAlumno;


        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = alumno_portal.Email;
        //            //_mailer.Message.Cc.AsString = remitente.Correo;

        //            _mailer.Message.Bcc.AsString = coordinador.Correo + ";" + "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesCoordinador();
        //        correo.RestablecerClave_AlumnoPortalWeb(alumno_integra, alumno_portal, coordinador);

        //        //adjuntos
        //        var rutaCabecera = HostingEnvironment.ApplicationPhysicalPath + "Content/img/" + "cabecera.png";
        //        _mailer.Message.Attachments.Add(rutaCabecera, null, "cabecera");

        //        //firma
        //        var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/coordinadores/" + coordinador.Firma;
        //        _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = correo.mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Recordatorio Clave - enviado correctamente.";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //        }
        //        else
        //        {
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = $"Hubo un error al intentar enviar el correo de Recordatorio Clave - {resultado_envio}.";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = $"Hubo un error al generar el Correo de Recordatorio Clave - {exception.Message}.";
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //    }
        //    return respuesta;
        //}

        //public RespuestaWeb Enviar_Convenio_Reprogramacion(ra_Convenio_Reprogramacion convenio, byte[] pdf, Coordinadoras coordinador)
        //{
        //    try
        //    {
        //        _mailer = new Smtp();

        //        Remitente remitente = ObtenerRemitente("modpru");

        //        var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
        //        _mailer.Message.Subject = "Convenio Reprogramación - " + convenio.CodigoAlumno + " al " +
        //                                  convenio.FechaInicio.ToShortDateString();

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = coordinador.AliasCorreo;
        //            _mailer.Message.Cc.AsString = "";
        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesCoordinador();
        //        correo.Convenio_Reprogramacion(convenio, coordinador);

        //        //adjuntos
        //        if (convenio.Aprobado)
        //        {
        //            byte[] archivo = System.IO.File.ReadAllBytes(convenio.RutaArchivo + convenio.NombreArchivo);
        //            _mailer.Message.Attachments.Add(archivo, convenio.NombreArchivo,
        //                "<574956asfssfgas87fghdf4h4df6go9h91781@tlffmdqjobxj>",
        //                convenio.ContentType, null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
        //        }
        //        else
        //        {
        //            _mailer.Message.Attachments.Add(pdf,
        //                $"Convenio Reprogramación - {convenio.CodigoAlumno} - {convenio.Alumno.ToUpper()}.pdf",
        //                "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null,
        //                NewAttachmentOptions.None, MailTransferEncoding.Base64);
        //        }

        //        ////firma
        //        //var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/" + remitente.Firma;
        //        //_mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = correo.mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Confirmación de Generación de Convenio Reprogramación - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Confirmación de Generación de Convenio Reprogramación. " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Confirmación de Generación de Convenio Reprogramación. " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}
    }
}