using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFeriado
    {
        public int Id { get; set; }
        public int? Tipo { get; set; }
        public DateTime Dia { get; set; }
        public string Motivo { get; set; }
        public int Frecuencia { get; set; }
        public int IdTroncalCiudad { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
