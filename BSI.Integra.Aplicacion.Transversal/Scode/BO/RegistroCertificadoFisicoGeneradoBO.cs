using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class RegistroCertificadoFisicoGeneradoBO : BaseBO
    {
        public int IdSolicitudCertificadoFisico { get; set; }
        public int IdUrlBlockStorage { get; set; }
        public string FormatoArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime UltimaFechaGeneracion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
