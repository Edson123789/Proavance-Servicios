using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TParametroEvaluacionNota
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int Grupo { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdEsquemaEvaluacionPgeneralDetalle { get; set; }
        public int IdParametroEvaluacion { get; set; }
        public int? IdEscalaCalificacionDetalle { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public string Retroalimentacion { get; set; }
        public string NombreArchivoRetroalimentacion { get; set; }
        public string UrlArchivoSubidoRetroalimentacion { get; set; }
        public int? IdEsquemaEvaluacionPgeneralDetalleCongelado { get; set; }
        public int? IdPespecificoHijo { get; set; }

        public virtual TEscalaCalificacionDetalle IdEscalaCalificacionDetalleNavigation { get; set; }
        public virtual TEsquemaEvaluacionPgeneralDetalle IdEsquemaEvaluacionPgeneralDetalleNavigation { get; set; }
    }
}
