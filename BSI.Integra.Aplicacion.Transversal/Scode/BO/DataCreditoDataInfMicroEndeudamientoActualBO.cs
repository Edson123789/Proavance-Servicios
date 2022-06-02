using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfMicroEndeudamientoActualBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public string SectorCodigoSector { get; set; }
        public string TipoCuenta { get; set; }
        public string TipoUsuario { get; set; }
        public string EstadoActual { get; set; }
        public string Calificacion { get; set; }
        public decimal? ValorInicial { get; set; }
        public decimal? SaldoActual { get; set; }
        public decimal? SaldoMora { get; set; }
        public decimal? CuotaMes { get; set; }
        public bool? ComportamientoNegativo { get; set; }
        public decimal? TotalDeudaCarteras { get; set; }
    }
}
