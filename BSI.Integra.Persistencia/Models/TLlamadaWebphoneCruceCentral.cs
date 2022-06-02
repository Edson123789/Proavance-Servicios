using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TLlamadaWebphoneCruceCentral
    {
        public int Id { get; set; }
        public int IdLlamadaWebphone { get; set; }
        public int IdLlamadaCentral { get; set; }
        public DateTime FechaIncioLlamadaWebphone { get; set; }
        public DateTime FechaFinLlamadaWebphone { get; set; }
        public DateTime FechaIncioLlamadaCentral { get; set; }
        public DateTime FechaFinLlamadaCentral { get; set; }
        public string AnexoWebphone { get; set; }
        public string AnexoCentral { get; set; }
        public int DuracionTimbradoWebPhone { get; set; }
        public int DuracionContestoWebPhone { get; set; }
        public int DuracionTimbradoCentral { get; set; }
        public int DuracionContestoCentral { get; set; }
        public int IdAlumno { get; set; }
        public int IdActividadDetalle { get; set; }
        public string TelefonoDestinoWebPhone { get; set; }
        public string TelefonoDestinoCentral { get; set; }
        public int IdLlamadaWebPhoneEstado { get; set; }
        public string EstadoLlamadaCentral { get; set; }
        public string SubEstadoLlamadaCentral { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int IdMigracion { get; set; }
        public string UrlAudio { get; set; }
        public string Troncal { get; set; }
    }
}
