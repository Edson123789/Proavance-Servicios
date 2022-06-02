using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class CriterioCalificacionBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Sigla { get; set; }
        public bool EstadoDocumento { get; set; }
        public bool DocOriginal { get; set; }
        public bool DocPasarela { get; set; }
        public bool DocPasCancelados { get; set; }
        public int? IdMigracion { get; set; }
    }
}
