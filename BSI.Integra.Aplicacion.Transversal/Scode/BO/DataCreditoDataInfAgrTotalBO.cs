using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfAgrTotalBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public string TipoMapeo { get; set; }
        public string CodigoTipo { get; set; }
        public string Tipo { get; set; }
        public string CalidadDeudor { get; set; }
        public string Participacion { get; set; }
        public string Cupo { get; set; }
        public string Saldo { get; set; }
        public string SaldoMora { get; set; }
        public string Cuota { get; set; }
    }
}
