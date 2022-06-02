using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TControlDocAlumno
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? IdCriterioCalificacion { get; set; }
        public string QuienEntrego { get; set; }
        public DateTime? FechaEntregaDocumento { get; set; }
        public string Observaciones { get; set; }
        public string ComisionableEditable { get; set; }
        public decimal? MontoComisionable { get; set; }
        public string ObservacionesComisionable { get; set; }
        public decimal? PagadoComisionable { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public string IdMigracion { get; set; }
        public int? IdMatriculaObservacion { get; set; }
    }
}
