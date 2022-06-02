using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
     public partial class TipoInteraccionBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Canal { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdTipoInteraccionGeneral { get; set; }
    }
}
