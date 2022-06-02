using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPgeneralCodigoPartnerVersionPrograma
    {
        public int Id { get; set; }
        public int IdPgeneralCodigoPartner { get; set; }
        public int? IdVersionPrograma { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int IdMigracion { get; set; }

        public virtual TPgeneralCodigoPartner IdPgeneralCodigoPartnerNavigation { get; set; }
        public virtual TVersionPrograma IdVersionProgramaNavigation { get; set; }
    }
}
