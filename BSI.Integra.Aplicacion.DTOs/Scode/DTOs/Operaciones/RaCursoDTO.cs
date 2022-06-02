using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class RaCursoDTO
	{
		public int Id { get; set; }
		public int? IdRaCentroCosto { get; set; }
		public string NombreCurso { get; set; }
		public int? IdRaTipoCurso { get; set; }
		public bool? Silabo { get; set; }
		public int? IdExpositor { get; set; }
		public int? PorcentajeAsistencia { get; set; }
		public int? Orden { get; set; }
		public int? Grupo { get; set; }
		public int? PlazoPagoDias { get; set; }
		public bool EsInicioAonline { get; set; }
		public int? IdMoneda { get; set; }
		public decimal? CostoHora { get; set; }
		public int? IdRaTipoContrato { get; set; }
		public int? IdMonedaTipoCambio { get; set; }
		public decimal? TipoCambio { get; set; }
		public DateTime? FechaTipoCambio { get; set; }
		public bool? Finalizado { get; set; }
		public RaExpositorDTO Docente { get; set; }
	}
}
