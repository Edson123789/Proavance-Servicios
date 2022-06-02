using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class RecuperacionSesionBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPespecificoSesion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
