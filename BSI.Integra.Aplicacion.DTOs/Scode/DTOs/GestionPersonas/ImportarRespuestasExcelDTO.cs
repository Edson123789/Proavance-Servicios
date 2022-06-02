using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ImportarRespuestasExcelDTO
	{
		public IFormFile File { get; set; }
		public int IdPregunta { get; set; }
		public string Usuario { get; set; }
	}
}
