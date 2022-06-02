using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WhatsAppConfiguracionEnvioBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPersonal { get; set; }
        public int IdPlantilla { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public DateTime? FechaDesactivacion { get; set; }
        public bool Activo { get; set; }
        public int? IdCampaniaGeneralDetalle { get; set; }
        public int? IdMigracion { get; set; }
    }
}
