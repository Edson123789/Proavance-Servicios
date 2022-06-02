using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosMatriculaManualDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreCompletoAlumno { get; set; }
        public int? IdAlumno { get; set; }
        public string Moneda { get; set; }
        public DateTime? FechaIniPago { get; set; }
        public double? TipoCambio { get; set; }
        public double? TotalPagar { get; set; }
        public int? NroCuotas { get; set; }
        public string Periodo { get; set; }
        public string NombrePrograma { get; set; }
        public int? IdPEspecifico { get; set; }
        public string Coordinador { get; set; }
        public string Asesor { get; set; }
        public string TituloAcuerdoPago { get; set; }
    }
}
