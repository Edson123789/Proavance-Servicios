using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class ModeloPredictivoInterceptoDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public decimal PeIntercepto { get; set; }
        public int PeEstado { get; set; }

    }
}
