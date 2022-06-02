using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CalculoOportunidadRn2BO : BaseBO
    {
        public int IdOportunidadRn2 { get; set; }
        public bool TieneOportunidadAbierta { get; set; }
        public int? IdOportunidadAbierta { get; set; }
        public bool TieneOportunidadCerradaSesentaDias { get; set; }
        public int? IdOportunidadCerradaSesentaDias { get; set; }
        public bool TieneOportunidadCerradaPosterior { get; set; }
        public int IdOportunidadCerradaPosterior { get; set; }
        public Guid? IdMigracion { get; set; }

        public CalculoOportunidadRn2BO()
        {

        }
    }
}
