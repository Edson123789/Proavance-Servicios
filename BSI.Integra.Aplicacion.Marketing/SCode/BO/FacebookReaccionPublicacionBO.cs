using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FacebookReaccionPublicacionBO : BaseBO
    {
        public int IdFacebookUsuario { get; set; }
        public int IdFacebookPost { get; set; }
        public int IdFacebookTipoReaccion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
