using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AdicionalesFormularioLandingPageDTO
    {
        public FormularioSolicitudFiltroDTO FormularioSolicitudFiltro { get; set; }
        public PEspecificoCentroCostoDTO PEspecificoCentroCosto { get; set; }
        public List<DatoAdicionalPaginaDTO> ListaDatoAdicional { get; set; }
    }
}
