using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEstructuraEspecifica
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPgeneralPadre { get; set; }
        public int IdPgeneralHijo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
