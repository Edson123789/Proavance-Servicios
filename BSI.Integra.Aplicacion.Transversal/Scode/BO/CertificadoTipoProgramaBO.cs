using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CertificadoTipoProgramaBO : BaseBO
    {
        public string NombreProgramaCertificado { get; set; }
        public string Codigo { get; set; }
        public bool AplicaFondoDiploma { get; set; }
        public bool AplicaSeOtorga { get; set; }
        public bool AplicaNota { get; set; }
        public int? IdMigracion { get; set; }
    }
}
