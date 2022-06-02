using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
	public class InteraccionChatWhatsAppBO : BaseBO
	{
		public string Numero { get; set; }
		public int TotalMensajesChat { get; set; }
		public int TotalMensajesVisitante { get; set; }
		public decimal PromedioRespuestaUsuario { get; set; }
		public int? IdAlumno { get; set; }
		public int IdPersonal { get; set; }
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }
		public bool TieneOportunidadSeguimiento { get; set; }
		public bool TieneOportunidadNueva { get; set; }
		public bool EsAtendido { get; set; }
		public int? IdPais { get; set; }
		public int? IdMigracion { get; set; }
	}
}
