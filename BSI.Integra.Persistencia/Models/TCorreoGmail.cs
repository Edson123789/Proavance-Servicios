using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCorreoGmail
    {
        public TCorreoGmail()
        {
            TCorreoGmailArchivoAdjunto = new HashSet<TCorreoGmailArchivoAdjunto>();
        }

        public int Id { get; set; }
        public long GmailCorreoId { get; set; }
        public int IdGmailFolder { get; set; }
        public string Asunto { get; set; }
        public DateTime Fecha { get; set; }
        public string CuerpoHtml { get; set; }
        public bool EsLeido { get; set; }
        public string NombreRemitente { get; set; }
        public string EmailRemitente { get; set; }
        public string Destinatarios { get; set; }
        public string EmailConCopiaOculta { get; set; }
        public string EmailConCopia { get; set; }
        public bool AplicaCrearOportunidad { get; set; }
        public bool CumpleCriterioCrearOportunidad { get; set; }
        public bool SeCreoOportunidad { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public bool? EsDesuscritoCorrectamente { get; set; }
        public bool? EsMarcadoDesuscrito { get; set; }
        public bool? EsDescartado { get; set; }
        public int? IdPersonal { get; set; }

        public virtual TPersonal IdPersonalNavigation { get; set; }
        public virtual ICollection<TCorreoGmailArchivoAdjunto> TCorreoGmailArchivoAdjunto { get; set; }
    }
}
