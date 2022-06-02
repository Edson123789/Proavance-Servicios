using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class ConjuntoAnuncioTipoObjetivoBO : BaseBO
    {
        public string Nombre { get; set; }
        public int IdConjuntoAnuncioFuente { get; set; }

    }
}
