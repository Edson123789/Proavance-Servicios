using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class GestionPersonasPlantillaWhatsAppDTO
	{
		public int IdPlantilla { get; set; }
		public int IdPersonal { get; set; }
		public int IdPostulante { get; set; }
		public string Usuario { get; set; }
		public DateTime? Fecha { get; set; }
	}
}
