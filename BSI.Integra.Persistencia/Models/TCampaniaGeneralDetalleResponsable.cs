using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCampaniaGeneralDetalleResponsable
    {
        public int Id { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public int IdPersonal { get; set; }
        public int Dia1 { get; set; }
        public int Dia2 { get; set; }
        public int Dia3 { get; set; }
        public int Dia4 { get; set; }
        public int Dia5 { get; set; }
        public int Total { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TCampaniaGeneralDetalle IdCampaniaGeneralDetalleNavigation { get; set; }
    }
}
