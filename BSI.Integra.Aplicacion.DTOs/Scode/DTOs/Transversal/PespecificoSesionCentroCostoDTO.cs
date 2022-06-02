using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PespecificoSesionCentroCostoDTO
    {
        public int id { get; set; }
        public int? IdPespecifico { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public decimal? DuracionTotal { get; set; }
        public string Comentario { get; set; }
        public bool allDay { get; set; }
        public bool editable { get; set; }
        public string NombreExpositor { get; set; }
        public string NombreAmbiente { get; set; }
    }
}
