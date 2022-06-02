using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class OportunidadIsVerificadaDTO
	{
		public int IdOportunidad { get; set; }
		public string Asesor { get; set; }
		public string Alumno { get; set; }
		public string CentroCosto { get; set; }
		public string CodigoFaseOportunidad { get; set; }
		public string CodigoMatricula { get; set; }
		public int? IdMatriculaCabecera { get; set; }
		public bool Verificado { get; set; }
		public DateTime? UltimaFechaProgramada { get; set; }
		public DateTime FechaCambioIs { get; set; }
	}

	public class OportunidadesVerificadasDTO
	{
		public string Coordinador { get; set; }
		public string Alumno { get; set; }
		public string CentroCosto { get; set; }
		public string FaseOportunidad { get; set; }
		public string CodigoMatricula { get; set; }
	}
}
