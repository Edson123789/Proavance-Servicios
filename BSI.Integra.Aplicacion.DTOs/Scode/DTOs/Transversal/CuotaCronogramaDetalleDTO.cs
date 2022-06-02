using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CuotaCronogramaDetalleDTO
    {
        public int NroCuota { get; set; }
        //public int NroSubCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Cuota { get; set; }
        public string Moneda { get; set; }
        public string SimboloMoneda { get; set; }
    }

    public class AutoEvaluacionCronogramaDetalleDTO
    {
        public string NombreAutoEvaluacion { get; set; }
        public DateTime FechaCronograma { get; set; }
    }


    public class AutoEvaluacionCompletaCronogramaDetalleDTO
    {
        public string NombreAutoEvaluacion { get; set; }
        public int Nota { get; set; }
        public DateTime FechaCronograma { get; set; }
    }

    public class PeriodoDuracionProgramaEspecificoDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public int DuracionTotalAproximadaMeses { get; set; }
        public DateTime FechaAproximadaCertificacion { get; set; }
    }

    public class ConjuntoSesionProgramaEspecificoDTO
    {
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico{ get; set; }
        public DateTime FechaSesion { get; set; }
        public string HorarioSesion { get; set; }
        public int DuracionSesionHoras { get; set; }
    }

    public class ConjuntoSesionProgramaEspecificoMaestroDTO
    {
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
        public List<ConjuntoSesionProgramaEspecificoDetalleDTO> Sesiones { get; set; }
    }

    public class ConjuntoSesionProgramaEspecificoDetalleDTO
    {
        public DateTime FechaSesion { get; set; }
        public string HorarioSesion { get; set; }
        public int DuracionSesionHoras { get; set; }
    }
}
