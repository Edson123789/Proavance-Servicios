using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
	public class CrucigramaProgramaCapacitacionBO : BaseBO
	{
		public int IdPgeneral { get; set; }
		public int IdPespecifico { get; set; }
		public int? OrdenFilaCapitulo { get; set; }
		public int? OrdenFilaSesion { get; set; }
		public string CodigoCrucigrama { get; set; }
		public int IdTipoMarcador { get; set; }
		public decimal ValorMarcador { get; set; }
		public int CantidadFila { get; set; }
		public int CantidadColumna { get; set; }
		public int? IdMigracion { get; set; }
	}
}
