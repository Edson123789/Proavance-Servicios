using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CondicionOcurrenciaLlamadaBO : BaseBO
    {
        public string Nombre { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
