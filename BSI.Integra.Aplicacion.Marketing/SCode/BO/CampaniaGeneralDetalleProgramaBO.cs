using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class CampaniaGeneralDetalleProgramaBO : BaseBO
    {
        public int IdCampaniaGeneralDetalle { get; set; }
        public int? IdPGeneral { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public int Orden { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
