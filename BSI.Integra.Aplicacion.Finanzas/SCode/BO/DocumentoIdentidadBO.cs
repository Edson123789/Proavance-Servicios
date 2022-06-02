using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class DocumentoIdentidadBO : BaseBO
    {
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
    }
}
