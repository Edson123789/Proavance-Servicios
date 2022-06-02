using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FlujoPorPespecificoRegistrarDTO
    {
        public int? IdFlujoPorPEspecifico { get; set; }
        public int IdPespecifico { get; set; }
        public int IdFlujoActividad { get; set; }
        public int? IdFlujoOcurrencia { get; set; }
        public int IdClasificacionPersona { get; set; }
        public DateTime? FechaEjecucion { get; set; }
        public DateTime FechaSeguimiento { get; set; }
        public TimeSpan HoraSeguimiento { get; set; }

        public string NombreUsuario { get; set; }
    }
}
