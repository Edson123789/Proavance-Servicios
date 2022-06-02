using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRespuestaPregunta
    {
        public TRespuestaPregunta()
        {
            TExamenRealizadoRespuestaEvaluador = new HashSet<TExamenRealizadoRespuestaEvaluador>();
        }

        public int Id { get; set; }
        public int? IdPregunta { get; set; }
        public bool? RespuestaCorrecta { get; set; }
        public int NroOrden { get; set; }
        public string EnunciadoRespuesta { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? NroOrdenRespuesta { get; set; }
        public int? Puntaje { get; set; }
        public string FeedbackPositivo { get; set; }
        public string FeedbackNegativo { get; set; }
        public bool? MostrarFeedBack { get; set; }
        public int? PuntajeTipoRespuesta { get; set; }

        public virtual ICollection<TExamenRealizadoRespuestaEvaluador> TExamenRealizadoRespuestaEvaluador { get; set; }
    }
}
