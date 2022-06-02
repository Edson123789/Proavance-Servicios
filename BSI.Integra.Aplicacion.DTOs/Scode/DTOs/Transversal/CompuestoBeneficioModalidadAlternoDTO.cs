using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class CompuestoBeneficioModalidadAlternoDTO
    {
        public int IdBeneficio { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreBeneficio { get; set; }
        public List<BeneficioArgumentoDTO> BeneficiosArgumentos { get; set; }
        public List<ModalidadCursoAlternoDTO> Modalidades { get; set; }
    }
    public class CompuestoMotivacionModalidadAlternoDTO
    {
        public int IdMotivacion { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreMotivacion { get; set; }
        public List<MotivacionArgumentoDTO> MotivacionesArgumentos { get; set; }
        public List<ModalidadCursoAlternoDTO> Modalidades { get; set; }
    }
    public class CompuestoCertificacionModalidadAlternoDTO
    {
        public int IdCertificacion { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreCertificacion { get; set; }
        public List<CertificacionArgumentoDTO> CertificacionesArgumentos { get; set; }
        public List<ModalidadCursoAlternoDTO> Modalidades { get; set; }
    }
    public class CompuestoProblemaModalidadAlternoDTO
    {
        public int IdProblema { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreProblema { get; set; }
        public List<ProblemaDetalleSolucionDTO> ProblemasArgumentos { get; set; }
        public List<ModalidadCursoAlternoDTO> Modalidades { get; set; }
    }
}
