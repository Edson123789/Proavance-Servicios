using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TLlamadaInicioDia
    {
        public int Id { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdOportunidad { get; set; }
        public DateTime Fecha { get; set; }
        public bool EsProgramada { get; set; }
        public bool EsVencida { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
