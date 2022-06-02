using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEsquemaEvaluacion
    {
        public TEsquemaEvaluacion()
        {
            TEsquemaEvaluacionDetalle = new HashSet<TEsquemaEvaluacionDetalle>();
            TEsquemaEvaluacionPgeneral = new HashSet<TEsquemaEvaluacionPgeneral>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdFormaCalculoEvaluacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public bool? EsModulo { get; set; }

        public virtual TFormaCalculoEvaluacion IdFormaCalculoEvaluacionNavigation { get; set; }
        public virtual ICollection<TEsquemaEvaluacionDetalle> TEsquemaEvaluacionDetalle { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneral> TEsquemaEvaluacionPgeneral { get; set; }
    }
}
