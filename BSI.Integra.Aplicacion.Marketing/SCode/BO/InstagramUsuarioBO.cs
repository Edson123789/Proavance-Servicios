using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class InstagramUsuarioBO : BaseBO
    {
        public string Usuario { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
