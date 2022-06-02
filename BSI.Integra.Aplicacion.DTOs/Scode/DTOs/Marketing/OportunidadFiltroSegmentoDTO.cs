using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadFiltroSegmentoDTO
    {
        public int IdFiltroSegmento { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdTipoDato { get; set; }
        public int IdOrigen { get; set; }
        public int IdFaseOportunidad { get; set; }
        public List<int> ListadoIdsAlumnos { get; set;}
        public string NombreUsuario { get; set; }
    }
}
