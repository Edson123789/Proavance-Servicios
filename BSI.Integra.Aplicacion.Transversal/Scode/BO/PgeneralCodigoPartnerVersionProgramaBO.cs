using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PgeneralCodigoPartnerVersionProgramaBO : BaseBO
    {
        public int IdPgeneralCodigoPartner { get; set; }
        public int? IdVersionPrograma { get; set; }
        public int IdMigracion { get; set; }
    }
}
