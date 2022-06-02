using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PgeneralConfiguracionPlantillaBO : BaseBO
    {
        public int IdPlantillaFrontal { get; set; }
        public int? IdPlantillaPosterior { get; set; }
        public int IdPgeneral { get; set; }
        public DateTime? UltimaFechaRemplazarCertificado { get; set; }
    }
}
