using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEsquemaEvaluacionPgeneral
    {
        public TEsquemaEvaluacionPgeneral()
        {
            TEsquemaEvaluacionPgeneralDetalle = new HashSet<TEsquemaEvaluacionPgeneralDetalle>();
            TEsquemaEvaluacionPgeneralModalidad = new HashSet<TEsquemaEvaluacionPgeneralModalidad>();
            TEsquemaEvaluacionPgeneralProveedor = new HashSet<TEsquemaEvaluacionPgeneralProveedor>();
        }

        public int Id { get; set; }
        public int IdEsquemaEvaluacion { get; set; }
        public int IdPgeneral { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? EsquemaPredeterminado { get; set; }

        public virtual TEsquemaEvaluacion IdEsquemaEvaluacionNavigation { get; set; }
        public virtual TPgeneral IdPgeneralNavigation { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneralDetalle> TEsquemaEvaluacionPgeneralDetalle { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneralModalidad> TEsquemaEvaluacionPgeneralModalidad { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneralProveedor> TEsquemaEvaluacionPgeneralProveedor { get; set; }
    }
}
