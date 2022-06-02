using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class PEspecificoSilaboBO : BaseBO
    {
        public int IdPespecifico { get; set; }
        public string ObjetivoAprendizaje { get; set; }
        public string PautaComplementaria { get; set; }
        public string PublicoObjetivo { get; set; }
        public string Material { get; set; }
        public string Bibliografia { get; set; }
        public bool? Aprobado { get; set; }
    }
}
