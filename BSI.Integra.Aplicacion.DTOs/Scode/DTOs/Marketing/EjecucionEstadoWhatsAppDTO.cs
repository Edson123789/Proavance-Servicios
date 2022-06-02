using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Marketing
{
    public class EjecucionEstadoWhatsAppDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int CantidadTiempoFrecuencia { get; set; }
        public int IdTiempoFrecuencia { get; set; }
        public string Usuario { get; set; }
    }
    public class EjecucionEstadoWhatsAppLogDTO
    {
        public int Id { get; set; }
        public int IdEjecucionEstadoWhatsApp { get; set; }
        public DateTime FechaEjecucion { get; set; }
        public string Usuario { get; set; }
    }
    public class ConfiguracionEjecucionEstadoWhatsAppDTO
    {
        public int Id { get; set; }
        public DateTime FechaEjecucion { get; set; }
        public DateTime FechaProximaEjecucion { get; set; }
        public int CantidadTiempoFrecuencia { get; set; }
        public int IdTiempoFrecuencia { get; set; }
    }

    public class GrillaEjecucionEstadoWhatsAppDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdTiempoFrecuencia { get; set; }
        public string NombreTiempoFrecuencia { get; set; }
        public int CantidadTiempoFrecuencia { get; set; }
        public string PeriodoEjecucion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
