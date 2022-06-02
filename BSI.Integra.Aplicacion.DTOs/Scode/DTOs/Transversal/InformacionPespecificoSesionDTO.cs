using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class InformacionPespecificoSesionDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public decimal Duracion { get; set; }
        public int? IdExpositor { get; set; }
        public string Comentario { get; set; }
        public bool SesionAutoGenerada { get; set; }
        public int? IdAmbiente { get; set; }
        public bool? Predeterminado { get; set; }
		public string Usuario { get; set; }
		public int Grupo { get; set; }
    }
}
