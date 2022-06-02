using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaEspecificoSesionDetalleDTO
    {
        public int IdPEspecifico { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public string NombrePEspecifico { get; set; }
        public DateTime FechaSesion { get; set; }
        public string HorarioSesion { get; set; }
        public int DuracionSesionHoras { get; set; }
    }
}
