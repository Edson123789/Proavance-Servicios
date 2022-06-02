using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CentroCostoAsignadoCertificadoPartnerComplementoDTO
    {
        public int? IdCertificadoPartnerComplemento { get; set; } 
        public int IdCentroCosto { get; set; } 
        public string NombreCentroCosto { get; set; }
    }

    public class CentroCostoAsignadoCertificadoBrochureDTO
    {
        public int? IdCertificadoBrochure { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
    }
}
