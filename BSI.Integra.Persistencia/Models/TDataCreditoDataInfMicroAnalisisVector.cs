using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataInfMicroAnalisisVector
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string NombreSector { get; set; }
        public string CuentaEntidad { get; set; }
        public string CuentaNumeroCuenta { get; set; }
        public string CuentaTipoCuenta { get; set; }
        public string CuentaEstado { get; set; }
        public bool? ContieneDatos { get; set; }
        public DateTime? Fecha { get; set; }
        public string SaldoDeudaTotalMora { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TDataCreditoBusqueda IdDataCreditoBusquedaNavigation { get; set; }
    }
}
