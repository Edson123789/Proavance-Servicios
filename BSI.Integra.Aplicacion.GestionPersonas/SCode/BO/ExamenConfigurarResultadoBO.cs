using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class ExamenConfigurarResultadoBO : BaseBO
    {
        public bool ExamenCalificado { get; set; }
        public decimal? PuntajeExamen { get; set; }
        public decimal? PuntajeAprobacion { get; set; }
        public bool MostrarResultado { get; set; }
        public bool MostrarPuntajeTotal { get; set; }
        public bool MostrarPuntajePregunta { get; set; }
        public int? IdMigracion { get; set; }
    }
}
