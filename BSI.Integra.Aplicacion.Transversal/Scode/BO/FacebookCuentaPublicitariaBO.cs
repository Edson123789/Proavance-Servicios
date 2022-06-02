using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class FacebookCuentaPublicitariaBO : BaseBO
    {
        public string FacebookIdNegocio { get; set; }
        public string FacebookIdCuentaPublicitaria { get; set; }
        public string Nombre { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
