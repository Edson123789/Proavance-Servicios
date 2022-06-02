using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosPespecificoFrecuenciaDetalleDTO
    {
        public int Id { get; set; }
        public int? IdPespecificoFrecuencia { get; set; }
        public byte DiaSemana { get; set; }
        public TimeSpan HoraDia { get; set; }
        public decimal Duracion { get; set; }
		public int IdPEspecifico { get; set; }

	}
}
