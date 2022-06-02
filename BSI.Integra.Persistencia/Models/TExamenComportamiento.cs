using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TExamenComportamiento
    {
        public int Id { get; set; }
        public bool PreguntaObligatoria { get; set; }
        public int? IdEvaluacionFeedbackAprobado { get; set; }
        public int? IdEvaluacionFeedbackDesaprobado { get; set; }
        public int? IdEvaluacionFeedbackCancelado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
