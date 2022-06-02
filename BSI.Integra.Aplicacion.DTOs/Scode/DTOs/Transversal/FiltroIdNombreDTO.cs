using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroIdNombreDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

    }

	public class FiltroIdNombrePKDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int PK { get; set; }

	}
}
