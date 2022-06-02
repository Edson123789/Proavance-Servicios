using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class FacebookUsuarioOportunidadDTO
	{
		public int IdPersonal { get; set; }
		public string PSID { get; set; }
		public int IdOportunidad { get; set; } //IdOportunidad
		public string Usuario { get; set; }
		public int? IdMessengerChatMasivo { get; set; }
		public string Email { get; set; }
		public int IdMessengerUsuario { get; set; }
		public bool Asociar { get; set; }
	}
}
