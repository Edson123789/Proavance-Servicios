using BSI.Integra.Aplicacion.Transversal.BO;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Comercial.Helper
{
    public class HelperMensajes
    {
        public string mensaje_email_proceso_pago_peru(string email, string password, string url, AlumnoBO contacto, ICollection<MontoPagoCronogramaDetalleBO> listaCronogama, string pgeneral, string CodigoMatricula, string Pais, string simboloMoneda)
        {
            string mensaje = string.Empty, montoCuota = string.Empty;
            string[] datosAdicionales = CodigoMatricula.Split('-');
            int contador = 0;

            mensaje += "Estimado(a) " + contacto.Nombre1 + "<br><br>";
            mensaje += "<p style='font-size:10pt;'>Según lo conversado le envió el código de alumno correspondiente a su inscripción en el <b>" + pgeneral + "</b><p>";
            mensaje += "<p style='font-size:10pt;'><b>NOMBRE DEL PARTICIPANTE:</b> " + contacto.Nombre1 + " " + contacto.Nombre2 + " " + contacto.ApellidoPaterno + " " + contacto.ApellidoMaterno + "</p>";

            if (Pais == "CO")
            {
                mensaje += "<p style='font-size:10pt;'><b>Nro Convenio Bancolombia:</b> 56470<p>";
                mensaje += "<p style='font-size:10pt;'><b>Nro de Referencia:</b> " + CodigoMatricula + "<p>";
            }
            else
            {
                mensaje += "<p style='font-size:10pt;'><b>CODIGO DE ALUMNO:</b> " + CodigoMatricula + "</p>";
            }

            mensaje += "<p style='font-size:10pt;'><b>CRONOGRAMA DE PAGOS:</b>" + "</p>";

            mensaje += "<table style='border: 1px solid #e6e6e6;border-collapse:collapse' border='' cellspacing='0' cellpadding='2'>";
            mensaje += "<tbody>";
            mensaje += "<tr>";
            mensaje += "<th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Descripcion</th>";
            mensaje += "<th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Fecha pago</th>";
            mensaje += "<th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Monto cuota con descuento</th>";
            mensaje += "</tr>";

            foreach (var item in listaCronogama)
            {
                contador = contador + 1;
                if (contador == 1)
                {
                    montoCuota = item.MontoCuotaDescuento.ToString("0.00");
                }
                mensaje += "<tr>";
                mensaje += "<th style='border: 1px solid #e6e6e6; font-weight: 100;'>" + item.CuotaDescripcion.ToString() + "</th>";
                mensaje += "<th style='border: 1px solid #e6e6e6; font-weight: 100;'>" + item.FechaPago.ToString("dd/MM/yyyy") + "</th>";
                mensaje += "<th style='border: 1px solid #e6e6e6; font-weight: 100;' align='right'>" + simboloMoneda + " " + item.MontoCuotaDescuento.ToString("0.00") + "</th>";
                mensaje += "</tr>";
            }
            mensaje += "</tbody>";
            mensaje += "</table>";

            if (Pais == "PE")
            {
                mensaje += "<p style='font-size:10pt;'><b><u>SOBRE EL PAGO:</u></b></p>";
                mensaje += "<p>Usted puede realizar el pago con su código de alumno mediante: </p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font -size:10pt'>Sitio Web de BSG Institute</span></li>";
                mensaje += "<li><span style='font -size:10pt'>Sitio Web VIABCP (si cuenta con clave de internet y clave token) </span></li>";
                mensaje += "<li><span style='font -size:10pt'>Oficinas BSG Institute</span></li>";
                mensaje += "<li><span style='font -size:10pt'>En los locales del BCP (Ventanilla)</span></li>";
                mensaje += "<li><span style='font -size:10pt'>Agente BCP</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><b><u>Por el Sitio Web de BSG Institute</u></b></p>";
                mensaje += "<br>";

                mensaje += "<p>Ingrese al siguiente enlace:</p>";
                mensaje += "<p><a href='" + url + "'>" + url + "</a></p><br>";
                mensaje += "<p style='font-size:10pt;'><b>Sus datos de acceso son</b></p>";
                mensaje += "<ul>";
                mensaje += "<li><b>Usuario:</b><span> " + email + "</span></li>";
                mensaje += "<li><b>Clave:</b><span> " + password + "</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><u><b>Por el Sito Web VIABCP – Banca por Internet</b> (Solo si dispone de clave token)</u></p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font-size:10pt'>Ingrese a la página del BCP <a href='https://www.viabcp.com/wps/portal/' target='_blank'>viabcp.com.pe</a></span></li>";
                mensaje += "<li><span style='font-size:10pt'>Dar click en “Banca por Internet” e ingrese a sus cuentas.</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Dar en click en Pagos y Transferencias y luego en “Pago de Servicios”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>En directorio de pagos escriba en el recuadro blanco <strong>BSG INSTITUTE </strong>y da click en buscar</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Aparecerá <strong>BSG Institute </strong>y luego da click en “" + datosAdicionales[1].ToString() + "”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Luego ingrese su código de alumno “" + datosAdicionales[0].ToString() + "”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Selecciona la cuota a pagar y da click en continuar</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Confirme el abono mediante el reenvió del correo de pago recibido del BCP a su asesor(a) comercial.</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><u><b>En las oficinas de BSG Institute:</b></u></p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font-size:10pt'><strong>Lima:</strong> Pardo 650 – Miraflores</span></li>";
                mensaje += "<li><span style='font-size:10pt'><strong>Arequipa:</strong> Víctor Andrés Belaunde A-9, Urb. Atlas - Umacollo</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><b>En los locales del BCP</b></p>"; ;
                mensaje += "<ul>";
                mensaje += "<li><span style='font-size:10pt'>Solicite en Ventanilla un&nbsp;<strong>CREDIPAGO</strong>o&nbsp;<strong>PAGO DE SERVICIO</strong>&nbsp;a la empresa&nbsp;<strong>BSG Institute</strong>&nbsp;</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Indicar que la cuenta a abonar es&nbsp;“" + datosAdicionales[1].ToString() + "”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Indicará su Código de Alumno el cual es “" + datosAdicionales[0].ToString() + "”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Confirma el abono mediante el envío del voucher de pago a su asesor(a) comercial.</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><u><b>Por Agente BCP</b> (Solo para importes menores a S/. 500)</u></p>";
                mensaje += "<ul>";
                //mensaje += "<li><span style='font -size:10pt'>Solicite realizar un pago a la empresa <strong>BSG Institute</strong> con código <strong>00323&nbsp;</strong></span></li>";
                mensaje += "<li><span style='font -size:10pt'>Solicite realizar un pago a la empresa <strong>BSG Institute</strong> con código <strong>18185&nbsp;</strong></span></li>";
                mensaje += "<li><span style='font -size:10pt'>Indicar que la cuenta a abonar es&nbsp;“" + datosAdicionales[1].ToString() + "”</span></li>";
                mensaje += "<li><span style='font -size:10pt'>Indicará su Código de Alumno el cual es “" + datosAdicionales[0].ToString() + "”</span></li>";
                mensaje += "<li><span style='font -size:10pt'>Confirma el abono mediante el envío del voucher de pago a su asesor(a) comercial.</span></li>";
                mensaje += "</ul>";

            }
            else if (Pais == "CO")
            {
                mensaje += "<p style='font-size:10pt;'><b><u>SOBRE EL PAGO:</u></b></p>";
                mensaje += "<p>Usted puede realizar el pago con su código de alumno mediante: </p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font -size:10pt'>Sitio Web de BSG Institute</span></li>";
                mensaje += "<li><span style='font -size:10pt'>Oficinas de Bancolombia (Ventanilla)</span></li>";
                mensaje += "<li><span style='font -size:10pt'>Por el Sitio Web de Bancolombia (Si tiene una cuenta en este banco)</span></li>";
                mensaje += "<li><span style='font -size:10pt'>Oficina BSG Institute</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><b><u>Por el Sitio Web de BSG Institute</u></b></p>";
                mensaje += "<br>";

                mensaje += "<p>Ingrese al siguiente enlace:</p>";
                mensaje += "<p><a href='" + url + "'>" + url + "</a></p><br>";
                mensaje += "<p style='font-size:10pt;'><b>Sus datos de acceso son</b></p>";
                mensaje += "<ul>";
                mensaje += "<li><b>Usuario:</b><span> " + email + "</span></li>";
                mensaje += "<li><b>Clave:</b><span> " + password + "</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><u><b>En las Oficinas de Bancolombia</b> (Ventanilla)</u></p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font-size:10pt'>Solicite en Ventanilla un deposito a la empresa con número de convenio 56470</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Indicar el monto a depositar “" + montoCuota + "”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Indicar como referencia a consignar su código de matrícula “Número de referencia”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Confirmar el abono mediante el comprobante de consignación a su asesor(a) comercial.</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><u><b>Por el Sitio Web de Bancolombia</b> (Si tiene una cuenta en este banco)</u></p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font-size:10pt'>Registrar a BSG Institute como proveedor con los siguientes datos:</span></li>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font-size:10pt'><b>Empresa:</b> BS GRUPO COLOMBIA SAS</span></li>";
                mensaje += "<li><span style='font-size:10pt'><b>NIT:</b> 900776296</span></li>";
                mensaje += "<li><span style='font-size:10pt'><b>Número de Cuenta:</b> 65231918412</span></li>";
                mensaje += "</ul>";
                mensaje += "<li><span style='font-size:10pt'>Realizar la transferencia del importe acordado “" + montoCuota + "”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Confirmar el abono reenviando el correo recibido por la plataforma de Bancolombia a su asesor(a) comercial.</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><u><b>En las oficinas de BSG Institute:</b></u></p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font-size:10pt'><b>Bogotá:</b> Av. Carrera 45 N° 108-27 Torre 1 Oficina 1008 – Bogotá (Centro Empresarial Paralelo)</span></li>";
                mensaje += "</ul>";
            }
            else
            {
                mensaje += "<p style='font-size:10pt;'><b><u>SOBRE EL PAGO:</u></b></p>";
                mensaje += "<p>Usted puede realizar el pago con su código de alumno mediante: </p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font -size:10pt'>Sitio Web de BSG Institute</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><b><u>Por el Sitio Web de BSG Institute</u></b></p>";
                mensaje += "<br>";

                mensaje += "<p>Ingrese al siguiente enlace:</p>";
                mensaje += "<p><a href='" + url + "'>" + url + "</a></p><br>";
                mensaje += "<p style='font-size:10pt;'><b>Sus datos de acceso son</b></p>";
                mensaje += "<ul>";
                mensaje += "<li><b>Usuario:</b><span> " + email + "</span></li>";
                mensaje += "<li><b>Clave:</b><span> " + password + "</span></li>";
                mensaje += "</ul>";
            }



            mensaje += "<p>Sin otro particular quedo a la espera de su confirmación.</p>";
            mensaje += "<br/>";
            mensaje += "<p>Saludos,</p>";

            return mensaje;
        }

        public string mensaje_email_finanzas(AlumnoBO contacto, List<MontoPagoCronogramaDetalleBO> listaCronogama, string CodigoMatricula, string simboloMoneda)
        {
            string mensaje = string.Empty;

            if (simboloMoneda == "COL $")
            {
                mensaje += "<p style='font-size:10pt;'><b>Nro Convenio Bancolombia:</b> 56470<p>";
                mensaje += "<p style='font-size:10pt;'><b>Nro de Referencia:</b> " + CodigoMatricula.Replace("A", "") + "<p>";
                mensaje += "<p style='font-size:10pt;'><b>Codigo de Alumno:</b> " + CodigoMatricula + "</p>";
            }
            else
            {
                mensaje += "<p style='font-size:10pt;'><b>CODIGO DE ALUMNO:</b> " + CodigoMatricula + "</p>";
            }

            mensaje += "<p><b>Datos del Alumno</b></p>";
            mensaje += "<ul>";
            mensaje += "<li><b>Nombres:</b> " + contacto.Nombre1 + " " + contacto.Nombre2 + "</li>";
            mensaje += "<li><b>Apellidos:</b> " + contacto.ApellidoPaterno + " " + contacto.ApellidoMaterno + "</li>";
            mensaje += "<li><b>Correo:</b> " + contacto.Email1 + "</li>";
            mensaje += "<li><b>Direccion:</b> " + contacto.Direccion + "</li>";
            mensaje += "<li><b>Documento:</b> " + contacto.Dni + "</li>";
            mensaje += "</ul>";

            mensaje += "<p style='font-size:10pt;'><b>CRONOGRAMA DE PAGOS:</b>" + "</p>";

            mensaje += "<table style='border: 1px solid #e6e6e6;border-collapse:collapse' border='' cellspacing='0' cellpadding='2'>";
            mensaje += "<tbody>";
            mensaje += "<tr>";
            mensaje += "<th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Descripcion</th>";
            mensaje += "<th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Fecha pago</th>";
            mensaje += "<th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Monto cuota con descuento</th>";
            mensaje += "</tr>";

            foreach (var item in listaCronogama)
            {
                mensaje += "<tr>";
                mensaje += "<th style='border: 1px solid #e6e6e6; font-weight: 100;'>" + item.CuotaDescripcion.ToString() + "</th>";
                mensaje += "<th style='border: 1px solid #e6e6e6; font-weight: 100;'>" + item.FechaPago.ToString("dd/MM/yyyy") + "</th>";
                mensaje += "<th style='border: 1px solid #e6e6e6; font-weight: 100;' align='right'>" + simboloMoneda + " " + item.MontoCuotaDescuento.ToString("0.00") + "</th>";
                mensaje += "</tr>";
            }
            mensaje += "</tbody>";
            mensaje += "</table>";

            mensaje += "<p>Sin otro particular quedo a la espera de su confirmación.</p>";
            mensaje += "<br/>";
            mensaje += "<p>Saludos,</p>";

            return mensaje;
        }
    }
}
