using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ListaBeneficioLaboralDTO
    {
        public List<BeneficioLaboralVentasDTO> ListaBeneficiados { get; set; }
        public int IdPeriodo { get; set; }
        public string UsuarioModificacion { get; set; }        
    }
}
