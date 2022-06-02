using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PortalEmpleoPaisBO : BaseBO
    {
        public int? IdPortalEmpleo { get; set; }
        public int? IdPais { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
