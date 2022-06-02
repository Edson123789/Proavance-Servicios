using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ChatDetalleIntegraDTO
	{
		public int? IdInteraccionChatIntegra { get; set; }
		public string NombreRemitente { get; set; }
		public string IdRemitente { get; set; }
		public string Mensaje { get; set; }
		public DateTime Fecha { get; set; }
	}
	public class ChatDetalleIntegraArchivoDTO
	{
		public IFormFile File { get; set; }
		public string Usuario { get; set; }
	}
}
