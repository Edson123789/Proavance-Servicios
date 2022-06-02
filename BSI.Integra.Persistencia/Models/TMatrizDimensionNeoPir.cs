using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMatrizDimensionNeoPir
    {
        public int Id { get; set; }
        public int IdSexo { get; set; }
        public string NombreEscalaValor { get; set; }
        public int Centil { get; set; }
        public string Clase { get; set; }
        public int ClaseValor { get; set; }
        public int Inferior { get; set; }
        public int Mayor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
