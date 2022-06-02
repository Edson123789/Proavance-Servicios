using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDatoOportunidadAreaVenta
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdPersonal { get; set; }
        public int? IdCentroCosto { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public int IdTipoDato { get; set; }
        public int IdSesionGuardado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TCategoriaOrigen IdCategoriaOrigenNavigation { get; set; }
        public virtual TFaseOportunidad IdFaseOportunidadNavigation { get; set; }
        public virtual TOportunidad IdOportunidadNavigation { get; set; }
        public virtual TSesionGuardado IdSesionGuardadoNavigation { get; set; }
        public virtual TTipoDato IdTipoDatoNavigation { get; set; }
    }
}
