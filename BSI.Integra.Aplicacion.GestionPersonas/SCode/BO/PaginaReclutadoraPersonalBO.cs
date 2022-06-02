using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class PaginaReclutadoraPersonalBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Url { get; set; }
        public int? IdMigracion { get; set; }
    }
}
