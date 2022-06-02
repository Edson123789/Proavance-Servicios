using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoSesionAsistenciaDetalleDTO
    {
		public int IdAsistencia { get; set; }
		public int IdMatriculaCabecera { get; set; }
		public int IdPEspecificoSesion { get; set; }
		public string CodigoMatricula { get; set; }
		public string NombreAlumno { get; set; }
		public bool Asistio { get; set; }
		public bool Justifico { get; set; }
	}

	public class EntregaMaterialDetalleDTO
	{
		public int IdAsistencia { get; set; }
		public string NombreMaterial { get; set; }
		public int IdMaterialVersion { get; set; }
		public int? IdMaterialEntrega { get; set; }
		public bool? Entregado { get; set; }
		public string Comentario { get; set; }
	}
}
