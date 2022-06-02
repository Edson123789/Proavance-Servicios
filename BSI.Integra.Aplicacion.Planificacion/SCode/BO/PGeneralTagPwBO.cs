using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class PGeneralTagPwBO : BaseBO
    {
        public int? IdPgeneral { get; set; }
        public int IdTagPW { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
