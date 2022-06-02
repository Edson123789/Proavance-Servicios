using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConvalidacionNota
    {
        public int Id { get; set; }
        public int IdSolicitudOperaciones { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPespecificoAnterior { get; set; }
        public int IdPespecificoNuevo { get; set; }
        public string NombreEvaluacion { get; set; }
        public int IdEvaluacionAnterior { get; set; }
        public int IdEvaluacionNueva { get; set; }
        public decimal Nota { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TMatriculaCabecera IdMatriculaCabeceraNavigation { get; set; }
        public virtual TSolicitudOperaciones IdSolicitudOperacionesNavigation { get; set; }
    }
}
