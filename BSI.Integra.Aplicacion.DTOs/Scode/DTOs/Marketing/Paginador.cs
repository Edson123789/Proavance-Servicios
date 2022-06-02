using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public partial class Paginador
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        //Adicional
        public string identificador { get; set; }
    }
}
