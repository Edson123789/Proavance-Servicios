using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TWhatsAppConfiguracionPreEnvio
    {
        public int Id { get; set; }
        public int IdWhatsappMensajePublicidad { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public int? IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdPais { get; set; }
        public int NroEjecucion { get; set; }
        public bool Validado { get; set; }
        public string Plantilla { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPlantilla { get; set; }
        public int IdWhatsAppEstadoValidacion { get; set; }
        public string ObjetoPlantilla { get; set; }
        public bool Procesado { get; set; }
        public string MensajeProceso { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }

        public virtual TAlumno IdAlumnoNavigation { get; set; }
        public virtual TPais IdPaisNavigation { get; set; }
        public virtual TPlantilla IdPlantillaNavigation { get; set; }
    }
}
