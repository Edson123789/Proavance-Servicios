using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PersonalDatosPorApellidoDTO
	{
		public int Id { get; set; }
		public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }

    }
}
