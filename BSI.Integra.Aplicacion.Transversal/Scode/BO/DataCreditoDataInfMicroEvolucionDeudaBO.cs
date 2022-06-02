using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataInfMicroEvolucionDeudaBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public string CodigoSector { get; set; }
        public string NombreSector { get; set; }
        public string TipoCuenta { get; set; }
        public string Trimestre { get; set; }
        public string Num { get; set; }
        public decimal? CupoInicial { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? SaldoMora { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? PorcentajeDeuda { get; set; }
        public string CodigoMenorCalificacion { get; set; }
        public string TextoMenorCalificacion { get; set; }
    }
}
