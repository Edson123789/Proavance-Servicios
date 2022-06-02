using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;


namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MontoPagoCronogramaDetalleBO : BaseBO
    {
        public int NumeroCuota { get; set; }
        public string CuotaDescripcion { get; set; }
        public float MontoCuota { get; set; }
        public DateTime FechaPago { get; set; }
        public float MontoCuotaDescuento { get; set; }
        public bool Pagado { get; set; }
        public int IdMontoPagoCronograma { get; set; }
        public bool Matricula { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
