using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPrioridadMailChimpListaCorreo
    {
        public TPrioridadMailChimpListaCorreo()
        {
            TWhatsAppConfiguracionEnvioDetalle = new HashSet<TWhatsAppConfiguracionEnvioDetalle>();
        }

        public int Id { get; set; }
        public int IdPrioridadMailChimpLista { get; set; }
        public int? IdAlumno { get; set; }
        public string Email1 { get; set; }
        public string Nombre1 { get; set; }
        public string ApellidoPaterno { get; set; }
        public int IdCodigoPais { get; set; }
        public int IdCampaniaMailing { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public bool? EsSubidoCorrectamente { get; set; }
        public string ObjetoSerializado { get; set; }
        public string EstadoSuscripcionMailChimp { get; set; }
        public int? IdCampaniaGeneral { get; set; }

        public virtual TCampaniaMailing IdCampaniaMailingNavigation { get; set; }
        public virtual ICollection<TWhatsAppConfiguracionEnvioDetalle> TWhatsAppConfiguracionEnvioDetalle { get; set; }
    }
}
