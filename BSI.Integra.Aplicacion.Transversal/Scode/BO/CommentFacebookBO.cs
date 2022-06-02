using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CommentFacebookBO : BaseBO
    {
        public string PSID { get; set; }
        public string Nombres { get; set; }
        public string Mensaje { get; set; }
        public int Asesor { get; set; }
        public string comment_id { get; set; }
        public string post_id { get; set; }
        public string parent_id { get; set; }
        public bool SonidoNuevoCommentario { get; set; }
        public bool Status { get; set; }
        public string ErrorMessage { get; set; }

        public CommentFacebookBO()
        {

        }
    }
}
