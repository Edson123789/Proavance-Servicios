using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRegistroLlamada
    {
        public int Id { get; set; }
        public string HistoryId { get; set; }
        public string CallId { get; set; }
        public string Duration { get; set; }
        public string TimeStart { get; set; }
        public string TimeAnswered { get; set; }
        public string TimeEnd { get; set; }
        public string ReasonTerminated { get; set; }
        public string FromNro { get; set; }
        public string ToNro { get; set; }
        public string FromDn { get; set; }
        public string ToDn { get; set; }
        public string DialNro { get; set; }
        public string ReasonChanged { get; set; }
        public string FinalNumber { get; set; }
        public string FinalDn { get; set; }
        public string BillCode { get; set; }
        public string BillRate { get; set; }
        public string BillCost { get; set; }
        public string BillName { get; set; }
        public string Chain { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
