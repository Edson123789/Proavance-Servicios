using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoPerfilContactoProgramaDTO
    {
        public CoeficienteScoringCargoDTO Cargo { get; set; }
        public CoeficienteScoringCiudadDTO Ciudad { get; set; }
        public CoeficienteScoringCategoriaDTO Categoria { get; set; }
        public CoeficienteScoringATrabajoDTO Trabajo { get; set; }
        public CoeficienteScoringAFormacionDTO Formacion { get; set; }
        public CoeficienteScoringModalidadDTO Modalidad { get; set; }
        public CoeficienteScoringIndustriaDTO Industria { get; set; }
        public List<TipoDatoPerFilContactoProgramaDTO> TipoDato { get; set; }
        public List<EscalaProbabilidadDTO> Escala { get; set; }
        public PerfilContactoInterceptoDTO Intercepto { get; set; }
        public string Usuario { get; set; }
        public int IdPGeneral { get; set; }
    }
}
