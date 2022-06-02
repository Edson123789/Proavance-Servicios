using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfAgrResumenEndeudamientoBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public DateTime? TrimestreFecha { get; set; }
        public string SectorSector { get; set; }
        public string SectorCodigoSector { get; set; }
        public string SectorGarantiaAdmisible { get; set; }
        public string SectorGarantiaOtro { get; set; }
        public string CarteraTipo { get; set; }
        public int? CarteraNumeroCuentas { get; set; }
        public decimal? CarteraValor { get; set; }
    }
}
