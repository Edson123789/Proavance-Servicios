using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class InteraccionChatIntegraSignalRDTO
	{
		public int Id { get; set; }
		public int IdAlumno { get; set; }
		public string Pais { get; set; }
		public string Ciudad { get; set; }
		public int? NroMensajes { get; set; }
		public int? NroPalabrasVisitor { get; set; }
		public DateTime? FechaFin { get; set; }
        public Guid? IdChatSession { get; set; }
    }
}
