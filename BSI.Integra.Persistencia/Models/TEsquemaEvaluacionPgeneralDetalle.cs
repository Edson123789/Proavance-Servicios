using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEsquemaEvaluacionPgeneralDetalle
    {
        public TEsquemaEvaluacionPgeneralDetalle()
        {
            TParametroEvaluacionNota = new HashSet<TParametroEvaluacionNota>();
        }

        public int Id { get; set; }
        public int IdEsquemaEvaluacionPgeneral { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public string Nombre { get; set; }
        public string UrlArchivoInstrucciones { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdProveedor { get; set; }

        public virtual TCriterioEvaluacion IdCriterioEvaluacionNavigation { get; set; }
        public virtual TEsquemaEvaluacionPgeneral IdEsquemaEvaluacionPgeneralNavigation { get; set; }
        public virtual TProveedor IdProveedorNavigation { get; set; }
        public virtual ICollection<TParametroEvaluacionNota> TParametroEvaluacionNota { get; set; }
    }
}
