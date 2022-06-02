using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class BeneficioVentasDTO
    {
        public CompuestoBeneficioModalidadDTO Beneficios { get; set; }
        public string Usuario { get; set; }
        public int IdPGeneral { get; set; }
    }
    public class MotivacionVentasDTO
    {
        public CompuestoMotivacionModalidadDTO Motivaciones { get; set; }
        public string Usuario { get; set; }
        public int IdPGeneral { get; set; }
    }
    public class CertificacionVentasDTO
    {
        public CompuestoCertificacionModalidadDTO Certificaciones { get; set; }
        public string Usuario { get; set; }
        public int IdPGeneral { get; set; }
    }
    public class ProblemaVentasDTO
    {
        public CompuestoProblemaModalidadDTO Problemas { get; set; }
        public string Usuario { get; set; }
        public int IdPGeneral { get; set; }
    }
}
