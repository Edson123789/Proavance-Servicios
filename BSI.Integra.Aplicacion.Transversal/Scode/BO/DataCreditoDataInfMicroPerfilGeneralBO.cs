using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfMicroPerfilGeneralBO : BaseBO
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string Tipo { get; set; }
        public string SectorFinanciero { get; set; }
        public string SectorCooperativo { get; set; }
        public string SectorReal { get; set; }
        public string SectorTelcos { get; set; }
        public string TotalComoPrincipal { get; set; }
        public string TotalComoCodeudorYotros { get; set; }
    }
}
