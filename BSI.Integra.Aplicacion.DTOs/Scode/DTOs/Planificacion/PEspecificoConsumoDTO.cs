using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoConsumoDTO
    {
		public int IdCentroCosto { get; set; }
        public int IdPespecificoSesion { get; set; }
		public int IdPespecifico { get; set; }
		public int IdHistoricoProductoProveedor { get; set; }
		public int IdProducto { get; set; }
		public int IdProveedor { get; set; }
        public decimal Cantidad { get; set; }
        public string Factor { get; set; }
		public int Semana { get; set; }
		public int AreaTrabajo { get; set; }
        public string Usuario { get; set; }
		public DateTime FechaHoraInicio { get; set; }
		public int Ciudad { get; set; }

	}
}
