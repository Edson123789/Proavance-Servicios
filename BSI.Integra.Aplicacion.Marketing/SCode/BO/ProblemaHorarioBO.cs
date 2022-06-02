using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public partial class ProblemaHorarioBO : BaseBO
    {
        public int Id { get; set; }
        public int IdProblema { get; set; }
        public int IdHora { get; set; }
        
    }
}
