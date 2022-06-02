using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FacebookConjuntoAnuncioEstadisticaDiariaBO : BaseBO
    {
        public int IdConjuntoAnuncioFacebook { get; set; }
        public DateTime Fecha { get; set; }
        public int Alcance { get; set; }
        public int Impresiones { get; set; }
        public int? IdMigracion { get; set; }
    }
}
