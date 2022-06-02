using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPregunta
    {
        public TPregunta()
        {
            TConfigurarExamenProgramaPregunta = new HashSet<TConfigurarExamenProgramaPregunta>();
            TExamenRealizadoRespuestaEvaluador = new HashSet<TExamenRealizadoRespuestaEvaluador>();
            TPreguntaEvaluacionTrabajo = new HashSet<TPreguntaEvaluacionTrabajo>();
        }

        public int Id { get; set; }
        public int? IdTipoRespuesta { get; set; }
        public int? IdPreguntaEscalaValor { get; set; }
        public string EnunciadoPregunta { get; set; }
        public bool? ConparacionValor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public bool? RequiereTiempo { get; set; }
        public int? MinutosPorPregunta { get; set; }
        public bool? RespuestaAleatoria { get; set; }
        public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
        public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
        public bool? MostrarFeedbackInmediato { get; set; }
        public bool? MostrarFeedbackPorPregunta { get; set; }
        public int? IdPreguntaIntento { get; set; }
        public int? IdPreguntaTipo { get; set; }
        public int? IdTipoRespuestaCalificacion { get; set; }
        public int? FactorRespuesta { get; set; }
        public int? IdPreguntaCategoria { get; set; }

        public virtual ICollection<TConfigurarExamenProgramaPregunta> TConfigurarExamenProgramaPregunta { get; set; }
        public virtual ICollection<TExamenRealizadoRespuestaEvaluador> TExamenRealizadoRespuestaEvaluador { get; set; }
        public virtual ICollection<TPreguntaEvaluacionTrabajo> TPreguntaEvaluacionTrabajo { get; set; }
    }
}
