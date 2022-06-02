using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProveedorCuentaBancoBO : BaseBO
    {
        public int IdProveedor { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public int IdTipoCuentaBanco { get; set; }
        public string NroCuenta { get; set; }
        public string CuentaInterbancaria { get; set; }
        public int IdMoneda { get; set; }
        public Guid IdMigracion { get; set; }
    }
}
