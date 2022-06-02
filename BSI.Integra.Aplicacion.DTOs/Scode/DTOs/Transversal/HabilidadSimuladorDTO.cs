using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class HabilidadSimuladorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int PuntajeMaximo { get; set; }
        public int PuntajeMinimo { get; set; }
        public string Usuario { get; set; }
    }
}
