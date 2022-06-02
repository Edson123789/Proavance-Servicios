using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ControlCambiodeFaseDTO
    {
        public string FaseOrigen { get; set; }
        public int IdFaseOrigen { get; set; }
        public int ActividadesEjecutadas { get; set; }
        public int ActividadesProgramadasAutomaticas { get; set; }
        public int ActividadesProgramadasManuales { get; set; }
        public int ProgramadasEjecutadasSinLlamada { get; set; }
        public int ActividadesTotales { get; set; }
        public int Contactabilidad { get; set; }
        public decimal MinPromedioEjecutadas { get; set; }
        public decimal MinPromedioprogramadasmanuales { get; set; }
        public decimal NumIntentoLlamadasPromedio { get; set; }
        public int Orden { get; set; }
    }


    public class ControlCambiodeFaseV2DTO
    {
        public string FaseOrigen { get; set; }
        public int IdFaseOrigen { get; set; }
        public int ActividadesEjecutadas { get; set; }
        public int ActividadesProgramadasAutomaticas { get; set; }
        public int ActividadesProgramadasManuales { get; set; }
        public int ProgramadasEjecutadasSinLlamada { get; set; }
        public int ActividadesTotales { get; set; }
        public int Contactabilidad { get; set; }
        public decimal MinPromedioEjecutadas { get; set; }
        public decimal MinPromedioprogramadasmanuales { get; set; }
        public decimal NumIntentoLlamadasPromedio { get; set; }
        public decimal TotalTimbradoAutomatica { get; set; }
        public int Orden { get; set; }
    }


    public class ControlCambiodeFaseAuxiliarDTO
    {
        public string FaseOrigen { get; set; }
        public int IdFaseOrigen { get; set; }
        public int ActividadesEjecutadas { get; set; }
        public int ActividadesProgramadasAutomaticas { get; set; }
        public int ActividadesProgramadasManuales { get; set; }
        public int ProgramadasEjecutadasSinLlamada { get; set; }
        public int ActividadesTotales { get; set; }
        public int Contactabilidad { get; set; }
        public decimal MinPromedioEjecutadas { get; set; }
        public decimal MinPromedioprogramadasmanuales { get; set; }
        public decimal NumIntentoLlamadasPromedio { get; set; }
        public int SumaTimbradoAutomatica { get; set; }
        public int CantidadTimbradoAutomatica { get; set; }
        public int Orden { get; set; }
    }

    public class DiferenciaLlamadasBloqueDTO
    {
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
    }

    public class ResultadoDiferenciaLlamadasBloqueDTO
    {
        public int? Cero { get; set; }
        public int? MasCero { get; set; }
        public int? MasUno { get; set; }
        public int? MasDos { get; set; }
        public int? MasTres { get; set; }
        public int? MasCuatro { get; set; }
        public int? MasCinco { get; set; }
        public int? MasSeis { get; set; }
    }

    public class ConteoDatosFaseDTO
    {
        public string Fase { get; set; }
        public int Inicio { get; set; }
        public int Momento { get; set; }
    }

    public class ResultadoConteoDatosFaseDTO
    {
        public string FaseOportunidad { get; set; }
        public int Total { get; set; }
    }
}
