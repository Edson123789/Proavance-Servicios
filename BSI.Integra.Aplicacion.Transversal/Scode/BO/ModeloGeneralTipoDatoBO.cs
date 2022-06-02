using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ModeloGeneralTipoDatoBO : BaseBO
    {
        public int IdModeloGeneral { get; set; }
        public int IdTipoDato { get; set; }
        public Guid IdMigracion { get; set; }
    }
}
