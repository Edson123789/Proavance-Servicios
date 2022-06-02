using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PostulanteLogBO : BaseBO
    {
        public int IdPostulante { get; set; }
        public string Clave { get; set; }
        public string Valor { get; set; }
        public int? IdMigracion { get; set; }
    }
}
