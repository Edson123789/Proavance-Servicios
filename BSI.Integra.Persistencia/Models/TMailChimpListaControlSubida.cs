using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMailChimpListaControlSubida
    {
        public int Id { get; set; }
        public int IdPrioridadMailLista { get; set; }
        public int Grupo { get; set; }
        public DateTime FechaInicioProceso { get; set; }
        public DateTime FechaFinProceso { get; set; }
        public bool EnProceso { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public string MailchimpBatchId { get; set; }
    }
}
