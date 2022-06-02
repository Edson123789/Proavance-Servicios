using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class GmailCorreoArchivoAdjuntoBO : BaseBO
    {
        public int IdGmailCorreo { get; set; }
        public string Nombre { get; set; }
        public string UrlArchivoRepositorio { get; set; }
        public int? IdMigracion { get; set; }
    }
}
