using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TReporteAnalyticsFiltroDetalle
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public bool Excluir { get; set; }
        public int IdReporteAnalyticsFiltro { get; set; }
        public int IdOperadorComparacion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
