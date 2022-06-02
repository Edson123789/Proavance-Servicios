using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteCuotasDTO
    {
        public int IdAlumno { get; set; }
        public int IdMatricula { get; set; }        
        public string CodigoMatricula { get; set; }
        public string Modalidad { get; set; }
        public string Ciudad { get; set; }
        public string CentroCosto { get; set; }        
        public int? IdCentroCosto { get; set; }
        public string Codigo { get; set; }
        public string Alumno { get; set; }
        public DateTime FechaCuota { get; set; }
        public decimal Cuota { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Cuota_SubCuota { get; set; }
        public string MonedaPago { get; set; }
        public string EstadoCuota { get; set; }

    }

}