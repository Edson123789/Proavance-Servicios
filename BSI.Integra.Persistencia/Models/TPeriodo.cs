using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPeriodo
    {
        public TPeriodo()
        {
            TReportePendienteHistorico = new HashSet<TReportePendienteHistorico>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaInicialFinanzas { get; set; }
        public DateTime FechaFinFinanzas { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public DateTime? FechaInicialRepIngresos { get; set; }
        public DateTime? FechaFinRepIngresos { get; set; }

        public virtual ICollection<TReportePendienteHistorico> TReportePendienteHistorico { get; set; }
    }
}
