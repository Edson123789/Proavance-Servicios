using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class ReporteAnalyticsFiltroDetalleBO : BaseBO
    {
        public string Texto { get; set; }
        public bool Excluir { get; set; }
        public int IdReporteAnalyticsFiltro { get; set; }
        public int IdOperadorComparacion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
