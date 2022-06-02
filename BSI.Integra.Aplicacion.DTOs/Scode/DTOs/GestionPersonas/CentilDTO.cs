using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CentilDTO
    {
        public int Id { get; set; }
        public int? IdExamenTest { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public int? IdExamen { get; set; }
        public int? IdSexo { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal ValorMaximo { get; set; }
        public decimal Centil { get; set; }
        public string CentilLetra { get; set; }
    }

	public class ObjetoCentilCompuestoDTO
	{
		public CentilDTO Centil { get; set; }
		public string Usuario { get; set; }
	}
}
