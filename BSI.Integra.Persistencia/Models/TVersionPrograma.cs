using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TVersionPrograma
    {
        public TVersionPrograma()
        {
            TConfiguracionBeneficioProgramaGeneralVersion = new HashSet<TConfiguracionBeneficioProgramaGeneralVersion>();
            TPgeneralAsubPgeneralVersionPrograma = new HashSet<TPgeneralAsubPgeneralVersionPrograma>();
            TPgeneralCodigoPartnerVersionPrograma = new HashSet<TPgeneralCodigoPartnerVersionPrograma>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TConfiguracionBeneficioProgramaGeneralVersion> TConfiguracionBeneficioProgramaGeneralVersion { get; set; }
        public virtual ICollection<TPgeneralAsubPgeneralVersionPrograma> TPgeneralAsubPgeneralVersionPrograma { get; set; }
        public virtual ICollection<TPgeneralCodigoPartnerVersionPrograma> TPgeneralCodigoPartnerVersionPrograma { get; set; }
    }
}
