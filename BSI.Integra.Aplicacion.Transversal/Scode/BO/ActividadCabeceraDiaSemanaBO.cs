using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class ActividadCabeceraDiaSemanaBO : BaseBO
    {
        public int IdActividadCabecera { get; set; }
        public int IdDiaSemana { get; set; }
        public int? IdMigracion { get; set; }
    }
}
