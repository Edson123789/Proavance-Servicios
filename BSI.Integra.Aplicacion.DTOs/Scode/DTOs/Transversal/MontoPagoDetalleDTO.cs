using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MontoPagoDetalleDTO
    {
        public List<SuscripcionProgramaFiltroDTO> Suscripciones { get; set; }
        public List<TipoPagoCategoriaFiltroDTO> TipoCategoria { get; set; }
        public List<MontoPagoPanelDTO> MontoPagos { get; set; }
    }
}
