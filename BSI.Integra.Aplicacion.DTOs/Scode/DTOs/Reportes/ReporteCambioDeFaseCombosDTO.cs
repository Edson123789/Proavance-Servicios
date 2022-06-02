using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteCambioDeFaseCombosDTO
    {
        public List<FiltroDTO> CentroCostos { get; set; }
        public List<AsesorFiltroDTO> Asesores { get; set; }
        public List<FiltroDTO> ListaTipoDato { get; set; }
        public List<CategoriaOrigenFiltroDTO> ListaCategoriaOrigen { get; set; }
    }
    public class ReporteCambioDeFaseCombosGeneralDTO
    {
        public List<FiltroDTO> CentroCostos { get; set; }
        public List<AsesorNombreFiltroDTO> Asesores { get; set; }
        public List<FiltroDTO> ListaTipoDato { get; set; }
        public List<CategoriaOrigenFiltroDTO> ListaCategoriaOrigen { get; set; }
    }
}
