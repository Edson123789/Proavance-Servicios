using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class FiltroSegmentoDetalleBO : BaseBO
    {
        public int Valor { get; set; }
        public int IdFiltroSegmento { get; set; }
        public int IdOperadorComparacion { get; set; }
        public int IdTiempoFrecuencia { get; set; }
        public int CantidadTiempoFrecuencia { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }
        public int? IdMigracion { get; set; }
    }
}
