using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMatrizEscalaNeoPir
    {
        public int Id { get; set; }
        public int IdPreguntaEscalaValor { get; set; }
        public int IdSexo { get; set; }
        public string Pd { get; set; }
        public decimal T { get; set; }
        public decimal Per { get; set; }
        public int? EstadoVariable { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
