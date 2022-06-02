using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class ExamenTestBO : BaseBO
    {
        public string Nombre { get; set; }
        public string NombreAbreviado { get; set; }
        public bool EsCalificadoPorPostulante { get; set; }
        public bool MostrarEvaluacionAgrupado { get; set; }
        public bool MostrarEvaluacionPorGrupo { get; set; }
        public bool MostrarEvaluacionPorComponente { get; set; }
        public int? IdMigracion { get; set; }
        public bool RequiereCentil { get; set; }
        public int? IdFormulaPuntaje { get; set; }
        public bool CalificarEvaluacion { get; set; }
        public bool EsCalificacionAgrupada { get; set; }
        public decimal? Factor { get; set; }
		public int? IdEvaluacionCategoria { get; set; }
	}
}
