using System;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class MailChimpListaIdBO : BaseBO
    {
        public int IdCampaniaMailingLista { get; set; }
        public string AsuntoLista { get; set; }
        public string IdCampaniaMailchimp { get; set; }
        public string IdListaMailchimp { get; set; }
        public Guid? IdMigracion { get; set; }


        public MailChimpListaIdBO()
        {
        }
    }

}
