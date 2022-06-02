using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class ConfigurarWebinarBO : BaseBO
    {        
        public int IdPespecifico { get; set; }
        public string Modalidad { get; set; }
        public string Codigo { get; set; }
        public int IdOperadorComparacionAvance { get; set; }
        public int ValorAvance { get; set; }
        public int? ValorAvanceOpc { get; set; }
        public int IdOperadorComparacionPromedio { get; set; }
        public int ValorPromedio { get; set; }
        public int? ValorPromedioOpc { get; set; }        
        public int? IdMigracion { get; set; }
        public int IdPespecificoPadre { get; set; }
    }
}
