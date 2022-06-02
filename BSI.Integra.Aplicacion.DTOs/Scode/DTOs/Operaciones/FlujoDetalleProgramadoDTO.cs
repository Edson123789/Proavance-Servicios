using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FlujoDetalleProgramadoDTO
    {
        public int IdFlujoPorPEspecifico { get; set; }
        public int IdClasificacionPersona { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdFlujoActividad { get; set; }
        public int? IdFlujoOcurrencia { get; set; }
        public DateTime? FechaEjecucion { get; set; }
        public DateTime? FechaSeguimiento { get; set; }
        public string NombrePEspecifico { get; set; }
        public string NombreActividad { get; set; }
        public string NombreOcurrencia { get; set; }
    }
}
