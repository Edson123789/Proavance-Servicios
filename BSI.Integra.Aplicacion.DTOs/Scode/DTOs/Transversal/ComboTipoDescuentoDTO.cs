using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ComboTipoDescuentoDTO
	{
		public List<FormulaTipoDescuentoDTO> FormulaTipoDescuento { get; set; }
        public List<AgendaTipoUsuarioDTO> AgendaTipoUsuario { get; set; }
        public List<PGeneralFiltroDTO> ProgramaGeneral { get; set; }

    }
}
