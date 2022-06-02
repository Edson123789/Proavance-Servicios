using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataTarjetaCreditoBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public bool? Bloqueada { get; set; }
        public string Entidad { get; set; }
        public string Numero { get; set; }
        public DateTime? FechaApertura { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Comportamiento { get; set; }
        public string FormaPago { get; set; }
        public decimal? ProbabilidadIncumplimiento { get; set; }
        public string Calificacion { get; set; }
        public string SituacionTitular { get; set; }
        public string Oficina { get; set; }
        public string Ciudad { get; set; }
        public string CodigoDaneCiudad { get; set; }
        public int? TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Sector { get; set; }
        public bool? CalificacionHd { get; set; }
        public string CaracteristicaFranquicia { get; set; }
        public string CaracteristicaClase { get; set; }
        public string CaracteristicaMarca { get; set; }
        public bool? CaracteristicaAmparada { get; set; }
        public string CaracteristicaCodigoAmparada { get; set; }
        public string CaracteristicaGarantia { get; set; }
        public string ValorMoneda { get; set; }
        public DateTime? ValorFecha { get; set; }
        public string ValorCalificacion { get; set; }
        public decimal? ValorSaldoActual { get; set; }
        public decimal? ValorSaldoMora { get; set; }
        public decimal? ValorDisponible { get; set; }
        public decimal? ValorCuota { get; set; }
        public decimal? ValorCuotasMora { get; set; }
        public int? ValorDiasMora { get; set; }
        public DateTime? ValorFechaPagoCuota { get; set; }
        public DateTime? ValorFechaLimitePago { get; set; }
        public decimal? ValorCupoTotal { get; set; }
        public string EstadoPlasticoCodigo { get; set; }
        public DateTime? EstadoPlasticoFecha { get; set; }
        public string EstadoCuentaCodigo { get; set; }
        public DateTime? EstadoCuentaFecha { get; set; }
        public string EstadoOrigenCodigo { get; set; }
        public DateTime? EstadoOrigenFecha { get; set; }
        public string EstadoPagoCodigo { get; set; }
        public DateTime? EstadoPagoFecha { get; set; }
        public string Llave { get; set; }
    }
}
