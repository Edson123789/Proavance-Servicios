using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class CuentaCorrienteBO : BaseBO
    {
        public string NumeroCuenta { get; set; }
        public int? IdCiudad { get; set; }
        public string Sucursal { get; set; }
        public int IdMoneda { get; set; }
        public string Cuenta { get; set; }
        public int IdBanco { get; set; }
        public int? IdMigracion { get; set; }
    }
}
