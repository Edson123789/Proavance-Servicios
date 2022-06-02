using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteCambioDeFaseDataDTO
    {
        public ReporteTasaContactoDTO ReporteTasaContacto { get; set; }
        public ReporteTasaContactoDTO ReporteTasaContactoRn2 { get; set; }
        public ReporteTasaContactoConySinLlamadaDTO ReporteTasaContactoConySinLlamada { get; set; }
        public ReporteTasaContactoConySinLlamadaDTO ReporteTasaContactoConySinLlamadaRn2 { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidad { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadConLlamada { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadSinLlamada { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteControlBICYE { get; set; }
        public List<ReporteCalidadProcesamientoDTO> ReporteCalidadProcesamiento { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteControlRN1yRN2 { get; set; }
        public List<ControlCambiodeFaseDTO> ControlCambiodeFase { get; set; }
        public List<EjecutadasSinCambiodeFaseDTO> EjecutadasSinCambiodeFase { get; set; }
        public IEnumerable<ActividadVencidaporTabPorDiaAgrupadoDTO> ActividadVencidaporTab { get; set; }
        public int ReporteMetasObtenerTotalIS { get; set; }
        public ReporteTasaDeCambioDTO ReporteTasaDeCambio { get; set; }
    }
    public class ReporteTasaDeCambioDTO
    {
        public List<TCRM_CambioDeFaseDTO> ReporteTasaDeCambioSemanal { get; set; }
        public List<TCRM_CambioDeFaseDTO> ReporteTasaDeCambioMensual { get; set; }
    }

    public class ReporteCambioDeFaseDataV2DTO
    {
        public ReporteTasaContactoDTO ReporteTasaContacto { get; set; }
        public ReporteTasaContactoDTO ReporteTasaContactoRn2 { get; set; }
        public ReporteTasaContactoConySinLlamadaDTO ReporteTasaContactoConySinLlamada { get; set; }
        public ReporteTasaContactoConySinLlamadaDTO ReporteTasaContactoConySinLlamadaRn2 { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidad { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadConLlamada { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadSinLlamada { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteControlBICYE { get; set; }
        public List<ReporteCalidadProcesamientoDTO> ReporteCalidadProcesamiento { get; set; }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteControlRN1yRN2 { get; set; }
        public List<ControlCambiodeFaseV2DTO> ControlCambiodeFase { get; set; }
        public List<EjecutadasSinCambiodeFaseDTO> EjecutadasSinCambiodeFase { get; set; }
        public IEnumerable<ActividadVencidaporTabPorDiaAgrupadoDTO> ActividadVencidaporTab { get; set; }
        public int ReporteMetasObtenerTotalIS { get; set; }
    }

    public class ReporteCalidadCambioDeFaseDTO
    {

        public List<ReporteCalidadProcesamientoDTO> ReporteCalidadProcesamiento { get; set; }
        public List<DiferenciaLlamadasBloqueDTO> DiferenciaLlamadasBloque { get; set; }
        public List<ConteoDatosFaseDTO> ConteoDatosFase { get; set; }
    }
}
