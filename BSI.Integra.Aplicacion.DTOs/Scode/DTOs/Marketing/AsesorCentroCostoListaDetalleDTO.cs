using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsesorCentroCostoListaDetalleDTO
    {
        public AsesorCentroCostoDTO AsesorCentroCosto { get; set; }
        public List<AsesorAreaDetalleDTO> ListaAsesorAreaDetalle { get; set; }
        public List<AsesorSubAreaDetalleDTO> ListaAsesorSubAreaDetalle { get; set; }
        public List<AsesorPGeneralDetalleDTO> ListaAsesorPGeneralDetalle { get; set; }
        public List<AsesorGrupoFiltroPCriticoDetalleDTO> ListaAsesorGrupoFiltroPCriticoDetalle { get; set; }
        public List<AsesorGrupoDetalleDTO> ListaAsesorGrupoDetalle { get; set; }
        public List<AsesorPaisDetalleDTO> ListaAsesorPaisDetalle { get; set; }
        public List<AsesorProbabilidadDetalleDTO> ListaAsesorProbabilidadDetalle { get; set; }
    }
}
