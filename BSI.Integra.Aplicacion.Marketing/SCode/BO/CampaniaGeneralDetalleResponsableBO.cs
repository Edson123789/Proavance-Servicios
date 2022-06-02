using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class CampaniaGeneralDetalleResponsableBO : BaseBO
    {
        public int IdCampaniaGeneralDetalle { get; set; }
        public int IdPersonal { get; set; }
        public int Dia1 { get; set; }
        public int Dia2 { get; set; }
        public int Dia3 { get; set; }
        public int Dia4 { get; set; }
        public int Dia5 { get; set; }
        public int Total { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
