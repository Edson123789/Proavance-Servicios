using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EventoCalendarioProyectoPersonalBO:BaseBO
    {
        public int IdEventoCalendarioProyecto { get; set; }
        public int IdPersonalRecurso { get; set; }
    }
}
