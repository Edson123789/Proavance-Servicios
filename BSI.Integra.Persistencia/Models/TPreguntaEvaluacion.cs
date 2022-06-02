using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPreguntaEvaluacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCategoriaPreguntaEvaluacion { get; set; }
        public int IdTipoPreguntaEvaluacion { get; set; }
        public string Enunciado { get; set; }
        public string UrlImagen { get; set; }
        public decimal? Puntaje { get; set; }
        public int? Tiempo { get; set; }
        public bool? RespuestaAleatoria { get; set; }
        public int? NumeroIntentos { get; set; }
        public bool? MostrarFeedbackInmediato { get; set; }
        public bool? MostrarFeedbackPorPregunta { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
