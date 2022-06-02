using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class GrupoComponenteEvaluacionDTO
    {
        public int Id { get; set; }
        public int IdFormula { get; set; }
        public string Nombre { get; set; }
        public GrupoComponentesDTO[] ListaComponentes { get; set; }
        public bool RequiereCentil { get; set; }
        public decimal? Factor { get; set; }
    }
    public class GrupoComponentesDTO {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class GrupoComponenteEvaluacionFormularioDTO {
        public GrupoComponenteEvaluacionDTO GrupoComponenteEvaluacion { get; set; }
        public string Usuario { get; set; }
        public int IdEvaluacion { get; set; }
    }


    public class GrupoEvaluacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdEvaluacion { get; set; }
        public int IdExamen { get; set; }
        public string NombreExamen { get; set; }
        public int IdFormula { get; set; }
        public decimal? Factor { get; set; }
        public bool RequiereCentil { get; set; }
    }

	public class GrupoComponenteDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdEvaluacion { get; set; }
		public int IdExamen { get; set; }
		public string NombreExamen { get; set; }
		//public int IdFormula { get; set; }
		public decimal? FactorComponente { get; set; }
		public bool RequiereCentil { get; set; }
	}


	public class GrupoComponenteFactorDTO
	{
		public int IdGrupoComponenteEvaluacion { get; set; }
		public decimal? Factor { get; set; }
		public string Usuario { get; set; }
	}
}
