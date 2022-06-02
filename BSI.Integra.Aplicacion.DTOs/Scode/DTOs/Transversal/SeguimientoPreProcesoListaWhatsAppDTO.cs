using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SeguimientoPreProcesoListaWhatsAppDTO
    {
    }

    public class RegistroSeguimientoPreProcesoListaWhatsAppDTO
    {
        public int Id { get; set; }
        public int IdEstadoSeguimientoPreProcesoListaWhatsApp { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
        public int IdConjuntoLista { get; set; }
    }
}
