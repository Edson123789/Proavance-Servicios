using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PagoCuotaCronogramaDTO
    {
        public string CodigoMatricula { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal MontoBase { get; set; }
        public decimal Mora { get; set; }
        public decimal Monto { get; set; }
        public string MonedaBase { get; set; }
        public int Moneda { get; set; }
        public decimal TipoCambio { get; set; }
        public string RUC { get; set; }
        public int FormaPago { get; set; }
        public int Documento { get; set; }
        public string NroDocumento { get; set; }
        public Nullable<int> NroCuenta { get; set; }
        public string NroCheque { get; set; }
        public DateTime Fecha { get; set; }
        public string NroDeposito { get; set; }
        public string usuario { get; set; }

    }
}
