using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.Clases
{
    public class MailTemplatePresencial
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

        //    mensaje = texto;
        //}

        //internal void SubidaMaterial_Confirmacion(string comentario, string nombre_archivo, ra_Curso curso, string nombre_usuario)
        //{
        //    DateTime? primera_sesion_regular = curso.ra_Sesion.Where(w => w.ra_Sesion_Tipo.Nombre == TipoSesion.Regular).Min(m => m.Fecha);

        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + "<p><b>Estimados(as)</b></p>";

        //    texto = texto + "<p>Sirva por la presente, informar que se subió el siguiente material:</p>";

        //    texto = texto + "<p><b><a href='" + UrlIntegra.Produccion + "Presencial/CentroCosto/Detalle/" + curso.ra_CentroCosto.Id + "'>" + curso.ra_CentroCosto.NombreCentroCosto + "</a></b> - " + curso.NombreCurso + " - Fecha Inicio: " + (primera_sesion_regular?.ToShortDateString() ?? "") + " - Usuario: " + nombre_usuario + "</p>";

        //    texto = texto + "<ul><li>" + nombre_archivo + "</li></ul>";

        //    texto = texto + "<p><b>Comentario:</b></p>";

        //    texto = texto + "<p>" + comentario + "</p>";

        //    texto = texto + "<p>Saludos.</p>";

        //    texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    //texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //internal void SubidaMaterialClonado_Confirmacion(string comentario, List<ra_Curso_Material> lista_materiales, ra_Curso curso, string nombre_usuario)
        //{
        //    DateTime? primera_sesion_regular = curso.ra_Sesion.Where(w => w.ra_Sesion_Tipo.Nombre == TipoSesion.Regular).Min(m => m.Fecha);

        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + "<p><b>Estimados(as)</b></p>";

        //    texto = texto + "<p>Sirva por la presente, informar que se subió el siguiente material:</p>";

        //    texto = texto + "<p><b>" + curso.ra_CentroCosto.NombreCentroCosto + "</b> - " + curso.NombreCurso + " - Fecha Inicio: " + (primera_sesion_regular?.ToShortDateString() ?? "") + " - Usuario: " + nombre_usuario + "</p>";

        //    foreach (var item in lista_materiales)
        //    {
        //        var nombre_archivo = item.NombreArchivoEditado != null ? item.NombreArchivoEditado : item.NombreArchivo;

        //        texto = texto + "<ul><li><a href='" + UrlIntegra.Produccion + "Presencial/CentroCosto/Detalle/" + curso.ra_CentroCosto.Id + "'>" + nombre_archivo + "</a></li></ul>";
        //    }

        //    texto = texto + "<p><b>Comentarios:</b></p>";
        //    texto = texto + "<p>" + comentario + "</p>";

        //    texto = texto + "<p>Saludos.</p>";

        //    texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    //texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //internal void MaterialProveedor(ra_Curso_Material material, ra_ProveedorMaterial proveedor, ra_Sede sede, List<ra_Curso_MaterialAlumno> listado_material_alumnos)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    if (proveedor.Genero)
        //        texto = texto + "<p><b>Estimado Sr. " + proveedor.Contacto + ".</b></p>";
        //    else
        //        texto = texto + "<p><b>Estimada Srta. " + proveedor.Contacto + ".</b></p>";

        //    texto = texto + "<p>El material solicitado podrá descargarlo desde el siguiente enlace:</p>";

        //    texto = texto + "<p><a href='https://s3.amazonaws.com/bsgrupo-operaciones-materiales/" + material.CarpetaAmazon + material.NombreArchivoEnviadoProveedor + "'>https://s3.amazonaws.com/bsgrupo-operaciones-materiales/" + material.CarpetaAmazon + material.NombreArchivoEnviadoProveedor + "</a></p>";

        //    texto = texto + "<p>Se requiere:</p>";

        //    //se colocan todos los materiales que se requieren

        //    texto = texto + @"
        //                        <table cellpadding='0' cellspacing='0' width='100%' border=1 align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify;'>    
        //                            <tr>
        //                                <th align='center'>Tipo</th>
        //                                <th align='center'>Cantidad</th>
        //                                <th align='center'>Comentarios</th>
        //                            </tr>
        //    ";
        //    foreach (var material_alumno in listado_material_alumnos)
        //    {
        //        texto = texto + @"
        //                            <tr>
        //                                <td>" + material_alumno.ra_Curso_MaterialAlumno_Tipo.Nombre + @"</td>
        //                                <td>" + material_alumno.Cantidad + @"</td>
        //                                <td>" + material_alumno.Comentario + @"</td>
        //                            </tr>
        //        ";
        //    }
        //    texto = texto + @"
        //                        </table>
        //    ";

        //    texto = texto + "<p><b>Obervaciones:</b>  " + material.ObservacionProveedor + "</p>";

        //    if (material.ra_Curso.ra_CentroCosto.NombreCentroCosto.Contains("BOGOTA"))
        //        texto = texto + "<p>Favor coordinar la entrega con la Srta. Eliana Sanchez.</p>";
        //    else
        //        texto = texto + "<p>Hacer la entrega en nuestra sede de la " + sede.Direccion + ".</p>";

        //    texto = texto + "<p>Fecha de Entrega: " + material.FechaRecepcionEstimadaImpresion.Value.ToString("dd-MM-yyyy") + "</p>";

        //    texto = texto + "<p>Saludos.</p>";

        //    //texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    texto = texto + "<p style='font-size: 10pt;'><b>PD:</b> El enlace estará activo en aprox. 10 minutos.</p>";

        //    texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //internal void CancelacionMaterialProveedor(ra_Curso_Material material, ra_ProveedorMaterial proveedor, string observacion_proveedor)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    if (proveedor.Genero)
        //        texto = texto + "<p><b>Estimado Sr. " + proveedor.Contacto + ".</b></p>";
        //    else
        //        texto = texto + "<p><b>Estimada Srta. " + proveedor.Contacto + ".</b></p>";

        //    texto = texto + "<p>Material:</p>";

        //    texto = texto + "<p><a href='https://s3.amazonaws.com/bsgrupo-operaciones-materiales/" + material.CarpetaAmazon + material.NombreArchivoEnviadoProveedor + "'>https://s3.amazonaws.com/bsgrupo-operaciones-materiales/" + material.CarpetaAmazon + material.NombreArchivoEnviadoProveedor + "</a></p>";

        //    texto = texto + "<p>Detalle:  " + observacion_proveedor + "</p>";

        //    texto = texto + "<p>Saludos.</p>";

        //    //texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        internal void EnviarAsistenciaDetalladaOnline(AlumnoPresencialDTO alumno, CoordinadoraBO coordinadora, List<ReporteAsistenciaOnlineDTO> listado_sesiones_asistencia)
        {
            var texto = @"<HTML><STYLE TYPE = 'text/css' >< !-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107 %; text-align: left }P.western { font-family: Calibri,'Arial', sans-serif; font-size: 11pt; so-language: en-US }P.cjk {font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA } A:link {color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>

                                <BODY LANG='es-PE' LINK='#0563c1' DIR='LTR' STYLE='border: none; padding: 0in;'>

                                     <table cellpadding='0' cellspacing='0' width='600' align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 12pt; text-align: justify;'>    
                                        <tr>
                                            <td align='center'>
                                                <img src='cid:cabecera' width='600' height='170'>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td style='text-align: justify;'>
                                           <div style='font-family: Calibri,Arial,Helveltica; font-size: 12pt; text-align: justify;'>";
            if (alumno.Genero == "M")
                texto = texto + "                <p style='text-align: justify;'><b>Estimado " + alumno.Nombre1.ToUpper() + ":</b></p>";
            else
            {
                if (alumno.Genero == "F")
                    texto = texto + "            <p style='text-align: justify;'><b>Estimada " + alumno.Nombre1.ToUpper() + ":</b></p>";
                else
                    texto = texto + "            <p style='text-align: justify;'><b>Estimado(a) " + alumno.Nombre1.ToUpper() + ":</b></p>";
            }

            texto = texto + @"                  <p style='text-align: justify;'>De mi especial consideraci&oacute;n.</p>
                                                <p style='text-align: justify;' >Tengo el agrado de dirigirme a usted, para expresarle un cordial y atento saludo en nombre de BSG INSTITUTE, y a la vez hacerle llegar el porcentaje de asistencia obtenido hasta el momento:</p>
                                                <br>
                                           </div>
                                        </td>";


            var listado_contador_asistencias = listado_sesiones_asistencia
                                                  .GroupBy(y => new { NombreCurso = y.NombreCurso })
                                                  .Select(x => new { NombreCurso = x.Key.NombreCurso, CantidadTotal = x.Count(), CantidadAsistio = x.Count(w => w.Asistio == "X") }).OrderBy(o => o.NombreCurso).ToList();

            texto = texto + @"         <tr>
                                          <td style='text-align: center;'>
                                             <table cellpadding='0' cellspacing='0' width='595' align='center' border=1 style='font-family: Calibri; font-size: 12pt; text-align: justify; border-color:#000000;' >
                                                <tr style='color:#ffffff; background-color:#9BBB59;'>
                                                    <td border=1 style='text-align: center; padding-bottom: 5px;border-color:#000000;'>
                                                         <b>Nombre del Curso</b>
                                                    </td>
                                                    <td border=1 style='text-align: center; padding-bottom: 5px;border-color:#000000;'>
                                                         <b>Asistencia</b>
                                                    </td>
                                                </tr>";

            foreach (var listado in listado_contador_asistencias)
            {
                double porcentaje = Math.Round((listado.CantidadAsistio * 1.0 / listado.CantidadTotal * 1.0) * 100.0, 2, MidpointRounding.AwayFromZero);

                texto = texto + @"
                                                <tr>
                                                    <td border=1 style='text-align: left; padding-left: 5px; padding-bottom: 5px;border-color:#000000;'>
                                                        " + listado.NombreCurso + @"  
                                                    </td>
                                                    <td border=1 style='text-align: center; padding-bottom: 5px;border-color:#000000;'>
                                                         " + porcentaje.ToString("0.00") + @" %  
                                                    </td>
                                                </tr>";
            }

            texto = texto + @"                  </table> 
                                          </td>
                                      </tr>
                                      <tr>
                                        <td style='text-align: justify;'>
                                            <div style='font-family: Calibri,Arial,Helveltica; font-size: 12pt; text-align: justify;'>       
                                              <p style='text-align: justify;'>
                                                  <br>
                                                   Asimismo adjunto también el ranking de asistencia detallado por cada sesión impartida.
                                              </p>    
                                              <p style='text-align: justify;'>
                                                  
                                                   No olvide que de acuerdo al reglamento interno en referencia a las normas académicas la asistencia y puntualidad tiene un requisito indispensable de un 70% de participaci&oacute;n para la aprobaci&oacute;n de los m&oacute;dulos.
                                              </p>                                                       
                                              <p style='text-align: justify;'>                                                  
                                                   Le recuerdo los n&uacute;meros telef&oacute;nicos y horarios en los que estoy a su disposici&oacute;n:
                                              </p>
            ";

            texto = texto + coordinadora.Htmlnumero + coordinadora.Htmlhorario;

            texto = texto + @"
                                               <p>
                                                   Saludos cordiales,
                                               </p>
                                               <p>
                                                    <img src = 'cid:firma' alt = '' >   
                                               </p>
                                          </td>
                                      </tr>
                                 </table>
                 </BODY>
            </HTML>
            ";
            Mensaje = texto;
        }

        //public void Envio_Diario_Programacion_Encuestas_Alumno(ra_Presencial_Programacion_Encuesta tipo_encuesta, AlumnoPresencialBOL alumno, Coordinadoras coordinador)
        //{
        //    var url_encuesta = Obtener_Url_Encuesta(tipo_encuesta, alumno, null);

        //    var url_queja_sugerencia = Obtener_Url_QuejaSugerencia(tipo_encuesta, alumno, null);

        //    var texto =
        //        "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto +
        //            "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='border: none; padding: 0in'>";

        //    if (alumno != null)
        //    {
        //        string saludo = "";
        //        saludo = TratamientoAlumno_PalabraEstimado(alumno.Genero);

        //        texto = texto + "<p  style='font-family: Calibri, sans-serif; font-size: 12pt;'>" + saludo + " <b>" + alumno.Alumno + "</b>:</p>";
        //    }

        //    texto = texto + @"
        //            <p  style='text-align: justify;' >
        //                Le saludo nuevamente, y solicito su apoyo resolviendo una encuesta, cuyo objetivo es conocer el nivel de su satisfacción respecto al Curso (" +
        //                tipo_encuesta.ra_Sesion.ra_Curso.NombreCurso + @"), para recoger sus impresiones y de esa manera brindarle un mejor servicio.
        //            </p>
        //    ";

        //    texto = texto + @"
        //            <ul>
        //                <li>
        //                    <a href='" + url_encuesta + @"'>
        //                        <button style='padding: 5px; background-color: #ffffff;'>Acceder a la Encuesta</button>
        //                    </a>
        //                </li>
        //                <li>
        //                    <a href='" + url_queja_sugerencia + @"'>
        //                        <button style='padding: 5px; background-color: #ffffff;'>Registrar una Queja o Sugerencia</button>
        //                    </a>
        //                </li>
        //            </ul>
        //    ";

        //    texto = texto + @"
        //            <p style='text-align: justify;' > 
        //                <b>Agradecemos su apoyo resolviendo la misma el día de hoy.</b>
        //                <br/>
        //                En caso de no poder acceder favor d&eacute; aviso a Soporte T&eacute;cnico y/o a mi persona.
        //            </p>
        //    ";

        //    if (coordinador.genero == true)
        //        texto = texto + "<p>Muy agradecido de antemano.</p>";
        //    else
        //        texto = texto + "<p>Muy agradecida de antemano.</p>";

        //    texto = texto + "<p>Saludos coordiales.</p>";

        //    texto = texto +
        //            "<p><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //public void Envio_Diario_Programacion_Encuestas_Docente(ra_Presencial_Programacion_Encuesta tipo_encuesta, CronogramaDocenteBOL docente, Coordinadoras coordinador)
        //{
        //    var url_encuesta = Obtener_Url_Encuesta(tipo_encuesta, null, docente);

        //    var url_queja_sugerencia = Obtener_Url_QuejaSugerencia(tipo_encuesta, null, docente);

        //    var texto =
        //        "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto +
        //            "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='border: none; padding: 0in'>";

        //    if (docente != null)
        //    {
        //        texto = texto + "<p  style='font-family: Calibri, sans-serif; font-size: 12pt;'>Estimado(a) <b>" +
        //                docente.PrimerNombre + " " + docente.SegundoNombre + " " + docente.ApellidoPaterno + " " +
        //                docente.ApellidoMaterno + "<b/>:</p>";
        //    }

        //    texto = texto + @"
        //            <p  style='text-align: justify;' >
        //                Le saludo nuevamente, y solicito su apoyo resolviendo una encuesta, cuyo objetivo es conocer el nivel de su satisfacción respecto al Curso (" +
        //            tipo_encuesta.ra_Sesion.ra_Curso.NombreCurso + @"), para recoger sus impresiones y de esa manera brindarle un mejor servicio.
        //            </p>
        //    ";

        //    texto = texto + @"
        //            <ul>
        //                <li>
        //                    <a href='" + url_encuesta + @"'>
        //                        <button style='padding: 5px; background-color: #ffffff;'>Acceder a la Encuesta</button>
        //                    </a>
        //                </li>
        //                <li>
        //                    <a href='" + url_queja_sugerencia + @"'>
        //                        <button style='padding: 5px; background-color: #ffffff;'>Registrar una Queja o Sugerencia</button>
        //                    </a>
        //                </li>
        //            </ul>
        //    ";

        //    texto = texto + @"
        //            <p style='text-align: justify;' > 
        //                <b>Agradecemos su apoyo resolviendo la misma el día de hoy y/o hasta máximo el día " + tipo_encuesta.ra_Sesion.Fecha.Value.AddDays(5).ToShortDateString() + @".</b>
        //                <br/>
        //                En caso de no poder acceder favor d&eacute; aviso a Soporte T&eacute;cnico y/o a mi persona.
        //            </p>
        //    ";

        //    if (coordinador.genero == true)
        //        texto = texto + "<p>Muy agradecido de antemano.</p>";
        //    else
        //        texto = texto + "<p>Muy agradecida de antemano.</p>";

        //    texto = texto + "<p>Saludos coordiales.</p>";

        //    texto = texto +
        //            "<p><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //public void Envio_CC_ConfirmacionApertura(ra_CentroCosto cc, ra_Curso_Material primer_material, DateTime? fecha_inicio)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";


        //    texto = texto + "<p>Estimados(as), la presente es para confirmar la apertura del siguiente centro de costo:</p>";

        //    //se colocan todos los materiales que se requieren

        //    texto = texto + @"
        //                        <table cellpadding='0' cellspacing='0' width='100%' border=1 align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify;'>    
        //                            <tr>
        //                                <th align='center'>Centro de Costo</th>
        //                                <th align='center'>Estado Centro de Costo</th>
        //                                <th align='center'>Fecha Inicio</th>
        //                                <th align='center'>Fecha Subida Primer Material</th>
        //                            </tr>
        //    ";
        //    texto = texto + @"
        //                            <tr>
        //                                <td>" + cc.NombreCentroCosto + @"</td>
        //                                <td>" + cc.ra_CentroCosto_Estado.Nombre + @"</td>
        //                                <td>" + (fecha_inicio?.ToShortDateString() ?? "") + @"</td>
        //                                <td>" + (primer_material?.FechaSubida?.ToString() ?? "") + @"</td>
        //                            </tr>
        //        ";

        //    texto = texto + @"
        //                        </table>
        //    ";

        //    texto = texto + "<p>Saludos.</p>";

        //    //texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //public void Enviar_CC_CorreoPreEjecucion(ra_CentroCosto cc, CC_CkeckList_PreEjecucion_ViewModel modelo_pre_ejecucion)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + "<p>Estimados(as), la presente es para enviar el Check List de Pre ejecución del centro de costo: " + cc.NombreCentroCosto + "</p>";

        //    texto = texto + @"
        //                        <table cellpadding='0' cellspacing='0' width='100%' border=1 align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify;'>    
        //                            <tr>
        //                                <th>Criterio</th>
        //                                <th>Valor</th>
        //                                <th>Estado</th>
        //                            </tr>
        //    ";

        //    #region Llenado CheckList
        //    texto = texto + @"
        //                            <tr>
        //                                <td>Total Cursos Registrados</td>
        //                                <td>" + modelo_pre_ejecucion.TotalCursos_Registrados + " de " + modelo_pre_ejecucion.TotalCursos_ObtenerCertificado + @"</td>
        //                                <td></td>
        //                            </tr>
        //    ";
        //    texto = texto + @"
        //                            <tr>
        //                                <td>Usuario Asignado en el Registro Académico</td>
        //                                <td>" + modelo_pre_ejecucion.UsuarioAsignado_RegistroAcademico + @"</td>
        //                                <td></td>
        //                            </tr>
        //    ";
        //    texto = texto + @"
        //                            <tr>
        //                                <td>Usuario Asignado a los Alumnos</td>
        //                                <td>" + modelo_pre_ejecucion.UsuarioAsignado_Alumnos + @"</td>
        //                                <td></td>
        //                            </tr>
        //    ";
        //    texto = texto + @"
        //                            <tr>
        //                                <td>Fecha Invitación Docente (envío plantilla lanzamiento)</td>
        //                                <td>" + (modelo_pre_ejecucion.Fecha_InvitacionDocente_Lanzamiento?.ToShortDateString() ?? "") + @"</td>
        //                                <td></td>
        //                            </tr>
        //    ";
        //    texto = texto + @"
        //                            <tr>
        //                                <td>Fecha Confirmación Docente</td>
        //                                <td>" + (modelo_pre_ejecucion.Fecha_ConfirmacionDocente?.ToShortDateString() ?? "") + @"</td>
        //                                <td></td>
        //                            </tr>
        //    ";
        //    ////pendiente
        //    //texto = texto + @"
        //    //                        <tr>
        //    //                            <td>Fecha Solicitud de Material y Sílabos a Docente</td>
        //    //                            <td>" + (modelo_pre_ejecucion.Fecha_Solicitud_MaterialYSilabo?.ToShortDateString() ?? "") + @"</td>
        //    //                            <td></td>
        //    //                        </tr>
        //    //";
        //    texto = texto + @"
        //                            <tr>
        //                                <td>Fecha Registro Subida de Material</td>
        //                                <td>" + (modelo_pre_ejecucion.Fecha_Registro_SubidaMaterial?.ToShortDateString() ?? "") + @"</td>
        //                                <td></td>
        //                            </tr>
        //    ";
        //    texto = texto + @"
        //                            <tr>
        //                                <td>Fecha Envió Confirmación Apertura Interna</td>
        //                                <td>" + (modelo_pre_ejecucion.Fecha_Envio_ConfirmacionAperturaInterna?.ToShortDateString() ?? "") + @"</td>
        //                                <td></td>
        //                            </tr>
        //    ";
        //    if (modelo_pre_ejecucion.AplicaPasaje)
        //    {
        //        texto = texto + @"
        //                            <tr>
        //                                <td>Fecha Compra Pasajes</td>
        //                                <td>" + (modelo_pre_ejecucion.Fecha_CompraPasaje?.ToShortDateString() ?? "") + @"</td>
        //                                <td></td>
        //                            </tr>
        //        ";
        //    }
        //    if (modelo_pre_ejecucion.AplicaEstadia)
        //    {
        //        texto = texto + @"
        //                            <tr>
        //                                <td>Fecha Coordinación Estadía</td>
        //                                <td>" + (modelo_pre_ejecucion.Fecha_CoordinacionEstadia?.ToShortDateString() ?? "") + @"</td>
        //                                <td></td>
        //                            </tr>
        //        ";
        //    }
        //    texto = texto + @"
        //                            <tr>
        //                                <td>Fecha Envió Confirmación a Docente (envío plantilla ejecución)</td>
        //                                <td>" + (modelo_pre_ejecucion.Fecha_InvitacionDocente_Ejecucion?.ToShortDateString() ?? "") + @"</td>
        //                                <td></td>
        //                            </tr>
        //     ";

        //    if (modelo_pre_ejecucion.AplicaAulaVirtual)
        //    {
        //        texto = texto + @"
        //                            <tr>
        //                                <td>Fecha Creación de Aula Virtual (incluye detalle del aula)</td>
        //                                <td>" + (modelo_pre_ejecucion.Fecha_CreacionAulaVirtual?.ToShortDateString() ?? "") + @"</td>
        //                                <td></td>
        //                            </tr>
        //        ";
        //    }
        //    ////pendiente
        //    //texto = texto + @"
        //    //                        <tr>
        //    //                            <td>Fecha Creación Usuarios Docente y Matricula en Aula Virtual</td>
        //    //                            <td>" + (modelo_pre_ejecucion.Fecha_CreacionUsuarioYMatriculaDocente?.ToShortDateString() ?? "") + @"</td>
        //    //                            <td></td>
        //    //                        </tr>
        //    //";
        //    texto = texto + @"
        //                            <tr>
        //                                <td>Fecha Envío Material al Coordinador</td>
        //                                <td>" + (modelo_pre_ejecucion.Fecha_EnvioMaterialCoordinador?.ToShortDateString() ?? "") + @"</td>
        //                                <td></td>
        //                            </tr>
        //    ";
        //    texto = texto + @"
        //                            <tr>
        //                                <td>Fecha Primer Envió Semanal (se envía perfil, contratos, etc.)</td>
        //                                <td>" + (modelo_pre_ejecucion.Fecha_EnvioDocente_Semanal?.ToShortDateString() ?? "") + @"</td>
        //                                <td></td>
        //                            </tr>
        //    ";
        //    #endregion

        //    texto = texto + @"
        //                        </table>
        //    ";

        //    texto = texto + "<p>Saludos.</p>";

        //    texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    //texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //public void Material_EnvioAprobacion(CursoMaterial_EnviarAprobacion_ViewModel modelo, ra_Curso curso, List<ra_Curso_Material> listado_material, bool prueba)
        //{
        //    string url = "";
        //    if (prueba)
        //        url = "http://localhost:53519/Presencial/CursoMaterial/Aprobar?";
        //    else
        //        url = "http://integra.bsginstitute.net:900/Presencial/CursoMaterial/Aprobar?";

        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";


        //    texto = texto + "<p>Estimados(as), la presente es para Solicitar la Aprobación del(los) siguiente(s) material(es):</p>";

        //    //se colocan todos los materiales que se requieren

        //    texto = texto + @"
        //                        <table cellpadding='0' cellspacing='0' width='100%' border=1 align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify;'>    
        //                            <tr>
        //                                <th align='center'>Centro de Costo</th>
        //                                <th align='center'>Estado Centro de Costo</th>
        //                                <th align='center'>Curso</th>
        //                                <th align='center'>Nombre Archivo</th>
        //                                <th align='center'>Url</th>
        //                            </tr>
        //        ";

        //    foreach (var material in listado_material)
        //    {
        //        url += "id_curso=" + material.IdCurso + "&grupo=" + material.Grupo;

        //        texto = texto + @"
        //                            <tr>
        //                                <td align='center'>" + curso.ra_CentroCosto.NombreCentroCosto + @"</td>
        //                                <td align='center'>" + curso.ra_CentroCosto.Estado + @"</td>
        //                                <td align='center'>" + curso.NombreCurso + @"</td>
        //                                <td align='center'>" + material.NombreArchivoAlumno + @"</td>
        //                                <td align='center'><a href='" + url + "'>" + url + @"</a></td>
        //                            </tr>
        //        ";
        //    }

        //    texto = texto + @"
        //                        </table>
        //    ";

        //    texto = texto + "<p>Saludos.</p>";

        //    texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}
        //public void Material_EnvioDesaprobacion(CursoMaterial_Aprobar_ViewModel modelo_aprobacion, ra_Curso curso, List<ra_Curso_Material> listado_material, bool prueba)
        //{
        //    string url = "";
        //    if (prueba)
        //        url = "http://localhost:53519/Presencial/CursoMaterial/Detalle_Modificado?";
        //    else
        //        url = "http://integra.bsginstitute.net:900/Presencial/CursoMaterial/Detalle_Modificado?";

        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";


        //    texto = texto + "<p>Estimados(as), la presente es para indicar la Desprobación del(los) siguiente(s) material(es):</p>";

        //    //se colocan todos los materiales que se requieren

        //    texto = texto + @"
        //                        <table cellpadding='0' cellspacing='0' width='100%' border=1 align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify;'>    
        //                            <tr>
        //                                <th align='center'>Centro de Costo</th>
        //                                <th align='center'>Estado Centro de Costo</th>
        //                                <th align='center'>Curso</th>
        //                                <th align='center'>Nombre Archivo</th>
        //                                <th align='center'>Url</th>
        //                            </tr>
        //        ";

        //    foreach (var material in listado_material)
        //    {
        //        url += "id_curso=" + material.IdCurso + "&grupo=" + material.Grupo;

        //        texto = texto + @"
        //                            <tr>
        //                                <td align='center'>" + curso.ra_CentroCosto.NombreCentroCosto + @"</td>
        //                                <td align='center'>" + curso.ra_CentroCosto.Estado + @"</td>
        //                                <td align='center'>" + curso.NombreCurso + @"</td>
        //                                <td align='center'>" + material.NombreArchivoAlumno + @"</td>
        //                                <td align='center'><a href='" + url + "'>" + url + @"</a></td>
        //                            </tr>
        //        ";
        //    }

        //    texto = texto + @"
        //                        </table>
        //    ";

        //    texto = texto + "<p><b>Motivo:</b> " + modelo_aprobacion.Observaciones + "</p>";

        //    texto = texto + "<p>Saludos.</p>";

        //    texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}
        //public void Envio_MaterialDigital_Alumno(AlumnosMatriculados alumno, ra_Curso curso, List<ra_Curso_Material> listado_material, Coordinadoras coordinador, DateTime? fecha_inicio_curso)
        //{
        //    //string url = "https://s3.amazonaws.com/bsgrupo-operaciones-materiales/";
        //    string url = "https://recordatorios.bsginstitute.com/seguimiento/material/descarga.php";

        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in;'>";

        //    //añade el aviso si el curso es de un grupo mayor al primero
        //    if (curso.Grupo > 1)
        //        texto = texto + "<p>Estimado(a), por medio de la presente se hace de su conocimiento la disponibilidad del material indicado, favor de regularizarlo con los alumnos del grupo correspondiente (<b>Grupo: " + curso.Grupo + "</b>).</p>";

        //    texto = texto + @"
        //                        <table cellpadding='0' cellspacing='0' width='600' align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify; font-weight: bolder;'>    
        //                            <tr>
        //                                <td align='center'>
        //                                    <img src='cid:cabecera' width='600' alt='BSG INSTITUTE'>
        //                                </td>
        //                            </tr>

        //                            <tr>
        //                                <td align='left' style='font-family: Calibri,Arial,Helveltica; font-size: 20pt; text-align: justify;'>
        //                                    " + (alumno != null ? alumno.nombre1 : "") + @"
        //                                </td>
        //                            </tr>

        //                            <tr>
        //                                <td align='center'>
        //                                    <img src='cid:imagen_material' width='600'>
        //                                </td>
        //                            </tr>


        //                            <tr>
        //                                <td style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify; font-weight: bolder;'>

        //                                    <div style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify; font-weight: bolder;'>                                        
        //                                        <br />
        //        ";

        //    //se colocan todos los materiales que se requieren
        //    texto = texto + @"
        //                                        <table cellpadding='0' cellspacing='0' width='600' border=0 align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify; font-weight: bolder;'>    
        //                                            <tr>
        //                                                <th align='center' style='color: #ffffff; background-color: #003399; font-size: 10pt;' width='80'>Programa</th>
        //                                                <th align='center' style='color: #ffffff; background-color: #003399; font-size: 10pt;' width='220'>Curso</th>
        //                                                <th align='center' style='color: #ffffff; background-color: #003399; font-size: 10pt;' width='180'>Archivo</th>
        //                                                <th align='center' style='color: #ffffff; background-color: #003399; font-size: 10pt;' width='120'>Url</th>
        //                                            </tr>
        //        ";

        //    bool primera_fila = true;
        //    //excluye a los materiales que no tiene envio para alumno
        //    foreach (var material in listado_material.Where(w => w.NombreArchivoAlumno != null))
        //    {
        //        string url_temporal = url + "?codigo=" + (alumno != null ? alumno.CodigoAlumno : "") + "&id_curso=" + material.IdCurso +
        //               "&grupo=" + material.Grupo + "&carpeta=" + material.CarpetaAmazon.Replace("/", "") + "&archivo=";

        //        int posicion = material.NombreArchivoAlumno.LastIndexOf("-");

        //        string nombre_archivo = material.NombreArchivoAlumno.Substring(
        //            posicion > 0 ? posicion + 1 : 0,
        //            material.NombreArchivoAlumno.LastIndexOf(".") - posicion - 1);

        //        texto = texto + @"              <tr>";
        //        if (primera_fila)
        //            texto = texto + @"
        //                                            <td align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; background-color: #d9e2f3;' rowspan=" + listado_material.Count + ">" + curso.ra_CentroCosto.NombreCentroCosto + @"</td>
        //                                            <td align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt;' rowspan=" + listado_material.Count + ">" + curso.NombreCurso + @"</td>
        //                ";
        //        texto = texto + @"
        //                                            <td align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; background-color: #d9e2f3;'>" + nombre_archivo + @"</td>
        //                                            <td align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt;'><a href='" + url_temporal + material.NombreArchivoAlumno + @"'><img src='cid:boton_material' /></a></td>
        //                                        </tr>";
        //        primera_fila = false;
        //    }

        //    texto = texto + @"
        //                                     </table>
        //    ";

        //    //añade el parraof de fecha de inicio de curso
        //    if (fecha_inicio_curso != null)
        //    {
        //        texto = texto + @"
        //                                    <br />
        //                                    <table cellpadding='0' cellspacing='0' width='600' border=0 align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify; font-weight: bolder;'>    
        //                                        <tr>
        //                                            <th align='right' style='color: #ffffff; font-size: 11pt;' width='300'><span style='background-color: #003399; padding: 2px 0;'>Fecha de inicio del curso:</span> </th>
        //                                            <th align='left' style='font-size: 11pt;' width='300'>&nbsp;" + fecha_inicio_curso.Value.ToString("dddd dd").Substring(0, 1).ToUpper() + fecha_inicio_curso.Value.ToString("dddd dd").Substring(1).ToLower() + " de " + fecha_inicio_curso.Value.ToString("MMMM").Substring(0, 1).ToUpper() + fecha_inicio_curso.Value.ToString("MMMM").Substring(1).ToLower() + " del " + fecha_inicio_curso.Value.Year + @"</th>
        //                                    </table>
        //        ";
        //    }

        //    texto = texto + @"               <p>Así mismo dicho material se encuentra disponible en el aula virtual.</p>

        //                                     <p>Le recuerdo los números telefónicos y horarios en los que estoy a su disposición:</p>";

        //    texto = texto + coordinador.HTMLNumero + coordinador.HTMLHorario;

        //    texto = texto + "                <p>Saludos cordiales.</p>";

        //    //texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    texto = texto + "               <p style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + @"
        //                            </div>
        //                        </td>
        //                    </tr>
        //                </table>
        //    ";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //public void Material_ConfirmacionAprobacion(CursoMaterial_Aprobar_ViewModel modelo_aprobacion, ra_Curso curso, List<ra_Curso_Material> listado_material, bool prueba)
        //{
        //    string url_base = "";
        //    if (prueba)
        //        url_base = "http://localhost:53519/Presencial/CursoMaterial/Detalle_Modificado?";
        //    else
        //        url_base = "http://integra.bsginstitute.net:900/Presencial/CursoMaterial/Detalle_Modificado?";

        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";


        //    texto = texto + "<p>Estimados(as), la presente es para indicar la Aprobación del(los) siguiente(s) material(es):</p>";

        //    //se colocan todos los materiales que se requieren

        //    texto = texto + @"
        //                        <table cellpadding='0' cellspacing='0' width='100%' border=1 align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify;'>    
        //                            <tr>
        //                                <th align='center'>Centro de Costo</th>
        //                                <th align='center'>Estado Centro de Costo</th>
        //                                <th align='center'>Curso</th>
        //                                <th align='center'>Nombre Archivo</th>
        //                                <th align='center'>Url</th>
        //                            </tr>
        //        ";

        //    foreach (var material in listado_material)
        //    {
        //        string url = url_base + "id_curso=" + material.IdCurso + "&grupo=" + material.Grupo;

        //        texto = texto + @"
        //                            <tr>
        //                                <td align='center'>" + curso.ra_CentroCosto.NombreCentroCosto + @"</td>
        //                                <td align='center'>" + curso.ra_CentroCosto.Estado + @"</td>
        //                                <td align='center'>" + curso.NombreCurso + @"</td>
        //                                <td align='center'>" + material.NombreArchivoAlumno + @"</td>
        //                                <td align='center'><a href='" + url + "'>" + url + @"</a></td>
        //                            </tr>
        //        ";
        //    }

        //    texto = texto + @"
        //                        </table>
        //    ";

        //    texto = texto + "<p><b>Observaciones:</b> " + modelo_aprobacion.Observaciones + "</p>";

        //    texto = texto + "<p>Saludos.</p>";

        //    texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //internal void Envio_Curso_Aviso_Registro_NotaDocente(ra_Curso curso, CronogramaDocenteBOL docente)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

        //    texto = texto + "<p><b>Estimados(as)</b></p>";

        //    texto = texto + "<p>Sirva por la presente, informar que se requistró/actualizó las notas de: " +
        //            curso.NombreCurso + " - <b>" + curso.ra_CentroCosto.NombreCentroCosto +
        //            "</b>, desde el usuario del docente: <b>" + (docente?.NombreCompleto) + "</b></p>";

        //    texto = texto + "<p>Saludos.</p>";

        //    texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    //texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        ////funciones
        //private string TratamientoAlumno_PalabraEstimado(string alumno_genero)
        //{
        //    try
        //    {
        //        string tratamiento = "Estimad" + (alumno_genero == null || alumno_genero.Trim() == ""
        //                                 ? "o(a)"
        //                                 : alumno_genero.ToLower() == "m"
        //                                     ? "o"
        //                                     : alumno_genero.ToLower() == "f"
        //                                         ? "a"
        //                                         : "o(a)") + " ";
        //        return tratamiento;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Estimado(a) ";
        //    }
        //}

        //private string Obtener_Url_Encuesta(ra_Presencial_Programacion_Encuesta tipo_encuesta, AlumnoPresencialBOL alumno, CronogramaDocenteBOL docente)
        //{
        //    string url_encuesta = "http://integra.bsginstitute.net:900/Presencial/EncuestaPresencial/";

        //    //EncuestaInicial?id_programacion_encuesta=200&id_alumno=8789&codigo_alumno=11774A8033&alumno=DELGADO%20GORDILLO%20JOSE%20ANTONIO
        //    if (tipo_encuesta.TipoEncuesta == TipoEncuestaPresencial.Inicial)
        //    {
        //        url_encuesta +=
        //            "EncuestaInicial?id_programacion_encuesta=" +
        //            tipo_encuesta.Id + "&id_alumno=" + alumno.IdAlumno + "&codigo_alumno=" + alumno.CodigoAlumno +
        //            "&alumno=" + alumno.Alumno;
        //    }
        //    //EncuestaIntermediaPresencial?id_programacion_encuesta=196&id_alumno=8536&codigo_alumno=9379476a5719&alumno=AGURTO%20CONTRERAS%20CECILIA%20FIORELA
        //    if (tipo_encuesta.TipoEncuesta == TipoEncuestaPresencial.Intermedia)
        //    {
        //        url_encuesta +=
        //            "EncuestaIntermediaPresencial?id_programacion_encuesta=" +
        //            tipo_encuesta.Id + "&id_alumno=" + alumno.IdAlumno + "&codigo_alumno=" + alumno.CodigoAlumno +
        //            "&alumno=" + alumno.Alumno;
        //    }
        //    //EncuestaFinal?id_programacion_encuesta=197&id_alumno=8536&codigo_alumno=9379476a5719&alumno=AGURTO%20CONTRERAS%20CECILIA%20FIORELA
        //    if (tipo_encuesta.TipoEncuesta == TipoEncuestaPresencial.Final)
        //    {
        //        url_encuesta +=
        //            "EncuestaFinal?id_programacion_encuesta=" +
        //            tipo_encuesta.Id + "&id_alumno=" + alumno.IdAlumno + "&codigo_alumno=" + alumno.CodigoAlumno +
        //            "&alumno=" + alumno.Alumno;
        //    }
        //    //EncuestaDocentesPresenciales?id_programacion_encuesta=199&id_docente=135
        //    if (tipo_encuesta.TipoEncuesta == TipoEncuestaPresencial.Docente)
        //    {
        //        url_encuesta +=
        //            "EncuestaDocentesPresenciales?id_programacion_encuesta=" +
        //            tipo_encuesta.Id + "&id_docente=" + docente.Id;
        //    }

        //    return url_encuesta;
        //}

        //private string Obtener_Url_QuejaSugerencia(ra_Presencial_Programacion_Encuesta tipo_encuesta, AlumnoPresencialBOL alumno, CronogramaDocenteBOL docente)
        //{
        //    string url_encuesta = "http://integra.bsginstitute.net:900/Presencial/EncuestaPresencial/";

        //    //http://localhost:53519/Presencial/EncuestaPresencial/EncuestaQuejasSugerencias?id_programacion_encuesta=200&id_alumno=8789&id_curso=10453 -Inicial
        //    //http://localhost:53519/Presencial/EncuestaPresencial/EncuestaQuejasSugerencias?id_programacion_encuesta=196&id_alumno=8536&id_curso=10303 -Intermedia
        //    //http://localhost:53519/Presencial/EncuestaPresencial/EncuestaQuejasSugerencias?id_programacion_encuesta=197&id_alumno=8536&id_curso=10303 -Final
        //    //http://localhost:53519/Presencial/EncuestaPresencial/EncuestaQuejasSugerencias?id_programacion_encuesta=199&id_docente=135&id_curso=10303 -Docente

        //    if (tipo_encuesta.TipoEncuesta == TipoEncuestaPresencial.Docente)
        //    {
        //        url_encuesta +=
        //            "EncuestaQuejasSugerencias?id_programacion_encuesta=" +
        //            tipo_encuesta.Id + "&id_docente=" + docente.Id + "&id_curso=" + tipo_encuesta.ra_Sesion.IdCurso;
        //    }
        //    else
        //    {
        //        url_encuesta +=
        //            "EncuestaQuejasSugerencias?id_programacion_encuesta=" +
        //            tipo_encuesta.Id + "&id_alumno=" + alumno.IdAlumno + "&id_curso=" + tipo_encuesta.ra_Sesion.IdCurso;
        //    }

        //    return url_encuesta;
        //}

        //public void Envio_Curso_ConfirmacionEvaluacion(ra_Curso curso)
        //{
        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";


        //    texto = texto + "<p>Estimados(as), la presente es para indicar la subida y confirmacion de(las) evaluación(es) del siguiente curso:</p>";

        //    texto = texto + @"
        //                        <table cellpadding='0' cellspacing='0' width='100%' border=1 align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify;'>    
        //                            <tr>
        //                                <th align='center'>Centro de Costo</th>
        //                                <th align='center'>Estado Centro de Costo</th>
        //                                <th align='center'>Curso</th>
        //                                <th align='center'>Orden</th>
        //                                <th align='center'>Grupo</th>
        //                            </tr>
        //    ";
        //    texto = texto + @"
        //                            <tr>
        //                                <td>" + curso.ra_CentroCosto.NombreCentroCosto + @"</td>
        //                                <td>" + curso.ra_CentroCosto.ra_CentroCosto_Estado.Nombre + @"</td>
        //                                <td>" + curso.NombreCurso + @"</td>
        //                                <td>" + curso.Orden + @"</td>
        //                                <td>" + curso.Grupo + @"</td>
        //                            </tr>
        //        ";

        //    texto = texto + @"
        //                        </table>
        //    ";

        //    texto = texto + "<p>Saludos.</p>";

        //    texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    texto = texto + "<p LANG=''  style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}

        //public void Envio_Trabajo_Alumno(ra_Curso_TrabajoAlumno trabajo_alumno, ra_Curso curso, AlumnosMatriculados alumno, Coordinadoras coordinador)
        //{
        //    string texto_estimado = TratamientoAlumno_PalabraEstimado(alumno.genero);

        //    var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

        //    texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

        //    texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 11pt; border: none; padding: 0in;'>";

        //    texto = texto + @"
        //                        <table cellpadding='0' cellspacing='0' width='600' align='center' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify;'>
        //                            <tr>
        //                                <td align='center'>
        //                                    <img src='cid:cabecera' width='600' alt='BSG INSTITUTE'>
        //                                </td>
        //                            </tr>

        //                            <tr>
        //                                <td align='left' style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify;'>
        //                                    <b>" + texto_estimado + " " + alumno.nombre1 + @"</b>
        //                                </td>
        //                            </tr>

        //                            <tr>
        //                                <td style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify;'>

        //                                    <div style='font-family: Calibri,Arial,Helveltica; font-size: 11pt; text-align: justify;'>  
        //                                        <br />
        //        ";

        //    texto = texto + @"               <p>
        //                                        Sirva la presente para saludarle y a la vez recordarle acerca de la presentación del trabajo, 
        //                                        asignado por el docente del " + curso.NombreCurso + @" - " + curso.ra_CentroCosto.NombreCentroCosto + @".</p>";

        //    texto = texto + "                <p>Dicho trabajo, como es de su conocimiento, consiste en lo siguiente:</p>";

        //    texto = texto + "                <p><b>Descripción:</b> " + trabajo_alumno.Nombre + "</p>";
        //    texto = texto + "                <p><b>Forma de entrega:</b> " + trabajo_alumno.ra_Curso_TrabajoAlumno_Tipo.Nombre + "</p>";
        //    texto = texto + "                <p><b>Fecha límite de entrega:</b> " + trabajo_alumno.FechaEntrega.ToShortDateString() + "</p>";

        //    texto = texto + "                <p>En caso Ud. ya haya realizado la presentación del mismo, por favor hacer caso omiso a esta comunicación.</p>";

        //    texto = texto + @" 
        //                                     <p>Le recuerdo los números telefónicos y horarios en los que estoy a su disposición:</p>";

        //    texto = texto + coordinador.HTMLNumero + coordinador.HTMLHorario;

        //    texto = texto + "                <p>Saludos cordiales.</p>";

        //    //texto = texto + "<p style='font-family: Calibri, Arial, sans-serif; font-size: 10pt;'><b>PD:</b> Favor no responder al presente, la cuenta de correo no es monitoreada.</p>";

        //    texto = texto + "               <p style='margin-bottom: 0.11in'><IMG SRC='cid:firma' ALIGN=BOTTOM BORDER=0></p>";

        //    texto = texto + @"
        //                            </div>
        //                        </td>
        //                    </tr>
        //                </table>
        //    ";

        //    texto = texto + "</BODY></HTML>";

        //    mensaje = texto;
        //}
    }
}