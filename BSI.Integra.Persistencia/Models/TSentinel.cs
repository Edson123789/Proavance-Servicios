using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSentinel
    {
        public TSentinel()
        {
            TSentinelRepLegItem = new HashSet<TSentinelRepLegItem>();
            TSentinelSdtEstandarItem = new HashSet<TSentinelSdtEstandarItem>();
            TSentinelSdtInfGen = new HashSet<TSentinelSdtInfGen>();
            TSentinelSdtLincreItem = new HashSet<TSentinelSdtLincreItem>();
            TSentinelSdtPoshisItem = new HashSet<TSentinelSdtPoshisItem>();
            TSentinelSdtRepSbsitem = new HashSet<TSentinelSdtRepSbsitem>();
            TSentinelSdtResVenItem = new HashSet<TSentinelSdtResVenItem>();
        }

        public int Id { get; set; }
        public string Dni { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TSentinelRepLegItem> TSentinelRepLegItem { get; set; }
        public virtual ICollection<TSentinelSdtEstandarItem> TSentinelSdtEstandarItem { get; set; }
        public virtual ICollection<TSentinelSdtInfGen> TSentinelSdtInfGen { get; set; }
        public virtual ICollection<TSentinelSdtLincreItem> TSentinelSdtLincreItem { get; set; }
        public virtual ICollection<TSentinelSdtPoshisItem> TSentinelSdtPoshisItem { get; set; }
        public virtual ICollection<TSentinelSdtRepSbsitem> TSentinelSdtRepSbsitem { get; set; }
        public virtual ICollection<TSentinelSdtResVenItem> TSentinelSdtResVenItem { get; set; }
    }
}
