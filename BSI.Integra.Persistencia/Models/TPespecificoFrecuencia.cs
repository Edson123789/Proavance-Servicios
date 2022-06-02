using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPespecificoFrecuencia
    {
        public int Id { get; set; }
        public int? IdPespecifico { get; set; }
        public DateTime FechaInicio { get; set; }
        public int Frecuencia { get; set; }
        public int NroSesiones { get; set; }
        public int? IdFrecuencia { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
