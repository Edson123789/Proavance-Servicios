using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ModeloPredictivoTipoDatoBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public int IdTipoDato { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
