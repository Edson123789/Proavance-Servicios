using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfAgrComposicionPortafolioBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public string Tipo { get; set; }
        public string CalidadDeudor { get; set; }
        public decimal? Porcentaje { get; set; }
        public int? Cantidad { get; set; }
        public string EstadoCodigo { get; set; }
        public int? EstadoCantidad { get; set; }
    }
}
