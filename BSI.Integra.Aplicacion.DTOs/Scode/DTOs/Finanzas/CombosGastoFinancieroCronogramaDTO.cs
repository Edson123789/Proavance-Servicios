using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CombosGastoFinancieroCronogramaDTO
    {
        public List <FiltroDTO> ListaEntidadFinanciera { get; set; }
        public List<FiltroGenericoDTO> ListaMoneda { get; set; }

    }
}
