using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RucSerieNumeroComprobanteDTO
    {
        public int Id { get; set; }
        public string Comprobante { get; set; }
        public decimal MontoNeto { get; set; }
        public int IdMoneda { get; set; }
        public int? IdTipoImpuesto { get; set; }
        public int? IdRetencion { get; set; }
        public int? IdDetraccion { get; set; }
        public int? IdPais { get; set; }
    }
}
