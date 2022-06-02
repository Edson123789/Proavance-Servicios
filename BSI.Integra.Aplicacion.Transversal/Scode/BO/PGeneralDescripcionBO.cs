using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PgeneralDescripcionBO : BaseBO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Texto { get; set; }
        public Guid? IdMigracion { get; set; }

    }
}
