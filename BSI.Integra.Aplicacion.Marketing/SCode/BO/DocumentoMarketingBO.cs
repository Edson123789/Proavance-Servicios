using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class DocumentoMarketingBO : BaseBO
    {
        public string NombreArchivo { get; set; }
        public int TamanioArchivo { get; set; }
        public string Ipcliente { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
