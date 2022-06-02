using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoHistorialParticipacionDocenteV2DTO
    {
        public string PGeneral { get; set; }
        public int IdPEspecifico { get; set; }
        public string PEspecifico { get; set; }
        public string Modalidad { get; set; }
        public string Ciudad { get; set; }
        public int IdExpositor { get; set; }
        public string EstadoParticipacion { get; set; }
        public int? IdClasificacionPersona { get; set; }
        
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
    }
}
