using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class ListaPlantillaBO :BaseBO
    {
        public string Nombre { get; set; }
        public string Disenho { get; set; }
        public int? TipoDispositivo { get; set; }
        public int? TipoPlantilla { get; set; }

        public Guid? IdMigracion { get; set; }
    }
}
