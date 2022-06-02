using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class GastoFinancieroCronogramaDatosDTO
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public string NombreEntidadFinanciera { get; set; }
        public int IdMoneda { get; set; }
        public string NombreMoneda { get; set; }
        public decimal CapitalTotal { get; set; }
        public decimal InteresTotal { get; set; }
        public DateTime FechaInicio { get; set; }
    }
}
