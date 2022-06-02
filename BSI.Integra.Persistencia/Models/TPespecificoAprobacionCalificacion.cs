using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPespecificoAprobacionCalificacion
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int Grupo { get; set; }
        public bool? EsNotaAprobada { get; set; }
        public bool? EsAsistenciaAprobada { get; set; }
        public DateTime? FechaAprobacionNota { get; set; }
        public DateTime? FechaAprobacionAsistencia { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
