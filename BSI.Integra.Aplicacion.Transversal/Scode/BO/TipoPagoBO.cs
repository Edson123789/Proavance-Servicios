using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class TipoPagoBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cuotas { get; set; }
        public bool Suscripciones { get; set; }
        public bool PorDefecto { get; set; }
        public Guid? IdMigracion { get; set; }

        public List<TipoPagoCategoriaBO> TipoPagoCategoria;
    }
}
