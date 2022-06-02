using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class LlamadaWebphoneCruceCentralBO : BaseBO
    {
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
        public string UrlAudio { get; set; }
        public int IdMigracion { get; set; }
    }
}
