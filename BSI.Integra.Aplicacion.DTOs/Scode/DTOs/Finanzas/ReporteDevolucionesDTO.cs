using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteDevolucionesDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public string TipoRetiro { get; set; }
        public int IdPrograma { get; set; }
        public string Programa { get; set; }
        public int IdAlumno { get; set; }
        public string Alumno { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoDevolucion { get; set; }
        public int IdMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public string Observaciones { get; set; }

    }

}