using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TReportePagoCongeladoPorPeriodo
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdCronogramaPagoDetalleFinal { get; set; }
        public string Ciudad { get; set; }
        public string Modalidad { get; set; }
        public string NombrePrograma { get; set; }
        public string CodigoAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public string Comprobante { get; set; }
        public string NroDocumento { get; set; }
        public string MonedaPago { get; set; }
        public decimal? TipoCambio { get; set; }
        public decimal Cuota { get; set; }
        public decimal Mora { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal TotalPagadoDisponible { get; set; }
        public DateTime FechaPagoOriginal { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaProcesoPago { get; set; }
        public string CuotaSubCuota { get; set; }
        public string FechaCuota { get; set; }
        public string Observaciones { get; set; }
        public string FormaIngreso { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string EstadoCuota { get; set; }
        public int IdPeriodo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
