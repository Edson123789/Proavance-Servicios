using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class TipoPagoCategoriaBO : BaseBO
    {
        public int IdCategoriaPrograma { get; set; }
        public int IdTipoPago { get; set; }
        public int IdModoPago { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
