using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ExamenDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public bool RequiereTiempo { get; set; }
        public int DuracionMinutos { get; set; }
        public string Instrucciones { get; set; }
        public string Usuario { get; set; }
    }
    public class ComponenteAsignacionDTO {
        public int Id { get; set; }
        public string NombreComponente { get; set; }
    }
    public class AsignacionComponenteEvaluacionDTO
    {
        public List<ComponenteAsignacionDTO> ListaComponenteAsignado { get; set; }
        public List<ComponenteAsignacionDTO> ListaComponenteNoAsignado { get; set; }
        public int IdEvaluacion { get; set; }
        public string Usuario { get; set; }
    }
	public class FactorComponenteDTO
	{
		public int IdExamen { get; set; }
		public decimal? FactorComponente { get; set; }
		public string Usuario { get; set; }
	}
}
