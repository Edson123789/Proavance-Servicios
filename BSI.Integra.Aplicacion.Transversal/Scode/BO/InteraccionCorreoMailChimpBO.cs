using BSI.Integra.Aplicacion.Base.BO;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class InteraccionCorreoMailChimpBO : BaseBO
    {
        public string Email { get; set; }
        public int IdTipoInteraccion { get; set; }
        public int IdPrioridadMailChimpListaCorreo { get; set; }
        public string EmailMailchimpId { get; set; }
        public int CantidadInteracciones { get; set; }
        public string EstadoSuscripcion { get; set; }
        public int? IdMigracion { get; set; } 
        public List<InteraccionCorreoDetalleMailChimpBO> ListaInteraccionCorreoDetalleMailChimp { get; set; }

        public InteraccionCorreoMailChimpBO() {
            ListaInteraccionCorreoDetalleMailChimp = new List<InteraccionCorreoDetalleMailChimpBO>();
        }

    }
}
