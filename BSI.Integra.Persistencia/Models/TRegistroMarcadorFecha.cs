using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRegistroMarcadorFecha
    {
        public int Id { get; set; }
        public int IdCiudad { get; set; }
        public int IdPersonal { get; set; }
        public string Pin { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan? M1 { get; set; }
        public TimeSpan? M2 { get; set; }
        public TimeSpan? M3 { get; set; }
        public TimeSpan? M4 { get; set; }
        public TimeSpan? M5 { get; set; }
        public TimeSpan? M6 { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
