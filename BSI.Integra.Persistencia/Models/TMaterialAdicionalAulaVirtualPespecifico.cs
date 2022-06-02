using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMaterialAdicionalAulaVirtualPespecifico
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int? IdMaterialAdicionalAulaVirtual { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TMaterialAdicionalAulaVirtual IdMaterialAdicionalAulaVirtualNavigation { get; set; }
        public virtual TPespecifico IdPespecificoNavigation { get; set; }
    }
}
