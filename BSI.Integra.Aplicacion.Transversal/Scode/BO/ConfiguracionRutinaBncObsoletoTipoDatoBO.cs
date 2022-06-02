using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ConfiguracionRutinaBncObsoletoTipoDatoBO : BaseBO
    {
        public int Id { get; set; }
        public int IdConfiguracionRutinaBncObsoleto { get; set; }
        public int IdTipoDato { get; set; }
    }
}
