using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class OportunidadVerificadaDTO
	{
		public int IdOportunidad { get; set; }
		public int IdMatriculaCabecera { get; set; }
		public bool Verificado { get; set; }
		public string Usuario { get; set; }
	}
    public class OportunidadCodigoMatriculaDTO
    {
        public int IdOportunidad { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool Verificado { get; set; }
        public string Usuario { get; set; }
        public string CodigoMatricula { get; set; }
    }
}
