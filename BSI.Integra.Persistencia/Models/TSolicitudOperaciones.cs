using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSolicitudOperaciones
    {
        public TSolicitudOperaciones()
        {
            TConvalidacionNota = new HashSet<TConvalidacionNota>();
            TSolicitudOperacionesAccesoTemporalDetalle = new HashSet<TSolicitudOperacionesAccesoTemporalDetalle>();
        }

        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdTipoSolicitudOperaciones { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public int IdPersonalAprobacion { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public bool Aprobado { get; set; }
        public bool EsCancelado { get; set; }
        public string ComentarioSolicitante { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdUrlBlockStorage { get; set; }
        public string NombreArchivo { get; set; }
        public string ContentType { get; set; }
        public bool? Realizado { get; set; }
        public string ObservacionEncargado { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public int? RelacionEstadoSubEstado { get; set; }
        public bool? EnvioAutomatico { get; set; }

        public virtual ICollection<TConvalidacionNota> TConvalidacionNota { get; set; }
        public virtual ICollection<TSolicitudOperacionesAccesoTemporalDetalle> TSolicitudOperacionesAccesoTemporalDetalle { get; set; }
    }
}
