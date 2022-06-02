using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class BeneficioPreRequisitoProgramaDTO
    {
        public List<CompuestoPreRequisitoModalidadDTO> PreRequisitos { get; set; }
        public List<CompuestoBeneficioModalidadDTO> Beneficios { get; set; }
        public string Usuario { get; set; }
        public int IdPGeneral { get; set; }

    }
}
