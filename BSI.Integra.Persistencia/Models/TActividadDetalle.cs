using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TActividadDetalle
    {
        public TActividadDetalle()
        {
            TLlamadaActividad = new HashSet<TLlamadaActividad>();
            TLlamadaWebphone = new HashSet<TLlamadaWebphone>();
        }

        public int Id { get; set; }
        public int? IdActividadCabecera { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaReal { get; set; }
        public int? DuracionReal { get; set; }
        public int? IdOcurrencia { get; set; }
        public int IdEstadoActividadDetalle { get; set; }
        public string Comentario { get; set; }
        public int? IdAlumno { get; set; }
        public string Actor { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdCentralLlamada { get; set; }
        public string RefLlamada { get; set; }
        public int? IdOcurrenciaActividad { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public DateTime? FechaOcultarWhatsapp { get; set; }
        public int? IdOcurrenciaAlterno { get; set; }
        public int? IdOcurrenciaActividadAlterno { get; set; }

        public virtual TOportunidad IdOportunidadNavigation { get; set; }
        public virtual ICollection<TLlamadaActividad> TLlamadaActividad { get; set; }
        public virtual ICollection<TLlamadaWebphone> TLlamadaWebphone { get; set; }
    }
}
