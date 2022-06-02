using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WhatsAppPlantillaPorOcurrenciaActividadBO : BaseBO
    {
        public int IdOcurrenciaActividad { get; set; }
        public int IdPlantilla { get; set; }
        public int NumeroDiasSinContacto { get; set; }
        public int? IdMigracion { get; set; }
    }
}
