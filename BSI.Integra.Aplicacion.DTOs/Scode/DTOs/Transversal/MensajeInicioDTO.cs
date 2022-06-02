using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
	public class MensajeInicioDTO
	{
		public bool ComentarioSinRpta { get; set; }
		public string Id { get; set; }
		public string comment_id { get; set; }
		public string parent_id { get; set; }
		public string post_id { get; set; }
		public string usuario_id { get; set; }
		public string comentario { get; set; }
		public string user { get; set; }
	}
}
