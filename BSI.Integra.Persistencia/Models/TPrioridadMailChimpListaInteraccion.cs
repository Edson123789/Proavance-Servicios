using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPrioridadMailChimpListaInteraccion
    {
        public int Id { get; set; }
        public int IdPrioridadMailChimpLista { get; set; }
        public double ClickRate { get; set; }
        public int Clicks { get; set; }
        public double OpenRate { get; set; }
        public int SubscriberClicks { get; set; }
        public int Opens { get; set; }
        public int UniqueOpens { get; set; }
        public int? MemberCount { get; set; }
        public int CleanedCount { get; set; }
        public int? EmailSend { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
