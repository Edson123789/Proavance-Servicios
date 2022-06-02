using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CertificadoDetallePartnerDTO
    {
        public int Id { get; set; }
        public int IdCertificadoDetalle { get; set; }
        public int IdUrlBlockStorage { get; set; }
        public string ContentType { get; set; }
    }
}
