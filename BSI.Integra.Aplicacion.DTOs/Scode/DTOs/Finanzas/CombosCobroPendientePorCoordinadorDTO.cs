using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CombosCobroPendientePorCoordinadorDTO
    {
        public List<ModalidadCursoFiltroDTO> ListaModalidades { get; set; }
        public List<FiltroDTO> ListaPeriodo { get; set; }
        public List<DatoPersonalCoordinadorDTO> ListaCoordinador { get; set; }
        public List<FiltroDTO> ListaAreaCapacitacion { get; set; }
        public List<SubAreaCapacitacionFiltroDTO> ListaSubAreaCapacitacion { get; set; }
        public List<PGeneralFiltroSubAreaDTO> ListaProgramaGeneral { get; set; }
		//public List<DatosListaPespecificoDePgeneralDTO> ListaProgramaEspecifico { get; set; }
		public List<DatosListaPespecificoDePgeneralDTO> ListaCentroCosto { get; set; }
		public List<FiltroDTO> ListaSede { get; set; }

    }
}
