using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class EnvioCorreoValidacionAccesoDTO
	{
		public string Usuario { get; set; }
		public string EmailRemitente { get; set; }
		public string PersonalRemitente { get; set; }
		public string PasswordCorreo { get; set; }
	}
}
