using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PespecificoSesionCompuestoDatosDTO
    {
        public int Id { get; set; }
        public int? IdPespecifico { get; set; }
        public int? PEspecificoHijoId { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public decimal? Duracion { get; set; }
        public int? IdExpositor { get; set; }
        public string Comentario { get; set; }
        public bool? SesionAutoGenerada { get; set; }
        public int? IdAmbiente { get; set; }
        public bool? Predeterminado { get; set; }
        public bool? EsSesionInicial { get; set; }
    }
}
