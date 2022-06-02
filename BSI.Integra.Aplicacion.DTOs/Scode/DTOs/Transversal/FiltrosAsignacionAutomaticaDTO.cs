using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltrosAsignacionAutomaticaDTO
    {
        public List<CategoriaOrigenFiltroDTO> filtroCategoriaOrigen { get; set; }
        public List<PaisFiltroDTO> filtroPais { get; set; }
        public List<ProbabilidadRegistroPwFiltroDTO> filtroProbabilidad { get; set; }
        public List<IndustriaFiltroDTO> filtroIndustria { get; set; }
        public List<CargoFiltroDTO> filtroCargo { get; set; }
        public List<AreaFormacionFiltroDTO> filtroAreaFormacion { get; set; }
        public List<AreaTrabajoFiltroDTO> filtroAreaTrabajo { get; set; }
        public List<FiltroDTO> filtroCentroCosto { get; set; }
        public List<CiudadDatosDTO> filtroCiudad { get; set; }

    }
}
