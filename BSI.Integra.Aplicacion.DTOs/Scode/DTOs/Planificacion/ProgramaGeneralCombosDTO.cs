using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaGeneralCombosDTO
    {
        public List<FiltroDTO> AreasCapacitacion {get; set; }
        public List<SubAreaCapacitacionFiltroDTO> SubAreasCapacitacion { get; set; }
        public List<PartnerFiltroDTO> Partners { get; set; }
        public List<ParametroSeoPwFiltroDTO> ParametrosSeo { get; set; }
        public List<ExpositorFiltroDTO> Expositores { get; set; }
        public List<CategoriaProgramaFiltroDTO> CategoriasPrograma { get; set; }
        public List<VisualizacionBsPlayFiltroDTO> VisualizacionBsPlays { get; set; }
        public List<TituloFiltroDTO> Titulos { get; set; }
        public List<CargoFiltroDTO> Cargos { get; set; }
        public List<AreaFormacionFiltroDTO> AreasFormacion{ get; set; }
        public List<AreaTrabajoFiltroDTO> AreasTrabajo { get; set; }
        public List<IndustriaFiltroDTO> Industrias { get; set; }
        public List<CiudadFiltroDTO> Ciudades { get; set; }
        public List<CompuestoCategoriaOrigenConHijosDTO> CategoriasOrigen { get; set; }
        public List<FiltroDTO> TiposDatos { get; set; }
        public List<PGeneralFiltroDTO> ProgramasGenerales { get; set; }
        public List<PerfilContactoProgramaColumnaFiltroDTO> ColumnasPerfilContacto { get; set; }
        public List<ModalidadCursoFiltroDTO> Modalidades { get; set; }
        public List<PaginaWebPwFiltroDTO>  Pagina { get; set; }
    }
    
}
