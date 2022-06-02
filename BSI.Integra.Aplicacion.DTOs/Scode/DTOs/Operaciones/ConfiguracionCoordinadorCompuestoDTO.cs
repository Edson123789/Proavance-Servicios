using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ConfiguracionCoordinadorCompuestoDTO
	{
		public int Id { get; set; }
		public int IdPais { get; set; }
		public int IdModalidad { get; set; }
		public int IdPersonal { get; set; }
		public string Pais { get; set; }
		public string Modalidad { get; set; }
		public string Personal { get; set; }
		public int? IdCiudad { get; set; }
		public string Ciudad { get; set; }
	}

	public class DTO1 //Configuracion
	{
		public int Id { get; set; }
		public List<DTO2> ListaPersonal { get; set; }
	}

	public class DTO2 //Personal
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public List<DTO3> ListaModalidad { get; set; }
	}
	public class DTO3 //Modalidad
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public List<DTO4> ListaPaises { get; set; }
	}
	public class DTO4 //Pais
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public List<DTO5> ListaCiudad { get; set; }
	}
	public class DTO5 //Ciudad
	{
		public int? Id { get; set; }
		public string Nombre { get; set; }
	}

	
}
