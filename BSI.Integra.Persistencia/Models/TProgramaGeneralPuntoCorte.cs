using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProgramaGeneralPuntoCorte
    {
        public int Id { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public decimal? PuntoCorteMedia { get; set; }
        public decimal? PuntoCorteAlta { get; set; }
        public decimal? PuntoCorteMuyAlta { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPgeneral IdProgramaGeneralNavigation { get; set; }
    }
}
