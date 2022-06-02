using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoMontoPagoDTO
    {
        public MontoPagoPanelDTO MontoPago { get; set; }
        public List<int> PlataformasPagos { get; set; }
        public List<int> SuscripcionesPagos { get; set; }
        public string Usuario { get; set; }
        public int IdPgeneral { get; set; }
    }
}
