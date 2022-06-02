using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoGastoFinancieroCronogramaDTO
    {
        public GastoFinancieroCronogramaDTO GastoFinancieroCronograma { get; set; }
        public List <GastoFinancieroCronogramaDetalleDTO> GastoFinancieroCronogramaDetalle { get; set; }
        public string Usuario { get; set; }

    }
}
