using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ModeloPredictivoEscalaProbabilidadBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public decimal ProbabilidaIinicial { get; set; }
        public decimal ProbabilidadActual { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
