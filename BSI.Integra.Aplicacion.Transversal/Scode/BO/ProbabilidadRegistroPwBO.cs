using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProbabilidadRegistroPwBO : BaseBO
    {
        public string Nombre { get; set; }
        public Guid IdCodigo { get; set; }
        public int Codigo { get; set; }

        public ProbabilidadRegistroPwBO()
        {

        }
    }
}
