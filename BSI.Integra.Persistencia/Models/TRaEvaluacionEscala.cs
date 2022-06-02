using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRaEvaluacionEscala
    {
        public int Id { get; set; }
        public string Ciudad { get; set; }
        public decimal Escala { get; set; }
        public decimal NotaAprobatoria { get; set; }
        public int Redondeo { get; set; }
        public string TextoEscala { get; set; }
        public string TextoNotaAprobatoria { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
