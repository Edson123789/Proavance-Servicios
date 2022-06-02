using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TReclamo
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Descripcion { get; set; }
        public int IdReclamoEstado { get; set; }
        public int IdOrigen { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdTipoReclamoAlumno { get; set; }
        public int? NroDiasSolucion { get; set; }
        public int? IdEstadoMatriculaPrevio { get; set; }
        public DateTime? FechaReclamoRealizadoFin { get; set; }

        public virtual TMatriculaCabecera IdMatriculaCabeceraNavigation { get; set; }
        public virtual TOrigen IdOrigenNavigation { get; set; }
        public virtual TReclamoEstado IdReclamoEstadoNavigation { get; set; }
        public virtual TTipoReclamoAlumno IdTipoReclamoAlumnoNavigation { get; set; }
    }
}
