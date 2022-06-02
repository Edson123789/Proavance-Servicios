using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CompuestoTipoDescuentoDTO
	{
		public TipoDescuentoDTO TipoDescuento { get; set; }
		public List<int> ListaTipos { get; set; }
        public string Usuario { get; set; }
    }
}
