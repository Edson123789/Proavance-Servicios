using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EstadoMatriculadoDTO
    {
        public int IdMatriculaCabecera  {get;set;}
        public string CodigoMatricula { get;set;}
        public string NombreProgramaGeneral { get;set;}
        public string EstadoCertificacion { get;set;}
        public string EstadoEvaluacion { get;set;}
        public string EstadoFinanciero { get;set;}
        public string TipoCuota { get;set;}
        public int NroCuota { get;set;}
        public int NroSubCuota { get;set;}
        public int IdCentroCosto { get;set;}
        public int Version { get;set;}
        public int? VersionPrograma { get;set;}
        public DateTime FechaVencimiento { get;set;}
        public string Documentos { get;set;}
    }
}
