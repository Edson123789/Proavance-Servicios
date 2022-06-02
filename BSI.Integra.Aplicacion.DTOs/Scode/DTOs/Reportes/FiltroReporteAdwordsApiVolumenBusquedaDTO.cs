using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroReporteAdwordsApiVolumenBusquedaDTO
    {
        //public string Palabras { get; set; }
        public List<FiltroReporteGrupoPalabrasTipoPalabra> ListaPalabras { get; set; }
        public int TipoPalabra { get; set; }
        public int[] Paises { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Usuario { get; set; }
        public int IdIdioma { get; set; }
    }

    public class FiltroReporteGrupoPalabrasTipoPalabra
    {
        public int TipoTexto { get; set; }
        public string CadenaTexto { get; set; }
    }
}
