using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MaterialEnvioBO : BaseBO
    {
        public int IdSedeTrabajo { get; set; }
        public int IdPersonalRemitente { get; set; }
        public int IdProveedorEnvio { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int? IdMigracion { get; set; }
        public List<MaterialEnvioDetalleBO> ListaMaterialEnvioDetalle { get; set; }

        public MaterialEnvioBO() {
            ListaMaterialEnvioDetalle = new List<MaterialEnvioDetalleBO>();
        }
    }
}
