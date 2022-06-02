using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class PlanContableBO : BaseBO
    {
        public long Cuenta { get; set; }
        public string Descripcion { get; set; }
        public int Padre { get; set; }
        public bool? Univel { get; set; }
        public string Cbal { get; set; }
        public string Debe { get; set; }
        public string Haber { get; set; }
        public int? IdPlanContableTipoCuenta { get; set; }
        public string Analisis { get; set; }
        public string CentroCosto { get; set; }        
        public int? IdFurTipoSolicitud { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
