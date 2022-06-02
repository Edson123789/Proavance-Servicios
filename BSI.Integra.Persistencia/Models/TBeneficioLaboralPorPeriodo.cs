using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TBeneficioLaboralPorPeriodo
    {
        public int Id { get; set; }
        public int IdAgendaTipoUsuario { get; set; }
        public int IdPeriodo { get; set; }
        public int IdBeneficioLaboralTipo { get; set; }
        public decimal Monto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
