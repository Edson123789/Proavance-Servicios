using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ConfirmacionWebinarBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPespecificoSesion { get; set; }
        public bool Confirmo { get; set; }
        public bool Asistio { get; set; }
    }
}
