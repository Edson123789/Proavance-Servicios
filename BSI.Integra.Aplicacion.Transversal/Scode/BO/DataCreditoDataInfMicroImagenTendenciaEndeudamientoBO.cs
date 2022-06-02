using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public string Serie { get; set; }
        public decimal? Valor { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
