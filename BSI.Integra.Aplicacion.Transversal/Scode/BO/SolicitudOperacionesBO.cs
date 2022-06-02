using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class SolicitudOperacionesBO : BaseBO
    {
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
        public int? IdMigracion { get; set; }
        public int? IdUrlBlockStorage { get; set; }
        public string NombreArchivo { get; set; }
        public string ContentType { get; set; }
        public bool? Realizado { get; set; }
        public string ObservacionEncargado { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public int? RelacionEstadoSubEstado { get; set; }
        public bool? EnvioAutomatico { get; set; }
    }
}
