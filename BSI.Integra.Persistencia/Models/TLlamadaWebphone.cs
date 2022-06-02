using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TLlamadaWebphone
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Anexo { get; set; }
        public string TelefonoDestino { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int? IdLlamadasWebphoneTipo { get; set; }
        public int? DuracionTimbrado { get; set; }
        public int? DuracionContesto { get; set; }
        public string WebPhoneId { get; set; }
        public int IdAlumno { get; set; }
        public int IdActividadDetalle { get; set; }
        public int? IdLlamadasWebphoneEstado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public string NombreGrabacion { get; set; }

        public virtual TActividadDetalle IdActividadDetalleNavigation { get; set; }
        public virtual TAlumno IdAlumnoNavigation { get; set; }
        public virtual TLlamadaWebphoneEstado IdLlamadasWebphoneEstadoNavigation { get; set; }
        public virtual TLlamadaWebphoneTipo IdLlamadasWebphoneTipoNavigation { get; set; }
        public virtual TPersonalAreaTrabajo IdPersonalAreaTrabajoNavigation { get; set; }
    }
}
