using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRecuperacionSesion
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPespecificoSesion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TMatriculaCabecera IdMatriculaCabeceraNavigation { get; set; }
        public virtual TPespecificoSesion IdPespecificoSesionNavigation { get; set; }
    }
}
