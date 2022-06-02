using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones
{
	public class RaCursoBO : BaseBO
	{
		public int? IdCursoIntegra { get; set; }
		public int? IdRaCentroCosto { get; set; }
		public string NombreCurso { get; set; }
		public bool? Silabo { get; set; }
		public int? IdExpositor { get; set; }
		public int? PorcentajeAsistencia { get; set; }
		public int? Orden { get; set; }
		public int? Grupo { get; set; }
		public bool? Finalizado { get; set; }
		public int? IdMoneda { get; set; }
		public decimal? CostoHora { get; set; }
		public int? IdMonedaTipoCambio { get; set; }
		public decimal? TipoCambio { get; set; }
		public DateTime? FechaTipoCambio { get; set; }
		public DateTime? FechaFinalizacion { get; set; }
		public DateTime? FechaSolicitudNota { get; set; }
		public DateTime? FechaEstimadaRecepcionNota { get; set; }
		public DateTime? FechaRecepcionNota { get; set; }
		public string RutaArchivoNota { get; set; }
		public string NombreArchivoNota { get; set; }
		public string MimetypeArchivoNota { get; set; }
		public int? IdPespecificoIntegra { get; set; }
		public string RutaContrato { get; set; }
		public string NombreContrato { get; set; }
		public string Mimetype { get; set; }
		public DateTime? FechaCreacionAulasVirtuales { get; set; }
		public int? PlazoPagoDias { get; set; }
		public DateTime? FechaConfirmacionDocente { get; set; }
		public DateTime? FechaIngresoConfirmacionDocente { get; set; }
		public string RutaConfirmacionDocente { get; set; }
		public string NombreArchivoConfirmacionDocente { get; set; }
		public string MimetypeConfirmacionDocente { get; set; }
		public string RutaSilabo { get; set; }
		public string NombreSilabo { get; set; }
		public string MimetypeSilabo { get; set; }
		public DateTime? FechaSubidaSilabo { get; set; }
		public bool? SilaboConfirmado { get; set; }
		public DateTime? FechaSolicitudMaterial { get; set; }
		public DateTime? FechaEstimadaEntregaMaterial { get; set; }
		public string RutaArchivoEvaluacion { get; set; }
		public string NombreArchivoEvaluacion { get; set; }
		public string MimetypeArchivoEvaluacion { get; set; }
		public DateTime? FechaSubidaEvaluacion { get; set; }
		public DateTime? FechaEstimadaEntregaEvaluacion { get; set; }
		public bool? EvaluacionConfirmada { get; set; }
		public decimal? PromedioDocenteEncuestaFinal { get; set; }
		public bool? ConformidadSilabo { get; set; }
		public bool? ConformidadHoraCronologica { get; set; }
		public bool EsInicioAonline { get; set; }
		public int? IdMigracion { get; set; }
		public int? IdRaTipoCurso { get; set; }
		public int? IdRaTipoContrato { get; set; }
	}
}
