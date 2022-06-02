using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MatriculaCabeceraBeneficiosDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string EstadoMatriculaCabeceraBeneficio { get; set; }
        //public bool corresponde { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public string EstadoSolicitudBeneficio { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public int IdConfiguracionBeneficioProgramaGeneral { get; set; }
        public DateTime? FechaEntregaBeneficio { get; set; }
    }
}
