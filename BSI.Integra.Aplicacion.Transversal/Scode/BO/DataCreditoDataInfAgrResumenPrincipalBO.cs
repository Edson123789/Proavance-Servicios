using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfAgrResumenPrincipalBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public string CreditosVigentes { get; set; }
        public string CreditosCerrados { get; set; }
        public string CreditosActualesNegativos { get; set; }
        public string HistNegUlt12Meses { get; set; }
        public string CuentasAbiertasAhoccb { get; set; }
        public string CuentasCerradasAhoccb { get; set; }
        public string ConsultadasUlt6meses { get; set; }
        public string DesacuerdosAlaFecha { get; set; }
        public string ReclamosVigentes { get; set; }
    }
}
