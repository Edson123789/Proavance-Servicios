using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProgramaGeneralPuntoCorteConfiguracion
    {
        public int Id { get; set; }
        public int IdTipoCorte { get; set; }
        public string Tipo { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int IdPgeneral { get; set; }
        public string Color { get; set; }
        public string Texto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
