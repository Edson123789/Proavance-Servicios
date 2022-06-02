using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CertificadoEnvioLogDTO
    {
        public int Id { get; set; }
        public int IdCertificadoDetalle { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool SoloDigital { get; set; }
        public string NombreUsuario { get; set; }
    }
}
