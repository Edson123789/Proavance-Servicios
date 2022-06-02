using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFeedbackConfigurarGrupoPregunta
    {
        public TFeedbackConfigurarGrupoPregunta()
        {
            TFeedbackGrupoPreguntaProgramaEspecifico = new HashSet<TFeedbackGrupoPreguntaProgramaEspecifico>();
            TFeedbackGrupoPreguntaProgramaGeneral = new HashSet<TFeedbackGrupoPreguntaProgramaGeneral>();
        }

        public int Id { get; set; }
        public int IdFeedbackConfigurar { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TFeedbackConfigurar IdFeedbackConfigurarNavigation { get; set; }
        public virtual ICollection<TFeedbackGrupoPreguntaProgramaEspecifico> TFeedbackGrupoPreguntaProgramaEspecifico { get; set; }
        public virtual ICollection<TFeedbackGrupoPreguntaProgramaGeneral> TFeedbackGrupoPreguntaProgramaGeneral { get; set; }
    }
}
