using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfMicroAnalisisVectorBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public string NombreSector { get; set; }
        public string CuentaEntidad { get; set; }
        public string CuentaNumeroCuenta { get; set; }
        public string CuentaTipoCuenta { get; set; }
        public string CuentaEstado { get; set; }
        public bool? ContieneDatos { get; set; }
        public DateTime? Fecha { get; set; }
        public string SaldoDeudaTotalMora { get; set; }
    }
}
