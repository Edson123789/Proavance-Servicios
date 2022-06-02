using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoFurRegistrarPagosDTO
    {
        public List<int> FurSeleccionados { get; set; }
        public PagoMasivoDTO FurPago { get; set; }
        public string Usuario { get; set; }
        public int IdComprobante { get; set; }

    }
}
