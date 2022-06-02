using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConfigurarWebinarDTO
    {
        public int Id { get; set; }
        public int IdPEspecifico { get; set; }
        public string Modalidad { get; set; }
        public string Codigo { get; set; }
        public int IdOperadorComparacionAvance { get; set; }
        public int ValorAvance { get; set; }
        public int? ValorAvanceOpc { get; set; }
        public int IdOperadorComparacionPromedio { get; set; }
        public int ValorPromedio { get; set; }
        public int? ValorPromedioOpc { get; set; }
        public int IdPespecificoPadre { get; set; }
        public string Usuario { get; set; }
        
        

    }
}
