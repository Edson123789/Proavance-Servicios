using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProveedorCuentaBanco
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public int IdTipoCuentaBanco { get; set; }
        public string NroCuenta { get; set; }
        public string CuentaInterbancaria { get; set; }
        public int IdMoneda { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
