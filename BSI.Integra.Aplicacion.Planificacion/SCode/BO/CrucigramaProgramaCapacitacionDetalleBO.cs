using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
	public class CrucigramaProgramaCapacitacionDetalleBO : BaseBO
	{
		public int IdCrucigramaProgramaCapacitacionDetalle { get; set; }
		public int NumeroPalabra { get; set; }
		public string Palabra { get; set; }
		public string Definicion { get; set; }
		public int Tipo { get; set; }
		public int ColumnaInicio { get; set; }
		public int FilaInicio { get; set; }
		public int? IdMigracion { get; set; }
	}
}
