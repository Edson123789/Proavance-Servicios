using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class GastoFinancieroCronogramaDTO
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public int IdMoneda { get; set; }
        public decimal CapitalTotal { get; set; }
        public decimal InteresTotal { get; set; }
        public DateTime FechaInicio { get; set; }
    }
}
