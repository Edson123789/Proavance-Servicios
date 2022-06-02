using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CodigoMatriculaDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
    }
    public class IdMatriculaCabeceraDTO
    {
        public int Id { get; set; }
    }
    public class CodigoMatriculaV2DTO
	{
		public int Id { get; set; }
		public string CodigoMatricula { get; set; }
		public string EstadoMatricula { get; set; }
	}
}
