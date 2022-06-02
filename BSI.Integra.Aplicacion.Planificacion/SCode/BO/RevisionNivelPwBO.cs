using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class RevisionNivelPwBO : BaseBO
    {
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public int IdTipoRevisionPw { get; set; }
        public int IdRevisionPw { get; set; }
    }
}
