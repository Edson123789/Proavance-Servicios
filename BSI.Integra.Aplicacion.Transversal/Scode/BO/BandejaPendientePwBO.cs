using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class BandejaPendientePwBO : BaseBO
    {
        public int? IdDocumentoPw { get; set; }
        public int IdRevisionNivelPw { get; set; }
        public int Secuencia { get; set; }
        public int EsFinal { get; set; }
        public int EsInicio { get; set; }
        public int IdPersonal { get; set; }
        public int EstadoRevisar { get; set; }
        public string Comentario { get; set; }
        public string ComentarioRechazar { get; set; }
    }
}
