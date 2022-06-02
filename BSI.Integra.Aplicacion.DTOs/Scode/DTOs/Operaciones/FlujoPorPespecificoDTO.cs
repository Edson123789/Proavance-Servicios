using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FlujoPorPespecificoDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int IdFlujoOcurrencia { get; set; }
        public int IdClasificacionPersona { get; set; }
        public DateTime? FechaEjecucion { get; set; }
        public DateTime FechaSeguimiento { get; set; }
        public TimeSpan HoraSeguimiento { get; set; }
    }
}
