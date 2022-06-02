using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteFlujoDTO
    {
        public string codigo { get; set; }
        public string EstadoP { get; set; }
        public string EstadoFinanzas { get; set; }
        public string EstadoMatricula { get; set; }   
        public string SubEstadoMatricula { get; set; }
        public string Alumno { get; set; }
        public DateTime fechavencimiento { get; set; }
        public decimal cuota { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal montopagado { get; set; }
        public decimal saldopendiente { get; set; }
        public decimal mora { get; set; }
        public string nrocuota { get; set; }
        public string moneda { get; set; }
        public decimal TotalCuotaD { get; set; }
        public decimal RealPagoD { get; set; }
        public decimal SaldoPendienteD { get; set; }
        public string OrigenPrograma { get; set; }
        public string CodigoMatricula { get; set; }
        public string Email { get; set; }
        public string TelFijo { get; set; }
        public string TelCel { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string DocumentoPago { get; set; }
        public string RazonSocial { get; set; }
        public string Coordinadoraacademica { get; set; }
        public string Coordinadoracobranza { get; set; }
        public string monedaPago { get; set; }
        public DateTime? fechavencimientoOriginal { get; set; }
        public decimal? cuotaOriginal { get; set; }
        public decimal? Modificacion { get; set; }
        public string EstadoMatriculaOperaciones { get; set; }
    }
    public class FlujoCongelamientoDTO
    {
        public string fechaCongelamiento { get; set; }
        public int idMatriculaCabecera { get; set; }
        public int idCoordAcademico { get; set; }
        public string coordinadorAcademico { get; set; }
        public int idPespecifico { get; set; }
        public string programa { get; set; }
        public string codigoMatricula { get; set; }
        public string alumno { get; set; }
        public DateTime fechaCuota { get; set; }
        public decimal montoCuota { get; set; }
        public DateTime? fechaPago { get; set; }
        public decimal pago { get; set; }
        public decimal saldoPendiente { get; set; }
        public decimal mora { get; set; }
        public int nroCuota { get; set; }
        public int nroSubCuota { get; set; }
        public string moneda { get; set; }
        public decimal totalUSD { get; set; }
        public decimal realUSD { get; set; }
        public decimal penUSD { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }
    public class FlujoCongelamientoPeriodoDTO
    {
        public int idPeriodo { get; set; }
        public string periodo { get; set; }
        public int idMatriculaCabecera { get; set; }
        public int idCoordAcademico { get; set; }
        public string coordinadorAcademico { get; set; }
        public int idPespecifico { get; set; }
        public string programa { get; set; }
        public string codigoMatricula { get; set; }
        public string alumno { get; set; }
        public DateTime fechaCuota { get; set; }
        public decimal montoCuota { get; set; }
        public DateTime? fechaPago { get; set; }
        public decimal pago { get; set; }
        public decimal saldoPendiente { get; set; }
        public decimal mora { get; set; }
        public int nroCuota { get; set; }
        public int nroSubCuota { get; set; }
        public string moneda { get; set; }
        public decimal totalUSD { get; set; }
        public decimal realUSD { get; set; }
        public decimal penUSD { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }
}
