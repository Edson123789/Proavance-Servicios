using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class AsesorChatDetallePaisDTO
	{
		public int Id { get; set; }
		public int? IdAsesorChat { get; set; }
		public int IdPais { get; set; }
		public int IdPgeneral { get; set; }
        public string NombreAsesor { get; set; }
    }

	public class IdAlumnoSoporteChatDTO
	{
		public int IdAlumno { get; set; }
	}

	public class IdPersonalSoporteChatDTO
	{
		public int IdPersonal { get; set; }
		public bool PorDefecto { get; set; }
		public int IdPGeneral { get; set; }
	}
}
