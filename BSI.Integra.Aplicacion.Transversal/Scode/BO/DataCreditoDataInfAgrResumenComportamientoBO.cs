using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfAgrResumenComportamientoBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public DateTime? Fecha { get; set; }
        public string Comportamiento { get; set; }
        public int? Cantidad { get; set; }
    }
}
