using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RegistroSesionDTO
    {
        public int Id { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
        public decimal? Duracion { get; set; }
        public int? IdAmbiente { get; set; }
        public int? IdExpositor { get; set; }
    }
}
