using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroSegmentoValorTipoDTO
    {
        public int Id { get; set; }
        public int Valor { get; set; }
        public int? IdCategoriaObjetoFiltro { get; set; }
    }

    public class ListaFiltroSegmentoValorTipoDTO
    {
        public List<FiltroSegmentoValorTipoDTO> ListaAreas { get; set; } = new List<FiltroSegmentoValorTipoDTO>();
        public List<FiltroSegmentoValorTipoDTO> ListaSubAreas { get; set; } = new List<FiltroSegmentoValorTipoDTO>();
        public List<FiltroSegmentoValorTipoDTO> ListaProgramas { get; set; } = new List<FiltroSegmentoValorTipoDTO>();
    }
}
