using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public  class ReporteContactabilidadDTO
    {
        public int Hora { get; set; }
        public string Clave { get; set; }
        public int Valor { get; set; }
        public int Tipo { get; set; }
        public int? TotalLlamadas { get; set; }
        public string Troncal { get; set; }
        public string Sede { get; set; }
        public int? CantidadIntentos { get; set; }
        public int? CantidadIntentoEjecutadoUno { get; set; }
        public int? CantidadIntentoEjecutadoDos { get; set; }
        public int? CantidadIntentoEjecutadoTres { get; set; }
        public int? CantidadIntentoEjecutadoMasTres { get; set; }
        public int? CantidadIntentoNoEjecutadoUno { get; set; }
        public int? CantidadIntentoNoEjecutadoDos { get; set; }
        public int? CantidadIntentoNoEjecutadoTres { get; set; }
        public int? CantidadIntentoNoEjecutadoMasTres { get; set; }
        public int? DuracionIntentoEjecutadoUno { get; set; }
        public int? DuracionIntentoEjecutadoDos { get; set; }
        public int? DuracionIntentoEjecutadoTres { get; set; }
        public int? DuracionIntentoEjecutadoMasTres { get; set; }
        public int? DuracionIntentoNoEjecutadoUno { get; set; }
        public int? DuracionIntentoNoEjecutadoDos { get; set; }
        public int? DuracionIntentoNoEjecutadoTres { get; set; }
        public int? DuracionIntentoNoEjecutadoMasTres { get; set; }
    }
    public class ReporteContactabilidadMinutosDTO
    {
        public string Pais { get; set; }
        public string Troncal { get; set; }
        public int Segundos { get; set; }
        public int Minutos { get; set; }
        public decimal Costominuto { get; set; }
    }
}
