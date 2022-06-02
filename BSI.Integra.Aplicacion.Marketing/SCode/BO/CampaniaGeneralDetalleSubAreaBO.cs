using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class CampaniaGeneralDetalleSubAreaBO : BaseBO
    {
        public int IdSubAreaCapacitacion { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
