using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class AscensoAreaFormacionBO : BaseBO
    {
        public int Id { get; set; }
        public int IdAscenso { get; set; }
        public int IdAreaFormacion { get; set; }

    }
}
