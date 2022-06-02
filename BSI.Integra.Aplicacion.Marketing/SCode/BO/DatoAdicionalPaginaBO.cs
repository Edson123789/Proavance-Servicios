using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class DatoAdicionalPaginaBO : BaseBO
    {
        public int IdFormularioLandingPage { get; set; }
        public int IdTitulo { get; set; }
        public string NombreTitulo { get; set; }
        public string Descripcion { get; set; }
        public string NombreImagen { get; set; }
        public string ColorTitulo { get; set; }
        public string ColorDescripcion { get; set; }

    }
}
