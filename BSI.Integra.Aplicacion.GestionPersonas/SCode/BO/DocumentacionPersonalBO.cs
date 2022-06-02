using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class DocumentacionPersonalBO : BaseBO
    {
        public int IdTipoDocumentacionPersonal { get; set; }
        public int IdPersonal { get; set; }
        public string UrlDocumento { get; set; }        
        public int? IdMigracion { get; set; }
    }
}
