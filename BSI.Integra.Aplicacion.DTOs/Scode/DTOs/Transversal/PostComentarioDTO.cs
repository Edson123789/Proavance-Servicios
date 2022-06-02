using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
	public class PostComentarioDTO
	{
		public int Id { get; set; }
		public string IdUsuario { get; set; }
		public string Nombres { get; set; }
		public int? IdPersonal { get; set; }
		public bool? TieneRespuesta { get; set; }
		public string Mensaje { get; set; }
		public string parent_id { get; set; }
		public string IdPostFacebook { get; set; }
		public string IdCommentFacebook { get; set; }
		public bool? ComentarioSinRpta { get; set; }
	}
}
