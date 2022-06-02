using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ChatZopimBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Script { get; set; }
        public string Filtro { get; set; }
        public int? IdAsesor { get; set; }
        public string Password { get; set; }
    }
}
