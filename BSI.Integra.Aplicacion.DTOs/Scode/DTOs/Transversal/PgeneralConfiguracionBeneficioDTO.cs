using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PgeneralConfiguracionBeneficioDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int IdBeneficio { get; set; }
        public int? TipoBeneficio { get; set; }
        public string Descripcion { get; set; }
        public List<ConfiguracionBeneficioProgramaGeneralEstadoMatriculaDTO> IdEstadoMatricula { get; set; }
        public List<ConfiguracionBeneficioProgramaGeneralSubEstadoDTO> IdSubEstadoMatricula { get; set; }
        public List<ConfiguracionBeneficioProgramaGeneralPaisDTO> IdPais { get; set; }
        public List<ConfiguracionBeneficioProgramaGeneralVersionDTO> IdVersion { get; set; }
        public List<ConfiguracionBeneficioProgramaGeneralDatoAdicionalDTO> IdDatoAdicional { get; set; }
        public int Entrega { get; set; }
        public bool Asosiar { get; set; }
        public bool? DeudaPendiente { get; set; }
        public int? AvanceAcademico { get; set; }
        public int? OrdenBeneficio { get; set; }
        public bool? DatosAdicionales { get; set; }

    }
}
