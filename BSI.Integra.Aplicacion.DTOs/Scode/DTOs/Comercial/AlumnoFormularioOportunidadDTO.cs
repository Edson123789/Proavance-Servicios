using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class AlumnoFormularioOportunidadDTO
	{
		public int Id { get; set; }
		public string Nombre1 { get; set; }
		public string Nombre2 { get; set; }
		public string ApellidoPaterno { get; set; }
		public string ApellidoMaterno { get; set; }
		public string DNI { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public string Celular { get; set; }
		public string Email1 { get; set; }
		public string Email2 { get; set; }
		public int? IdCargo { get; set; }
		public int? IdAFormacion { get; set; }
		public int? IdATrabajo { get; set; }
		public int? IdIndustria { get; set; }
		public int? IdReferido { get; set; }
		public int? IdCodigoPais { get; set; }
		public int? IdCodigoCiudad { get; set; }
		public string HoraContacto { get; set; }
		public string HoraPeru { get; set; }
		public string Telefono2 { get; set; }
		public string Celular2 { get; set; }
		public int? IdEmpresa { get; set; }
	}
}
