using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class NotificacionActividadParaEjecutarDTO
    {
        public bool EsCorrecto { get; set; }
        public List<ActividadParaEjecutarDTO> ListaActividades { get; set; }
    }


    // Mantener para pruebas directas desde POSTMAN
    public class NotificacionActividadParaEjecutarPruebaDTO
    {
        public bool EsCorrecto { get; set; }
        public List<ActividadParaEjecutarPruebaDTO> ListaActividades { get; set; }
    }

    // Mantener para pruebas directas desde POSTMAN
    public class ActividadParaEjecutarPruebaDTO
    {
        public bool? ActivoEjecutarFiltro { get; set; }
        public int? IdConjuntoLista { get; set; }
    }

    public class ActividadParaEjecutarDTO
    {
        public int Id { get; set; }
        public int? IdConjuntoLista { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public int? DiaFrecuenciaMensual { get; set; }
        public int? IdFrecuencia { get; set; }
        public int? CantidadIntevaloTiempo { get; set; }
        public int? IdTiempoIntervalo { get; set; }
        public bool? ActivoEjecutarFiltro { get; set; }
        public string ActividadBase { get; set; }
        public List<FiltroDTO> ActividadDiaSemana { get; set; }
    }
    public class ActividadCampaniaGeneralMailingParaEjecutarDTO
    {
        public int IdCampaniaGeneral { get; set; }
        public DateTime FechaEnvio { get; set; }
        public TimeSpan HoraEnvio { get; set; }

    }

    public class ActividadCampaniaGeneralWhatsappParaEjecutarDTO
    {
        public int IdCampaniaGeneral { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public int IdCampaniaGeneralDetalleResponsable { get; set; }
        public int IdPersonal { get; set; }
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public int IdPlantilla { get; set; }
        public int Dia1 { get; set; }
        public int Dia2 { get; set; }
        public int Dia3 { get; set; }
        public int Dia4 { get; set; }
        public int Dia5 { get; set; }
        public int Dia { get; set; }
        public DateTime FechaInicioEnvioWhatsapp { get; set; }
        public DateTime FechaFinEnvioWhatsapp { get; set; }
        public TimeSpan HoraEnvio { get; set; }

    }
}
