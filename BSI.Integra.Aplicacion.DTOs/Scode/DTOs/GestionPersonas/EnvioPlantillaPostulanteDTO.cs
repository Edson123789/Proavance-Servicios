using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class EnvioPlantillaPostulanteDTO
	{
		public List<int> ListaIdPostulanteProcesoSeleccion { get; set; }
		public int IdPlantilla { get; set; }
		public DateTime? Fecha { get; set; }
		public string Usuario { get; set; }
	}
}
