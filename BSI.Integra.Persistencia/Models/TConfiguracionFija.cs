using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionFija
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string NombreTabla { get; set; }
        public int IdTabla { get; set; }
        public string NombreColumna { get; set; }
        public string TipoDato { get; set; }
        public string Valor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
