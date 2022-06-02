using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PersonalRecursoHabilidadBO :BaseBO
    {
        public int IdPersonalRecurso { get; set; }
        public int IdHabilidadSimulador { get; set; }
        public int Puntaje { get; set; }
    }
}
