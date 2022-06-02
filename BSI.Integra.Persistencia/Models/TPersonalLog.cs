using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPersonalLog
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string Rol { get; set; }
        public string TipoPersonal { get; set; }
        public int? IdJefe { get; set; }
        public bool EstadoRol { get; set; }
        public bool EstadoTipoPersonal { get; set; }
        public bool EstadoIdJefe { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdCerrador { get; set; }
        public bool? EsCerrador { get; set; }
        public bool? EstadoCerrador { get; set; }
        public int? IdPuestoTrabajoNivel { get; set; }

        public virtual TPersonal IdPersonalNavigation { get; set; }
    }
}
