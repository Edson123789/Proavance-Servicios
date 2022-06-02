using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPreguntaProgramaCapacitacion
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int? OrdenFilaCapitulo { get; set; }
        public int? OrdenFilaSesion { get; set; }
        public int? IdTipoRespuesta { get; set; }
        public int? IdPreguntaEscalaValor { get; set; }
        public string EnunciadoPregunta { get; set; }
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
        public string GrupoPregunta { get; set; }
        public int? IdTipoMarcador { get; set; }
        public decimal? ValorMarcador { get; set; }
        public int? OrdenPreguntaGrupo { get; set; }
        public int? IdPespecifico { get; set; }

        public virtual TPespecifico IdPespecificoNavigation { get; set; }
        public virtual TPgeneral IdPgeneralNavigation { get; set; }
        public virtual TPreguntaEscalaValor IdPreguntaEscalaValorNavigation { get; set; }
        public virtual TPreguntaIntento IdPreguntaIntentoNavigation { get; set; }
        public virtual TPreguntaTipo IdPreguntaTipoNavigation { get; set; }
        public virtual TTipoMarcador IdTipoMarcadorNavigation { get; set; }
        public virtual TTipoRespuestaCalificacion IdTipoRespuestaCalificacionNavigation { get; set; }
        public virtual TTipoRespuesta IdTipoRespuestaNavigation { get; set; }
    }
}
