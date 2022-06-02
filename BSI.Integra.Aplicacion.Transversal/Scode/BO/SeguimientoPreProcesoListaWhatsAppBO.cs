using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class SeguimientoPreProcesoListaWhatsAppBO : BaseBO
    {
        public int IdEstadoSeguimientoPreProcesoListaWhatsApp { get; set; }
        public int IdConjuntoLista { get; set; }
        public int? IdCampaniaGeneralDetalle { get; set; }
    }
}
