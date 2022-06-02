using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class SemaforoFinancieroBO: BaseBO
    {
        public int IdPais { get; set; }
        public bool Activo { get; set; }
        public int? IdMigracion { get; set; }
    }
}
