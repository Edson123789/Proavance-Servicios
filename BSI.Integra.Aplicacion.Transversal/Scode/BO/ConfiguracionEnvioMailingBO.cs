using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class ConfiguracionEnvioMailingBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public int IdPlantilla { get; set; }
        public int? IdMigracion { get; set; }
        public bool Activo { get; set; }
        public virtual ICollection<ConfiguracionEnvioMailingDetalleBO> ListaConfiguracionEnvioMailingDetalle { get; set; }
    }
}
