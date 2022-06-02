using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPgeneralCodigoPartner
    {
        public TPgeneralCodigoPartner()
        {
            TPgeneralCodigoPartnerModalidadCurso = new HashSet<TPgeneralCodigoPartnerModalidadCurso>();
            TPgeneralCodigoPartnerVersionPrograma = new HashSet<TPgeneralCodigoPartnerVersionPrograma>();
        }

        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Codigo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPgeneral IdPgeneralNavigation { get; set; }
        public virtual ICollection<TPgeneralCodigoPartnerModalidadCurso> TPgeneralCodigoPartnerModalidadCurso { get; set; }
        public virtual ICollection<TPgeneralCodigoPartnerVersionPrograma> TPgeneralCodigoPartnerVersionPrograma { get; set; }
    }
}
