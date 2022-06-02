using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class FlujoActividadBO : BaseBO
    {
        public int IdFlujoFase { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
    }
}
