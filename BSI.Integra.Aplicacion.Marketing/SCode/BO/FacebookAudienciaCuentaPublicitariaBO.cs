using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FacebookAudienciaCuentaPublicitariaBO : BaseBO
    {
        public int IdFacebookAudiencia { get; set; }
        public int IdFacebookCuentaPublicitaria { get; set; }
        public string Subtipo { get; set; }
        public string Origen { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }


        public FacebookAudienciaCuentaPublicitariaBO()
        {
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
        }
    }
}
