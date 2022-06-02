using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class DocenteAmbientePEspecificoDTO
	{
		public int Id { get; set; }
		public int? IdExpositor_Referencia { get; set; }
		public int? IdProveedor { get; set; }
		public string Duracion { get; set; }
		public int? IdAmbiente { get; set; }
        public int? IdEstadoPEspecifico { get; set; }
        public int? IdModalidadCurso { get; set; }
        public int? IdCursoMoodle { get; set; }
        public int? IdCursoMoodlePrueba { get; set; }
		public string Usuario { get; set; }
	}
    public class ReporteSeguiminetoCriterioObservacionDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdTabla { get; set; }
        public string Usuario { get; set; }
    }
}
