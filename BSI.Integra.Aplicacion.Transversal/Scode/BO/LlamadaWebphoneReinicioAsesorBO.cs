using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class LlamadaWebphoneReinicioAsesorBO : BaseBO
    {
        public int IdPersonal { get; set; }
        public bool AplicaReinicio { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
