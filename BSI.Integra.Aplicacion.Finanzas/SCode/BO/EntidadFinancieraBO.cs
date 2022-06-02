using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class EntidadFinancieraBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdMoneda { get; set; }
        public string CuentaCte { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdMigracion { get; set; }
    }
}
