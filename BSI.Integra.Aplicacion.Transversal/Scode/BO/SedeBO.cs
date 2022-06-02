using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class SedeBO: BaseBO
    {
        public int IdPais { get; set; }
        public string Codigo { get; set; }
        public int? IdCiudad { get; set; }
    }
}
