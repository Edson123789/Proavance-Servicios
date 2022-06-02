using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSI.Integra.Aplicacion.Operaciones.Clases
{
    public class MailTemplateCoordinador
    {
        public string Mensaje { get; set; }

        //public void Plantilla()
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='border: none; padding: 0in'>";
        //    texto = texto + "<p><b>Estimad@ " + "profesor" + "</b></p>";
        //    texto = texto + "<br>Me es grato saludarle y poner a su consideraci&oacute;n el estado de calificaciones de";

        //    texto = texto + "<p style='margin-bottom: 0.11in'>Los n&uacute;meros telef&oacute;nicos en los que podr&aacute; comunicarse conmigo son:</p>";
        //    texto = texto + "<ul>";
        //    texto = texto + "<li><b>Desde Colombia: 01 3819462 anexo " + "anexo" + "</b></li>";
        //    texto = texto + "<li><b>Desde el extranjero: +51 1 3819462 anexo " + "anexo" + "</b></li>";
        //    texto = texto + "</ul>";

        //    texto = texto + "<p style='margin-bottom: 0.11in'>Los horarios de atenci&oacute;n para consultas telef&oacute;nicas y respuesta a correos electr&oacute;nicos son:</p>";
        //    texto = texto + "<ul>";
        //    texto = texto + "<li><b>Lunes a Viernes: 09:00 am - 02:00 pm / 03:00 pm - 07:00 pm</b></li>";
        //    texto = texto + "<li><b>S&aacute;bado: 09:00 am - 12:00 pm</b></li>";
        //    texto = texto + "</ul>";

        //    texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:picture1' ALIGN=BOTTOM WIDTH=415 HEIGHT=249 BORDER=0></p>";


        //    texto = texto + "</BODY></HTML>";

        //    Mensaje = texto;
        //}

        //internal void Confirmacion_EmisionConstancia(ra_ConstanciaAlumno constancia, Coordinadoras coordinador)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + "<p><b>Estimados(as)</b></p>";

        //    texto = texto + "<p>Sirva por la presente, confirmar la generación de la siguiente constancia solicitada:</p>";

        //    texto = texto + @"<ul>
        //                        <li>Tipo: " + constancia.Tipo + @"</li>
        //                        <li>Código Alumno: " + constancia.CodigoAlumno + @"</li>
        //                        <li>Alumno: " + constancia.Alumno + @"</li>
        //                        <li>Centro Costo: " + constancia.CentroCosto + @"</li>
        //                        <li>Solicitado Por: " + coordinador.Coordinadora + @"</li>
        //                        <li>Fecha Solicitud: " + constancia.FechaSolicitud.ToShortDateString() + @"</li>
        //                        <li>Fecha Emisión: " + constancia.FechaEmision.Value.ToShortDateString() + @"</li>
        //                    </ul>";

        //    texto = texto + "<p>Saludos.</p>";

        //    //texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    //texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //internal void SolicitudConfirmacion_PagoConstancia(ra_ConstanciaAlumno constancia, Coordinadoras coordinador)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + "<p><b>Estimado(a)</b></p>";

        //    texto = texto + "<p>Sirva por la presente, solicitarle pueda brindarme la confirmacion de pago de la siguiente constancia:</p>";

        //    texto = texto + @"<ul>
        //                        <li>Tipo: " + constancia.Tipo + @"</li>
        //                        <li>Código Alumno: " + constancia.CodigoAlumno + @"</li>
        //                        <li>Alumno: " + constancia.Alumno + @"</li>
        //                        <li>Centro Costo: " + constancia.CentroCosto + @"</li>
        //                        <li>Fecha Solicitud: " + constancia.FechaSolicitud.ToShortDateString() + @"</li>
        //                        <li>Solicitado Por: " + coordinador.Coordinadora + @"</li>
        //                    </ul>";

        //    texto = texto + "<p>Saludos.</p>";

        //    //texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    //texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //internal void Constancia_Coordinador(ra_ConstanciaAlumno constancia, Coordinadoras coordinador)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + "<p><b>Estimado(a)</b></p>";

        //    texto = texto + "<p>Sirva por la presente, hacerle llegar la constancia solicitada:</p>";

        //    texto = texto + @"<ul>
        //                        <li>Tipo: " + constancia.Tipo + @"</li>
        //                        <li>Código Alumno: " + constancia.CodigoAlumno + @"</li>
        //                        <li>Alumno: " + constancia.Alumno + @"</li>
        //                        <li>Centro Costo: " + constancia.CentroCosto + @"</li>
        //                        <li>Solicitado Por: " + coordinador.Coordinadora + @"</li>
        //                        <li>Fecha Solicitud: " + constancia.FechaSolicitud.ToShortDateString() + @"</li>
        //                        <li>Fecha Emisión: " + constancia.FechaEmision.Value.ToShortDateString() + @"</li>
        //                    </ul>";

        //    texto = texto + "<p>Saludos.</p>";

        //    texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //internal void Certificado_Alumno(AlumnosMatriculados alumno, ra_Certificado_Detalle certificado, Coordinadoras coordinador)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + @"
        //                        <table cellspacing='0' cellpadding='0' style='width: 600px; text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                            <tr>
        //                                <td>
        //                                    <img src='cid:cabecera' alt='BSG INSTITUTE'>
        //                                </td>
        //                            </tr>
        //                            <tr>
        //                                <td>
        //                                    <div style='text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                                        <p>
        //                                            <b>
        //                                                Estimado(a) " + alumno.nombre1 + " " + alumno.nombre2 + @":
        //                                            </b>
        //                                        </p>
        //                                        <p>
        //                                            Sirva la presente para saludarle y a la vez hacerle llegar su certificado en formato digital, el cual puede utilizar para
        //                                            los fines que considere necesarios.
        //                                        </p>
        //                                        <p>
        //                                            Posteriormente estar&eacute; comunic&aacute;ndome con Ud. para coordinar la entrega del mismo en f&iacute;sico.
        //                                        </p>
        //                                        <p>
        //                                            Mis datos de contacto y horario de atenci&oacute;n de consultas son los siguientes:
        //                                        </p>
        //                                        <ul>
        //                                            <li>
        //                                                <b>Correo Electr&oacute;nico: <a href='mailto:" + coordinador.Correo + @"'>" + coordinador.Correo + @"</a></b>
        //                                            </li>
        //                                        </ul>
        //        ";
        //    texto = texto + coordinador.HTMLNumero;
        //    texto = texto + coordinador.HTMLHorario;
        //    texto = texto + @"
        //                                        <p>
        //                                            Agradezco su atenci&oacute;n al presente y quedo a su disposici&oacute;n.
        //                                        </p>
        //                                        <p>
        //                                            Saludos cordiales,
        //                                        </p>
        //                                        <p>
        //                                            <img src='cid:firma' alt=''>
        //                                        </p>
        //                                    </div>
        //                                </td>
        //                            </tr>
        //                        </table>
        //    ";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //internal void Certificado_Alumno_Recogo_Arequipa(AlumnosMatriculados alumno, ra_Certificado_Detalle certificado, ra_Certificado_Envio envio, Coordinadoras coordinador)
        //{
        //    var saludo_estimado = Util.Saludo_Estimado(alumno.genero);
        //    DateTime fecha_recojo = envio.FechaEnvio.AddDays(2);

        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + @"
        //                        <table cellspacing='0' cellpadding='0' align='center' style='width: 600px; text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                            <tr>
        //                                <td>
        //                                    <img src='cid:cabecera' alt='BSG INSTITUTE'>
        //                                </td>
        //                            </tr>
        //                            <tr>
        //                                <td>
        //                                    <div style='text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                                        <p>
        //                                            <b>
        //                                                " + saludo_estimado + @" " + alumno.nombre1 + " " + alumno.nombre2 + @":
        //                                            </b>
        //                                        </p>
        //                                        <p>
        //                                            Le saludamos muy cordialmente. La presente es para informarle que puede recoger su certificado de nuestras oficinas en la ciudad de Arequipa, 
        //                                            portando su DNI a partir del " + fecha_recojo.Day + " de " + fecha_recojo.ToString("MMMM") + " de " + fecha_recojo.Year + @", preguntando por el Sr. Jonathan Pérez,
        //                                            considerando lo siguiente:
        //                                        </p>
        //                                        <p>
        //                                            <b>DIRECCIÓN:</b> Urb. León XIII Calle 2 Nro 107 Cayma - Arequipa
        //                                        </p>
        //                                        <p>
        //                                            <b>HORARIO:</b> Lunes a Viernes de 10am. a 7pm. / Sábado de 9am. a 1pm.
        //                                        </p>
        //                                        <p>
        //                                            Envío adjunto su certificado en formato digital.
        //                                        </p>
        //        ";

        //    texto = texto + ContactoCoodinador(coordinador);

        //    texto = texto + @"
        //                                        <p>
        //                                            Atentamente, 
        //                                        </p>
        //                                        <p>
        //                                            <img src='cid:firma' alt=''>
        //                                        </p>
        //                                    </div>
        //                                </td>
        //                            </tr>
        //                        </table>
        //    ";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}
        //internal void Certificado_Alumno_Recogo_Lima(AlumnosMatriculados alumno, ra_Certificado_Detalle certificado, ra_Certificado_Envio envio, Coordinadoras coordinador)
        //{
        //    var saludo_estimado = Util.Saludo_Estimado(alumno.genero);
        //    DateTime fecha_recojo = envio.FechaEnvio.AddDays(2);

        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + @"
        //                        <table cellspacing='0' cellpadding='0' align='center' style='width: 600px; text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                            <tr>
        //                                <td>
        //                                    <img src='cid:cabecera' alt='BSG INSTITUTE'>
        //                                </td>
        //                            </tr>
        //                            <tr>
        //                                <td>
        //                                    <div style='text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                                        <p>
        //                                            <b>
        //                                                " + saludo_estimado + @" " + alumno.nombre1 + " " + alumno.nombre2 + @":
        //                                            </b>
        //                                        </p>
        //                                        <p>
        //                                            Le saludamos muy cordialmente. La presente es para informarle que puede recoger su certificado de nuestras oficinas en la ciudad de Lima, 
        //                                            portando su DNI a partir del " + fecha_recojo.Day + " de " + fecha_recojo.ToString("MMMM") + " de " + fecha_recojo.Year + @", preguntando por la Srta. Geraldine Morán o la Srta. Gabriela Aguirre,
        //                                            considerando lo siguiente:
        //                                        </p>
        //                                        <p>
        //                                            <b>DIRECCIÓN:</b> Av. José Pardo 650. Miraflores
        //                                        </p>
        //                                        <p>
        //                                            <b>HORARIO:</b> Lunes a Viernes de 3pm. a 8pm. / Sábado de 9am. a 1pm.
        //                                        </p>
        //                                        <p>
        //                                            Envío adjunto su certificado en formato digital.
        //                                        </p>
        //        ";

        //    texto = texto + ContactoCoodinador(coordinador);

        //    texto = texto + @"
        //                                        <p>
        //                                            Atentamente, 
        //                                        </p>
        //                                        <p>
        //                                            <img src='cid:firma' alt=''>
        //                                        </p>
        //                                    </div>
        //                                </td>
        //                            </tr>
        //                        </table>
        //    ";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}
        //internal void Certificado_Alumno_Recogo_Bogota(AlumnosMatriculados alumno, ra_Certificado_Detalle certificado, ra_Certificado_Envio envio, Coordinadoras coordinador)
        //{
        //    var saludo_estimado = Util.Saludo_Estimado(alumno.genero);
        //    DateTime fecha_recojo = envio.FechaEnvio.AddDays(2);

        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + @"
        //                        <table cellspacing='0' cellpadding='0' align='center' style='width: 600px; text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                            <tr>
        //                                <td>
        //                                    <img src='cid:cabecera' alt='BSG INSTITUTE'>
        //                                </td>
        //                            </tr>
        //                            <tr>
        //                                <td>
        //                                    <div style='text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                                        <p>
        //                                            <b>
        //                                                " + saludo_estimado + @" " + alumno.nombre1 + " " + alumno.nombre2 + @":
        //                                            </b>
        //                                        </p>
        //                                        <p>
        //                                            Le saludamos muy cordialmente. La presente es para informarle que puede recoger su certificado de nuestras oficinas en la ciudad de Bogotá, 
        //                                            portando su documento de identidad a partir del " + fecha_recojo.Day + " de " + fecha_recojo.ToString("MMMM") + " de " + fecha_recojo.Year + @", preguntando en recepción del edificio por la Srta. Eliana Sánchez, 
        //                                            considerando lo siguiente:
        //                                        </p>
        //                                        <p>
        //                                            <b>DIRECCIÓN:</b> Avenida Carrera 45 (autopista Norte) Número 108-27 Edificio Paralelo 108 Torre 1 Oficina 1008 (10mo piso) 
        //                                        </p>
        //                                        <p>
        //                                            <b>HORARIO:</b> Lunes a Viernes de 3pm. a 7pm. / Sábado de 9am. a 1pm.
        //                                        </p>
        //                                        <p>
        //                                            Envío adjunto su certificado en formato digital.
        //                                        </p>
        //        ";

        //    texto = texto + ContactoCoodinador(coordinador);

        //    texto = texto + @"
        //                                        <p>
        //                                            Atentamente, 
        //                                        </p>
        //                                        <p>
        //                                            <img src='cid:firma' alt=''>
        //                                        </p>
        //                                    </div>
        //                                </td>
        //                            </tr>
        //                        </table>
        //    ";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}
        //internal void Certificado_Alumno_Recogo_TNT(AlumnosMatriculados alumno, ra_Certificado_Detalle certificado, ra_Certificado_Envio envio, Coordinadoras coordinador)
        //{
        //    var saludo_estimado = Util.Saludo_Estimado(alumno.genero);

        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + @"
        //                        <table cellspacing='0' cellpadding='0' align='center' style='width: 600px; text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                            <tr>
        //                                <td>
        //                                    <img src='cid:cabecera' alt='BSG INSTITUTE'>
        //                                </td>
        //                            </tr>
        //                            <tr>
        //                                <td>
        //                                    <div style='text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                                        <p>
        //                                            <b>
        //                                                " + saludo_estimado + @" " + alumno.nombre1 + " " + alumno.nombre2 + @":
        //                                            </b>
        //                                        </p>
        //                                        <p>
        //                                            Le saludamos muy cordialmente. La presente es para informarle que se ha realizado el envío de su certificado a la dirección indicada, a través de la empresa TNT Express.
        //                                            El documento estará llegando a destino en el plazo de 15 a 20 días a partir de la fecha.
        //                                        </p>
        //                                        <p>
        //                                            Envío adjunto su certificado en formato digital.
        //                                        </p>
        //        ";

        //    texto = texto + ContactoCoodinador(coordinador);

        //    texto = texto + @"
        //                                        <p>
        //                                            Atentamente, 
        //                                        </p>
        //                                        <p>
        //                                            <img src='cid:firma' alt=''>
        //                                        </p>
        //                                    </div>
        //                                </td>
        //                            </tr>
        //                        </table>
        //    ";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}
        //internal void Certificado_Alumno_Recogo_OLVA(AlumnosMatriculados alumno, ra_Certificado_Detalle certificado, ra_Certificado_Envio envio, Coordinadoras coordinador)
        //{
        //    var saludo_estimado = Util.Saludo_Estimado(alumno.genero);

        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + @"
        //                        <table cellspacing='0' cellpadding='0' align='center' style='width: 600px; text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                            <tr>
        //                                <td>
        //                                    <img src='cid:cabecera' alt='BSG INSTITUTE'>
        //                                </td>
        //                            </tr>
        //                            <tr>
        //                                <td>
        //                                    <div style='text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                                        <p>
        //                                            <b>
        //                                                " + saludo_estimado + @" " + alumno.nombre1 + " " + alumno.nombre2 + @":
        //                                            </b>
        //                                        </p>
        //                                        <p>
        //                                            Le saludamos muy cordialmente. La presente es para informarle que se ha realizado el envío de su certificado a la dirección indicada, 
        //                                            a través de la empresa Olva Courier con Nº de remito " + envio.CodigoSeguimiento + @".
        //                                        </p>
        //                                        <p>
        //                                            Puede realizar el seguimiento del envío en la siguiente URL, ingresando el código de remito proporcionado: <a href='https://www.olvacourier.com/'>https://www.olvacourier.com/</a>
        //                                        </p>
        //                                        <p>
        //                                            Envío adjunto su certificado en formato digital.
        //                                        </p>
        //        ";

        //    texto = texto + ContactoCoodinador(coordinador);

        //    texto = texto + @"
        //                                        <p>
        //                                            Atentamente, 
        //                                        </p>
        //                                        <p>
        //                                            <img src='cid:firma' alt=''>
        //                                        </p>
        //                                    </div>
        //                                </td>
        //                            </tr>
        //                        </table>
        //    ";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}
        //internal void Certificado_Alumno_SoloDigital(AlumnosMatriculados alumno, ra_Certificado_Detalle certificado, Coordinadoras coordinador)
        //{
        //    var saludo_estimado = Util.Saludo_Estimado(alumno.genero);

        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + @"
        //                        <table cellspacing='0' cellpadding='0' align='center' style='width: 600px; text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                            <tr>
        //                                <td>
        //                                    <img src='cid:cabecera' alt='BSG INSTITUTE'>
        //                                </td>
        //                            </tr>
        //                            <tr>
        //                                <td>
        //                                    <div style='text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                                        <p>
        //                                            <b>
        //                                                " + saludo_estimado + @" " + alumno.nombre1 + " " + alumno.nombre2 + @":
        //                                            </b>
        //                                        </p>
        //                                        <p>
        //                                            Nos es grato saludarlo en esta oportunidad. 
        //                                            El motivo de la presente es para hacerle llegar el certificado correspondiente a su capacitación, en formato digital.
        //                                        </p>
        //                                        <p>
        //                                            Posteriormente me estaré comunicándose con Ud. para coordinar la entrega del mismo en físico.
        //                                        </p>
        //        ";

        //    texto = texto + @"
        //                                        <p>
        //                                            Atentamente, 
        //                                        </p>
        //                                        <p>
        //                                            <img src='cid:firma' alt=''>
        //                                        </p>
        //                                    </div>
        //                                </td>
        //                            </tr>
        //                        </table>
        //    ";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        internal void HistorialCambioAlumnoRegistro(HistorialCambioDetalleDTO solicitud, AlumnoMatriculadoDatoDTO alumno, CoordinadoraBO coordinadorSolicitante, bool prueba)
        {
            string url = "";
            if (prueba)
                url = "http://localhost:63048/";
            else
                url = "http://integra.bsginstitute.net:900/";
            url += "Coordinador/HistorialCambioAlumno/Detalle/" + solicitud.IdHistoricoCambioAlumno;

            var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

            texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

            texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

            texto = texto + "<p><b>Estimados(as)</b></p>";

            texto = texto + "<p>Sirva por la presente, indicar el registro de la siguiente solicitud:</p>";

            texto = texto + @"<ul>
                                <li>Id: " + solicitud.IdHistoricoCambioAlumno + @"</li>
                                <li>Tipo: " + solicitud.TipoHistoricoCambioAlumno + @"</li>
                                <li>Código Alumno: " + solicitud.CodigoAlumno + @"</li>
                                <li>Alumno: " + (alumno != null ? alumno.NombreAlumno : "") + @"</li>
                                <li>Centro Costo Origen: " + solicitud.CentroCostoOrigen + @"</li>
                                <li>Centro Costo Destino: " + solicitud.CentroCostoDestino + @"</li>
                                <li>Solicitante: " + coordinadorSolicitante.NombreResumido + @"</li>
                                <li>Comentario: " + solicitud.ComentarioSolicitud + @"</li>
                                <li>Url: <a href=" + url + ">" + url + @"</a></li>
                            </ul>";

            texto = texto + "<p>Saludos.</p>";

            //texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

            texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

            texto = texto + "</BODY></HTML>";

            Mensaje = texto;
        }
        internal void HistorialCambioAlumnoCancelar(HistorialCambioDetalleDTO solicitud, AlumnoMatriculadoDatoDTO alumno, CoordinadoraBO coordinadorSolicitante, bool prueba)
        {
            string url = "";
            if (prueba)
                url = "http://localhost:53519/";
            else
                url = "http://integra.bsginstitute.net:900/";
            url += "Coordinador/HistorialCambioAlumno/Detalle/" + solicitud.IdHistoricoCambioAlumno;

            var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

            texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

            texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

            texto = texto + "<p><b>Estimados(as)</b></p>";

            texto = texto + "<p>Sirva por la presente, indicar que se <b>Canceló</b> la siguiente solicitud:</p>";

            texto = texto + @"<ul>
                                <li>Id: " + solicitud.IdHistoricoCambioAlumno + @"</li>                                
                                <li>Tipo: " + solicitud.TipoHistoricoCambioAlumno + @"</li>
                                <li>Código Alumno: " + solicitud.CodigoAlumno + @"</li>
                                <li>Alumno: " + (alumno != null ? alumno.NombreAlumno : "") + @"</li>
                                <li>Centro Costo Origen: " + solicitud.CentroCostoOrigen + @"</li>
                                <li>Centro Costo Destino: " + solicitud.CentroCostoDestino + @"</li>
                                <li>Solicitante: " + coordinadorSolicitante.NombreResumido + @"</li>
                                <li>Url: <a href=" + url + ">" + url + @"</a></li>
                            </ul>";

            texto = texto + "<p>Saludos.</p>";

            texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

            //texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

            texto += "</BODY></HTML>";

            Mensaje = texto;
        }
        internal void HistorialCambioAlumnoAprobar(HistorialCambioDetalleDTO solicitud, AlumnoMatriculadoDatoDTO alumno, CoordinadoraBO coordinadorSolicitante, bool prueba)
        {
            string url = "";
            if (prueba)
                url = "http://localhost:53519/";
            else
                url = "http://integra.bsginstitute.net:900/";
            url += "Coordinador/HistorialCambioAlumno/Detalle/" + solicitud.IdHistoricoCambioAlumno;

            var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

            texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

            texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

            texto = texto + "<p><b>Estimados(as)</b></p>";

            if (solicitud.Aprobado == true)
                texto = texto + "<p>Sirva por la presente, indicar que se <b>Aprobó</b> la siguiente solicitud:</p>";
            else
                texto = texto + "<p>Sirva por la presente, indicar que se <b>Desaprobó</b> la siguiente solicitud:</p>";

            texto = texto + @"<ul>
                                <li>Id: " + solicitud.IdHistoricoCambioAlumno + @"</li>                                
                                <li>Tipo: " + solicitud.TipoHistoricoCambioAlumno + @"</li>
                                <li>Código Alumno: " + solicitud.CodigoAlumno + @"</li>
                                <li>Alumno: " + (alumno != null ? alumno.NombreAlumno : "") + @"</li>
                                <li>Centro Costo Origen: " + solicitud.CentroCostoOrigen + @"</li>
                                <li>Centro Costo Destino: " + solicitud.CentroCostoDestino + @"</li>
                                <li>Solicitante: " + coordinadorSolicitante.NombreResumido + @"</li>
                                <li>Url: <a href=" + url + ">" + url + @"</a></li>
                            </ul>";

            texto = texto + "<p>Saludos.</p>";

            texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

            //texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

            texto = texto + "</BODY></HTML>";

            Mensaje = texto;
        }

        //public void SolicitudAcceso_PortalWeb_Registrar(Registro_Solicitud_AccesoPortalWebViewModel solicitud, Coordinadoras coordinador)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + "<p><b>Estimados(as)</b></p>";

        //    texto = texto + "<p>Sirva por la presente, Solicitar los Accesos al Portal Web del(los) siguiente(s) alumno(s):</p>";

        //    texto = texto + @"<ul>
        //                        <li>Código Alumno: " + solicitud.CodigoAlumno + @"</li>
        //                        <li>Alumno: " + solicitud.Alumno + @"</li>
        //                        <li>Centro Costo: " + solicitud.CentroCosto + @"</li>
        //                        <li>Email 1: " + solicitud.Email1 + @"</li>
        //                    </ul>";

        //    texto = texto + "<p>Saludos.</p>";

        //    //texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //public void SolicitudAcceso_PortalWeb_RegistrarMultiple(List<Registro_Solicitud_AccesoPortalWebViewModel> listado_solicitud, Coordinadoras coordinador)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + "<p><b>Estimados(as)</b></p>";

        //    texto = texto + "<p>Sirva por la presente, Solicitar los Accesos al Portal Web del(los) siguiente(s) alumno(s):</p>";

        //    foreach (var solicitud in listado_solicitud)
        //    {
        //        texto = texto + @"<ul>
        //                        <li>Código Alumno: " + solicitud.CodigoAlumno + @"</li>
        //                        <li>Alumno: " + solicitud.Alumno + @"</li>
        //                        <li>Centro Costo: " + solicitud.CentroCosto + @"</li>
        //                        <li>Email 1: " + solicitud.Email1 + @"</li>
        //                    </ul>";
        //    }

        //    texto = texto + "<p>Saludos.</p>";

        //    //texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //public void Envio_ManualBsPlay(ra_AccesoAulaVirtual_DatosCronograma alumno, Coordinadoras coordinador)
        //{
        //    string tratamiento_alumno = Util.Saludo_EstimadoSr(alumno.genero);

        //    var texto =
        //        "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western";
        //    texto = texto + " { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl";
        //    texto = texto + " { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";


        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='border: none; padding: 0in'>";

        //    texto = texto + "<P style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'><B>" + tratamiento_alumno + alumno.nombre1.ToUpper() + "</B></P>";

        //    texto = texto + "<p style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'>Me es grato dirigirme a Ud., y poner a su alcance el manual de uso del aplicativo BS Play.</p>";
        //    texto = texto + "<p style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'>El cual le permitir&aacute; poder descargar los videos de vuestro curso en su dispositivo m&oacute;vil para ser reproducidos sin conexi&oacute;n a Internet.</p>";

        //    //Requisitos Apps
        //    texto = texto + "<p style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'>La aplicaci&oacute;n en menci&oacute;n tiene los siguientes requisitos:</p>";
        //    texto = texto + "<ul><li style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'>Sistema Operativo: Android 4 (&oacute; superior, Tablet o Smartphone), IOS 6 (&oacute; superior, tablet Ipad)</li>";
        //    //texto = texto + "<ul><li style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'>Sistema Operativo: Android 4 (&oacute; superior, Tablet o Smartphone)</li>";
        //    texto = texto + "<li style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'>Memoria Interna Libre del dispositivo: 1GB m&iacute;nimo (cada video tiene un tama&ntilde;o aproximado de 400MB)</li></ul>";

        //    //Enlaces de Descarga en la Apps Store
        //    texto = texto + "<p style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'>Podr&aacute; instalar la aplicaci&oacute;n en su dispositivo desde los siguientes enlaces:</p>";
        //    //texto = texto + "<ul><li style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'>Para Android - <a href=\"https://play.google.com/store/apps/details?id=com.ispringsolutions.bsplay\">desde la Play Store</a></li>";
        //    texto = texto + "<ul><li style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'>Para Android - <a href=\"http://d33a1bwwgr1uvf.cloudfront.net/BS%20Play_.apk\">Descargar</a></li>";
        //    texto = texto + "<li style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'>Para Ipad - <a href=\"https://itunes.apple.com/pe/app/bs-play/id974621455?mt=8\">desde la App Store</a></li></ul>";
        //    //texto = texto + "<p style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'>Podr&aacute; instalar la aplicaci&oacute;n en su dispositivo desde el siguiente enlace:</p>";
        //    //texto = texto + "<ul><li style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'><a href='http://d33a1bwwgr1uvf.cloudfront.net/BS%20Play_.apk'>Descargar Aplicativo BS Play</a></li></ul>";

        //    //texto = texto + "<p style='text-aling: center;'>" +
        //    //   "<a href=\"http://virtual.bsginstitute.com/index.php?id=" + alumno.CodigoAlumno + "&manual=BSPlay\" style='background-color: #004b83;border:1px solid #004b83;border-radius:5px;color:#ffffff;display:inline-block;font-family:sans-serif;font-size:14px;height:25px;text-align:center;text-decoration:none;width:150px;-webkit-text-size-adjust:none;mso-hide:all;margin-left: 30px;padding: 5px;'><b>Descargar aqu&iacute;</b></a>" +
        //    // "</p>";
        //    //Enlace de descarga del Manual de BS Play
        //    texto = texto + "<p style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'>El manual respectivo podr&aacute; descargarlo mediante el siguiente enlace:</p>";
        //    texto = texto + "<p style='text-aling: center;'>" +
        //                     "<a href=\"http://virtual.bsginstitute.com/manual/bsplay.php?id=" + alumno.CodigoAlumno + "&manual=BSPlay\" style='font-family:sans-serif;font-size:14px;margin-left: 30px;'><b>Descargar Manual BS Play</b></a>" +
        //                     "</p>";

        //    //numero de contacto
        //    texto = texto + ContactoCoodinador(coordinador);

        //    texto = texto + "<P style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'>Agradezco la atenci&oacute;n al presente y quedo a su servicio. ";
        //    texto = texto + "<P style='font-family: Arial, sans-serif; font-size: 14px; line-height: 20px;'>Saludos.</p>";

        //    texto = texto + "<P LANG=\"\"  STYLE=\"margin-bottom: 0.11in\"><FONT COLOR=\"#17365d\"><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></FONT></P>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //internal void SolicitudAcceso_AulaVirtual_Aviso_Regular(AccesoAulaVirtual_ModeloCorreo modelo)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='border: none; padding: 0in'>";

        //    texto = texto + "<p>Estimados,</p>";

        //    texto = texto + "<p>Se confirma la creación y envío de accesos:</p>";

        //    texto = texto + "<p>TIPO DE MATRÍCULA - " + modelo.Solicitud.ra_AccesoAulaVirtual_Tipo.Nombre + "</p>";
        //    texto = texto + "<p>NOMBRE- " + modelo.Alumno.Alumno.ToUpper() + "</p>";
        //    texto = texto + "<p>CENTRO COSTO - " + modelo.Alumno.centrocosto.ToUpper() + "</p>";

        //    string nombreProgramaEspecifico = modelo.Alumno.nombrePE.ToUpper();
        //    if (nombreProgramaEspecifico.Contains("("))
        //        nombreProgramaEspecifico.Substring(0, nombreProgramaEspecifico.IndexOf("(")).ToUpper();

        //    texto = texto + "<p>PROGRAMA - " + nombreProgramaEspecifico + "</p>";
        //    texto = texto + "<p>CODIGO MATRÍCULA - " + modelo.Alumno.CodigoAlumno.ToUpper() + "</p>";
        //    texto = texto + "<p>COORDINADOR(A) ACADÉMICO(A)- " + modelo.Coordinador.Coordinadora.ToUpper() + "</p>";


        //    texto = texto + "<p>Saludos.</p>";

        //    //texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:picture1' ALIGN=BOTTOM WIDTH=415 HEIGHT=249 BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //internal void SolicitudAcceso_AulaVirtual_Aviso_Traslado(AccesoAulaVirtual_ModeloCorreo modelo)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='border: none; padding: 0in'>";

        //    texto = texto + "<p>Estimados,</p>";

        //    texto = texto + "<p>Se confirma la creación y envío de accesos:</p>";

        //    texto = texto + "<p>TIPO DE MATRÍCULA - " + modelo.Solicitud.ra_AccesoAulaVirtual_Tipo.Nombre.ToUpper() + "</p>";
        //    texto = texto + "<p>NOMBRE- " + modelo.Alumno.Alumno.ToUpper() + "</p>";
        //    texto = texto + "<p>CENTRO COSTO - " + modelo.Alumno.centrocosto.ToUpper() + "</p>";

        //    string nombreProgramaEspecifico = modelo.Alumno.nombrePE.ToUpper();
        //    if (nombreProgramaEspecifico.Contains("("))
        //        nombreProgramaEspecifico.Substring(0, nombreProgramaEspecifico.IndexOf("(")).ToUpper();

        //    texto = texto + "<p>PROGRAMA - " + nombreProgramaEspecifico + "</p>";
        //    texto = texto + "<p>CODIGO MATRÍCULA - " + modelo.Alumno.CodigoAlumno.ToUpper() + "</p>";
        //    texto = texto + "<p>COORDINADOR(A) ACADÉMICO(A)- " + modelo.Coordinador.Coordinadora.ToUpper() + "</p>";


        //    texto = texto + "<p>Saludos.</p>";

        //    //texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:picture1' ALIGN=BOTTOM WIDTH=415 HEIGHT=249 BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //public void SolicitudAcceso_AulaVirtual_Aviso_Temporal(AccesoAulaVirtual_ModeloCorreo modelo)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='border: none; padding: 0in'>";

        //    texto = texto + "<p>Estimados,</p>";

        //    texto = texto + "<p>Se confirma la creación del acceso temporal solicitado:</p>";

        //    texto = texto + "<p> <b>LINK:</b> <a href='http://virtual.bsginstitute.com/login/index.php'> http://virtual.bsginstitute.com/login/index.php </a> <br>";
        //    texto = texto + "<b>USUARIO:</b> " + modelo.Solicitud.UsuarioMoodle + "<br>";
        //    texto = texto + "<b>CONTRASEÑA:</b> " + modelo.Solicitud.ClaveMoodle + "<br>";
        //    texto = texto + "<b>VIGENCIA:</b> " + modelo.Solicitud.FechaMatricula.Value.AddDays(modelo.Solicitud.DuracionAcceso.Value).ToShortDateString() + "<br> </p>";

        //    texto = texto + "<p>Saludos cordiales.</p>";

        //    //texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:picture1' ALIGN=BOTTOM WIDTH=415 HEIGHT=249 BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //public void SolicitudAcceso_AulaVirtual_Aviso_Recuperacion(AccesoAulaVirtual_ModeloCorreo modelo)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='border: none; padding: 0in'>";

        //    texto = texto + "<p>Estimados,</p>";

        //    texto = texto + "<p>Se confirma la matrícula solicitada para el alumno(a): <b>" + modelo.Alumno.Alumno.ToUpper() + "</b></p>";

        //    texto = texto + "<p> <b>LINK:</b> <a href='http://virtual.bsginstitute.com/login/index.php'> http://virtual.bsginstitute.com/login/index.php </a> <br>";
        //    texto = texto + "<p> <b>USUARIO:</b> " + modelo.Solicitud.UsuarioMoodle + "<br>";
        //    texto = texto + "<b>CONTRASEÑA:</b> " + modelo.Solicitud.ClaveMoodle + "<br>";
        //    texto = texto + "<b>VIGENCIA:</b> " + modelo.Solicitud.FechaMatricula.Value.AddDays(modelo.Solicitud.DuracionAcceso.Value).ToShortDateString() + "<br> </p>";

        //    texto = texto + "<p>Saludos cordiales.</p>";

        //    //texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:picture1' ALIGN=BOTTOM WIDTH=415 HEIGHT=249 BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //private string ContactoCoodinador(Coordinadoras coordinadora)
        //{
        //    string texto = "";

        //    texto = texto + "<P    STYLE=\"margin-bottom: 0.11in\">Los n&uacute;meros telef&oacute;nicos en los que podr&aacute; comunicarse conmigo son:</P>";
        //    texto = texto + coordinadora.HTMLNumero;

        //    texto = texto + "<P    STYLE=\"margin-bottom: 0.11in\">Los horarios de atenci&oacute;n para consultas telef&oacute;nicas y respuesta a correos electr&oacute;nicos son:</P> ";
        //    texto = texto + coordinadora.HTMLHorario;

        //    return texto;
        //}

        //public void RestablecerClave_AlumnoPortalWeb(AlumnoPresencialBOL alumno_integra, AspNetUsers alumno_portal, Coordinadoras coordinador)
        //{
        //    string saludo_estimado = Util.Saludo_Estimado(alumno_integra.Genero);

        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + @"
        //                        <table cellspacing='0' cellpadding='0' align='center' style='width: 600px; text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                            <tr>
        //                                <td>
        //                                    <img src='cid:cabecera' alt='BSG INSTITUTE'>
        //                                </td>
        //                            </tr>
        //                            <tr>
        //                                <td>
        //                                    <div style='text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
        //                                        <p>
        //                                            <b>
        //                                                " + saludo_estimado + " " + alumno_integra.nombre1 + " " + alumno_integra.nombre2 + @":
        //                                            </b>
        //                                        </p>
        //                                        <p>
        //                                            Sirva la presente para recordarle su usuario y clave de nuestro Portal Web:
        //                                        </p>
        //                                        <ul>
        //                                            <li>
        //                                                <b>Url:</b> <a href='https://bsginstitute.com/Cuenta'>https://bsginstitute.com/</a>
        //                                            </li>
        //                                            <li>
        //                                                <b>Usuario:</b> " + alumno_portal.UserName + @"
        //                                            </li>
        //                                            <li>
        //                                                <b>Clave:</b> " + alumno_portal.us_clave + @"
        //                                            </li>
        //                                        </ul>
        //        ";
        //    //numero de contacto
        //    texto = texto + ContactoCoodinador(coordinador);

        //    texto = texto + @"
        //                                        <p>
        //                                            Agradezco su atenci&oacute;n al presente y quedo a su disposici&oacute;n.
        //                                        </p>
        //                                        <p>
        //                                            Saludos cordiales,
        //                                        </p>
        //                                        <p>
        //                                            <img src='cid:firma' alt=''>
        //                                        </p>
        //                                    </div>
        //                                </td>
        //                            </tr>
        //                        </table>
        //    ";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //public void Convenio_Reprogramacion(ra_Convenio_Reprogramacion convenio, Coordinadoras coordinador)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='border: none; padding: 0in'>";

        //    texto = texto + "<p>Estimado(a):</p>";
        //    texto = texto + "<p><b>" + coordinador.Coordinadora + "</b></p>";

        //    texto = texto + "<p>Mediante la presente le hago llegar el convenio de Reprogramación solicitado.</p>";

        //    texto = texto + "<p>Saludos.</p>";

        //    //texto = texto + "<p> <IMG SRC = 'cid:firma' ALIGN = BOTTOM BORDER = 0> </p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}
    }
}