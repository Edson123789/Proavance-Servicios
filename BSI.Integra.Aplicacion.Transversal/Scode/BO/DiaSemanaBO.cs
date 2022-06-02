using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DiaSemanaBO:BaseBO
    {
        public string Nombre { get; set; }
        public int OrdenInicio0 { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
