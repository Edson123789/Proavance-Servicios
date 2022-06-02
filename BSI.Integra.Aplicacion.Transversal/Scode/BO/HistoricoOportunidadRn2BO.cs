using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class HistoricoOportunidadRn2BO : BaseBO
    {
        public int IdOportunidadRn2 { get; set; }
        public int IdAlumno { get; set; }
        public string EstadoValidacionRn2 { get; set; }
        public int IdFaseOportunidadActual { get; set; }
        public DateTime? FechaCreacionActual { get; set; }
        public DateTime? FechaProgramacionActual { get; set; }
        public DateTime? FechaProgramacionNueva { get; set; }
        public int? IdOportunidadClasificacion { get; set; }
        public int? IdFaseOportunidadClasificacion { get; set; }
        public DateTime? FechaLog { get; set; }
        public int? IdMigracion { get; set; }

        public HistoricoDetalleOportunidadRn2BO HistoricoDetalleOportunidadRn2Inicio;
        public HistoricoDetalleOportunidadRn2BO HistoricoDetalleOportunidadRn2;
        public HistoricoOportunidadRn2BO()
        {

        }
    }
}
