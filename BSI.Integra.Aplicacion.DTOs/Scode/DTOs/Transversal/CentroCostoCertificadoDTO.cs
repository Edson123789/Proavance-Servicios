using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CentroCostoCertificadoDTO
    {
        public int Id { get; set; }
        public int IdCentroCosto { get; set; }
        public int? IdCertificadoBrochure { get; set; }
        public int? IdCertificadoPartnerComplemento { get; set; }
    }
}
