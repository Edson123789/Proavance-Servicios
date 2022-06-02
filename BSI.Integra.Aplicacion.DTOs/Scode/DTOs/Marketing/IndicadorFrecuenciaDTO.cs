using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class IndicadorFrecuenciaDTO
    {
        public int Id { get; set; }
        public int IdIndicadorProblema { get; set; }
        public int IdHora { get; set; }
       
        public string Usuario { get; set; }
    }
}
