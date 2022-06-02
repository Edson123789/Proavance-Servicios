using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteSeguimientoOportunidadLogDTO
    {
        public string FaseInicio { get; set; }
        public string FaseDestino { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaSiguienteLlamada { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public int? IdFaseOportunidadIP { get; set; }
        public int? IdFaseOportunidadPF { get; set; }
        public int? IdFaseOportunidadIC { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPF { get; set; }
        public DateTime? FechaPagoFaseOportunidadPF { get; set; }
        public DateTime? FechaPagoFaseOportunidadIC { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdEstadoOcurrencia { get; set; }
        public string TiempoDuracion { get; set; }
        public string TiempoDuracionMinutos { get; set; }
        public string TiempoDuracion3CX { get; set; }
        public int? IdCentralLLamada { get; set; }
        public int? IdTresCX { get; set; }
        public int IdOportunidadLog { get; set; }
        public DateTime? FechaIncioLlamadaIntegra { get; set; }
        public DateTime? FechaIncioLlamadaTresCX { get; set; }
        public DateTime? FechaFinLlamadaIntegra { get; set; }
        public DateTime? FechaFinLlamadaTresCX { get; set; }
        public List<LlamadaIntegraDTO> LlamadaIntegra { get; set; }
        public List<LlamadaIntegraDTO> LlamadaTresCX { get; set; }
        public string EstadoLlamadaTresCX { get; set; }
        public string EstadoLlamadaIntegra { get; set; }
        public string SubEstadoLlamadaIntegra { get; set; }
        public string SubEstadoLlamadaTresCX { get; set; }
        public string NombreActividad { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreGrabacionIntegra { get; set; }
        public string NombreGrabacionTresCX { get; set; }

        public string EstadoLlamadaSegunFlow { get; set; }
        public string Webphone { get; set; }
        public string Personal { get; set; }

    }
}
