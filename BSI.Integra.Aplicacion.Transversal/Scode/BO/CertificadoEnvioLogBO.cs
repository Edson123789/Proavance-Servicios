using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using Mandrill.Models;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CertificadoEnvioLogBO : BaseBO
    {
        public int IdCertificadoDetalle { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool SoloDigital { get; set; }
        public int? IdMigracion { get; set; }

        public CertificadoEnvioLogBO()
        {
            
        }

        public EmailAttachment GenerarArchivoAdjunto( string url,string nombreArchivo , string contenType )
        {
            EmailAttachment archivo = new EmailAttachment()
            {
                Base64 = true,
                Content = Convert.ToBase64String(ExtendedWebClient.GetFile(url)),
                Name = nombreArchivo,
                Type = contenType
            };

            return archivo;
        }
        public string GenerarMensajeEnvioDigital(DatosAlumnoEnvioCertificadoDTO alumno )
        {
            var saludo_estimado = this.SaludoEstimado(alumno.Genero);

            var texto = "<HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>";

            texto = texto + "<head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";

            texto = texto + "<BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='font-family: Calibri, Arial, sans-serif; font-size: 12pt; border: none; padding: 0in'>";

            texto = texto + @"
                                <table cellspacing='0' cellpadding='0' align='center' style='width: 600px; text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
                                    <tr>
                                        <td>
                                            <img src='cid:cabecera' alt='BSG INSTITUTE'>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style='text-align: justify; font-family: Calibri, Aril, serif; font-size: 12pt;'>
                                                <p>
                                                    <b>
                                                        " + saludo_estimado + @" " + alumno.Nombre1 + " " + alumno.Nombre2 + @":
                                                    </b>
                                                </p>
                                                <p>
                                                    Nos es grato saludarlo en esta oportunidad. 
                                                    El motivo de la presente es para hacerle llegar el certificado correspondiente a su capacitación, en formato digital.
                                                </p>
                                                <p>
                                                    Posteriormente me estaré comunicándose con Ud. para coordinar la entrega del mismo en físico.
                                                </p>
                ";

            texto = texto + @"
                                                <p>
                                                    Atentamente, 
                                                </p>
                                                <p>
                                                    <img src='"+GenerarFirma(alumno.UrlFoto,alumno.IdCodigoPais,alumno.IdCiudad) +@"' alt=''>
                                                </p>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
            ";

            texto = texto + "</BODY></HTML>";

            return texto;
        }

        public string SaludoEstimado(string Genero)
        {
            try
            {
                string tratamiento = "Estimad" + (Genero == null || Genero.Trim() == ""
                                         ? "o(a)"
                                         : Genero.ToLower() == "m"
                                             ? "o"
                                             : Genero.ToLower() == "f"
                                                 ? "a"
                                                 : "o(a)") + " ";
                return tratamiento;
            }
            catch (Exception e)
            {
                return "Estimado(a) ";
            }
        }
        public string GenerarFirma(string urlFoto, int idCodigoPais , int idCiudad) 
        {
            try
            {
                var urlInicial = urlFoto;
                if (idCodigoPais == 51 && idCiudad == 4)
                {
                    urlInicial += "pa";
                }
                else if (idCodigoPais == 51 && idCiudad == 14)
                {
                    urlInicial += "pl";
                }
                else if (idCodigoPais == 57)
                {
                    urlInicial += "c";
                }
                else if (idCodigoPais == 591)
                {
                    urlInicial += "b";
                }
                else
                {
                    urlInicial += "pl";
                }

                urlInicial += ".png";
                return urlInicial;
            }
            catch(Exception e)
            {
                return "";
            }
            
        }
    }
}
