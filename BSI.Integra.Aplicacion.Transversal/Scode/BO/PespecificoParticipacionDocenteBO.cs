using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PespecificoParticipacionDocenteBO : BaseBO
    {
        public int? IdPespecifico { get; set; }
        public int? IdExpositor { get; set; }
        public bool? ConfirmaParticipacion { get; set; }
        public DateTime? FechaConfirmacion { get; set; }
        public bool? EsSilaboAprobado { get; set; }
    }
}
