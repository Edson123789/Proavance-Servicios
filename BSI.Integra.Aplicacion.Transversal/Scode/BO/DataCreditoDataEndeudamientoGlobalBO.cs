using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataEndeudamientoGlobalBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public string Calificacion { get; set; }
        public string Fuente { get; set; }
        public string SaldoPendiente { get; set; }
        public string TipoCredito { get; set; }
        public string Moneda { get; set; }
        public string NumeroCreditos { get; set; }
        public string Independiente { get; set; }
        public DateTime? FechaReporte { get; set; }
        public string EntidadNombre { get; set; }
        public string EntidadNit { get; set; }
        public string EntidadSector { get; set; }
        public string GarantiaTipo { get; set; }
        public string GarantiaValor { get; set; }
        public string Llave { get; set; }
    }
}
