using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionEnvioMailingDetalle
    {
        public int Id { get; set; }
        public int IdConfiguracionEnvioMailing { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public string Asunto { get; set; }
        public string CuerpoHtml { get; set; }
        public bool EnviadoCorrectamente { get; set; }
        public string MensajeError { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int IdMandrilEnvioCorreo { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdOportunidad { get; set; }

        public virtual TConfiguracionEnvioMailing IdConfiguracionEnvioMailingNavigation { get; set; }
    }
}
