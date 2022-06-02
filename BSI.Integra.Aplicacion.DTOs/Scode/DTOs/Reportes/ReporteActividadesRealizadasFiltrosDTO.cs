using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class ReporteActividadesRealizadasFiltrosDTO
    {
        public int? IdEstadoOcurrencia { get; set; }
        public int? IdAlumno { get; set; }
        public int IdAsesor { get; set; }
        public List<int> IdFasesOportunidadOrigen { get; set; }
        public List<int> IdFasesOportunidadDestino { get; set; }
        public int? IdTipoDato { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? EstadoLlamada { get; set; }
        public int? IdTipoCategoriaOrigen { get; set; }
        public int? IdProbabilidadActual { get; set; }
        public int HoraInicio { get; set; }
        public int MinutosInicio { get; set; }
        public int HoraFin { get; set; }
        public int MinutosFin { get; set; }
        public bool EstadoFiltroHora { get; set; }
        public int? EstadoPersonal { get; set; }
    }
}
