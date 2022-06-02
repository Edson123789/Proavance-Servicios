using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FacebookTipoReaccionBO : BaseBO
    {
        public string Nombre { get; set; }
        public Guid? IdMigracion { get; set; }

    }
}
