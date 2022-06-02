using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEmbudo
    {
        public int Id { get; set; }
        public int? IdAlumno { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdEmbudoNivel { get; set; }
        public int IdEmbudoSubNivel { get; set; }
        public int? IdTipoCategoriaOrigen { get; set; }
        public int IdProgramaGeneral { get; set; }
        public decimal? PrecioProgramaGeneral { get; set; }
        public int? IdCantidadInteraccion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
