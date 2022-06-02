using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ModeloGeneralDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal PeIntercepto { get; set; }
        public int PeEstado { get; set; }
        public int PeVersion { get; set; }
        public int? IdPadre { get; set; }
        public string Usuario { get; set; }
    }
}
