using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoBeneficioModalidadDTO
    {
        public int IdBeneficio { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreBeneficio { get; set; }
        public List<BeneficioArgumentoDTO> BeneficiosArgumentos { get; set; }
        public List<ModalidadCursoDTO> Modalidades { get; set; }
    }
    public class CompuestoMotivacionModalidadDTO
    {
        public int IdMotivacion { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreMotivacion { get; set; }
        public List<MotivacionArgumentoDTO> MotivacionesArgumentos { get; set; }
        public List<ModalidadCursoDTO> Modalidades { get; set; }
    }
    public class CompuestoCertificacionModalidadDTO
    {
        public int IdCertificacion { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreCertificacion { get; set; }
        public List<CertificacionArgumentoDTO> CertificacionesArgumentos { get; set; }
        public List<ModalidadCursoDTO> Modalidades { get; set; }
    }
    public class CompuestoProblemaModalidadDTO
    {
        public int IdProblema { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreProblema { get; set; }
        public bool EsVisibleAgenda { get; set; }
        public List<ProblemaArgumentoDTO> ProblemasArgumentos { get; set; }
        public List<ModalidadCursoDTO> Modalidades { get; set; }
    }
}
