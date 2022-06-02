using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models.Asterisk
{
    public partial class Cdr
    {
        public DateTime Calldate { get; set; }
        public string Fechainicio { get; set; }
        public string Clid { get; set; }
        public string Src { get; set; }
        public string Dst { get; set; }
        public string Dcontext { get; set; }
        public string Channel { get; set; }
        public string Dstchannel { get; set; }
        public string Lastapp { get; set; }
        public string Lastdata { get; set; }
        public int Duration { get; set; }
        public int Billsec { get; set; }
        public string Disposition { get; set; }
        public string CallType { get; set; }
        public int Amaflags { get; set; }
        public string Accountcode { get; set; }
        public string Uniqueid { get; set; }
        public string Userfield { get; set; }
        public string Did { get; set; }
        public string Cnum { get; set; }
        public string Cnam { get; set; }
        public string OutboundCnum { get; set; }
        public string OutboundCnam { get; set; }
        public string DstCnam { get; set; }
        public int RecordingId { get; set; }
        public string Recordingfile { get; set; }
        public string CampaingId { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public string Custom4 { get; set; }
        public string Custom5 { get; set; }
        public string Custom6 { get; set; }
        public string Custom7 { get; set; }
        public string Custom8 { get; set; }
        public int IdActividadDetalle { get; set; }
        public string VariableRespaldo { get; set; }
    }
}
