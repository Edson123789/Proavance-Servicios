using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class SubAreaCampaniaMailingDetalleBO: BaseBO
    {
        public int IdSubAreaCapacitacion { get; set; }
        public int IdCampaniaMailingDetalle { get; set; }
        public int? IdMigracion { get; set; }
    }
}
