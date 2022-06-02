using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class CajaPorRendirBO :BaseBO
    {
        public int? IdCaja { get; set; }
        public int? IdCajaPorRendirCabecera { get; set; }
        public int? IdFur { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public int IdPersonalResponsableCaja { get; set; }
        public string Descripcion { get; set; }
        public int IdMoneda { get; set; }
        public decimal TotalEfectivo { get; set; }
        public DateTime FechaEntregaEfectivo { get; set; }
        public bool EsEnviado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
