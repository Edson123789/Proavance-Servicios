using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ProgramaEspecificoIdDTO
	{
		public int Id { get; set; }
	}

    public class TokenWebexDTO
    {
        public int Id { get; set; }
        public string Cuenta { get; set; }
        public string Token { get; set; }
    }
    public class SesionesWebexDTO
    {
        public int Id { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public int IdCuentaWebex { get; set; }
    }
}
