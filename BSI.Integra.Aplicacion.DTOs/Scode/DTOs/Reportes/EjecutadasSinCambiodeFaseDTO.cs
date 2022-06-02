using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EjecutadasSinCambiodeFaseDTO
    {
        public int IdFaseOrigen { get; set; }
        public string FaseOrigen { get; set; }
        public int Orden { get; set; }
        public int? DiaActual { get; set; }
        public int Uno { get; set; }
        public int Dos { get; set; }
        public int Tres { get; set; }
        public int Cuatro { get; set; }
        public int MasDeCuatro { get; set; }
        public decimal TiempoTotal { get; set; }
        public decimal? DuracionContesto { get; set; }
        public decimal? DuracionLlamadaultimaActividad { get; set; }
    }
}
