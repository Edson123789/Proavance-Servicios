using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionCreacion
    {
        public TConfiguracionCreacion()
        {
            TConfiguracionInvitacion = new HashSet<TConfiguracionInvitacion>();
        }

        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int IdTiempoFrecuencia { get; set; }
        public int Valor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdTiempoFrecuenciaCorreo { get; set; }
        public int? ValorFrecuenciaCorreo { get; set; }
        public int? IdTiempoFrecuenciaWhatsapp { get; set; }
        public int? ValorFrecuenciaWhatsapp { get; set; }
        public int? IdPlantillaFrecuenciaCorreo { get; set; }
        public int? IdPlantillaFrecuenciaWhatsapp { get; set; }
        public int? IdTiempoFrecuenciaCorreoConfirmacion { get; set; }
        public int? ValorFrecuenciaCorreoConfirmacion { get; set; }
        public int? IdPlantillaCorreoConfirmacion { get; set; }
        public int? IdTiempoFrecuenciaCorreoDocente { get; set; }
        public int? ValorFrecuenciaDocente { get; set; }
        public int? IdPlantillaDocente { get; set; }

        public virtual TPespecifico IdPespecificoNavigation { get; set; }
        public virtual ICollection<TConfiguracionInvitacion> TConfiguracionInvitacion { get; set; }
    }
}
