using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.AsteriskCdr
{
    public class CdrResumenDTO
    {
        public DateTime? FechaInicio { get; set; }
        public string Src { get; set; }
        public string Dst { get; set; }
        public int Duration { get; set; }
        public int Billsec { get; set; }
        public string CallType { get; set; }
        public int RecordingId { get; set; }
        public string Recordingfile { get; set; }
        public int IdActividadDetalle { get; set; }
        public string VariableRespaldo { get; set; }
    }
}
