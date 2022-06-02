using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class DatosPGeneralDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Codigo { get; set; }
		public int IdArea { get; set; }
		public int IdSubArea { get; set; }
		public int IdCategoria { get; set; }
	}

    public class DatosProgramasWebexDTO
    {
        public int IdPEspecifico { get; set; }
        public int IdTiempoFrecuencia { get; set; }
        public int Valor { get; set; }
        public int IdTiempoFrecuenciaCorreo { get; set; }
        public int ValorFrecuenciaCorreo { get; set; }
        public int IdPlantillaFrecuenciaCorreo { get; set; }
        public int IdTiempoFrecuenciaWhatsapp { get; set; }
        public int ValorFrecuenciaWhatsapp { get; set; }
        public int IdPlantillaFrecuenciaWhatsapp { get; set; }
        public int IdTiempoFrecuenciaCorreoConfirmacion { get; set; }
        public int ValorFrecuenciaCorreoConfirmacion { get; set; }
        public int IdPlantillaCorreoConfirmacion { get; set; }
        public int IdTiempoFrecuenciaCorreoDocente { get; set; }
        public int ValorFrecuenciaDocente { get; set; }
        public int IdPlantillaDocente { get; set; }

    }

    public class DatosEnvioAutomaticoOperacionesDTO
    {
        public DateTime FechaEnvio { get; set; }
    }

    public class DatosConfiguracionEnvioAutomaticoOperacionesDTO
    {
        public int IdConfiguracionEnvioAutomatico { get; set; }
        public int IdEstadoInicial { get; set; }
        public int IdSubEstadoInicial { get; set; }
        public int IdEstadoDestino { get; set; }
        public int IdSubEstadoDestino { get; set; }
        public bool EnvioWhatsApp { get; set; }
        public bool EnvioCorreo { get; set; }
        public bool EnvioMensajeTexto { get; set; }
        public int IdTipoEnvioAutomatico { get; set; }
        public int IdTiempoFrecuencia { get; set; }
        public int IdPlantilla { get; set; }
        public int Valor { get; set; }
        public TimeSpan? Hora { get; set; }
        public bool Estado { get; set; }
    }

    public class DatosConfiguracionProgramasWebexDTO
    {
        public int IdPEspecifico { get; set; }
        public int IdTiempoFrecuencia { get; set; }
        public int Valor { get; set; }
        public int IdTiempoFrecuenciaCorreo { get; set; }
        public int ValorFrecuenciaCorreo { get; set; }
        public int IdPlantillaFrecuenciaCorreo { get; set; }
        public int IdTiempoFrecuenciaWhatsapp { get; set; }
        public int ValorFrecuenciaWhatsapp { get; set; }
        public int IdPlantillaFrecuenciaWhatsapp { get; set; }
        public int IdTiempoFrecuenciaCorreoConfirmacion { get; set; }
        public int ValorFrecuenciaCorreoConfirmacion { get; set; }
        public int IdPlantillaCorreoConfirmacion { get; set; }
        public int IdTiempoFrecuenciaCorreoDocente { get; set; }
        public int ValorFrecuenciaDocente { get; set; }
        public int IdPlantillaDocente { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int IdFrecuencia { get; set; }

    }

    //webex
    public class DetalleCreacionReunionWebexDTO
    {
        public string title { get; set; }
        public string password { get; set; }
        public string start { get; set; }
        public DateTime end { get; set; }


    }

    public class CallInNumber
    {
        public string label { get; set; }
        public string callInNumber { get; set; }
        public string tollType { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string method { get; set; }
    }

    public class Telephony
    {
        public string accessCode { get; set; }
        public List<CallInNumber> callInNumbers { get; set; }
        public List<Link> links { get; set; }
    }

    public class Root
    {
        public string id { get; set; }
        public string meetingNumber { get; set; }
        public string title { get; set; }
        public string password { get; set; }
        public string meetingType { get; set; }
        public string state { get; set; }
        public string timezone { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string hostUserId { get; set; }
        public string hostDisplayName { get; set; }
        public string hostEmail { get; set; }
        public string hostKey { get; set; }
        public string webLink { get; set; }
        public string sipAddress { get; set; }
        public string dialInIpAddress { get; set; }
        public bool enabledAutoRecordMeeting { get; set; }
        public bool allowAnyUserToBeCoHost { get; set; }
        public Telephony telephony { get; set; }
    }

    public class DatosProgramasWebinarDTO
    {
        public int IdPespecifico { get; set; }
        public int IdTiempoFrecuenciaCorreoConfirmacion { get; set; }
        public int ValorFrecuenciaCorreoConfirmacion { get; set; }
        public int? IdTiempoFrecuenciaCreacion { get; set; }
        public int? ValorCreacion { get; set; }
        public int? IdTiempoFrecuenciaCorreo { get; set; }
        public int? ValorFrecuenciaCorreo { get; set; }
        public int? IdTiempoFrecuenciaWhatsApp { get; set; }
        public int? ValorFrecuenciaWhasApp { get; set; }
        public int? IdPlantillaCorreoConfirmacion { get; set; }
        public int? IdPlantillaCorreo { get; set; }
        public int? IdPlantillaWhasApp { get; set; }
    }

    public class ParametrosEnvioSesionesWebexDTO
    {
        public List<DatosProgramasWebexDTO> ListaProgramasWebex { get; set; }
        public DateTime Fecha { get; set; }
    }
}
