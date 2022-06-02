using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class DatosProgramaDTO
	{
		public string LinkPrograma { get; set; }
        public string NombrePrograma { get; set; }
        public string Ubicacion { get; set; }
		public string LinkPrevia { get; set; }
        public string NombreProgramaPrevio { get; set; }
		public string IdInteraccionChatIntegra { get; set; }
	}
}
