using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralPerfilEscalaProbabilidadBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public double ProbabilidadInicial { get; set; }
        public double ProbabilidadActual { get; set; }
        public int Orden { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
