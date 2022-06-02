using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public partial class ControlDocBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdCriterioDoc { get; set; }
        public string IdMigracion { get; set; }
        public bool EstadoDocumento { get; set; }
    }
}
