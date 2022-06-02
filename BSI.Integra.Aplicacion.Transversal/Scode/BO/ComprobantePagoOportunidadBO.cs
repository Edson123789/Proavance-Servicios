using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ComprobantePagoOportunidadBO : BaseBO
    {
        public int Id { get; set; }
        public int? IdContacto { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Celular { get; set; }
        public string Dni { get; set; }
        public string Correo { get; set; }
        public string NombrePais { get; set; }
        public int IdPais { get; set; }
        public string NombreCiudad { get; set; }
        public string TipoComprobante { get; set; }
        public string NroDocumento { get; set; }
        public string NombreRazonSocial { get; set; }
        public string Direccion { get; set; }
        public int BitComprobante { get; set; }
        public int? IdOcurrencia { get; set; }
        public int IdAsesor { get; set; }
        public int? IdOportunidad { get; set; }
        public string Comentario { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public ComprobantePagoOportunidadBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }

        public string MensajeEmailComprobantePago()
        {
            string mensaje = string.Empty;
            if (this.Dni == null) { this.Dni = ""; }
            if (this.Direccion == null) { this.Direccion = ""; }
            if (this.NroDocumento == null) { this.NroDocumento = ""; }
            if (this.NombreRazonSocial == null) { this.NombreRazonSocial = ""; }
            if (this.Comentario == null) { this.Comentario = ""; }

            if (this.BitComprobante == 0)
            {
                mensaje += "<p><b>Datos del Alumno</b></p>";
                mensaje += "<ul>";
                mensaje += "<li><b>Nombres:</b> " + this.Nombres + "</li>";
                mensaje += "<li><b>Apellidos:</b> " + (this.Apellidos == null ? "No se ingreso apellido" : this.Apellidos) + "</li>";
                mensaje += "<li><b>Tipo comprobante:</b> " + this.TipoComprobante + "</li>";
                mensaje += "<li><b>País:</b> " + this.NombrePais + "</li>";
                mensaje += "<li><b>Ciudad:</b> " + this.NombreCiudad + "</li>";
                mensaje += "<li><b>Correo:</b> " + this.Correo + "</li>";
                mensaje += "<li><b>Direccion:</b> " + this.Direccion + "</li>";
                mensaje += "<li><b>Documento:</b> " + this.Dni + "</li>";
                mensaje += "<li><b>Celular:</b> " + this.Celular + "</li>";
                mensaje += "<li><b>Comentario de Asesor:</b> " + this.Comentario + "</li>";
                mensaje += "</ul>";
            }
            else
            {
                if (this.IdPais == 51)
                {
                    mensaje += "<p><b>Datos del Alumno</b></p>";
                    mensaje += "<ul>";
                    mensaje += "<li><b>Nombres:</b> " + this.Nombres + "</li>";
                    mensaje += "<li><b>Apellidos:</b> " + (this.Apellidos == null ? "No se ingreso apellido" : this.Apellidos) + "</li>";
                    mensaje += "<li><b>Tipo comprobante:</b> " + this.TipoComprobante + "</li>";
                    mensaje += "<li><b>RUC:</b> " + this.NroDocumento + "</li>";
                    mensaje += "<li><b>Razón Social:</b> " + this.NombreRazonSocial + "</li>";
                    mensaje += "<li><b>País:</b> " + this.NombrePais + "</li>";
                    mensaje += "<li><b>Ciudad:</b> " + this.NombreCiudad + "</li>";
                    mensaje += "<li><b>Correo:</b> " + this.Correo + "</li>";
                    mensaje += "<li><b>Direccion:</b> " + this.Direccion + "</li>";
                    mensaje += "<li><b>Documento:</b> " + this.Dni + "</li>";
                    mensaje += "<li><b>Celular:</b> " + this.Celular + "</li>";
                    mensaje += "<li><b>Comentario de Asesor:</b> " + this.Comentario + "</li>";
                    mensaje += "</ul>";
                }
                else
                {
                    mensaje += "<p><b>Datos del Alumno</b></p>";
                    mensaje += "<ul>";
                    mensaje += "<li><b>Nombres:</b> " + this.Nombres + "</li>";
                    mensaje += "<li><b>Apellidos:</b> " + this.Apellidos + "</li>";
                    mensaje += "<li><b>Tipo comprobante:</b> " + this.TipoComprobante + "</li>";
                    mensaje += "<li><b>RUT:</b> " + this.NroDocumento + "</li>";
                    mensaje += "<li><b>Razón Social:</b> " + this.NombreRazonSocial + "</li>";
                    mensaje += "<li><b>País:</b> " + this.NombrePais + "</li>";
                    mensaje += "<li><b>Ciudad:</b> " + this.NombreCiudad + "</li>";
                    mensaje += "<li><b>Correo:</b> " + this.Correo + "</li>";
                    mensaje += "<li><b>Direccion:</b> " + this.Direccion + "</li>";
                    mensaje += "<li><b>Documento:</b> " + this.Dni + "</li>";
                    mensaje += "<li><b>Celular:</b> " + this.Celular + "</li>";
                    mensaje += "<li><b>Comentario de Asesor:</b> " + this.Comentario + "</li>";
                    mensaje += "</ul>";
                }

            }
            return mensaje;
        }
    }

}
