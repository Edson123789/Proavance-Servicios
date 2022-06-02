using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ContenidoSolicitudCertificadoFisicoDTO
    {
        public List<DataSolicitudCertificadoFisicoDTO> ListaSolicitusCertificado { get; set; }
        public int Total { get; set; }
    }
}
