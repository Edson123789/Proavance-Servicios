using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TIntentoPreguntaEvaluacion
    {
        public int Id { get; set; }
        public int? NumeroMaximoIntento { get; set; }
        public bool? MostrarFeedbackInmediato { get; set; }
        public bool? MostrarFeedbackPorPregunta { get; set; }
        public bool? ActivarFeedbackPorNumeroIntento { get; set; }
        public string MensajeFeedback { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
