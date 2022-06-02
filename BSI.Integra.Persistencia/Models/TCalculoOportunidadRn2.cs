using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCalculoOportunidadRn2
    {
        public int Id { get; set; }
        public int IdOportunidadRn2 { get; set; }
        public bool TieneOportunidadAbierta { get; set; }
        public int? IdOportunidadAbierta { get; set; }
        public bool TieneOportunidadCerradaSesentaDias { get; set; }
        public int? IdOportunidadCerradaSesentaDias { get; set; }
        public bool TieneOportunidadCerradaPosterior { get; set; }
        public int IdOportunidadCerradaPosterior { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
