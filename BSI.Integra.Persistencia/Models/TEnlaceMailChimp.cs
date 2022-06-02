using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEnlaceMailChimp
    {
        public TEnlaceMailChimp()
        {
            TInteraccionEnlaceMailchimp = new HashSet<TInteraccionEnlaceMailchimp>();
        }

        public int Id { get; set; }
        public string CampaniaMailChimpId { get; set; }
        public string UrlMailChimpId { get; set; }
        public string Url { get; set; }
        public int TotalClicks { get; set; }
        public decimal PorcentajeClicks { get; set; }
        public int ClicksUnicos { get; set; }
        public decimal PorcentajeClicksUnicos { get; set; }
        public DateTime? FechaUltimoClick { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TInteraccionEnlaceMailchimp> TInteraccionEnlaceMailchimp { get; set; }
    }
}
