using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class InteraccionEnlaceMailchimpBO : BaseBO
    {
        public int IdEnlaceMailChimp { get; set; }
        public int IdPrioridadMailChimpLista { get; set; }
        public string Email { get; set; }
        public string EmailMailChimpId { get; set; }
        public int IdTipoInteraccion { get; set; }
        public bool EsVip { get; set; }
        public string EstadoSuscripcion { get; set; }
        public int NroClicks { get; set; }
        public int? IdMigracion { get; set; }
        public List<InteraccionEnlaceDetalleMailChimpBO> ListaInteraccionEnlaceDetalleMailChimp { get; set; }
        public InteraccionEnlaceMailchimpBO() {
            ListaInteraccionEnlaceDetalleMailChimp = new List<InteraccionEnlaceDetalleMailChimpBO>();
        }
    }
}
