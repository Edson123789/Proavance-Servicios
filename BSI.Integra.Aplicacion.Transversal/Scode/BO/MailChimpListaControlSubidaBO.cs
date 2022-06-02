using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MailChimpListaControlSubidaBO : BaseBO
    {
        public int IdPrioridadMailLista { get; set; }
        public int Grupo { get; set; }
        public DateTime FechaInicioProceso { get; set; }
        public DateTime FechaFinProceso { get; set; }
        public bool EnProceso { get; set; }
        public int? IdMigracion { get; set; }
    }
}
