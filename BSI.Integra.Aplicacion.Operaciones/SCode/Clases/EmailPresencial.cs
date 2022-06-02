using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Persistencia.Models;
using MailBee.Mime;
using MailBee.SmtpMail;
using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSI.Integra.Aplicacion.Operaciones.Clases
{
    public class EmailPresencial
    {
        private Smtp _mailer;
        private LicenciaRepositorio _repLicencia = new LicenciaRepositorio();
        private RaRemitenteRepositorio _repRemitente = new RaRemitenteRepositorio();
        public bool prueba { get; set; }

        public EmailPresencial()
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

        //Envios Curso
        //internal RespuestaWeb Enviar_Curso_SolicitudNotaDocente(string mensaje, Curso_Correo_SolicitarNotaDocente_ViewModel modelo)
        //{
        //    RespuestaWeb respuesta = new RespuestaWeb();
        //    try
        //    {
        //        _mailer = new Smtp();

        //        //Remitente remitente = ObtenerRemitente(modelo.CentroCosto.ResponsableCoordinacion);
        //        //Coordinadoras coordinador = modelo.Coordinador;

        //        var server = new SmtpServer("smtp.gmail.com", modelo.Coordinador.Correo, modelo.Coordinador.Password);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = modelo.Coordinador.Coordinadora + "<" + modelo.Coordinador.AliasCorreo + ">";
        //        _mailer.Message.Subject = "Solicitud de Notas - " + modelo.Curso.NombreCurso + " - " + modelo.Curso.ra_CentroCosto.NombreCentroCosto;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = modelo.Docente.EmailCompleto;
        //            _mailer.Message.Cc.AsString = modelo.Coordinador.AliasCorreo + ";" + (modelo.Remitente != null ? modelo.Remitente.AliasCorreo : "");

        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        //var correo = new MailTemplatesPresencial();
        //        //correo.Enviar_SolicitudMaterialDocente(modelo);

        //        //adjuntos
        //        var rutaCabecera = HostingEnvironment.ApplicationPhysicalPath + "Content/img/" + "cabecera.png";
        //        _mailer.Message.Attachments.Add(rutaCabecera, null, "cabecera");

        //        var rutaBotonRegistrarCalificacion = HostingEnvironment.ApplicationPhysicalPath + "Content/img/Presencial/" + "boton_registrar_calificacion.png";
        //        _mailer.Message.Attachments.Add(rutaBotonRegistrarCalificacion, null, "boton_registrar_calificacion");

        //        //if (consolidado_nota_curso != null)
        //        //{
        //        //    _mailer.Message.Attachments.Add(consolidado_nota_curso, "FormatoNotaAsistencia.xlsx", "<1sf2gdfg766a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
        //        //}

        //        //firma
        //        var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/coordinadores/" + modelo.Coordinador.Firma;
        //        _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = mensaje;


        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Solicitud de Notas - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //        }
        //        else
        //        {
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Solicitud de Notas - " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo Solicitud de Notas - " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //    }
        //    return respuesta;
        //}

        //internal RespuestaWeb Enviar_Curso_Aviso_Registro_NotaDocente(ra_Curso curso, CronogramaDocenteBOL docente, Coordinadoras coordinador, ra_Remitente responsable_coordinacion)
        //{
        //    RespuestaWeb respuesta = new RespuestaWeb();
        //    try
        //    {
        //        _mailer = new Smtp();

        //        Remitente remitente = ObtenerRemitente("modpru");
        //        //Coordinadoras coordinador = modelo.Coordinador;

        //        var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
        //        _mailer.Message.Subject = "Aviso Registro Nota Docente - " + curso.NombreCurso + " - " + curso.ra_CentroCosto.NombreCentroCosto;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = responsable_coordinacion?.AliasCorreo;
        //            _mailer.Message.Cc.AsString = coordinador?.AliasCorreo;

        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        //var correo = new MailTemplatesPresencial();
        //        //correo.Enviar_SolicitudMaterialDocente(modelo);

        //        ////adjuntos
        //        //var rutaCabecera = HostingEnvironment.ApplicationPhysicalPath + "Content/img/" + "cabecera.png";
        //        //_mailer.Message.Attachments.Add(rutaCabecera, null, "cabecera");


        //        ////firma
        //        //var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/" + modelo.Remitente.RutaFirma + modelo.Remitente.Firma;
        //        //_mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        MailTemplatesPresencial correo = new MailTemplatesPresencial();
        //        correo.Envio_Curso_Aviso_Registro_NotaDocente(curso, docente);
        //        _mailer.Message.BodyHtmlText = correo.mensaje;


        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Aviso de Registro Nota Docente - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //        }
        //        else
        //        {
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Aviso de Registro Nota Docente - " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo Aviso de Registro Nota Docente - " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //    }
        //    return respuesta;
        //}

        //public RespuestaWeb Enviar_Curso_ConfirmacionEvaluacion(ra_Curso curso)
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
        //        _mailer.Message.Subject = "Confirmación de Evaluaciones - " + curso.NombreCurso + " - " +
        //                                  curso.ra_CentroCosto.NombreCentroCosto;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = "jeperez@bsginstitute.com;";
        //            _mailer.Message.Cc.AsString = "gchirinos@bsginstitute.com;";

        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }
        //        var correo = new MailTemplatesPresencial();
        //        correo.Envio_Curso_ConfirmacionEvaluacion(curso);

        //        //adjuntos
        //        //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

        //        //firma
        //        //var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/" + remitente.Firma;
        //        //_mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = correo.mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Confirmación Evaluación - enviado correctamente.";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //        }
        //        else
        //        {
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Confirmación Evaluación - " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Confirmación Evaluación - " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //    }
        //    return respuesta;
        //}

        ////Envíos Material
        //public RespuestaWeb Enviar_SubidaMaterial_Confirmacion(string comentario, string nombre_archivo, ra_Curso curso, string nombre_usuario)
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
        //        _mailer.Message.Subject = "Confirmación - Subida de Material - " + curso.ra_CentroCosto.NombreCentroCosto + " - " + curso.NombreCurso;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = "avillanuevah@bsginstitute.com;" +
        //                                          "jeperez@bsginstitute.com;" +
        //                                          "gchirinos@bsginstitute.com;";
        //            //_mailer.Message.Cc.AsString = remitente.Correo;
        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesPresencial();
        //        correo.SubidaMaterial_Confirmacion(comentario, nombre_archivo, curso, nombre_usuario);

        //        //adjuntos
        //        //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

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
        //            respuesta.Mensaje = "Correo de Confirmación de Subida de Material - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Confirmación de Subida de Material. " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Confirmación de Subida de Material. " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}

        //public RespuestaWeb Enviar_SubidaMaterialClonado_Confirmacion(string comentario, List<ra_Curso_Material> materiales_clonados, ra_Curso curso, string nombre_usuario)
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
        //        _mailer.Message.Subject = "Confirmación - Subida de Material - " + curso.ra_CentroCosto.NombreCentroCosto + " - " + curso.NombreCurso;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = "avillanuevah@bsginstitute.com;" +
        //                                          "jeperez@bsginstitute.com;" +
        //                                          "gchirinos@bsginstitute.com;";
        //            //_mailer.Message.Cc.AsString = remitente.Correo;
        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesPresencial();
        //        correo.SubidaMaterialClonado_Confirmacion(comentario, materiales_clonados, curso, nombre_usuario);

        //        //adjuntos
        //        //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

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
        //            respuesta.Mensaje = "Correo de Confirmación de Subida de Material - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Confirmación de Subida de Material. " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Confirmación de Subida de Material. " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}

        //internal RespuestaWeb Enviar_MaterialProveedor(ra_Curso_Material material, ra_ProveedorMaterial proveedor, ra_Sede sede, bool enviar_correo, List<ra_Curso_MaterialAlumno> listado_material_alumnos)
        //{
        //    RespuestaWeb respuesta = new RespuestaWeb();

        //    //evita el envío del correo al proveedor
        //    if (enviar_correo)
        //    {
        //        try
        //        {
        //            _mailer = new Smtp();

        //            Remitente remitente = ObtenerRemitente("gchirinos");

        //            var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
        //            //server.SslMode = SslStartupMode.UseStartTls;
        //            server.Port = 465; //587
        //            _mailer.SmtpServers.Add(server);

        //            // Set static From and Subject.
        //            _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
        //            var centro_costo = material.ra_Curso.ra_CentroCosto.NombreCentroCosto;
        //            var curso = material.ra_Curso.NombreCurso;
        //            _mailer.Message.Subject = "Confirmación - Impresión de Material - " + centro_costo + " - " + curso +
        //                                      " - Grupo -" + material.Grupo;

        //            // Set templates for To, body, and attachment.
        //            if (this.prueba)
        //            {
        //                _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //                _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //            }
        //            else
        //            {
        //                _mailer.Message.To.AsString = proveedor.Email;
        //                _mailer.Message.Cc.AsString =
        //                    "avillanueva@bsginstitute.com;" + "gchirinos@bsginstitute.com;";
        //                //coloca a Eliana en copia de los correos de confirmacion de material para el proveedor en Colombia
        //                if (material.ra_Curso.ra_CentroCosto.NombreCentroCosto.Contains("BOGOTA"))
        //                    _mailer.Message.Cc.AsString += ";" + "esanchez@bsginstitute.com;";
        //                _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //            }

        //            var correo = new MailTemplatesPresencial();
        //            correo.MaterialProveedor(material, proveedor, sede, listado_material_alumnos);

        //            //adjuntos
        //            //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

        //            //firma
        //            var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/" +
        //                            remitente.Firma;
        //            _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //            _mailer.Message.BodyHtmlText = correo.mensaje;

        //            // Generate and send e-mails.
        //            var resultado_envio = EnviarMails();

        //            if (resultado_envio == "OK")
        //            {
        //                respuesta.Estado = true;
        //                respuesta.Mensaje = "Correo de Confirmación a Proveedor de Material - enviado correctamente";
        //                respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //            }
        //            else
        //            {
        //                respuesta.Estado = false;
        //                respuesta.Mensaje =
        //                    "Hubo un error al intentar enviar el Correo de Confirmación a Proveedor de Material. " +
        //                    resultado_envio;
        //                respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al generar el Correo de Confirmación a Proveedor de Material. " +
        //                                exception.Message;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //        }
        //    }
        //    else
        //    {
        //        respuesta.Estado = true;
        //        respuesta.Mensaje = "Se selecciono no enviar correo al proveedor.";
        //        respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //    }
        //    return respuesta;
        //}

        //internal RespuestaWeb Enviar_CancelacionProveedor(ra_Curso_Material material, ra_ProveedorMaterial proveedor, string observacion_proveedor)
        //{
        //    try
        //    {
        //        _mailer = new Smtp();

        //        Remitente remitente = ObtenerRemitente("gchirinos");

        //        var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
        //        var centro_costo = material.ra_Curso.ra_CentroCosto.NombreCentroCosto;
        //        var curso = material.ra_Curso.NombreCurso;
        //        _mailer.Message.Subject = "Cancelación - Impresión de Material - " + centro_costo + " - " + curso + " - Grupo -" + material.Grupo;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = proveedor.Email;
        //            if (curso.Contains("BOGOTA"))
        //                _mailer.Message.Cc.AsString =
        //                    "gchirinos@bsginstitute.com;" + "ymejia@bsginstitute.com;";
        //            else
        //                _mailer.Message.Cc.AsString = "gchirinos@bsginstitute.com;";
        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesPresencial();
        //        correo.CancelacionMaterialProveedor(material, proveedor, observacion_proveedor);

        //        //adjuntos
        //        //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

        //        //firma
        //        var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/" + remitente.Firma;
        //        _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = correo.mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Cancelación a Proveedor de Material - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Cancelación a Proveedor de Material. " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Cancelación a Proveedor de Material. " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}

        //public RespuestaWeb Enviar_SolicitudMaterialDocente(string mensaje, Curso_Correo_SolicitarMaterialDocente_ViewModel modelo, byte[] perfil_alumnos, bool prueba_envio)
        //{
        //    RespuestaWeb respuesta = new RespuestaWeb();
        //    try
        //    {
        //        _mailer = new Smtp();

        //        Remitente remitente = ObtenerRemitente(modelo.CentroCosto.ResponsableCoordinacion);

        //        var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
        //        _mailer.Message.Subject = "Solicitud de Material - " + modelo.CentroCosto.NombreCentroCosto;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            if (prueba_envio == false)
        //                _mailer.Message.To.AsString = string.Join(";", modelo.ListadoDocente.Select(s => s.EmailCompleto));

        //            _mailer.Message.Cc.AsString = remitente.AliasCorreo + ";";

        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }
        //        //var correo = new MailTemplatesPresencial();
        //        //correo.Enviar_SolicitudMaterialDocente(modelo);

        //        //adjuntos
        //        //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

        //        //formato material
        //        var rutaFormatoMaterial = HostingEnvironment.ApplicationPhysicalPath +
        //                                           "Content/Attachments/Presencial/Docente/" + @"PLANTILLA-BSG-INSTITUTE.PPTX";
        //        _mailer.Message.Attachments.Add(rutaFormatoMaterial, null);

        //        //archivos
        //        foreach (var archivo in modelo.Archivos)
        //        {
        //            if (archivo.ArchivoBytes != null)
        //                _mailer.Message.Attachments.Add(archivo.ArchivoBytes, archivo.Nombre,
        //                    " < 12s4a8a87dsgd78c1b1$ir671781@tlffmdqjobxj>" + archivo.Nombre, archivo.ContentType, null,
        //                    NewAttachmentOptions.None, MailTransferEncoding.Base64);
        //        }
        //        //silabo
        //        foreach (var archivo in modelo.Silabos)
        //        {
        //            if (archivo.ArchivoBytes != null)
        //                _mailer.Message.Attachments.Add(archivo.ArchivoBytes, archivo.Nombre,
        //                    "<12s4a8a87dsgd78c1b1$ir671781@tlffmsdqjobxj>" + archivo.Nombre, archivo.ContentType, null,
        //                    NewAttachmentOptions.None, MailTransferEncoding.Base64);
        //        }
        //        //evaluaciones
        //        foreach (var archivo in modelo.Evaluaciones)
        //        {
        //            if (archivo.ArchivoBytes != null)
        //                _mailer.Message.Attachments.Add(archivo.ArchivoBytes, archivo.Nombre,
        //                    "<12s4a8a87dsgd78c1b1$ir671781@tlffmsdqjobxj>" + archivo.Nombre, archivo.ContentType, null,
        //                    NewAttachmentOptions.None, MailTransferEncoding.Base64);
        //        }
        //        //material temporal
        //        foreach (var archivo in modelo.MaterialTemporal)
        //        {
        //            if (archivo.ArchivoBytes != null)
        //                _mailer.Message.Attachments.Add(archivo.ArchivoBytes, archivo.Nombre,
        //                    "<12s4a8a87ddft58c1b1$ir671781@tlffmsdqjobxj>" + archivo.Nombre, archivo.ContentType, null,
        //                    NewAttachmentOptions.None, MailTransferEncoding.Base64);
        //        }


        //        if (perfil_alumnos != null)
        //        {
        //            _mailer.Message.Attachments.Add(perfil_alumnos, "PerfilAlumnos.xlsx", "<1sf2s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
        //        }

        //        //encuesta
        //        if (modelo.CentroCosto.NombreCentroCosto.ToUpper().Contains(ModalidadCentroCosto.Online.ToUpper()))
        //        {
        //            var rutaEncuestaOnlineIntermedia = HostingEnvironment.ApplicationPhysicalPath +
        //                                               "Content/Attachments/Online/Docente/" +
        //                                               @"Encuesta Intermedia Online.xlsx";
        //            _mailer.Message.Attachments.Add(rutaEncuestaOnlineIntermedia, null);
        //        }
        //        else
        //        {
        //            var rutaEncuestaPresencial = HostingEnvironment.ApplicationPhysicalPath +
        //                                         "Content/Attachments/Presencial/Docente/" +
        //                                         @"Encuesta Evaluación Programas Presenciales.pdf";
        //            _mailer.Message.Attachments.Add(rutaEncuestaPresencial, null);
        //        }


        //        //firma
        //        var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/" + remitente.Firma;
        //        _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = mensaje;


        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Solicitud de Material - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //        }
        //        else
        //        {
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Solicitud de Material - " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo Solicitud de Material - " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //    }
        //    return respuesta;
        //}

        //public RespuestaWeb Enviar_Material_EnvioAprobacion(CursoMaterial_EnviarAprobacion_ViewModel modelo, ra_Curso curso, List<ra_Curso_Material> listado_material, string nombre_usuario)
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
        //        _mailer.Message.Subject = "Material - Envío a Aprobación - " + curso.ra_CentroCosto.NombreCentroCosto + " - " + curso.NombreCurso;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = "echirinos@bsginstitute.com;" + "oscar@bsginstitute.com;";
        //            _mailer.Message.Cc.AsString = nombre_usuario + "@bsginstitute.com";
        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesPresencial();
        //        correo.Material_EnvioAprobacion(modelo, curso, listado_material, this.prueba);

        //        //adjuntos
        //        //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

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
        //            respuesta.Mensaje = "Correo de Material Envío a Aprobación - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Material Envío a Aprobación. " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Material Envío a Aprobación. " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}

        //public RespuestaWeb Enviar_MaterialDigital_Alumno(ra_Curso curso, List<ra_Curso_Material> listado_material, List<AlumnosMatriculados> listado_alumno, Coordinadoras coordinador, DateTime? fecha_inicio_curso)
        //{
        //    RespuestaWeb respuesta = new RespuestaWeb();

        //    //envia el material a los alumnos si el curso es de grupo 1
        //    if (curso.Grupo == 1)
        //    {
        //        foreach (var alumno in listado_alumno)
        //        {
        //            try
        //            {
        //                _mailer = new Smtp();

        //                //Remitente remitente = ObtenerRemitente("modpru");
        //                Remitente remitente = new Remitente(coordinador.Coordinadora, coordinador.Correo,
        //                    coordinador.Password, coordinador.Firma);

        //                var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
        //                //server.SslMode = SslStartupMode.UseStartTls;
        //                server.Port = 465; //587
        //                _mailer.SmtpServers.Add(server);

        //                // Set static From and Subject.
        //                _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
        //                var nombre_centro_costo = curso.ra_CentroCosto.NombreCentroCosto;
        //                var nombre_curso = curso.NombreCurso;
        //                _mailer.Message.Subject = "MATERIAL DIGITAL - " + nombre_centro_costo + " - " +
        //                                          nombre_curso + " - " + alumno.CodigoAlumno;

        //                // Set templates for To, body, and attachment.
        //                if (this.prueba)
        //                {
        //                    _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //                    _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //                }
        //                else
        //                {
        //                    _mailer.Message.To.AsString = alumno.email1 + ";" + alumno.email2;
        //                    _mailer.Message.Cc.AsString =
        //                        remitente.AliasCorreo + ";" + "gchirinos@bsginstitute.com;";
        //                    _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //                }

        //                var correo = new MailTemplatesPresencial();
        //                correo.Envio_MaterialDigital_Alumno(alumno, curso, listado_material, coordinador,
        //                    fecha_inicio_curso);

        //                //adjuntos
        //                //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

        //                var rutaCabecera = HostingEnvironment.ApplicationPhysicalPath + "Content/img/" + "cabecera.png";
        //                _mailer.Message.Attachments.Add(rutaCabecera, null, "cabecera");

        //                var rutaImagen = HostingEnvironment.ApplicationPhysicalPath + "Content/img/Presencial/" +
        //                                 "img_correo_material.png";
        //                _mailer.Message.Attachments.Add(rutaImagen, null, "imagen_material");

        //                var rutaBotonMaterial = HostingEnvironment.ApplicationPhysicalPath + "Content/img/Presencial/" +
        //                                        "boton_material.png";
        //                _mailer.Message.Attachments.Add(rutaBotonMaterial, null, "boton_material");

        //                //firma
        //                var rutaFirma = HostingEnvironment.ApplicationPhysicalPath +
        //                                "Content/img/firmas/coordinadores/" +
        //                                remitente.Firma;
        //                _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //                _mailer.Message.BodyHtmlText = correo.mensaje;

        //                // Generate and send e-mails.
        //                var resultado_envio = EnviarMails();

        //                if (resultado_envio == "OK")
        //                {
        //                    respuesta.Estado = true;
        //                    respuesta.Mensaje += "Correo de Envío Material Alumno - enviado correctamente - " +
        //                                         alumno.CodigoAlumno + ".";
        //                    respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //                }
        //                else
        //                {
        //                    respuesta.Estado = false;
        //                    respuesta.Mensaje +=
        //                        "Hubo un error al intentar enviar el Correo de Envío Material Alumno. - " +
        //                        alumno.CodigoAlumno + " " + resultado_envio;
        //                    respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                respuesta.Estado = false;
        //                respuesta.Mensaje +=
        //                    "Hubo un error al generar el Correo de Confirmación a Proveedor de Material. " +
        //                    ex.Message;
        //                respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //envia a la coordinadora si el grupo es diferente
        //        AlumnosMatriculados alumno = null; //ficticio
        //        try
        //        {
        //            _mailer = new Smtp();

        //            //Remitente remitente = ObtenerRemitente("modpru");
        //            Remitente remitente = new Remitente(coordinador.Coordinadora, coordinador.Correo,
        //                coordinador.Password, coordinador.Firma);

        //            var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
        //            //server.SslMode = SslStartupMode.UseStartTls;
        //            server.Port = 465; //587
        //            _mailer.SmtpServers.Add(server);

        //            // Set static From and Subject.
        //            _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
        //            var nombre_centro_costo = curso.ra_CentroCosto.NombreCentroCosto;
        //            var nombre_curso = curso.NombreCurso;
        //            _mailer.Message.Subject = "MATERIAL DIGITAL - " + nombre_centro_costo + " - " +
        //                                      nombre_curso + " - Grupo: " + curso.Grupo;

        //            // Set templates for To, body, and attachment.
        //            if (this.prueba)
        //            {
        //                _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //                _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //            }
        //            else
        //            {
        //                _mailer.Message.To.AsString = coordinador.coordinadoraacademica;
        //                _mailer.Message.Cc.AsString =
        //                    remitente.AliasCorreo + ";" + "gchirinos@bsginstitute.com;";
        //                _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //            }

        //            var correo = new MailTemplatesPresencial();
        //            correo.Envio_MaterialDigital_Alumno(alumno, curso, listado_material, coordinador,
        //                fecha_inicio_curso);

        //            //adjuntos
        //            //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

        //            var rutaCabecera = HostingEnvironment.ApplicationPhysicalPath + "Content/img/" + "cabecera.png";
        //            _mailer.Message.Attachments.Add(rutaCabecera, null, "cabecera");

        //            var rutaImagen = HostingEnvironment.ApplicationPhysicalPath + "Content/img/Presencial/" +
        //                             "img_correo_material.png";
        //            _mailer.Message.Attachments.Add(rutaImagen, null, "imagen_material");

        //            var rutaBotonMaterial = HostingEnvironment.ApplicationPhysicalPath + "Content/img/Presencial/" +
        //                                    "boton_material.png";
        //            _mailer.Message.Attachments.Add(rutaBotonMaterial, null, "boton_material");

        //            //firma
        //            var rutaFirma = HostingEnvironment.ApplicationPhysicalPath +
        //                            "Content/img/firmas/coordinadores/" +
        //                            remitente.Firma;
        //            _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //            _mailer.Message.BodyHtmlText = correo.mensaje;

        //            // Generate and send e-mails.
        //            var resultado_envio = EnviarMails();

        //            if (resultado_envio == "OK")
        //            {
        //                respuesta.Estado = true;
        //                respuesta.Mensaje += "Correo de Envío Material Alumno - enviado correctamente al coordinador(a).";
        //                respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //            }
        //            else
        //            {
        //                respuesta.Estado = false;
        //                respuesta.Mensaje +=
        //                    "Hubo un error al intentar enviar el Correo de Envío Material al coordinador(a) - " + resultado_envio;
        //                respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            respuesta.Estado = false;
        //            respuesta.Mensaje +=
        //                "Hubo un error al generar el Correo de Confirmación a envío Material . " +
        //                ex.Message;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //        }
        //    }
        //    return respuesta;
        //}

        //public RespuestaWeb Enviar_Material_EnvioDesaprobacion(ra_Curso curso, CursoMaterial_Aprobar_ViewModel modelo_aprobacion, List<ra_Curso_Material> listado_material)
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
        //        _mailer.Message.Subject = "Material - Desaprobación - " +
        //                                  curso.ra_CentroCosto.NombreCentroCosto + " - " + curso.NombreCurso +
        //                                  " - Grupo Edición: " + modelo_aprobacion.Grupo;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = "gchirinos@bsginstitute.com;";
        //            _mailer.Message.Cc.AsString = "echirinos@bsginstitute.com;";
        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesPresencial();
        //        correo.Material_EnvioDesaprobacion(modelo_aprobacion, curso, listado_material, this.prueba);

        //        //adjuntos
        //        //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

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
        //            respuesta.Mensaje = "Correo de Material Envío Desaprobación - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Material Envío Desaprobación. " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Material Envío Desaprobación. " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}

        //public RespuestaWeb Enviar_Material_ConfirmacionAprobacion(ra_Curso curso, CursoMaterial_Aprobar_ViewModel modelo_aprobacion, List<ra_Curso_Material> listado_material, bool? enviar_material_alumno)
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
        //        _mailer.Message.Subject = "Material - Confirmación Aprobación - " +
        //                                  curso.ra_CentroCosto.NombreCentroCosto + " - " + curso.NombreCurso +
        //                                  " - Grupo Edición: " + modelo_aprobacion.Grupo;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = "gchirinos@bsginstitute.com;";
        //            _mailer.Message.Cc.AsString = "gchirinos@bsginstitute.com;";
        //            if (enviar_material_alumno == true)
        //                _mailer.Message.Cc.AsString += ";" + "jeperez@bsginstitute.com;";
        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }

        //        var correo = new MailTemplatesPresencial();
        //        correo.Material_ConfirmacionAprobacion(modelo_aprobacion, curso, listado_material, this.prueba);

        //        //adjuntos
        //        //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

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
        //            respuesta.Mensaje = "Correo de Material Confirmación Aprobación - enviado correctamente.";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Material Confirmación Aprobación. " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Material Confirmación Aprobación. " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}

        //Asistencia
        public RespuestaWebDTO EnviarAsistenciaDetalladaOnline(AlumnoPresencialDTO alumno, CoordinadoraBO coordinadora, byte[] excel, List<ReporteAsistenciaOnlineDTO> listadoSesionesAsistencia)
        {
            try
            {
                _mailer = new Smtp();

                RespuestaWebDTO respuestaWeb = new RespuestaWebDTO();
                //Remitente remitente = new Remitente("Mod Pru", "modpru@bsginstitute.com", "BSgrupo123", "");

                var server = new SmtpServer("smtp.gmail.com", coordinadora.Correo, coordinadora.Password);
                //server.SslMode = SslStartupMode.UseStartTls;
                server.Port = 465; //587
                _mailer.SmtpServers.Add(server);

                // Set static From and Subject.
                _mailer.Message.From.AsString = coordinadora.NombreResumido + "<" + coordinadora.AliasCorreo + ">";
                _mailer.Message.Subject = "Consolidado porcentaje de asistencia al " + DateTime.Now.ToShortDateString() + " - " + alumno.CodigoAlumno;

                // Set templates for To, body, and attachment.
                if (this.prueba)
                {
                    _mailer.Message.To.AsString = "wchoque@bsginstitute.com;";
                    _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
                }
                else
                {
                    _mailer.Message.To.AsString = alumno.Email1 + ";" + alumno.Email2;
                    _mailer.Message.Cc.AsString = coordinadora.AliasCorreo;
                    _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
                }

                var correo = new MailTemplatePresencial();
                correo.EnviarAsistenciaDetalladaOnline(alumno, coordinadora, listadoSesionesAsistencia);

                //adjuntos
                _mailer.Message.Attachments.Add(excel, "Reporte_detallado_asistencia.xlsx", "<12s4a8a87dsgd78c$56vnc64i1b1$ir67fdgdf1781@tlffmdqjobxj>", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

                var rutaFirma = new HostingEnvironment().WebRootPath + "Content/img/firmas/coordinadores/" + coordinadora.Firma; //ApplicationPhysicalPath
                _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

                //var rutaCabecera = HostingEnvironment.ApplicationPhysicalPath + "Content/img/" + "cabecera.png";
                var rutaCabecera = new HostingEnvironment().WebRootPath + "Content/img/" + "cabecera.png";
                _mailer.Message.Attachments.Add(rutaCabecera, null, "cabecera");

                _mailer.Message.BodyHtmlText = correo.Mensaje;

                // Generate and send e-mails.
                var resultadoEnvio = EnviarMails();

                respuestaWeb.Mensaje = "Correo de Consolidado de asistencia por Alumno - enviado correctamente";
                respuestaWeb.Estado = true;
                return respuestaWeb;
            }
            catch (Exception exception)
            {
                RespuestaWebDTO RespuestaWeb = new RespuestaWebDTO();
                RespuestaWeb.Mensaje = "Hubo un error al generar el Correo de Consolidado de asistencia por Alumno " + exception.Message;
                throw new Exception (RespuestaWeb.Mensaje);
            }
        }

        ////encuestas
        //public RespuestaWeb Enviar_EncuestaPresencial_Alumno(ra_Presencial_Programacion_Encuesta tipo_encuesta, AlumnoPresencialBOL alumno, Coordinadoras coordinador)
        //{
        //    try
        //    {
        //        _mailer = new Smtp();

        //        //Remitente remitente = new Remitente("Mod Pru", "modpru@bsginstitute.com", "BSgrupo123", "");

        //        var server = new SmtpServer("smtp.gmail.com", coordinador.Correo, coordinador.Password);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = coordinador.Coordinadora + "<" + coordinador.AliasCorreo + ">";
        //        _mailer.Message.Subject = "BSG INSTITUTE - ENCUESTA " + tipo_encuesta.TipoEncuesta.ToUpper()
        //                                  + " - " + tipo_encuesta.ra_Sesion.ra_Curso.ra_CentroCosto.NombreCentroCosto
        //                                  + " - " + tipo_encuesta.ra_Sesion.ra_Curso.NombreCurso
        //                                  + " - " + alumno.CodigoAlumno;

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
        //        var correo = new MailTemplatesPresencial();
        //        correo.Envio_Diario_Programacion_Encuestas_Alumno(tipo_encuesta, alumno, coordinador);

        //        //adjuntos
        //        //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

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
        //            respuesta.Mensaje = "Correo de Encuesta por Alumno - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Encuesta por Alumno  - " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de de Encuesta a Alumno - " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}

        //public RespuestaWeb Enviar_EncuestaPresencial_AlumnoGrupo(ra_Presencial_Programacion_Encuesta tipo_encuesta, List<AlumnoPresencialBOL> listado_alumnos, Coordinadoras coordinador)
        //{
        //    RespuestaWeb respuesta = new RespuestaWeb();

        //    try
        //    {
        //        _mailer = new Smtp();

        //        //Remitente remitente = new Remitente("Mod Pru", "modpru@bsginstitute.com", "BSgrupo123", "");

        //        var server = new SmtpServer("smtp.gmail.com", coordinador.Correo, coordinador.Password);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        foreach (var alumno in listado_alumnos)
        //        {
        //            RespuestaWeb respuesta_envio_alumno = Enviar_EncuestaPresencial_Alumno(tipo_encuesta, alumno, coordinador);

        //            if (respuesta_envio_alumno.Estado == true)
        //            {
        //                respuesta.Estado = true;
        //                respuesta.Mensaje += alumno.Alumno + " - envío realizado correctamente." + Environment.NewLine;
        //            }
        //            else
        //            {
        //                respuesta.Estado = false;
        //                respuesta.Mensaje += "Error - " + alumno.Alumno + ": " + respuesta_envio_alumno.Mensaje + ".";
        //                if (respuesta.Estado == true && respuesta_envio_alumno.Estado == true)
        //                    respuesta.Tipo = Tipo_RespuestaWeb.Info;
        //            }
        //            if (respuesta.Tipo != Tipo_RespuestaWeb.Info)
        //                respuesta.Tipo = respuesta_envio_alumno.Tipo;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Encuesta a Alumno - " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //    }

        //    return respuesta;
        //}

        //public RespuestaWeb Enviar_EncuestaPresencial_Docente(ra_Presencial_Programacion_Encuesta tipo_encuesta, CronogramaDocenteBOL docente, Coordinadoras coordinador)
        //{
        //    try
        //    {
        //        _mailer = new Smtp();

        //        //Remitente remitente = new Remitente("Mod Pru", "modpru@bsginstitute.com", "BSgrupo123", "");

        //        var server = new SmtpServer("smtp.gmail.com", coordinador.Correo, coordinador.Password);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = coordinador.Coordinadora + "<" + coordinador.AliasCorreo + ">";
        //        _mailer.Message.Subject = "BSG INSTITUTE - ENCUESTA " + tipo_encuesta.TipoEncuesta.ToUpper()
        //                                  + " - " + tipo_encuesta.ra_Sesion.ra_Curso.ra_CentroCosto.NombreCentroCosto
        //                                  + " - " + tipo_encuesta.ra_Sesion.ra_Curso.NombreCurso;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = docente.Email + ";" + docente.EmailCompleto;
        //            _mailer.Message.Cc.AsString = coordinador.AliasCorreo;
        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }
        //        var correo = new MailTemplatesPresencial();
        //        correo.Envio_Diario_Programacion_Encuestas_Docente(tipo_encuesta, docente, coordinador);

        //        //adjuntos
        //        //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

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
        //            respuesta.Mensaje = "Correo de Encuesta para el Docente - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;

        //            return respuesta;
        //        }
        //        else
        //        {
        //            RespuestaWeb respuesta = new RespuestaWeb();
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Encuesta al Docente  - " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        RespuestaWeb respuesta = new RespuestaWeb();
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de de Encuesta a Alumno - " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;

        //        return respuesta;
        //    }
        //}

        ////Centro de costo
        //public RespuestaWeb Enviar_CC_ConfirmacionApertura(ra_CentroCosto cc, ra_Curso_Material primer_material, List<Coordinadoras> listado_coordinador, DateTime? fecha_inicio, string nombre_usuario)
        //{
        //    RespuestaWeb respuesta = new RespuestaWeb();
        //    try
        //    {
        //        _mailer = new Smtp();

        //        Remitente remitente = ObtenerRemitente(nombre_usuario);

        //        var server = new SmtpServer("smtp.gmail.com", remitente.Correo, remitente.Clave);
        //        //server.SslMode = SslStartupMode.UseStartTls;
        //        server.Port = 465; //587
        //        _mailer.SmtpServers.Add(server);

        //        // Set static From and Subject.
        //        _mailer.Message.From.AsString = remitente.Nombre + "<" + remitente.AliasCorreo + ">";
        //        _mailer.Message.Subject = "Confirmación de apertura - " + cc.NombreCentroCosto;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = string.Join(";", listado_coordinador.Select(s => s.AliasCorreo));
        //            _mailer.Message.Cc.AsString = remitente.AliasCorreo + ";" +
        //                                          "pbeltran@bsginstitute.com;" + "krueda@bsginstitute.com;" +
        //                                          "gchirinos@bsginstitute.com;" +
        //                                          "echirinos@bsginstitute.com;" +
        //                                          "jeperez@bsginstitute.com;" + "esalguero@bsginstitute.com;" +
        //                                          //solicitado spor alexis
        //                                          "sruiz@bsginstitute.com;" + "mpacheco@bsginstitute.com;" +
        //                                          "aarcana@bsginstitute.com;" + "jillanes@bsginstitute.com;" +
        //                                          "mlara@bsginstitute.com;" + "avillanuevah@bsginstitute.com;" +
        //                                          "atorres@bsginstitute.com;" + "fdelvalle@bsginstitute.com;" +
        //                                          //solicitado pr elizabeth
        //                                          "oscar@bsginstitute.com;" + "gcalla@bsginstitute.com;" +
        //                                          "kmendozab@bsginstitute.com;" + "ymollinedop@bsginstitute.com;"
        //                                          + "gcastro@bsginstitute.com;" + "aarizaca@bsginstitute.com";

        //            //se ponen en copia a los asistentes segun sede
        //            if (cc.NombreCentroCosto.Contains(TipoCiudadesCentroCosto.Arequipa.ToUpper()))
        //                _mailer.Message.Cc.AsString += ";" + "ehuamani@bsginstitute.com;";
        //            if (cc.NombreCentroCosto.Contains(TipoCiudadesCentroCosto.Lima.ToUpper()))
        //                _mailer.Message.Cc.AsString += ";" + "ymejia@bsginstitute.com;" +
        //                                               "gmoran@bsginstitute.com;";
        //            if (cc.NombreCentroCosto.Contains(TipoCiudadesCentroCosto.Bogota.ToUpper()))
        //                _mailer.Message.Cc.AsString += ";" + "cleon@bsginstitute.com;";
        //            if (cc.NombreCentroCosto.Contains(TipoCiudadesCentroCosto.Santa_Cruz.ToUpper()))
        //                _mailer.Message.Cc.AsString += ";" + "cleon@bsginstitute.com;";

        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }
        //        var correo = new MailTemplatesPresencial();
        //        correo.Envio_CC_ConfirmacionApertura(cc, primer_material, fecha_inicio);

        //        //adjuntos
        //        //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

        //        //firma
        //        var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/" + remitente.Firma;
        //        _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = correo.mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Confirmación apertura de CC - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //        }
        //        else
        //        {
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Confirmación apertura de CC - " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Confirmación apertura de CC - " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //    }
        //    return respuesta;
        //}

        //public RespuestaWeb Enviar_CC_CorreoPreEjecucion(ra_CentroCosto cc, CC_CkeckList_PreEjecucion_ViewModel modelo_pre_ejecucion, string nombre_usuario)
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
        //        _mailer.Message.Subject = "Check List Pre Ejecución - " + cc.NombreCentroCosto;

        //        // Set templates for To, body, and attachment.
        //        if (this.prueba)
        //        {
        //            _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //            _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //        }
        //        else
        //        {
        //            _mailer.Message.To.AsString = "";
        //            _mailer.Message.Cc.AsString = remitente.AliasCorreo + ";";
        //            //"pbeltran@bsginstitute.com;" + "krueda@bsginstitute.com;" +

        //            _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //        }
        //        var correo = new MailTemplatesPresencial();
        //        correo.Enviar_CC_CorreoPreEjecucion(cc, modelo_pre_ejecucion);

        //        //adjuntos
        //        //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);

        //        ////firma
        //        //var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/" + remitente.Firma;
        //        //_mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //        _mailer.Message.BodyHtmlText = correo.mensaje;

        //        // Generate and send e-mails.
        //        var resultado_envio = EnviarMails();

        //        if (resultado_envio == "OK")
        //        {
        //            respuesta.Estado = true;
        //            respuesta.Mensaje = "Correo de Pre Ejecucion CC - enviado correctamente";
        //            respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //        }
        //        else
        //        {
        //            respuesta.Estado = false;
        //            respuesta.Mensaje = "Hubo un error al intentar enviar el Correo de Pre Ejecucion CC - " + resultado_envio;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        respuesta.Estado = false;
        //        respuesta.Mensaje = "Hubo un error al generar el Correo de Pre Ejecucion CC - " + exception.Message;
        //        respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //    }
        //    return respuesta;
        //}

        ////Curso trabajo
        //public RespuestaWeb Enviar_CursoTrabajo_RegistrarAlumno(ra_Curso_TrabajoAlumno trabajo_alumno, ra_Curso curso, Coordinadoras coordinador, List<AlumnosMatriculados> listado_alumno, string nombre_usuario)
        //{
        //    RespuestaWeb respuesta = new RespuestaWeb();

        //    foreach (var alumno in listado_alumno)
        //    {
        //        try
        //        {
        //            _mailer = new Smtp();
        //            //Remitente remitente = new Remitente("Mod Pru", "modpru@bsginstitute.com", "BSgrupo123", "");

        //            var server = new SmtpServer("smtp.gmail.com", coordinador.Correo, coordinador.Password);
        //            //server.SslMode = SslStartupMode.UseStartTls;
        //            server.Port = 465; //587
        //            _mailer.SmtpServers.Add(server);

        //            // Set static From and Subject.
        //            _mailer.Message.From.AsString = coordinador.Coordinadora + "<" + coordinador.AliasCorreo + ">";
        //            _mailer.Message.Subject = "RECORDATORIO PRESENTACION DE ENTREGA - " +
        //                                      curso.NombreCurso + " - " +
        //                                      curso.ra_CentroCosto.NombreCentroCosto
        //                                      + " - " + alumno.CodigoAlumno;

        //            // Set templates for To, body, and attachment.
        //            if (this.prueba)
        //            {
        //                _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //                _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //            }
        //            else
        //            {
        //                _mailer.Message.To.AsString = alumno.email1 + ";" + alumno.email2;
        //                _mailer.Message.Cc.AsString = coordinador.AliasCorreo;
        //                _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //            }
        //            var correo = new MailTemplatesPresencial();
        //            correo.Envio_Trabajo_Alumno(trabajo_alumno, curso, alumno, coordinador);

        //            //adjuntos
        //            //_mailer.Message.Attachments.Add(consolidado, "ConsolidadoNotas.pdf", "<12s4a8a87dsgd78c$5664i1b1$ir671781@tlffmdqjobxj>", "application/pdf", null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
        //            var rutaCabecera = HostingEnvironment.ApplicationPhysicalPath + "Content/img/" + "cabecera.png";
        //            _mailer.Message.Attachments.Add(rutaCabecera, null, "cabecera");

        //            //firma
        //            var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/coordinadores/" +
        //                            coordinador.Firma;
        //            _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //            _mailer.Message.BodyHtmlText = correo.mensaje;

        //            // Generate and send e-mails.
        //            var resultado_envio = EnviarMails();

        //            if (resultado_envio == "OK")
        //            {
        //                respuesta.Estado = true;
        //                respuesta.Mensaje += "Correo de Trabajo Alumno - enviado correctamente - " + alumno.CodigoAlumno;
        //                respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //            }
        //            else
        //            {
        //                respuesta.Estado = false;
        //                respuesta.Mensaje += "Hubo un error al intentar enviar el Correo de Trabajo Alumno - " +
        //                                     resultado_envio + " - " + alumno.CodigoAlumno;
        //                respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            respuesta.Estado = false;
        //            respuesta.Mensaje += "Hubo un error al generar el Correo de Trabajo Alumno - " +
        //                                 exception.Message + " - " + alumno.CodigoAlumno;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //        }
        //    }

        //    return respuesta;
        //}

        //public RespuestaWeb Enviar_Recordatorio_Alumno_PrimeraSesion(string mensaje, ra_Curso curso, Coordinadoras coordinador, List<AlumnosMatriculados> listado_lumno)
        //{
        //    RespuestaWeb respuesta = new RespuestaWeb();

        //    foreach (var alumno in listado_lumno)
        //    {
        //        try
        //        {
        //            _mailer = new Smtp();

        //            //Remitente remitente = ObtenerRemitente(modelo.CentroCosto.ResponsableCoordinacion);

        //            var server = new SmtpServer("smtp.gmail.com", coordinador.Correo, coordinador.Password);
        //            //server.SslMode = SslStartupMode.UseStartTls;
        //            server.Port = 465; //587
        //            _mailer.SmtpServers.Add(server);

        //            // Set static From and Subject.
        //            _mailer.Message.From.AsString = coordinador.Coordinadora + "<" + coordinador.AliasCorreo + ">";
        //            _mailer.Message.Subject = "BSG INSTITUTE - CURSO " + curso.Orden + " - " + curso.NombreCurso.ToUpper() +
        //                                      " - " + alumno.CodigoAlumno;

        //            // Set templates for To, body, and attachment.
        //            if (this.prueba)
        //            {
        //                _mailer.Message.To.AsString = "modpru@bsginstitute.com;";
        //                _mailer.Message.Subject = "Prueba - " + _mailer.Message.Subject;
        //            }
        //            else
        //            {
        //                _mailer.Message.To.AsString = alumno.email1 + "," + alumno.email2;
        //                _mailer.Message.Cc.AsString = coordinador.AliasCorreo;

        //                _mailer.Message.Bcc.AsString = "modpru@bsginstitute.com;";
        //            }

        //            //var correo = new MailTemplatesPresencial();
        //            //correo.Enviar_SolicitudMaterialDocente(modelo);

        //            //adjuntos
        //            var rutaCabecera = HostingEnvironment.ApplicationPhysicalPath + "Content/img/" + "cabecera_800.png";
        //            _mailer.Message.Attachments.Add(rutaCabecera, null, "cabecera");

        //            //firma
        //            var rutaFirma = HostingEnvironment.ApplicationPhysicalPath + "Content/img/firmas/coordinadores/" +
        //                            coordinador.Firma;
        //            _mailer.Message.Attachments.Add(rutaFirma, null, "firma");

        //            _mailer.Message.BodyHtmlText = mensaje;

        //            // Generate and send e-mails.
        //            var resultado_envio = EnviarMails();

        //            if (resultado_envio == "OK")
        //            {
        //                respuesta.Estado = true;
        //                respuesta.Mensaje += "Correo de Invitación Primera Sesión - enviado correctamente - " + alumno.CodigoAlumno;
        //                respuesta.Tipo = Tipo_RespuestaWeb.Success;
        //            }
        //            else
        //            {
        //                respuesta.Estado = false;
        //                respuesta.Mensaje +=
        //                    "Hubo un error al intentar enviar el Correo de Invitación Primera Sesión - " +
        //                    alumno.CodigoAlumno + " - " + resultado_envio;
        //                respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            respuesta.Estado = false;
        //            respuesta.Mensaje += "Hubo un error al generar el Correo Invitación Primera Sesión - " +
        //                                alumno.CodigoAlumno + " - " + exception.Message;
        //            respuesta.Tipo = Tipo_RespuestaWeb.Danger;
        //        }
        //    }

        //    return respuesta;
        //}
    }
}