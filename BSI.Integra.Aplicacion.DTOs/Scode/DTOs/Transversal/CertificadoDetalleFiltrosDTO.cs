using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CertificadoDetalleFiltrosDTO
    {
        public List<FiltroDTO> TipoPrograma { get; set; }
        public List<FiltroDTO> TipoCertificado { get; set; }
    }
}
