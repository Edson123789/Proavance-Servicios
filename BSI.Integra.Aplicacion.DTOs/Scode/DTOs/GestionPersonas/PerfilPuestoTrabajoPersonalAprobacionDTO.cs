using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PerfilPuestoTrabajoPersonalAprobacionDTO
	{
		public List<int> ListaPersonal { get; set; }
		public List<int> ListaPuestoTrabajo { get; set; }
		public string Usuario { get; set; }
	}
	public class PerfilPuestoTrabajoPersonalAprobacionDatosDTO
	{
		public int Id { get; set; }
		public int IdPersonal { get; set; }
		public int IdPuestoTrabajo { get; set; }
		public string PuestoTrabajo { get; set; }
		public string Personal { get; set; }
	}
	
	public class PerfilPuestoTrabajoPersonalAprobacionAgrupadoDTO
	{
		public int IdPersonal { get; set; }
		public string Personal { get; set; }
		public List<int> ListaPuestoTrabajo { get; set; }
		public List<string> PuestoTrabajo { get; set; }
	}
}
