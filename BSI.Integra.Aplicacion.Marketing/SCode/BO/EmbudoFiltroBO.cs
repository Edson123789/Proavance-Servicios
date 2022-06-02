using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class EmbudoFiltroBO : BaseBO
    {
        public int? IdFiltroSegmento { get; set; }
        public string Nivel { get; set; }
        public string SubNivel { get; set; }
        public string SubNivelGrupos { get; set; }
        public string SubNivelFases { get; set; }
        public Guid? IdMigracion { get; set; }

    }
}
