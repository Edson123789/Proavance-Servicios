using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class AsignacionPreguntaExamenBO : BaseBO
    {
        public int IdExamen { get; set; }
        public int IdPregunta { get; set; }
        public int? NroOrden { get; set; }
        public int? Puntaje { get; set; }
        public int? IdMigracion { get; set; }
    }
}
