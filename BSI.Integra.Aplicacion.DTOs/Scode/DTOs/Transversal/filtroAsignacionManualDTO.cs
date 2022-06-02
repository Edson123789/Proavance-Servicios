using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class filtroAsignacionManualDTO
    {
        public List<CategoriaOrigenFiltroDTO> filtroCategoriaOrigen { get; set; }
        public List<PaisFiltroDTO> filtroPais { get; set; }
        public List<ProbabilidadRegistroPwFiltroDTO> filtroProbabilidad { get; set; }
        public List<IndustriaFiltroDTO> filtroIndustria { get; set; }
        public List<CargoFiltroDTO> filtroCargo { get; set; }
        public List<AreaFormacionFiltroDTO> filtroAreaFormacion { get; set; }
        public List<AreaTrabajoFiltroDTO> filtroAreaTrabajo { get; set; }
        public List<FiltroDTO> filtroTipoDato { get; set; }
        public List<PGeneralFiltroDTO> filtroPgeneral { get; set; }
        public List<FiltroDTO> filtroAreaCapacitacion{ get; set; }
        public List<SubAreaCapacitacionFiltroDTO> filtroSubAreaCapacitacion { get; set; }
        public List<FaseOportunidadFiltroDTO> filtroFaseOportunidad { get; set; }
        public List<FiltroDTO> filtroCentroCosto { get; set; }
        public List<PersonalAutocompleteDTO> filtroPersonal { get; set; }
		public List<OrigenFiltroDTO> filtroOrigen { get; set; }
		public List<FiltroDTO> filtroTipoCategoriaOrigen { get; set; }
		public List<FiltroDTO> filtroOperadorComparacion { get; set; }
    }
}
