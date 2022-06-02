using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCambioPorPeriodoCronograma
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal? MontoCambio { get; set; }
        public string TipoModificacion { get; set; }
        public int? IdPeriodoCambio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
