using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class InstagramComentarioBO : BaseBO
    {
        public string InstagramId { get; set; }
        public string Texto { get; set; }
        public DateTime FechaInteraccion { get; set; }
        public int IdInstagramUsuario { get; set; }
        public int IdInstagramPublicacion { get; set; }
        public int IdPersonalAsociado { get; set; }
        public bool EsUsuarioInstagram { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
