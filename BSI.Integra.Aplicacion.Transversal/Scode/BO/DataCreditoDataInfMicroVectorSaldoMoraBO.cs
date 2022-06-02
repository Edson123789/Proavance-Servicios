using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfMicroVectorSaldoMoraBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public bool? PoseeSectorCooperativo { get; set; }
        public bool? PoseeSectorFinanciero { get; set; }
        public bool? PoseeSectorReal { get; set; }
        public bool? PoseeSectorTelcos { get; set; }
        public DateTime? Fecha { get; set; }
        public int? TotalCuentasMora { get; set; }
        public decimal? SaldoDeudaTotalMora { get; set; }
        public decimal? SaldoDeudaTotal { get; set; }
        public string MorasMaxSectorFinanciero { get; set; }
        public string MorasMaxSectorReal { get; set; }
        public string MorasMaxSectorTelcos { get; set; }
        public string MorasMaximas { get; set; }
        public int? NumCreditos30 { get; set; }
        public int? NumCreditosMayorIgual60 { get; set; }
    }
}
