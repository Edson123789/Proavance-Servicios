using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteTasaConversionConsolidadaGraficasVistaDTO
    {
        public List<TCRM_ConsolidadTCAsesoresGraficas> Consolidado { get; set; }
    }

    public class ReporteTasaConversionConsolidadaMensualGraficasVistaDTO
    {
        public List<TCRM_ConsolidadTCAsesoresMensualGraficas> Consolidado { get; set; }
    }

    public class TCRM_ConsolidadTCAsesoresGraficas
    {

        public int IdAsesor { get; set; }
        public decimal IR { get; set; }
        public decimal Ingreso_en_el_mes_USD { get; set; }
        public decimal PrecioSinDesc { get; set; }
        public decimal PrecioListaFinal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Descuento_Promedio { get; set; }
        public decimal PP_OC_USD { get; set; }
        public decimal IS { get; set; }
        public decimal OC { get; set; }
        public bool EstadoAsesor { get; set; }
        public string Nombres { get; set; }
        public string Semana { get; set; }
        public string Mes { get; set; }
        public decimal TCMeta { get; set; }
        public decimal TC_Real { get; set; }
        public decimal TC_Meta { get; set; }
        public decimal TCReal_TCMeta { get; set; }
        public decimal PP_IM_USD { get; set; }
        public decimal PP_IM_PP_OC { get; set; }
        public decimal Porcentaje_ingreso_mes { get; set; }
        public decimal IM { get; set; }
        public decimal IR_IM { get; set; }

        public int NroSemana { get; set; }
        public int Ano { get; set; }


    }

    public class TCRM_ConsolidadTCAsesoresMensualGraficas
    {
        public int IdAsesor { get; set; }
        public decimal IR { get; set; }
        public decimal Ingreso_en_el_mes_USD { get; set; }
        public decimal PrecioSinDesc { get; set; }
        public decimal PrecioListaFinal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Descuento_Promedio { get; set; }
        public decimal PP_OC_USD { get; set; }
        public decimal IS { get; set; }
        public decimal OC { get; set; }
        public bool EstadoAsesor { get; set; }
        public string Nombres { get; set; }
        public int? MesNumero { get; set; }
        public string Mes { get; set; }
        public decimal TCMeta { get; set; }
        public decimal TC_Real { get; set; }
        public decimal TC_Meta { get; set; }
        public decimal TCReal_TCMeta { get; set; }
        public decimal PP_IM_USD { get; set; }
        public decimal PP_IM_PP_OC { get; set; }
        public decimal Porcentaje_ingreso_mes { get; set; }
        public decimal IM { get; set; }
        public decimal IR_IM { get; set; }
        public int Ano { get; set; }
    }
}
