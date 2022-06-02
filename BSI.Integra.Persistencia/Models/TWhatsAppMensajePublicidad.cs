using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TWhatsAppMensajePublicidad
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int? IdConjuntoListaResultado { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public int IdPais { get; set; }
        public bool EsValido { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdWhatsAppEstadoValidacion { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }

        public virtual TWhatsAppConfiguracionEnvio IdWhatsAppConfiguracionEnvioNavigation { get; set; }
        public virtual TWhatsAppEstadoValidacion IdWhatsAppEstadoValidacionNavigation { get; set; }
    }
}
