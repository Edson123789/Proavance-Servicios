using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsignarCertificadoPartnerComplementoDTO
    {
        public int Id { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class AsignarCertificadoBrochureDTO
    {
        public int Id { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreUsuario { get; set; }
    }
}
