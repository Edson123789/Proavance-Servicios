using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ContactoOportunidadDTO
    {
        public List<PaisFiltroDTO> Paises { get; set; }
        public List<CiudadDatosDTO> Ciudades { get; set; }
        public List<FiltroDTO> TipoDatoChats{ get; set; }
        public List<FaseOportunidadFiltroDTO> FaseOportunidades { get; set; }
        public List<CategoriaOrigenFiltroDTO> CategoriaOrigenes { get; set; }
        public List<CargoFiltroDTO> Cargos { get; set; }
        public List<AreaFormacionFiltroDTO> AreasFormacion { get; set; }
        public List<AreaTrabajoDTO> AreasTrabajo { get; set; }
        public List<IndustriaFiltroDTO> Industrias { get; set; }
		public List<OrigenFiltroDTO> Origenes { get; set; }

    }
}
