using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class NotaBO : BaseBO
    {
        public int Id { get; set; }
        public int IdEvaluacion { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public decimal Nota { get; set; }
    }
}
