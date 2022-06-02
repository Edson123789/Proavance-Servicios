using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPespecificoMatriculaAlumno
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPespecifico { get; set; }
        public int IdPespecificoTipoMatricula { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdUsuarioMoodle { get; set; }
        public int? IdCursoMoodle { get; set; }
        public int Grupo { get; set; }
        public int? Duracion { get; set; }
        public bool? EsAonline { get; set; }
        public bool? Regularizado { get; set; }
        public string ErrorCongelamiento { get; set; }
        public bool? AplicaNuevaAulaVirtual { get; set; }
        public decimal? NotaAulaVirtualAnterior { get; set; }
        public int? IdEscalaCalificacionDetalle { get; set; }
        public bool? RecuperaNuevaAulaVirtual { get; set; }
    }
}
