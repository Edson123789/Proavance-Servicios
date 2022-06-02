using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class BeneficioModalidadDTO
    {
        public int IdBeneficio { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreBeneficio { get; set; }
        public int IdModalidadCurso { get; set; }
        public string NombreModalidad { get; set; }
        public int? IdArgumentoBeneficio { get; set; }
        public string NombreArgumentoBeneficio { get; set; }
        public int IdModalidadBeneficio { get; set; }
    }
    public class MotivacionModalidadDTO
    {
        public int IdMotivacion { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreMotivacion { get; set; }
        public int IdModalidadCurso { get; set; }
        public string NombreModalidad { get; set; }
        public int? IdArgumentoMotivacion { get; set; }
        public string NombreArgumentoMotivacion { get; set; }
        public int IdModalidadMotivacion { get; set; }
    }
    public class CertificacionModalidadDTO
    {
        public int IdCertificacion { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreCertificacion { get; set; }
        public int IdModalidadCurso { get; set; }
        public string NombreModalidad { get; set; }
        public int? IdArgumentoCertificacion { get; set; }
        public string NombreArgumentoCertificacion { get; set; }
        public int IdModalidadCertificacion { get; set; }
    }
    public class ProblemaModalidadDTO
    {
        public int IdProblema { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreProblema { get; set; }
        public int IdModalidadCurso { get; set; }
        public string NombreModalidad { get; set; }
        public int? IdArgumentoProblema { get; set; }
        public string DetalleArgumentoProblema { get; set; }
        public string SolucionArgumentoProblema { get; set; }
        public int IdModalidadProblema { get; set; }
    }
}
