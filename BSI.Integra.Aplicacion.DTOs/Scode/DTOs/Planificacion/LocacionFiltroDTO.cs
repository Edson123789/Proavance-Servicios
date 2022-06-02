using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class LocacionFiltroDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
        public int IdPais { get; set; }
        public int IdRegion { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion { get; set; }
        public string Usuario { get; set; }
    }
}
