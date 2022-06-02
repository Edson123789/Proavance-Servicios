using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteraccionCorreoMailChimp
    {
        public TInteraccionCorreoMailChimp()
        {
            TInteraccionCorreoDetalleMailChimp = new HashSet<TInteraccionCorreoDetalleMailChimp>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public int IdTipoInteraccion { get; set; }
        public int IdPrioridadMailChimpListaCorreo { get; set; }
        public string EmailMailchimpId { get; set; }
        public int CantidadInteracciones { get; set; }
        public string EstadoSuscripcion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TInteraccionCorreoDetalleMailChimp> TInteraccionCorreoDetalleMailChimp { get; set; }
    }
}
