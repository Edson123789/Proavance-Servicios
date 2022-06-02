using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class CorrespondeBeneficiosDTO
    {
        public List<MatriculaCabeceraBeneficiosDTO> beneficios { get; set; }
        public bool corresponde {get;set;}

    }
}
