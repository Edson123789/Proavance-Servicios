using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPgeneralCodigoPartnerModalidadCurso
    {
        public int Id { get; set; }
        public int IdPgeneralCodigoPartner { get; set; }
        public int IdModalidadCurso { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TModalidadCurso IdModalidadCursoNavigation { get; set; }
        public virtual TPgeneralCodigoPartner IdPgeneralCodigoPartnerNavigation { get; set; }
    }
}
