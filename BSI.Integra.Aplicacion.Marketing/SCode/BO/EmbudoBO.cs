using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class EmbudoBO : BaseBO
    {
        public int? IdAlumno { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdEmbudoNivel { get; set; }
        public int IdEmbudoSubNivel { get; set; }
        public int? IdTipoCategoriaOrigen { get; set; }
        public int IdProgramaGeneral { get; set; }
        public decimal? PrecioProgramaGeneral { get; set; }
        public int? IdCantidadInteraccion { get; set; }
        public int? IdMigracion { get; set; }

        public EmbudoBO() {

        }
    }
}
