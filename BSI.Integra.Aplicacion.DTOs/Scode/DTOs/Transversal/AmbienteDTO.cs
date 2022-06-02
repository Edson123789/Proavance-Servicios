using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AmbienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdLocacion { get; set; }
        public int? IdTipoAmbiente { get; set; }
        public int? Capacidad { get; set; }
        public bool? Virtual { get; set; }
    }
}
