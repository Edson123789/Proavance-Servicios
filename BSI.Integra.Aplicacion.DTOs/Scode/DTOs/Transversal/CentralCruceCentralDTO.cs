using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CentralCruceCentralDTO
    {
        public int Id { get; set; }
		public int IdLlamadaWebPhone { get; set; }
        public int? IdLlamadaCentral { get; set; }
        public DateTime FechaIncioLlamadaWebPhone { get; set; }
        public DateTime? FechaFinLlamadaWebPhone { get; set; }
        public DateTime? FechaIncioLlamadaCentral { get; set; }
        public DateTime? FechaFinLlamadaCentral { get; set; }
        public string AnexoWebPhone { get; set; }
        public string AnexoCentral { get; set; }
        public int? DuracionTimbradoWebPhone  { get; set; }
        public int? DuracionContestoWebPhone { get; set; }
        public int? DuracionTimbradoCentral { get; set; }
        public int? DuracionContestoCentral { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdActividadDetalle { get; set; }
        public string TelefonoDestinoWebPhone { get; set; }
        public string TelefonoDestinoCentral { get; set; }
        public int? IdLlamadaWebPhoneEstado { get; set; }
        public string EstadoLlamadaCentral { get; set; }
        public string SubEstadoLlamadaCentral { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UrlAudio { get; set; }
        public string Usuario { get; set; }
    }
}
