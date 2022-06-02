using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class FacebookUsuarioOportunidadBO : BaseBO
    {
        public string PSID { get; set; }
        public int IdOportunidad { get; set; }
        public int IdPersonal { get; set; }
        public int? IdMessengerChat { get; set; }
        public Guid? IdMigracion { get; set; }
        public FacebookUsuarioOportunidadBO()
        {

        }
    }
}
