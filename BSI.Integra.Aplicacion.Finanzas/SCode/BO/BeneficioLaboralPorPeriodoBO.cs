using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class BeneficioLaboralPorPeriodoBO : BaseBO
    {
        public int IdAgendaTipoUsuario { get; set; }
        public int IdPeriodo { get; set; }
        public int IdBeneficioLaboralTipo { get; set; }
        public decimal Monto { get; set; }
        public int? IdMigracion { get; set; }
    }
}
