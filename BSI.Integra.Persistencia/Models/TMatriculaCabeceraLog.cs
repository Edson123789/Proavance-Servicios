using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMatriculaCabeceraLog
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPespecificoAnterior { get; set; }
        public string CodigoCentroCostoAnterior { get; set; }
        public int IdPespecificoNuevo { get; set; }
        public string CodigoCentroCostoNuevo { get; set; }
        public string IdMatriculaCabecera { get; set; }
        public int? IdAlumno { get; set; }
        public string Alumno { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
