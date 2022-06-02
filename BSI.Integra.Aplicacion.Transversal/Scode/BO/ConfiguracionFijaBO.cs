using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ConfiguracionFijaBO: BaseBO
    {
        public string Codigo { get; set; }
        public string NombreTabla { get; set; }
        public int IdTabla { get; set; }
        public string NombreColumna { get; set; }
        public string TipoDato { get; set; }
        public string Valor { get; set; }
    }
}
