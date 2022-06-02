using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class GastoFinancieroCronogramaDetalleDTO
	{
        public int Id { get; set; }
        public int IdGastoFinancieroCronograma { get; set; }
        public int NumeroCuota { get; set; }
        public decimal CapitalCuota { get; set; }
        public decimal InteresCuota { get; set; }
        public DateTime FechaVencimientoCuota { get; set; }
    }
}
