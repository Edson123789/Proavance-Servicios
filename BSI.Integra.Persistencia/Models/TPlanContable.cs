using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPlanContable
    {
        public int Id { get; set; }
        public long Cuenta { get; set; }
        public string Descripcion { get; set; }
        public int Padre { get; set; }
        public bool? Univel { get; set; }
        public string Cbal { get; set; }
        public string Debe { get; set; }
        public string Haber { get; set; }
        public int? IdPlanContableTipoCuenta { get; set; }
        public string Analisis { get; set; }
        public string CentroCosto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdFurTipoSolicitud { get; set; }
    }
}
