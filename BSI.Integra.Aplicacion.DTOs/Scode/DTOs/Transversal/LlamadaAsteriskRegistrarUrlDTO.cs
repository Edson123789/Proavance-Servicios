using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class LlamadaAsteriskRegistrarUrlDTO
    {
        public int Id { get; set; }
        public int IdProveedorNube { get; set; }
        public int NroBytes { get; set; }
        public string Url { get; set; }
        public string Usuario { get; set; }
    }
}
