using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProveedorCuentaBancoDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public string NombreBanco { get; set; }
        public int IdTipoCuentaBanco { get; set; }
        public string TipoCuenta { get; set; }
        public string NroCuenta { get; set; }
        public string CuentaInterbancaria { get; set; }
        public int IdMoneda { get; set; }
        public string Moneda { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
