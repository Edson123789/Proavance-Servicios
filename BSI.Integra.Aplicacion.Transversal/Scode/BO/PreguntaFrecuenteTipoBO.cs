using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PreguntaFrecuenteTipoBO : BaseBO
    {
        public int? IdPreguntaFrecuente { get; set; }
        public int IdTipo { get; set; }
    }
}
