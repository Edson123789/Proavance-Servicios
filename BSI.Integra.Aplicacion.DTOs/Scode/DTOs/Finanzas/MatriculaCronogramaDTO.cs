using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MatriculaCronogramaDTO
    {

        public int IdAlumno { get; set; }
        public int IdPespecifico { get; set; }//Centrocosto 
        public int IdCoordinador { get; set; }
        public int IdAsesor { get; set; }
        public string Codigobanco { get; set; }
        public int[] ListaIdDocumento { get; set; }
        public string Periodo { get; set; }
        public string Moneda { get; set; }
        public string AcuerdoPago { get; set; }
        public double TipoCambio { get; set; }
        //public decimal? TipoCambio { get; set; }
        public double TotalPagar { get; set; }
        public int NroCuotas { get; set; }
        public DateTime FechaInicioPago { get; set; }
        public string OpcionPagoNDias { get; set; }
        public int? Ndias { get; set; }
        public int[] CursosMatriculados { get; set; }
        public string NombreUsuario { get; set; }
    }
}
