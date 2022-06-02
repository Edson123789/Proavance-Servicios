using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CentroEstudioDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdPais { get; set; }
		public int IdCiudad { get; set; }
		public string Pais { get; set; }
		public string Ciudad { get; set; }
		public int IdTipoCentroEstudio { get; set; }
		public string TipoCentroEstudio { get; set; }
	}
	public class CentroEstudioFiltroDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdPais { get; set; }
		public int IdCiudad { get; set; }
		public int IdTipoCentroEstudio { get; set; }
		public string Usuario { get; set; }
	}
}
