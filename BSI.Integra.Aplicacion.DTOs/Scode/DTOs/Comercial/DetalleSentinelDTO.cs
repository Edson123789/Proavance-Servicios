using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DetalleSentinelDTO
    {
        public IList<SentinelSdtEstandarItemDniRucDTO> DniRuc { get; set; }
        public IList<SentinelSdtInfGenDatosGeneralesDTO> DatosGenerales { get; set; }
        public IList<SentinelSdtRepSbsitemLineaDeudaDTO> Deuda { get; set; }
        public IList<SentinelSdtResVenItemDatosVencidosDTO> DatosVencidas { get; set; }
        public IList<AlumnosSentinelLineasCreditoDTO> LineaCredito { get; set; }
        public IList<SentinelSdtPoshisItemPosicionHistoriaDTO> PosicionHistoria { get; set; }
    }
}
