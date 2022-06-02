using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionEnvioAutomaticoDetalle
    {
        public int Id { get; set; }
        public int? IdConfiguracionEnvioAutomatico { get; set; }
        public int? IdTipoEnvioAutomatico { get; set; }
        public int? IdTiempoFrecuencia { get; set; }
        public int? IdPlantilla { get; set; }
        public int? Valor { get; set; }
        public bool? EnvioWhatsApp { get; set; }
        public bool? EnvioCorreo { get; set; }
        public bool? EnvioMensajeTexto { get; set; }
        public TimeSpan? HoraEnvioAutomatico { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
