using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPespecificoSesion
    {
        public TPespecificoSesion()
        {
            TRecuperacionSesion = new HashSet<TRecuperacionSesion>();
        }

        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public decimal Duracion { get; set; }
        public int? IdExpositor { get; set; }
        public string Comentario { get; set; }
        public bool SesionAutoGenerada { get; set; }
        public int? IdAmbiente { get; set; }
        public bool? Predeterminado { get; set; }
        public int Grupo { get; set; }
        public bool EsSesionInicio { get; set; }
        public int Version { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? GrupoSesion { get; set; }
        public int? IdSesionRa { get; set; }
        public int? IdProveedor { get; set; }
        public string UrlWebex { get; set; }
        public int? CuentaWebex { get; set; }
        public int? IdModalidadCurso { get; set; }
        public DateTime? FechaCancelacionWebinar { get; set; }
        public string ComentarioCancelacionWebinar { get; set; }
        public bool? EsWebinarConfirmado { get; set; }
        public bool? MostrarPortalWeb { get; set; }
        public int? IdEstadoEnvioCorreo { get; set; }
        public bool? EnvioSesionCorreo { get; set; }
        public bool? EnvioSesionCorreoRegularizacion { get; set; }
        public DateTime? FechaHoraRegularizacion { get; set; }
        public bool? EnvioAutomaticoCorreoWebinar { get; set; }
        public bool? EnvioAutomaticoWhatsAppWebinar { get; set; }

        public virtual ICollection<TRecuperacionSesion> TRecuperacionSesion { get; set; }
    }
}
