using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TotalCuotaDTO
    {
        public int Id { get; set; }
        public int? NroCuota { get; set; }
        public int? NroSubCuota { get; set; }
        public int? Anho { get; set; }
        public int? Mes { get; set; }
        public decimal? Cuota { get; set; }
        public decimal Mora { get; set; }
        public bool Cancelado { get; set; }
        public string Tipocuota { get; set; }
        public string Moneda { get; set; }
        public int? AnhoPago { get; set; }
        public int? MesPago { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaPagoBanco { get; set; }
        public DateTime? FechaProcesoPago { get; set; }
        public decimal? MontoPagado { get; set; }
        public string EstadoAlumno { get; set; }
        public string EstadoMatricula { get; set; }
        public string UsuarioCoordinadorAcademico { get; set; }
        public string EmpresaPaga { get; set; }
        public int CentroCosto { get; set; }
        public int IdCronograma { get; set; }
    }
}
