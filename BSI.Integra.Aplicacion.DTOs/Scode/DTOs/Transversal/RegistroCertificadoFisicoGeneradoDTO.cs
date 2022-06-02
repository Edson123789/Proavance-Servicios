using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RegistroCertificadoFisicoGeneradoDTO
    {
        public int Id { get; set; }
        public int IdSolicitudCertificadoFisico { get; set; }
        public int IdUrlBlockStorage { get; set; }
        public string FormatoArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime UltimaFechaGeneracion { get; set; }
        public string Usuario { get; set; }
    }
}
