using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DetallesProgramasDTO
    {
        public List<ParametrosSeoProgramaDTO> ParametrosSeo { get; set; }
        public List<PgeneralDescripcionProgramaDTO> DescripcionesGenerales { get; set; }
        public List<PgeneralAdicionalInformacionDTO> DescripcionesAdicionales { get; set; }
        public List<int> Expositores { get; set; }
        public List<int> Modalidad { get; set; }
        public List<int> AreasRelacionadas { get; set; }
        public List<SuscripcionProgramaDTO> Suscripciones { get; set; }
        public List<PgeneralConfiguracionBeneficioDTO> ConfiguracionBeneficio { get; set; }
        public List<PgeneralConfiguracionPlantillaDTO> ConfiguracionPlantilla { get; set; }
        public List<PgeneralConfiguracionPlantillaDTO> ConfiguracionPlantillaConstancia { get; set; }
        public List<MontoPagoPanelDTO> MontoPago { get; set; }
        public List<ListaPgeneralVersionProgramaDTO> PgeneralVersionPrograma { get; set; }
        public List<PgeneralCodigoPartnerDTO> PgeneralCodigoPartner { get; set; }
        public List<PgeneralProyectoAplicacionDTO> pgeneralProyectoAplicacion { get; set; }
        public List<PGeneralForoAsignacionProveedorDTO> PGeneralForoAsignacionProveedor { get; set; }
        public bool? CambioPGeneralForoAsignacion { get; set; }
    }
}
