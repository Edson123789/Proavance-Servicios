using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFeedbackConfigurar
    {
        public TFeedbackConfigurar()
        {
            TFeedbackConfigurarDetalle = new HashSet<TFeedbackConfigurarDetalle>();
            TFeedbackConfigurarGrupoPregunta = new HashSet<TFeedbackConfigurarGrupoPregunta>();
        }

        public int Id { get; set; }
        public int IdFeedbackTipo { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TFeedbackTipo IdFeedbackTipoNavigation { get; set; }
        public virtual ICollection<TFeedbackConfigurarDetalle> TFeedbackConfigurarDetalle { get; set; }
        public virtual ICollection<TFeedbackConfigurarGrupoPregunta> TFeedbackConfigurarGrupoPregunta { get; set; }
    }
}
