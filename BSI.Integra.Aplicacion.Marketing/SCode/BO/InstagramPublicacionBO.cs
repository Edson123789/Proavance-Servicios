using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class InstagramPublicacionBO : BaseBO
    {
        public string InstagramId { get; set; }
        public string Subtitulo { get; set; }
        public string TipoMedia { get; set; }
        public string UrlMedia { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
