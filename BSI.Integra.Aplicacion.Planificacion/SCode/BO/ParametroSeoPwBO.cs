using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class ParametroSeoPwBO : BaseBO
    {
        public string Nombre { get; set; }
        public int NumeroCaracteres { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
