using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FacebookUsuarioBO : BaseBO
    {
        public string FacebookId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
