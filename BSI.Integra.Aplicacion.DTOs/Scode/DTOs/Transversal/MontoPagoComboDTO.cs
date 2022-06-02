using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MontoPagoComboDTO
    {
        public List<SubAreaCapacitacionAutoselectDTO> SubAreas { get; set; }
        public List<FiltroDTO> Areas { get; set; }
        public List<TipoDescuentoFiltroDTO> Descuento { get; set; }
        public List<PaisFiltroComboDTO> Paises { get; set; }
        public List<MonedaPaisFiltroDTO> Monedas { get; set; }
        public List<CategoriaProgramaFiltroPorNombreDTO> CategoriasProgramas { get; set; }
        public List<TipoPagoFiltroDTO> TipoPagos { get; set; }
        public List<SuscripcionProgramaFiltroDTO> Suscripciones { get; set; }
        public List<PlataformaPagoFiltroDTO> PlataformaPagos { get; set; }

    }
}
