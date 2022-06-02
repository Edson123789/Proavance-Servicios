using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ComboPreguntaFrecuenteDTO
	{
		public List<AreaCapacitacionDatosFiltroDTO> AreaCapacitacion { get; set; }
        public List<SubAreaCapacitacionAutoselectDTO> SubAreaCapacitacion { get; set; }
        public List<TroncalPgeneralFiltroDTO> TroncalPgeneral { get; set; }
        public List<ModalidadCursoFiltroDTO> ModalidadCurso { get; set; }
        public List<SeccionPreguntaFrecuenteFiltroDTO> SeccionPreguntaFrecuente { get; set; }

    }
}
