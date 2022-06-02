using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class HabilidadSimuladorBO:BaseBO
    {
        public string Nombre { get; set; }
        public int PuntajeMinimo { get; set; }
        public int PuntajeMaximo { get; set; }
    }
}
