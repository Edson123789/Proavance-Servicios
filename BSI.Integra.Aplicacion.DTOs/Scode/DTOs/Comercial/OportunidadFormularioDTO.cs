using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class OportunidadFormularioDTO
	{
		public int Id { get; set; }
		public int IdAlumno { get; set; }
		public int IdCentroCosto { get; set; }
		public int IdFaseOportunidad { get; set; }
		public int IdOrigen { get; set; }
		public int IdPersonal_Asignado { get; set; }
		public int IdTipoDato { get; set; }
		public string UltimoComentario { get; set; }
		public int fk_id_tipointeraccion { get; set; }
	}
}
