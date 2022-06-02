using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAvanceTiempoRealLog
    {
        public int Id { get; set; }
        public int IdAvanceTiempoReal { get; set; }
        public int IdPersonal { get; set; }
        public int? NroSesion { get; set; }
        public int? NroCapitulo { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdAlumno { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
