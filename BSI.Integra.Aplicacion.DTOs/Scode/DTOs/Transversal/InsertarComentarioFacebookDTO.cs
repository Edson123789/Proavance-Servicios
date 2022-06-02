using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class InsertarComentarioFacebookDTO
	{
		public string PSID { get; set; }
		public string Nombres { get; set; }
		public string Mensaje { get; set; }
		public string IdComentario { get; set; }
		public string IdPost { get; set; }
		public string IdParent { get; set; }
		public string Usuario { get; set; }
		public int? Asesor { get; set; }
    }
}
