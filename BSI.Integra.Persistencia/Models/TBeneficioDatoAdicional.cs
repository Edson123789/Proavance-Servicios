using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TBeneficioDatoAdicional
    {
        public TBeneficioDatoAdicional()
        {
            TConfiguracionBeneficioProgramaGeneralDatoAdicional = new HashSet<TConfiguracionBeneficioProgramaGeneralDatoAdicional>();
            TContenidoDatoAdicional = new HashSet<TContenidoDatoAdicional>();
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

        public virtual ICollection<TConfiguracionBeneficioProgramaGeneralDatoAdicional> TConfiguracionBeneficioProgramaGeneralDatoAdicional { get; set; }
        public virtual ICollection<TContenidoDatoAdicional> TContenidoDatoAdicional { get; set; }
    }
}
