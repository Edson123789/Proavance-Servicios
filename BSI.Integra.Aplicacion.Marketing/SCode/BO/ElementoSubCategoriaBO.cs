using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class ElementoSubCategoriaBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdElementoCategoria { get; set; }
              
    }
}
