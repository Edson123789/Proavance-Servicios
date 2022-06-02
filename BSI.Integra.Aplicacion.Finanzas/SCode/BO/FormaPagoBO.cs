using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public partial class FormaPagoBO : BaseBO
    {
        public string Descripcion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
