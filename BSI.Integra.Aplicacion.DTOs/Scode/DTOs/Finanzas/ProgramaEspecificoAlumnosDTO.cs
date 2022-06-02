using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ProgramaEspecificoAlumnosDTO
	{
		public int Id { get; set; }
		public string NombresAlumno { get; set; }
		public string NombreDocumento { get; set; }
		public string RutaDocumento { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaCronograma { get; set; }
        public DateTime? FechaCalificacion { get; set; }
        public int IdPEspecifico { get; set; }
        public string Programa { get; set; }
        public string EstadoMatricula { get; set; }
        public int? Version { get; set; }
    }
}
