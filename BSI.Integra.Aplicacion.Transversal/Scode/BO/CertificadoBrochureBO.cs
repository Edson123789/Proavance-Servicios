using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CertificadoBrochureBO : BaseBO
    {
        public string Nombre { get; set; }
        public string NombreEnCertificado { get; set; }
        public string Contenido { get; set; }
        public int TotalHoras { get; set; }
        public int? IdMigracion { get; set; }
    }
}
