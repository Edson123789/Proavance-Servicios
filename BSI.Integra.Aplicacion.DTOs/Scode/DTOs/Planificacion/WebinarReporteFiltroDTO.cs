using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class WebinarReporteFiltroDTO
    {
		public List<int> ListaPGeneral { get; set; }
		public List<int> ListaPEspecifico { get; set; }
		public string EstadoSesion { get; set; }
		public DateTime Fecha { get; set; }
		public string Hora { get; set; }
		public DateTime? FechaInicio { get; set; }
		public DateTime? FechaFin { get; set; }
		public string FechaPorDefecto { get; set; }
		public string CodigoMatricula { get; set; }

	}

	public class WebinarDDetalleSesionDTO
	{
		public int IdPEspecificoSesion { get; set; }

		public string EstadoSesion { get; set; }
		public DateTime Fecha { get; set; }
		public string Hora { get; set; }
		public string NombrePrograma { get; set; }
		public string NombreWebinar { get; set; }
		public string EsWebinarConfirmado { get; set; }
		public string EsCancelado { get; set; }

	}

	public class ControlCursosFiltroDTO
	{
		public string IdPGeneral { get; set; }
		public string IdProgramaEspecifico { get; set; }
		public string IdCentroCosto { get; set; }
		public string IdEstadoPEspecifico { get; set; }		
		public DateTime? FechaInicio { get; set; }
		public DateTime? FechaFin { get; set; }
	}

	public class DatosListaControlCursosDTO
    {
        public int Id { get; set; }
        public int? IdPGeneralPadre { get; set; }
        public string PGeneralPadre { get; set; }
        public int? IdPEspecificoPadre { get; set; }
        public string PEspecificoPadre { get; set; }
        public string Modalidad { get; set; }
		public int? IdCentroCosto { get; set; }
		public string CentroCosto { get; set; }
        public int? IdPEspecificoHijo { get; set; }
        public string PEspecificoHijo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
		public DateTime? FechaFinalizacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public string EstadoP { get; set; }
        public int? EstadoPId { get; set; }
		public int? Tiempo { get; set; }
		public string Docente { get; set; }
        public string Coordinadora { get; set; }
        public string ObservacionCursoFinalizado { get; set; }

    }

	public class ObservacionControlCursosDTO
	{
		public string Observacion { get; set; }
		public int IdPEspecifico { get; set; }
		public string Usuario { get; set; }
		public DateTime FechaFinalizacion { get; set; }
	}
}
