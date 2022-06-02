using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ModeloPredictivoBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public decimal PeIntercepto { get; set; }
        public int PeEstado { get; set; }
        public Guid? IdMigracion { get; set; }

    }
}
