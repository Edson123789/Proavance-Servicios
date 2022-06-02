using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	public class PostulanteEquipoComputoBO : BaseBO
	{
		public int IdPostulante { get; set; }
		public string TipoEquipo { get; set; }
		public string MemoriaRam { get; set; }
		public string SistemaOperativo { get; set; }
		public string Procesador { get; set; }
		public bool Mouse { get; set; }
		public bool Auricular { get; set; }
		public bool Camara { get; set; }
		public int? IdMigracion { get; set; }
	}
}
