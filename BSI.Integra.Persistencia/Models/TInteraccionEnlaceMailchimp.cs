using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteraccionEnlaceMailchimp
    {
        public TInteraccionEnlaceMailchimp()
        {
            TInteraccionEnlaceDetalleMailChimp = new HashSet<TInteraccionEnlaceDetalleMailChimp>();
        }

        public int Id { get; set; }
        public int IdEnlaceMailChimp { get; set; }
        public int IdPrioridadMailChimpLista { get; set; }
        public string Email { get; set; }
        public string EmailMailChimpId { get; set; }
        public int IdTipoInteraccion { get; set; }
        public bool EsVip { get; set; }
        public string EstadoSuscripcion { get; set; }
        public int NroClicks { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }

        public virtual TEnlaceMailChimp IdEnlaceMailChimpNavigation { get; set; }
        public virtual ICollection<TInteraccionEnlaceDetalleMailChimp> TInteraccionEnlaceDetalleMailChimp { get; set; }
    }
}
