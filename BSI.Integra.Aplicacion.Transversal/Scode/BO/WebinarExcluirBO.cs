using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WebinarExcluirBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int? IdMigracion { get; set; }
    }
}
