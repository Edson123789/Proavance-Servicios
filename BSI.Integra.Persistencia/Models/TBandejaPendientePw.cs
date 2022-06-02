using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TBandejaPendientePw
    {
        public int Id { get; set; }
        public int? IdDocumentoPw { get; set; }
        public int IdRevisionNivelPw { get; set; }
        public int Secuencia { get; set; }
        public int EsFinal { get; set; }
        public int EsInicio { get; set; }
        public int IdPersonal { get; set; }
        public int EstadoRevisar { get; set; }
        public string Comentario { get; set; }
        public string ComentarioRechazar { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TDocumentoPw IdDocumentoPwNavigation { get; set; }
        public virtual TPersonal IdPersonalNavigation { get; set; }
        public virtual TRevisionNivelPw IdRevisionNivelPwNavigation { get; set; }
    }
}
