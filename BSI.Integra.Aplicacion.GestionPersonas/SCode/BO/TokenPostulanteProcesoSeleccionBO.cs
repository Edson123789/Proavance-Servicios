using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	public class TokenPostulanteProcesoSeleccionBO : BaseBO
	{
		public int IdPostulanteProcesoSeleccion { get; set; }
		public string Token { get; set; }
		public string TokenHash { get; set; }
		public Guid GuidAccess { get; set; }
		public bool Activo { get; set; }
		public int? IdMigracion { get; set; }
		public DateTime? FechaEnvioAccesos { get; set; }

		public string GenerarClave(int longitud)
		{
			const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
			StringBuilder res = new StringBuilder();
			Random rnd = new Random();
			while (0 < longitud--)
			{
				res.Append(valid[rnd.Next(valid.Length)]);
			}

			return res.ToString();
		}
	}

}
