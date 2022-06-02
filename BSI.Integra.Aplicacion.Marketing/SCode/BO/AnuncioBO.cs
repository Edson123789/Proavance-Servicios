using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class AnuncioBO : BaseBO
    {
        public string Nombre { get; set; }
        public string IdAnuncioFacebook { get; set; }
        public int IdConjuntoAnuncio { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdCreativoPublicidad { get; set; }
        public string EnlaceFormulario { get; set; }
        public int? NroAnuncioCorrelativo { get; set; }

    }
}
