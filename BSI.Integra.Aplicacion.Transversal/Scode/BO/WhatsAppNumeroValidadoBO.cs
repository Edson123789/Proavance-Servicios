using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WhatsAppNumeroValidadoBO : BaseBO
    {
        public int IdAlumno { get; set; }
        public string NumeroCelular { get; set; }
        public int IdPais { get; set; }
        public int? IdMigracion { get; set; }
    }
}
