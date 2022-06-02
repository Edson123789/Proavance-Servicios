using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMontoPagoCronogramaDetalle
    {
        public int Id { get; set; }
        public int NumeroCuota { get; set; }
        public double MontoCuota { get; set; }
        public DateTime FechaPago { get; set; }
        public string CuotaDescripcion { get; set; }
        public double MontoCuotaDescuento { get; set; }
        public bool Pagado { get; set; }
        public int? IdMontoPagoCronograma { get; set; }
        public bool Matricula { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TMontoPagoCronograma IdMontoPagoCronogramaNavigation { get; set; }
    }
}
