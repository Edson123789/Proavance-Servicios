using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class CompetidorVentajaDesventajaBO : BaseBO
    {
        public int Id { get; set; }
        public int IdCompetidor { get; set; }
        public int Tipo { get; set; }
        public string Contenido { get; set; }
        
    }
}
