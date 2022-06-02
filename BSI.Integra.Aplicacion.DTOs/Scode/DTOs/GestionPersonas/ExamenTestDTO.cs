using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ExamenTestDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreAbreviado { get; set; }
        public bool EsCalificadoPorPostulante { get; set; }
        public bool MostrarEvaluacionAgrupado { get; set; }
        public bool MostrarEvaluacionPorGrupo { get; set; }
        public bool MostrarEvaluacionPorComponente { get; set; }
        public bool RequiereCentil { get; set; }
        public int? IdFormulaPuntaje { get; set; }
        public bool CalificarEvaluacion { get; set; }
        public bool EsCalificacionAgrupada { get; set; }
        public decimal? Factor { get; set; }
        public string Usuario { get; set; }     
        public List<CentilDTO> ListaCentilEvaluacion { get; set; }
        public List<EvaluacionAgrupadaComponenteDTO> ListaExamenVisualizacion { get; set; }
		public int? IdEvaluacionCategoria { get; set; }
    }
    public class ExamenTestEncriptadoDTO
    {
        public string DatosEncriptados { get; set; }
    }
}
