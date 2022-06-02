using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PerfilPuestoTrabajoDTO
	{
		public int Id { get; set; }
		public int IdPuestoTrabajo { get; set; }
		public string PuestoTrabajo { get; set; }
		public int Version { get; set; }
		public string Objetivo { get; set; }
		public string Descripcion { get; set; }
		public string Personal_Solicitud { get; set; }
		public string Personal_Aprobacion { get; set; }
		public DateTime FechaSolicitud { get; set; }
		public DateTime? FechaAprobacion { get; set; }
		public int IdPerfilPuestoTrabajoEstadoSolicitud { get; set; }
		public string PerfilPuestoTrabajoEstadoSolicitud { get; set; }
		public string Observacion { get; set; }
		public bool EsActual { get; set; }
	}

	public class AprobacionRechazoPerfilPuestoTrabajoDTO
	{
		public int IdPerfilPuestoTrabajo { get; set; }
		public bool TipoBoton { get; set; }
		public int IdPersonal { get; set; }
		public string Observacion { get; set; }
		public string Usuario { get; set; }
	}

	public class ValidarAprobacionDTO
	{		
		public bool Existe { get; set; }
		public List<int> ListaUsuarioAprobracion { get; set; }
	}
}
