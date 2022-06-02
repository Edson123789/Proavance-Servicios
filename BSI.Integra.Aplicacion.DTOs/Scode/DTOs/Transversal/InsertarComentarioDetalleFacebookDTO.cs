using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class InsertarComentarioDetalleFacebookDTO
	{
		public string Message { get; set; }
		public string IdCommentFacebook { get; set; }
		public string IdPostFacebook { get; set; }
		public int? IdPersonal { get; set; }
		public bool? Tipo { get; set; }
		public string IdUsuarioFacebook { get; set; }
	}
}
