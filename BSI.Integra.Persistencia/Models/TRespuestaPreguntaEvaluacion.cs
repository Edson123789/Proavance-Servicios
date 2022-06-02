using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRespuestaPreguntaEvaluacion
    {
        public int Id { get; set; }
        public int IdPreguntaEvaluacion { get; set; }
        public string EnunciadoRespuesta { get; set; }
        public string EnunciadoEmparejar { get; set; }
        public bool? EsRespuestaCorrecta { get; set; }
        public int Orden { get; set; }
        public string Feedback { get; set; }
        public bool? MostrarFeedback { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
