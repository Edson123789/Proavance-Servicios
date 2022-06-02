using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class CorreoGmailArchivoAdjuntoBO : BaseBO
    {
        public int IdCorreoGmail { get; set; }
        public string Nombre { get; set; }
        public string UrlArchivoRepositorio { get; set; }
        public int? IdMigracion { get; set; }
        public CorreoGmailArchivoAdjuntoBO() { 
        }


    }
}
