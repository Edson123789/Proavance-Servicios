using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroSesionEspecialDTO
    {
        public int PEspecificoPadreId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Duracion { get; set; }
		public int Grupo { get; set; }
    }
}
