using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteIngresosMatriculaNuevaDTO
    {
        public int AsignadoA { get; set; }
        public int Cantidad { get; set; }
        public int IdOportunidad { get; set; }

        public string FechaLog { get; set; }
        public string Alumno { get; set; }
        public string CentroCosto { get; set; }

        public string Idmatricula { get; set; }
        public Nullable<int> IdCiudad { get; set; }
        public decimal PrecioReal { get; set; }
        public decimal PreciosIndesc { get; set; }

        public decimal Mes { get; set; }
        public decimal MesTotal { get; set; }
        public Nullable<DateTime> FechaVencimiento { get; set; }
        public Nullable<DateTime> FechaPagoOriginal { get; set; }
        public Nullable<DateTime> FechaPago { get; set; }

        public string DiaPago { get; set; }
        public int DiasDeposito { get; set; }
        public int DiasDisponible { get; set; }

        public bool CuentaFeriados { get; set; }
        public bool CuentaFeriadosEstatales { get; set; }
        public bool ConsiderarDiasHabilesLV { get; set; }
        public bool ConsiderarDiasHabilesLS { get; set; }
        public Nullable<DateTime> FechaPagoReal { get; set; }

        public decimal PorcentajeCobro { get; set; }
        public Nullable<DateTime> FechaDepositaron { get; set; }
        public Nullable<DateTime> FechaDisponible { get; set; }
        public string EstadoEfectivo { get; set; }

        public string FechaInicioPeriodo { get; set; }
        public string FechaFinPeriodo { get; set; }
        public string FechaInicioFiltro { get; set; }
        public string FechaFinFiltro { get; set; }

        //nuevo carlos para cuadrar
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal CuotaOriginal { get; set; }
        public string MonedaOriginal { get; set; }
        public string FormaPago { get; set; }
        public string WebMoneda { get; set; }
        public decimal WebTipoCambio { get; set; }
    }
    public class ReporteIngresosFinalFinanzasDTO {
        public string RangoFechas { get; set; }

        public int InscritosRgulares { get; set; }
        public int InscritosInstituto { get; set; }

        public decimal Total { get; set; } //TOTAL
        public decimal TotalIngresosSinInstituto { get; set; }//INGRESO TOTAL
        public decimal IngresosInstituto { get; set; }//INGRESO TOTAL

        public decimal IngresoCursosRegularesMN { get; set; }//MATRICULAS NUEVAS
        public decimal IngresoCursosRegularesMNCuenta { get; set; }//MATRICULAS NUEVAS CUENTA
        public decimal IngresoInstitutoMN { get; set; }//MATRICULAS NUEVAS
        public decimal PorcentajeIngresoMN { get; set; }//MATRICULAS NUEVAS

        public decimal IngresoCursosRegularesPAT { get; set; }//PAGOS ATRASADOS
        public decimal IngresoInstitutoPAT { get; set; }//PAGOS ATRASADOS
        public decimal PorcentajeIngresoPAT { get; set; }//PAGOS ATRASADOS

        public decimal IngresoCursosRegularesPM { get; set; }//PAGOS CUOTA MES 
        public decimal IngresoInstitutoPM { get; set; }//PAGOS CUOTA MES 
        public decimal PorcentajeIngresoPM { get; set; }//PAGOS CUOTA MES 

        public decimal IngresoCursosRegularesPAD { get; set; }//PAGOS ADELANTADOS 
        public decimal IngresoInstitutoPAD { get; set; }//PAGOS ADELANTADOS
        public decimal PorcentajeIngresoPAD { get; set; }//PAGOS ADELANTADOS

        public decimal IngresosInHouse { get; set; }//OTROS INGRESOS 
        public decimal IngresosAlquileres { get; set; }//OTROS INGRESOS 
        public decimal PagosPorTramites { get; set; }//OTROS INGRESOS 
        public decimal IngresosFinancieros { get; set; }//OTROS INGRESOS (mora)
        public decimal PorcentajeIngresoOI { get; set; }//OTROS INGRESOS
    }
}
