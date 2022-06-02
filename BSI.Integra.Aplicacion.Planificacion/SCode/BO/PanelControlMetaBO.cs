using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class PanelControlMetaBO : BaseBO
    {
        public string Nombre { get; set; }
        public int Meta { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
