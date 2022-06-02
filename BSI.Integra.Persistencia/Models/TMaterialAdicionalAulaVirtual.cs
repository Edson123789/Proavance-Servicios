using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMaterialAdicionalAulaVirtual
    {
        public TMaterialAdicionalAulaVirtual()
        {
            TMaterialAdicionalAulaVirtualPespecifico = new HashSet<TMaterialAdicionalAulaVirtualPespecifico>();
            TMaterialAdicionalAulaVirtualRegistro = new HashSet<TMaterialAdicionalAulaVirtualRegistro>();
        }

        public int Id { get; set; }
        public string NombreConfiguracion { get; set; }
        public int IdPgeneral { get; set; }
        public bool? EsOnline { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPgeneral IdPgeneralNavigation { get; set; }
        public virtual ICollection<TMaterialAdicionalAulaVirtualPespecifico> TMaterialAdicionalAulaVirtualPespecifico { get; set; }
        public virtual ICollection<TMaterialAdicionalAulaVirtualRegistro> TMaterialAdicionalAulaVirtualRegistro { get; set; }
    }
}
