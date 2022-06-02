using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class SemaforoFinancieroVariableBO: BaseBO
    {
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
        public bool? AplicaUnidad { get; set; }
    }
}
