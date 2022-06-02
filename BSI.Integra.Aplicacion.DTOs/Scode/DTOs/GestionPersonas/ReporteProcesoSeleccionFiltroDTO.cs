using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ReporteProcesoSeleccionFiltroDTO
	{
		public string IdSexo { get; set; }
		public int IdPuesto { get; set; }
		public string IdSede { get; set; }
		public string Psicotecnico { get; set; }
		public string Psicologico { get; set; }
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }

		public string Postulante { get; set; }

		public string EstadoFiltro { get; set; }
	}

	public class ReporteGmatPmaDTO
	{
		public int IdPostulante { get; set; }
		public string Postulante { get; set; }
		public int Edad { get; set; }
		public string Examen { get; set; }
		public string Titulo { get; set; }
		public int Orden { get; set; }
		public string Nombre { get; set; }
		public string Registro { get; set; }
	}

	public class ProcesoSelecionGmatPmaDTO
	{
		public int IdPostulante { get; set; }
		public string Postulante { get; set; }
		public int Edad { get; set; }
		public string Examen { get; set; }
		public string Titulo { get; set; }
		public int Orden { get; set; }
		public string Nombre { get; set; }
		public string Registro { get; set; }
	}

	public class DTOReporte1
	{
		public int g { get; set; }
		public List<DTOReporte1Detalle> l { get; set; }
	}
	public class DTOReporte1Postulante
	{
		public int IdPostulante { get; set; }
		public string Postulante { get; set; }
		public int Edad { get; set; }
	}
	public class DTOReporte1Detalle
	{
		public int IdPostulante { get; set; }
		public string Postulante { get; set; }
		public int Edad { get; set; }
		public string Examen { get; set; }
		public string Titulo { get; set; }
		public string Nombre { get; set; }
		public string Registro { get; set; }
	}
	public class ProcesoSelecionPsicologicoDTO
	{
		public int IdPostulante { get; set; }
		public string Postulante { get; set; }
		public int Edad { get; set; }
		public string Titulo { get; set; }
		public string NombreDimension { get; set; }
		public string Registro { get; set; }
		public string Valor { get; set; }
		public int Orden { get; set; }

	}
}
